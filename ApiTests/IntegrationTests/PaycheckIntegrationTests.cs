using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class PaycheckIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllPaychecks_ShouldReturnAllPaychecks()
    {
        var response = await HttpClient.GetAsync("/api/v1/paychecks/1");
        var paychecks = new List<Paycheck>
        {
            new Paycheck { PaycheckId = 1, FirstName = "LeBron", LastName = "James", Pay = 2439.26m },
            new Paycheck { PaycheckId = 2, FirstName = "LeBron", LastName = "James", Pay = 2439.26m },
            new Paycheck { PaycheckId = 3, FirstName = "LeBron", LastName = "James", Pay = 2439.26m },
            new Paycheck { PaycheckId = 4, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 5, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 6, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 7, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 8, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 9, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 10, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 11, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 12, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 13, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 14, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 15, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 16, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 17, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 18, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 19, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 20, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 21, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 22, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 23, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 24, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 25, FirstName = "LeBron", LastName = "James", Pay = 2439.27m },
            new Paycheck { PaycheckId = 26, FirstName = "LeBron", LastName = "James", Pay = 2439.27m }
        };
        await response.ShouldReturn(HttpStatusCode.OK, paychecks);
    }

    [Fact]
    public async Task WhenAskedForAnPaycheck_ShouldReturnCorrectPaycheck()
    {
        var response = await HttpClient.GetAsync("/api/v1/paychecks/2/3");
        var paycheck = new Paycheck { PaycheckId = 3, FirstName = "Ja", LastName = "Morant", Pay = 2189.15m };
        await response.ShouldReturn(HttpStatusCode.OK, paycheck);
    }

    [Fact]
    public async Task WhenAskedForANonexistentPaychecks_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/paychecks/{10}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenAskedForANonexistentPaycheck_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/paychecks/{10}/{10}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}