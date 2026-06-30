using AutoMapper;
using Doccure.DoctorService.Dtos.BranchDtos;
using Doccure.DoctorService.Dtos.DoctorDtos;
using Doccure.DoctorService.Entities;
using Doccure.DoctorService.Settings;
using MongoDB.Driver;
using System.Net.Http;

namespace Doccure.DoctorService.Services.DoctorServices
{
    public class DoctorService : IDoctorService
    {
        private readonly IMongoCollection<Doctor> _doctorCollection;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public DoctorService(IMapper mapper, IDatabaseSettings settings, HttpClient httpClient)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _doctorCollection = database.GetCollection<Doctor>(settings.DoctorCollectionName);
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task CreateAsync(CreateDoctorDto dto)
        {
            var value = _mapper.Map<Doctor>(dto);
            await _doctorCollection.InsertOneAsync(value);
        }

        public async Task DeleteAsync(string id)
        {
            await _doctorCollection.DeleteOneAsync(x => x.DoctorId == id);
        }

        public async Task<List<ResultDoctorDto>> GetAllAsync()
        {
            var values = await _doctorCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultDoctorDto>>(values);
        }

        public async Task<GetDoctorByIdDto> GetByIdAsync(string id)
        {
            var value = await _doctorCollection.Find(x => x.DoctorId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetDoctorByIdDto>(value);
        }

        public async Task UpdateAsync(UpdateDoctorDto dto)
        {
            var value = _mapper.Map<Doctor>(dto);
            await _doctorCollection.FindOneAndReplaceAsync(x => x.DoctorId == dto.DoctorId, value);
        }


        public async Task<GetDoctorNameAndSurnameByIdDto> GetDoctorByIdAsync(string id)
        {
            var value = await _doctorCollection.Find(x => x.DoctorId == id).FirstOrDefaultAsync();

            var branch = await _httpClient.GetFromJsonAsync<BranchDto>($"https://localhost:7001/api/branches/GetBranch?id={value.BranchId}");

            return new GetDoctorNameAndSurnameByIdDto
            {
                DoctorId = value.DoctorId,
                Name = value.Name,
                Surname = value.Surname,
                BranchId = value.BranchId,
                BranchName = branch.BranchName
            };
        }
    }
}
