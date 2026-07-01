using Doccure.PharmacyService.Dtos.MedicineDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doccure.PharmacyService.Services.MedicineServices
{
    public interface IMedicineService
    {
        Task<List<ResultMedicineDto>> GetAllMedicinesAsync();
        Task<GetByIdMedicineDto?> GetMedicineByIdAsync(int id);
        Task CreateMedicineAsync(CreateMedicineDto dto);
        Task UpdateMedicineAsync(UpdateMedicineDto dto);
        Task DeleteMedicineAsync(int id);
    }
}
