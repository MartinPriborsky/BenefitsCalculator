using Api.Data;
using Api.Dtos.Employee;
using Api.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    // Repository for accessing Employees from the database
    // Using AutoMapper library for mapping Employee into GetEmployeeDto
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployees(CancellationToken cancellationToken)
        {
            var employees = await _dbContext.Employees
                .Include(e => e.Dependents)
                .ToListAsync(cancellationToken);

            employees.ForEach(e => e.ValidateDependents());

            return _mapper.Map<List<GetEmployeeDto>>(employees);
        }

        public async Task<GetEmployeeDto> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees
                .Include(e => e.Dependents)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            employee?.ValidateDependents();

            return _mapper.Map<GetEmployeeDto>(employee);
        }
    }
}
