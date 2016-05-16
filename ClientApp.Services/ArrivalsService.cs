namespace ClientApp.Services
{
    using ClientApp.Database;
    using ClientApp.Services.Contracts;
    using ClientApp.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public class ArrivalsService : IArrivalService
    {
        // Poor man's IoC. Replace with Dependency container like Ninject.
        private ApplicationDbContext db;
        public ArrivalsService()
            : this(new ApplicationDbContext())
        {
        }


        public ArrivalsService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }


        public IEnumerable<EmployeeArrivalViewModel> GetEmployeeArrivalsByPage(int page, int pageSize)
        {
            page = page < 0 ? 0 : page;
            return this.db.EmployeeArrivals.OrderBy(x => x.EmployeeId)
                                            .Skip(pageSize * page)
                                            .Take(pageSize)
                                            .Select(EmployeeArrivalViewModel.FromDbModel).ToList();
        }
    }
}
