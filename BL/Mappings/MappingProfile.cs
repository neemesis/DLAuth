using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BL.Mapping {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<DL.Models.User, BL.Models.User>().ReverseMap();
            CreateMap<DL.Models.Role, BL.Models.Role>().ReverseMap();

            CreateMap<Task<DL.Models.Role>, Task<BL.Models.Role>>().ReverseMap();
            CreateMap<Task<DL.Models.User>, Task<BL.Models.User>>().ReverseMap();
        }
    }
}
