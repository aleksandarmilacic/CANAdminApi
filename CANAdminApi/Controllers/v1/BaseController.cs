using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CANAdminApi.Services.Enums;
using CANAdminApi.Services.Interfaces;
using CANAdminApi.Services.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CANAdminApi.Controllers.v1
{
    [ApiController]
    [EnableCors("CSPolicy")]
    public abstract class BaseController<TDTOModel, TDTOListModel, TBMOModel> : ControllerBase
       where TDTOModel : class
       where TDTOListModel : class
       where TBMOModel : class
    {
        protected IService<TDTOModel, TDTOListModel, TBMOModel> Service { get; }



        protected BaseController(IService<TDTOModel, TDTOListModel, TBMOModel> entityService)
        {

            Service = entityService;
        }

    

        [HttpGet]
        protected async Task<IActionResult> GetAllAsync()
        {
            // pagination
            var config = GetPaginationParams();

            var result = await Service.GetAllAsync(config).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        protected async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await Service.GetAsync(id).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        protected async Task<IActionResult> CreateAsync(TBMOModel model)
        {


            var result = await Service.CreateAsync(model).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        protected async Task<IActionResult> CreateBulkAsync(IEnumerable<TBMOModel> models)
        {
            var result = await Service.CreateBulkAsync(models).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPut]
        protected async Task<IActionResult> UpdateAsync(Guid id, TBMOModel model)
        {
            var result = await Service.UpdateAsync(id, model).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpDelete]
        protected async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Service.DeleteAsync(id).ConfigureAwait(false);
            return Ok();
        }

        protected PaginationParameters GetPaginationParams()
        {
            var config = new PaginationParameters();
            var query = HttpContext.Request.Query; // case insensitive (implementation is like that, RFC says that should be case-sensitive)

            if (query != null)
            {

                int.TryParse(query["skip"], out int skip);
                int.TryParse(query["take"], out int take);
                config.Skip = skip;
                config.Take = take;

                var order = query["ordering"];
                if (order.Count > 0)
                {
                    if (order.ToString().Equals("asc", StringComparison.InvariantCultureIgnoreCase))
                    {
                        config.Ordering = Ordering.asc;
                    }
                    else if (order.ToString().Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                    {
                        config.Ordering = Ordering.desc;
                    }
                }

                config.Column = query["column"];
                var q = query["queries"].ToString();
                if (!string.IsNullOrEmpty(q))
                    config.Query = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryModel[]>(q);
            }

            return config;
        }
    }
}