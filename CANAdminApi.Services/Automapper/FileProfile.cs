using AutoMapper;
using CANAdminApi.Services.BMOModels;
using CANAdminApi.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Services.Automapper
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<FileBMO, Data.Entities.File>().PreserveReferences();
            CreateMap<CanMessageBMO, Data.Entities.CanMessage>()
                .ForMember(a => a.Id, opt => opt.MapFrom(a => a.Id))
                .ForMember(a => a.ID, opt => opt.MapFrom(a => a.ID))
                .PreserveReferences();
            CreateMap<CanSignalBMO, Data.Entities.CanSignal>().PreserveReferences();
            CreateMap<NetworkNodeBMO, Data.Entities.NetworkNode>().PreserveReferences();

            CreateMap<Data.Entities.File, FileDTO>();
            CreateMap<Data.Entities.CanMessage, CanMessageDTO>()
                .ForMember(a => a.Id, opt => opt.MapFrom(a => a.Id))
                .ForMember(a => a.ID, opt => opt.MapFrom(a => a.ID));
            CreateMap<Data.Entities.CanSignal, CanSignalDTO>();
            CreateMap<Data.Entities.NetworkNode, NetworkNodeDTO>();
        }
    }
}
