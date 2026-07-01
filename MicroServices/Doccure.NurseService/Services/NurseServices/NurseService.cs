using AutoMapper;
using Doccure.NurseService.Context;
using Doccure.NurseService.Dtos.NurseDtos;
using Doccure.NurseService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doccure.NurseService.Services.NurseServices
{
    public class NurseService : INurseService
    {
        private readonly NurseContext _context;
        private readonly IMapper _mapper;

        public NurseService(NurseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResultNurseDto>> GetAllAsync()
        {
            var nurses = await _context.Nurses.ToListAsync();
            return _mapper.Map<List<ResultNurseDto>>(nurses);
        }

        public async Task<GetByIdNurseDto?> GetByIdAsync(int id)
        {
            var nurse = await _context.Nurses.FindAsync(id);
            return nurse == null ? null : _mapper.Map<GetByIdNurseDto>(nurse);
        }

        public async Task CreateAsync(CreateNurseDto dto)
        {
            var nurse = _mapper.Map<Nurse>(dto);
            await _context.Nurses.AddAsync(nurse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateNurseDto dto)
        {
            var nurse = _mapper.Map<Nurse>(dto);
            _context.Nurses.Update(nurse);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var nurse = await _context.Nurses.FindAsync(id);
            if (nurse != null)
            {
                _context.Nurses.Remove(nurse);
                await _context.SaveChangesAsync();
            }
        }
    }
}
