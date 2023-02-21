using Amalia.Data;
using Amalia.Services;

namespace Amalia.Models
{
    public class ListMessagesViewModel
    {
        private DataService dataService;
        public IEnumerable<SupportRequest> Messages { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public ListMessagesViewModel(DataService dataService, int? categoryId, string userName)
        {
            this.dataService = dataService;
            Categories = dataService.GetCategories(userName);
            Messages = dataService.GetSupportRequests();
            if(categoryId.HasValue)
            {
                Messages = Messages.Where(x => x.CategoryId == categoryId.Value);
            }
        }

    }
}
