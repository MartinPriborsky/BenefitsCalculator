using Api.Data;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

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
            var empoyees = await _dbContext.Employees
                .Include(e => e.Dependents)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<GetEmployeeDto>>(empoyees);
        }

        public async Task<GetEmployeeDto> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var empoyee = await _dbContext.Employees
                .Include(e => e.Dependents)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<GetEmployeeDto>(empoyee);
        }
    }
}
