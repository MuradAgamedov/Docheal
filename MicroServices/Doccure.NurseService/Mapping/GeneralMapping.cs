using AutoMapper;
using Doccure.NurseService.Dtos.NurseDtos;
using Doccure.NurseService.Entities;

namespace Doccure.NurseService.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Nurse, ResultNurseDto>().ReverseMap();
            CreateMap<Nurse, GetByIdNurseDto>().ReverseMap();
            CreateMap<Nurse, CreateNurseDto>().ReverseMap();
            CreateMap<Nurse, UpdateNurseDto>().ReverseMap();
        }
    }
}
