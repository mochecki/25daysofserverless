using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Slack.Webhooks;

namespace Moc.Serverless
{
    public static class NotifyCosmosTrigger
    {
        [FunctionName("NotifyCosmosTrigger")]
        public static void Run([CosmosDBTrigger(
            databaseName: "SantaDB",
            collectionName: "Wishes",
            ConnectionStringSetting = "CosmosDBConnection",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> documents, ILogger log)
        {
            if(documents != null && documents.Count > 0)
            {
                using(var client = new SlackClient(Environment.GetEnvironmentVariable("SlackWebhookUrl")))
                {
                    foreach(var document in documents)
                    {
                        client.Post(new SlackMessage() {
                            Text = $"New wish: Dear Santa, my name is { document.GetPropertyValue<string>("Author") } " +
                                   $"and I live in { document.GetPropertyValue<string>("Address") }. " +
                                   $"I want { document.GetPropertyValue<string>("Type") }, " +
                                   $"more precisely { document.GetPropertyValue<string>("Description") }."
                        });
                    }
                }  
            }

        }
    }
}
