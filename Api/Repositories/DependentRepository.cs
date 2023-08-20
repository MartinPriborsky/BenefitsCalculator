using Api.Data;
using Api.Dtos.Dependent;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    // Repository for accessing Dependents from the database
    // Using AutoMapper library for mapping Dependent into GetDependentDto
    public class DependentRepository : IDependentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DependentRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GetDependentDto>> GetAllDependents(CancellationToken cancellationToken)
        {
            var empoyees = await _dbContext.Dependents
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<GetDependentDto>>(empoyees);
        }

        public async Task<GetDependentDto> GetDependentById(int id, CancellationToken cancellationToken)
        {
            var empoyee = await _dbContext.Dependents
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<GetDependentDto>(empoyee);
        }
    }
}
