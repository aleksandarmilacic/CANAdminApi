using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CANAdminApi.Helpers;
using CANAdminApi.Services.BMOModels;
using CANAdminApi.Services.DTOModels;
using CANAdminApi.Services.Enums;
using CANAdminApi.Services.Models;
using CANAdminApi.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CANAdminApi.Controllers.v1
{
    [Route("api/v1/File")] 
    public class FileController : BaseController<FileDTO, FileDTO, FileBMO>
    {
        private readonly FileService _service;
        public FileController(FileService service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        [ProducesDefaultResponseType(typeof(PaginationList<IEnumerable<FileDTO>>))]
        public async Task<IActionResult> GetAll(string column = null, string queries = null,
          int? skip = null, int? take = null, Ordering ordering = Ordering.desc)
        {
           
            return await GetAllAsync().ConfigureAwait(false);
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        [ProducesDefaultResponseType(typeof(FileDTO))]
        public async Task<IActionResult> Get(Guid id)
        {
            return await GetAsync(id).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("")]
        [ProducesDefaultResponseType(typeof(FileDTO))]
        [ImportFileParamType.SwaggerForm("file", "Upload files")]
        public async Task<IActionResult> Post(IFormFile file)
        {

            var result = await _service.CreateAsync(file);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await DeleteAsync(id).ConfigureAwait(false);
        }
    }
}