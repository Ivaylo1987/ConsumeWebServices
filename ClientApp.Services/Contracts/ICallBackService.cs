namespace ClientApp.Services.Contracts
{
    using ClientApp.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICallBackService
    {
        Task<int> HandleEmployeeArrivals(string requestTocken, IEnumerable<EmployeeArrivalViewModel> arrivals);
    }
}
