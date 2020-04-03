using CANAdminApi.Data.Repositories;
using CANAdminApi.Services.BMOModels;
using CANAdminApi.Services.DTOModels;
using CANAdminApi.Services.Services.Custom;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANAdminApi.Services.Services
{
    public class FileService : BaseService<Data.Entities.File, FileDTO, FileDTO, FileBMO>
    {
        public FileService(Repository<Data.Entities.File> repository) : base(repository)
        {

        }

        public async Task<FileDTO> CreateAsync(IFormFile file)
        {
            var dbcParser = new DbcParser();
       
            var model = await dbcParser.LoadFromStreamAsync(file.FileName, file.OpenReadStream());
            return await base.CreateAsync(model);
        }

        public async Task<FileDTO> CreateAsync(System.IO.Stream stream, string fileName)
        {
            var dbcParser = new DbcParser();

            var model = await dbcParser.LoadFromStreamAsync(fileName, stream);
            return await base.CreateAsync(model);
        }

        protected override IQueryable<Data.Entities.File> GetAllEntities()
        {
            return base.GetAllEntities().Include("NetworkNodes.CanMessages.CanSignals");
        }
    }

}
