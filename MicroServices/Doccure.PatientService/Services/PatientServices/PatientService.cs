using AutoMapper;
using Doccure.PatientService.Context;
using Doccure.PatientService.Dtos.AppointmentDtos;
using Doccure.PatientService.Dtos.DoctorDtos;
using Doccure.PatientService.Dtos.IdentityDtos;
using Doccure.PatientService.Dtos.PatientDtos;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace Doccure.PatientService.Services.PatientServices
{
    public class PatientService : IPatientService
    {
        private readonly IMapper _mapper;
        private readonly PatientContext _context;
        private readonly HttpClient _httpClient;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        public PatientService(IMapper mapper, PatientContext context, HttpClient httpClient, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            _mapper = mapper;
            _context = context;
            _httpClient = httpClient;
            _env = env;
        }

        public async Task<List<ResultPatientDto>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            var result = new List<ResultPatientDto>();

            foreach (var patient in patients)
            {
                var mapped = await MapPatientToResultDtoAsync(patient);
                if (mapped != null)
                    result.Add(mapped);
            }

            return result;
        }

        public async Task<ResultPatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return null;

            return await MapPatientToResultDtoAsync(patient);
        }

        public async Task CreatePatientAsync(CreatePatientDto dto)
        {
            var savedImagePath = await SaveImageAsync(dto.ImageFile);

            if (savedImagePath != null)
                dto.ImageUrl = savedImagePath;

            var registerData = new
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                City = dto.City,
                ImageUrl = dto.ImageUrl,
                BloodGroup = dto.BloodGroup
            };

            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7186/api/Auth/register",
                registerData);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Identity register uğursuz oldu: {(int)response.StatusCode} - {responseBody}");

            var resultObj = await response.Content.ReadFromJsonAsync<RegisterResponseDto>();

            if (resultObj == null || string.IsNullOrEmpty(resultObj.UserId))
                throw new Exception("İstifadəçi ID-si əldə edilə bilmədi.");

            var patient = new Entities.Patient
            {
                AppUserId = resultObj.UserId,
                TcKimlikNo = dto.TcKimlikNo,
                InsuranceType = dto.InsuranceType,
                CreatedDate = DateTime.UtcNow,
                Status = dto.Status
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(UpdatePatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(dto.PatientId);
            if (patient == null)
                return;

            var savedImagePath = await SaveImageAsync(dto.ImageFile);

            if (savedImagePath != null)
                dto.ImageUrl = savedImagePath;

            if (!string.IsNullOrEmpty(patient.AppUserId))
            {
                var identityUpdate = new
                {
                    Name = dto.Name,
                    Surname = dto.Surname,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Gender = dto.Gender,
                    BirthDate = dto.BirthDate,
                    City = dto.City,
                    ImageUrl = dto.ImageUrl,
                    BloodGroup = dto.BloodGroup
                };

                var response = await _httpClient.PutAsJsonAsync(
                    $"https://localhost:7186/api/Users/{patient.AppUserId}",
                    identityUpdate);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Identity update uğursuz oldu: {(int)response.StatusCode} - {errorBody}");
                }
            }

            patient.TcKimlikNo = dto.TcKimlikNo;
            patient.InsuranceType = dto.InsuranceType;
            patient.Status = dto.Status;

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        private async Task<string?> SaveImageAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var uploadsDir = Path.Combine(webRoot, "uploads", "patients");
            Directory.CreateDirectory(uploadsDir);

            var ext = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsDir, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/patients/{fileName}";
        }

        private async Task<ResultPatientDto> MapPatientToResultDtoAsync(Entities.Patient patient)
        {
            IdentityUserDto identityUser = null;

            try
            {
                identityUser = await _httpClient.GetFromJsonAsync<IdentityUserDto>(
                    $"https://localhost:7186/api/Users/{patient.AppUserId}");
            }
            catch { }

            LastAppointmentDto lastAppointment = null;

            try
            {
                lastAppointment = await _httpClient.GetFromJsonAsync<LastAppointmentDto>(
                    $"https://localhost:7280/api/Appointment/patient/{patient.AppUserId}/last");
            }
            catch { }

            DoctorSummaryDto doctor = null;

            if (lastAppointment != null && !string.IsNullOrEmpty(lastAppointment.DoctorId))
            {
                try
                {
                    doctor = await _httpClient.GetFromJsonAsync<DoctorSummaryDto>(
                        $"https://localhost:7002/api/Doctors/{lastAppointment.DoctorId}/summary");
                }
                catch { }
            }

            return new ResultPatientDto
            {
                PatientId = patient.PatientId,
                AppUserId = patient.AppUserId,
                TcKimlikNo = patient.TcKimlikNo,
                InsuranceType = patient.InsuranceType,
                CreatedDate = patient.CreatedDate,
                Status = patient.Status,

                Name = identityUser?.Name,
                Surname = identityUser?.Surname,
                FullName = identityUser != null ? $"{identityUser.Name} {identityUser.Surname}" : null,
                Email = identityUser?.Email,
                PhoneNumber = identityUser?.PhoneNumber,
                Gender = identityUser?.Gender,
                BirthDate = identityUser?.BirthDate,
                BloodGroup = identityUser?.BloodGroup,
                ImageUrl = identityUser?.ImageUrl,
                City = identityUser?.City,
                Address = identityUser?.Address,

                LastVisitDate = lastAppointment?.AppointmentDate,
                CurrentDiagnosis = lastAppointment?.Diagnosis,

                DoctorId = doctor?.DoctorId,
                DoctorName = doctor != null ? $"{doctor.Name} {doctor.Surname}" : null,
                BranchId = doctor?.BranchId,
                BranchName = doctor?.BranchName
            };
        }
    }
}