using Amalia.Data;
using Amalia.Services;

namespace Amalia.Models
{
    public class ViewMessageModel
    {
        public SupportRequest Message { get; set; }

        public ViewMessageModel(DataService dataService, long id)
        {
            Message = dataService.GetSupportRequest(id);
        }
    }
}
