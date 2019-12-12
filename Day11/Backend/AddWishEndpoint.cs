using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moc.Serverless.Model;

namespace Moc.Serverless
{
    public static class AddWishEndpoint
    {
        [FunctionName("wish")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [CosmosDB("SantaDB", "Wishes", ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<Wish> wishes,
            ILogger log)
        {
            var formData = await req.ReadFormAsync();

            await wishes.AddAsync(new Wish()
            {
                Type = formData["Type"],
                Address = formData["Address"],
                Author = formData["Author"],
                Description = formData["Description"]
            });
            
            return new OkResult();
        }
    }
}
