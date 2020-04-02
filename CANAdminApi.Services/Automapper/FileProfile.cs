using AutoMapper;
using CANAdminApi.Services.BMOModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Services.Automapper
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<FileBMO, Data.Entities.File>();
            CreateMap<CanMessageBMO, Data.Entities.CanMessage>();
            CreateMap<CanSignalBMO, Data.Entities.CanSignal>();
            CreateMap<NetworkNodeBMO, Data.Entities.NetworkNode>();


        }
    }
}
