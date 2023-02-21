using Amalia.Data;
using Amalia.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace Amalia.Services
{
    public class DataService
    {
        private ApplicationDbContext _db { get; set; }
        public DataService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<SupportRequest> GetSupportRequests()
        {
            return _db.SupportRequests.OrderByDescending(x => x.CreatedAt);
        }

        public SupportRequest GetSupportRequest(long id)
        {
            return _db.SupportRequests.Include(x => x.Category).SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Category> GetCategories(string username)
        {
            return _db.Categories.Where(x => x.Username == username).OrderBy(x => x.Name);
        }

        public Category GetCategoryByName(string username, string name)
        {
            return _db.Categories.FirstOrDefault(x => x.Username == username && x.Name == name);
        }

        public Category GetCategoryById(int id)
        {
            return _db.Categories.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<CategoryExample> GetCategoryExamples(string username)
        {
            return _db.CategoryExamples.Include(x => x.Category).Where(x => x.Category.Username == username);
        }

        public IEnumerable<CategoryExample> GetCategoryExamplesById(int id)
        {
            return _db.CategoryExamples.Include(x => x.Category).Where(x => x.CategoryId == id);
        }

        public void SaveCategory(string categoryName, string userName)
        {
            Category category = new Category
            {
                Name = categoryName,
                Username = userName
            };
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        public void SaveCategoryExample(int categoryId, string name)
        {
            CategoryExample category = new CategoryExample
            {
                Name = name,
                CategoryId = categoryId
            };
            _db.CategoryExamples.Add(category);
            _db.SaveChanges();
        }

        public IEnumerable<string> GetAvailableUsers()
        {
            var users = _db.Categories.Select(x => x.Username).Distinct();
            return users;
        }

        public async Task SaveMessage(IConfiguration configuration, string title, string message, string from, string to, string username)
        {
            var client = new HttpClient();
            List<CategoryRequestExample> exampleList =  GetCategoryExamples(username).Select(x => new CategoryRequestExample { text = x.Name, label = x.Category.Name }).ToList();
            
            ClassificationRequest classificationRequest = new ClassificationRequest
            {
                 inputs = new List<string> { message },
                  truncate = "END",
                  examples = exampleList
            };
            var json = JsonConvert.SerializeObject(classificationRequest);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.cohere.ai/classify"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Cohere-Version", "2022-12-06" },
                    { "authorization", "Bearer "+configuration["CohereAPI"] },
                },
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<ClassificationResponse>(body);
                if(respuesta != null)
                {
                    SupportRequest supportRequest = new SupportRequest
                    {
                        CreatedAt = DateTime.Now,
                        From = from,
                        Message = message,
                        Title = title,
                        To = to
                    };
                    var category = GetCategoryByName(username,respuesta.classifications.OrderByDescending(x => x.confidence).FirstOrDefault().prediction);
                    if(category != null)
                    {
                        supportRequest.CategoryId = category.Id;
                    }
                    _db.SupportRequests.Add(supportRequest);
                    _db.SaveChanges();
                }
            }
        }
    }
}
