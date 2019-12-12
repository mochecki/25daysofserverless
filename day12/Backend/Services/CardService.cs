using System.Linq;
using System.Threading.Tasks;
using Markdig;

namespace Backend.Services
{
    public class CardService : ICardService
    {
        private readonly IGistService _gistService;

        public CardService(IGistService gistService)
        {
            _gistService = gistService;
        }

        public async Task<string> GetCardAsync(string gistId, string name)
        {
            var allCards = await _gistService.GetCardsAsync(gistId);
            var filename = name + ".md";
            var card = allCards.FirstOrDefault(e => e.Filename == filename);
            if(card != null)
            {
                return Markdown.ToHtml(card.Content);
            }

            //Default card
            return "<p>Marry Chrismas</p>";
        }
    }
}