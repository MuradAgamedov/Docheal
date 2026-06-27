using AutoMapper;
using Doccure.DoctorService.Dtos.DoctorDtos;
using Doccure.DoctorService.Entities;
using Doccure.DoctorService.Settings;
using MongoDB.Driver;

namespace Doccure.DoctorService.Services.DoctorServices
{
    public class DoctorService : IDoctorService
    {
        private readonly IMongoCollection<Doctor> _doctorCollection;
        private readonly IMapper _mapper;

        public DoctorService(IMapper mapper, IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _doctorCollection = database.GetCollection<Doctor>(settings.DoctorCollectionName);
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateDoctorDto dto)
        {
            var value = _mapper.Map<Doctor>(dto);
            await _doctorCollection.InsertOneAsync(value);
        }

        public async Task DeleteAsync(string id)
        {
            await _doctorCollection.DeleteOneAsync(x => x.BranchId == id);
        }

        public async Task<List<ResultDoctorDto>> GetAllAsync()
        {
            var values = await _doctorCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultDoctorDto>>(values);
        }

        public async Task<GetDoctorByIdDto> GetByIdAsync(string id)
        {
            var value = await _doctorCollection.Find(x => x.BranchId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetDoctorByIdDto>(value);
        }

        public async Task UpdateAsync(UpdateDoctorDto dto)
        {
            var value = _mapper.Map<Doctor>(dto);
            await _doctorCollection.FindOneAndReplaceAsync(x => x.BranchId == dto.BranchId, value);
        }
    }
}
