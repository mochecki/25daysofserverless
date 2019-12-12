using System.Threading.Tasks;

namespace Backend.Services
{
    public interface ICardService
    {
         Task<string> GetCardAsync(string gistId, string name);
    }
}