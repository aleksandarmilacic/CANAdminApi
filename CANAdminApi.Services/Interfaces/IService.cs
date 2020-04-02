using CANAdminApi.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CANAdminApi.Services.Interfaces
{
    public interface IService<TDTOModel, TDTOListModel, TBMOModel>
          where TDTOModel : class
          where TDTOListModel : class
          where TBMOModel : class
    {
      
        Task<PaginationList<IEnumerable<TDTOListModel>>> GetAllAsync(PaginationParameters paginateConfig);

        Task<TDTOModel> GetAsync(Guid id);

        Task<TDTOModel> CreateAsync(TBMOModel data);

        Task<IEnumerable<TDTOListModel>> CreateBulkAsync(IEnumerable<TBMOModel> data);

        Task<TDTOModel> UpdateAsync(Guid id, TBMOModel data);

        Task DeleteAsync(Guid id);
    }
}
