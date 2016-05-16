namespace ClientApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ClientApp.Database;
    using ClientApp.DataModels;
    using ClientApp.Services.Contracts;
    using ClientApp.ViewModels;
    using ClientApp.Services.Exceptions;
    using System.Threading.Tasks;

    public class CallBackService : ICallBackService
    {
        private ApplicationDbContext db;

        // Poor man's IoC. Replace with Dependency container like Ninject.
        public CallBackService()
            : this(new ApplicationDbContext())
        {
        }

        public CallBackService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public async Task<int> HandleEmployeeArrivals(string requestTocken, IEnumerable<EmployeeArrivalViewModel> arrivals)
        {
            if (!IsTokenValid(requestTocken))
            {
                throw new InvalidTokenException();
            }

           return await this.CreateEmployeeArrival(arrivals, requestTocken);
        }


        private bool IsTokenValid(string requestTocken)
        {
            return this.db.WebTokens.Any(wt => wt.Token == requestTocken && wt.Expires > DateTime.UtcNow);
        }


        private async Task<int> CreateEmployeeArrival(IEnumerable<EmployeeArrivalViewModel> arrivals, string requestTocken)
        {
            foreach (var item in arrivals)
            {
                var empl = new EmployeeArrival() { EmployeeId = item.EmployeeId, When = item.When };
                empl.WebToken = db.WebTokens.FirstOrDefault(t => t.Token == requestTocken);
                db.EmployeeArrivals.Add(empl);
            }

            return await db.SaveChangesAsync();
        }
    }
}
