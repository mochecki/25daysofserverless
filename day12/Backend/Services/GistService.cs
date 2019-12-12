using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.Extensions.Caching.Distributed;
using Octokit;

namespace Backend.Services
{
    public class GistService : IGistService
    {
        private readonly GitHubClient _githubClient;
        private readonly IDistributedCache _cache;

        public GistService(GitHubClient githubClient, IDistributedCache cache)
        {
            _githubClient = githubClient;
            _cache = cache;
        }

        public async Task<IEnumerable<MarkdownCard>> GetCardsAsync(string gistId)
        {
            List<MarkdownCard> cachedValues = await _cache.GetAsync<List<MarkdownCard>>(gistId);
            if(cachedValues == null) 
            {
                var gist = await _githubClient.Gist.Get(gistId);

                cachedValues = new List<MarkdownCard>();
                foreach(var file in gist.Files)
                {
                    cachedValues.Add(new MarkdownCard() { Filename = file.Key, Content = file.Value.Content });
                }

                await _cache.SetAsync<List<MarkdownCard>>(gistId, cachedValues, new DistributedCacheEntryOptions());
            }

            return cachedValues;
        }
    }
}