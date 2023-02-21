using Amalia.Data;
using Amalia.Services;

namespace Amalia.Models
{
    public class CategoryDetailViewModel
    {
        public Category Category { get; set; } 
        public IEnumerable<CategoryExample> Examples { get; set; }

        public CategoryDetailViewModel(DataService dataService, int id)
        {
            Examples = dataService.GetCategoryExamplesById(id);
            Category = dataService.GetCategoryById(id);
        }
    }
}
