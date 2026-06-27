using AutoMapper;
using Doccure.AppointmentService.Context;
using Doccure.AppointmentService.Dtos.AppointmentDetailDtos;
using Doccure.AppointmentService.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Doccure.AppointmentService.Services.AppointmentDetailServices
{
    public class AppointmentDetailService : IAppointmentDetailService
    {
        private readonly IMapper _mapper;
        private readonly AppointmentContext _context;

        public AppointmentDetailService(IMapper mapper, AppointmentContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task CreateAsync(CreateAppointmentDetailDto dto)
        {
            var value = _mapper.Map<AppointmentDetail>(dto);
            value.CompletedDate = DateTime.Now;
            await _context.AppointmentDetails.AddAsync(value);
            await _context.SaveChangesAsync();
        }

        public async Task<GetByIdAppointmentDetail> GetByIdAsync(int id)
        {
            var value = await _context.AppointmentDetails.FirstOrDefaultAsync(x=>x.AppointmentDetailId == id);
            return _mapper.Map<GetByIdAppointmentDetail>(value);
        }

        public async Task UpdateAsync(UpdateAppointmentDetailDto dto)
        {
            var value = await _context.AppointmentDetails.FirstOrDefaultAsync(x => x.AppointmentDetailId == dto.AppointmentDetailId);
            if (value == null)
            {
                return;
            }
            value.Complaint = dto.Complaint;
            value.Notes = dto.Notes;
            value.Diagnosis = dto.Diagnosis;
            value.Prescription = dto.Prescription;
            _context.AppointmentDetails.Update(value);
            await _context.SaveChangesAsync();
        }
    }
}
