using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Services
{
    public interface IGistService
    {
         Task<IEnumerable<MarkdownCard>> GetCardsAsync(string gistId);
    }
}