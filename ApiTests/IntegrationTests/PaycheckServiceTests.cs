using Api.Dtos.Employee;
using Api.Models;
using Api.Services;
using System;
using Xunit;

namespace ApiTests.IntegrationTests
{
    // Testing a few use cases on correct calculation
    public class PaycheckCalculationServiceTests
    {
        [Fact]
        public void TestEmployeeOnlyWithBaseCost()
        {
            var service = new PaycheckCalculationService();
            var employee = new GetEmployeeDto
            {
                FirstName = "Test",
                LastName = "Test",
                Salary = 38000m
            };

            var result = service.CalculatePaycheck(employee);
            Assert.Equal(1000m, result.Pay);
            Assert.Equal(0, result.Remainder);
        }

        [Fact]
        public void TestEmployeeOnlyWithBaseAndOver80kCost()
        {
            var service = new PaycheckCalculationService();
            var employee = new GetEmployeeDto
            {
                FirstName = "Test",
                LastName = "Test",
                Salary = 82000m // 70000 - 1640
            };

            var result = service.CalculatePaycheck(employee);
            Assert.Equal(2629.23m, result.Pay);
            Assert.Equal(2, result.Remainder);
        }

        [Fact]
        public void TestEmployeeOnlyWithOneDependent()
        {
            var service = new PaycheckCalculationService();
            var employee = new GetEmployeeDto
            {
                FirstName = "Test",
                LastName = "Test",
                Salary = 38000m,
                Dependents =
                {
                    new()
                    {
                        Relationship = Relationship.Child
                    }
                }
            };

            var result = service.CalculatePaycheck(employee);
            Assert.Equal(630.76m, result.Pay);
            Assert.Equal(24, result.Remainder);
        }

        [Fact]
        public void TestEmployeeWithNoneRelationshipDependent()
        {
            var service = new PaycheckCalculationService();
            var employee = new GetEmployeeDto
            {
                FirstName = "Test",
                LastName = "Test",
                Salary = 38000m,
                Dependents =
                {
                    new()
                    {
                        Relationship = Relationship.Child
                    },
                    new()
                    {
                        Relationship = Relationship.None,
                        DateOfBirth = new DateTime(1950,1,1)
                    },
                }
            };

            var result = service.CalculatePaycheck(employee);
            Assert.Equal(630.76m, result.Pay);
            Assert.Equal(24, result.Remainder);
        }

        [Fact]
        public void TestEmployeeWithDependentOver50()
        {
            var service = new PaycheckCalculationService();
            var employee = new GetEmployeeDto
            {
                FirstName = "Test",
                LastName = "Test",
                Salary = 38000m, // After base: 26000
                Dependents =
                {
                    new()
                    {
                        Relationship = Relationship.Spouse,
                        DateOfBirth = DateTime.Now.AddYears(-51)  // 9600
                    },
                    new()
                    {
                        Relationship = Relationship.Child,
                        DateOfBirth = DateTime.Now.AddYears(-49)  // 7200
                    },
                    new()
                    {
                        Relationship = Relationship.Child,
                        DateOfBirth = DateTime.Now.AddYears(-10) // 7200
                    },
                }
            };

            var result = service.CalculatePaycheck(employee);
            Assert.Equal(76.92m, result.Pay);
            Assert.Equal(8, result.Remainder);
        }
    }
}
