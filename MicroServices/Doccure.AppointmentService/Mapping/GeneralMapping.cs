using Doccure.AppointmentService.Dtos.AppointmentDtos;
using Doccure.AppointmentService.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AutoMapper;
using Doccure.AppointmentService.Dtos.AppointmentDetailDtos;
namespace Doccure.AppointmentService.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Appointment, ResultAppointmentDto>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentDto>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentDto>().ReverseMap();
            CreateMap<Appointment, GetAppointmentById>().ReverseMap();


            CreateMap<AppointmentDetail, ResultAppointmentDetailDto>().ReverseMap();
            CreateMap<AppointmentDetail, CreateAppointmentDetailDto>().ReverseMap();
            CreateMap<AppointmentDetail, UpdateAppointmentDetailDto>().ReverseMap();
            CreateMap<AppointmentDetail, GetByIdAppointmentDetail>().ReverseMap();
        }
    } 
}
