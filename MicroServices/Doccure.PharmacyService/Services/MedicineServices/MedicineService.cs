using AutoMapper;
using Doccure.PharmacyService.Context;
using Doccure.PharmacyService.Dtos.MedicineDtos;
using Doccure.PharmacyService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doccure.PharmacyService.Services.MedicineServices
{
    public class MedicineService : IMedicineService
    {
        private readonly PharmacyContext _context;
        private readonly IMapper _mapper;

        public MedicineService(PharmacyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResultMedicineDto>> GetAllMedicinesAsync()
        {
            var medicines = await _context.Medicines.ToListAsync();
            return _mapper.Map<List<ResultMedicineDto>>(medicines);
        }

        public async Task<GetByIdMedicineDto?> GetMedicineByIdAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
                return null;

            return _mapper.Map<GetByIdMedicineDto>(medicine);
        }

        public async Task CreateMedicineAsync(CreateMedicineDto dto)
        {
            var medicine = _mapper.Map<Medicine>(dto);
            await _context.Medicines.AddAsync(medicine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicineAsync(UpdateMedicineDto dto)
        {
            var medicine = await _context.Medicines.FindAsync(dto.MedicineId);
            if (medicine != null)
            {
                _mapper.Map(dto, medicine);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMedicineAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine != null)
            {
                _context.Medicines.Remove(medicine);
                await _context.SaveChangesAsync();
            }
        }
    }
}
