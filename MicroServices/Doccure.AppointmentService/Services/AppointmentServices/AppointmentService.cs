using AutoMapper;
using Doccure.AppointmentService.Context;
using Doccure.AppointmentService.Dtos.AppointmentDtos;
using Doccure.AppointmentService.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Doccure.AppointmentService.Services.AppointmentServices
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly AppointmentContext _context;

        public AppointmentService(IMapper mapper, AppointmentContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task CreateAsync(CreateAppointmentDto dto)
        {
            var value = _mapper.Map<Appointment>(dto);
            value.Status = "Pending";
            await _context.Appointments.AddAsync(value);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var value = await _context.Appointments.FindAsync(id);
            if (value == null) {
                return;
            }
            _context.Appointments.Remove(value);
            await _context.SaveChangesAsync();

        }

        public async Task<List<ResultAppointmentDto>> GetAllAsync()
        {
            var values = await _context.Appointments.ToListAsync();
            return  _mapper.Map<List<ResultAppointmentDto>>(values);
        }

        public async Task<GetAppointmentById> GetByIdAsync(int id)
        {
            var value = await _context.Appointments.FindAsync(id);
            return _mapper.Map<GetAppointmentById>(value);
        }

        public async Task UpdateAsync(UpdateAppointmentDto dto)
        {
            var value = await _context.Appointments.FindAsync(dto.AppointmentId);
            if (value == null)
            {
                return;
            }
            value.AppointmentDate = dto.AppointmentDate;
            value.Status = dto.Status;
            _context.Appointments.Update(value);
            await _context.SaveChangesAsync();
        }


        public async Task<LastAppointmentDto> GetLastAppointmentByPatientIdAsync(string patientId)
        {
            var value = await _context.Appointments
         .Include(x => x.AppointmentDetail)
         .Where(x => x.PatientId == patientId)
         .OrderByDescending(x => x.AppointmentDate)
         .FirstOrDefaultAsync();

            if (value == null)
                return null;

            return new LastAppointmentDto
            {
                AppointmentId = value.AppointmentId,
                DoctorId = value.DoctorId,
                BranchId = value.BranchId,
                AppointmentDate = value.AppointmentDate,
                Diagnosis = value.AppointmentDetail?.Diagnosis,
                Status = value.Status
            };
        }

    }
}
