namespace ClientApp.Services
{
    using ClientApp.Database;
    using ClientApp.DataModels;
    using ClientApp.Services.Contracts;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    public class SubscribeService : ISubscribeService
    {
        private ApplicationDbContext db;

        // Poor man's IoC. Replace with Dependency container like Ninject.
        public SubscribeService() :
            this(new ApplicationDbContext())
        {
        }

        public SubscribeService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public async Task<string> Subscribe(DateTime date, string webServiceUri, string clientCallBackUri)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept-Client", "Fourth-Monitor");

                var url = this.BuildUrl(date, webServiceUri, clientCallBackUri);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var webToken = await response.Content.ReadAsAsync<WebToken>();

                    this.db.WebTokens.Add(webToken);
                    await db.SaveChangesAsync();
                }

                return url;
            }
        }

        private string BuildUrl(DateTime date, string webServiceUri, string clientCallBackUri)
        {
            var query = HttpUtility.ParseQueryString("date=" + date.ToString("yyyy-MM-dd") + "&callback=" + clientCallBackUri);
            var uriBuilder = new UriBuilder(webServiceUri);
            uriBuilder.Query = query.ToString();

            return uriBuilder.ToString();
        }
    }
}
