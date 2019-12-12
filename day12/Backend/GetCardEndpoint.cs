using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Backend.Services;

namespace Backend
{
    public class GetCardEndpoint
    {
        private readonly ICardService _cardService;

        public GetCardEndpoint(ICardService cardService)
        {
            _cardService = cardService;
        }

        [FunctionName("GetCardEndpoint")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string name = req.Query["name"];

            var cardData = await _cardService.GetCardAsync(Environment.GetEnvironmentVariable("SourceGistId"), name);

            return new ContentResult()
            {
                Content = $"<html><body>{cardData}</body></html>",
                ContentType = "text/html"
            };
        }
    }
}
