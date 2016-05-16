namespace ClientApp.Services.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface ISubscribeService
    {
        Task<string> Subscribe(DateTime date, string webServiceUri, string clientCallBackUri);
    }
}
