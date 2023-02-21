using Amalia.Data;
using Amalia.Services;

namespace Amalia.Models
{
    public class CategoriesViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public CategoriesViewModel(DataService dataService, string? name)
        {
            Categories = dataService.GetCategories(name);
        }
    }
}
