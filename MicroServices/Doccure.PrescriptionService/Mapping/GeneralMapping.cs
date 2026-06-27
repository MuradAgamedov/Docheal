using AutoMapper;
using Doccure.PrescriptionService.Dtos.PrescriptionDtos;
using Doccure.PrescriptionService.Entities;

namespace Doccure.PrescriptionService.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Prescription, CreatePrescriptionDto>().ReverseMap();
            CreateMap<Prescription, ResultPrescriptionDto>().ReverseMap();
            CreateMap<PrescriptionItem, PrescriptionItemDto>().ReverseMap();
        }
    }
}
