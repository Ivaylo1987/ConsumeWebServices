namespace ClientApp.Services.Contracts
{
    using ClientApp.ViewModels;
    using System.Collections.Generic;
    public interface IArrivalService
    {
        IEnumerable<EmployeeArrivalViewModel> GetEmployeeArrivalsByPage(int page, int pageSize);
    }
}
