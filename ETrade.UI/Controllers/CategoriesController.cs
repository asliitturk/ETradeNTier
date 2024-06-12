using Etrade.Data.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ETrade.UI.Controllers
{
    public class CategoriesController : Controller
    {
        //httpClient nesnesinin oluşturulması
        private HttpClient httpClient;

        public CategoriesController()
        {
            httpClient = new HttpClient();
        }

        //GET : Categories
        //Tüm kategorileri listelemek için kullanılan metod
        public async Task<IActionResult> Index()
        {
            //API'ye HTTP GET istedeği gönderme
            var responseMessage = await httpClient.GetAsync("https://localhost:7075/api/Categories");

            if (responseMessage.IsSuccessStatusCode)
            {
                //API'den gelen JSON verisini okuma ve modelleme
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<Category>>(jsonString);

                //Kategorilerin Listenlendiği view'e yönlendirme
                return View(values);
            }

            return NotFound("Could not get Category List!!!");
        }
        //GET : Categories/Create
        //Yeni bir Kategori oluşturulmak için kullanılan metod

        //GET: Categories/Details/5
        //Belirli bit kategori detayını göstermek için kullanılan metod
        public async Task<IActionResult> Details(int? id)
        {
            //API'ya HTTP GET isteği gönderme
            var responsemessage = await httpClient.GetAsync("https://localhost:7075/api/Categories/" + id);

            if (responsemessage.IsSuccessStatusCode)
            {
                //API'dan gelen JSON verisini okuma ve modelleme
                var jsonString = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<Category>(jsonString);

                //Kategori detaylarının gösterildiği view'a yönlendirme
                return View(value);
            }

            return NotFound("Category Not Found");
        }
        [Authorize(Roles =("Admin,Moderator"))]
        public IActionResult Create()
        {
            //Yeni kategori oluşturulduğu view'e yönlendirme
            return View();
        }
        //POST : Categories/Create
        //Yeni bir Kategori oluşturulmak için kullanılan HTTP POST metod
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category)
        {
            //Kategori modelini Json Formatına dönüştüme
            var jsonCategory = JsonConvert.SerializeObject(category);
            var stringContent = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

            //API'ya HTTP POST isteği gönderme
            var responseMessage = await httpClient.PostAsync("https://localhost:7075/api/Categories", stringContent);

            //Bşarı durumuna göre yönlendirme
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            //Başarısız durumda, aynı view'a geri gönderme
            return View(category);
        }
        [Authorize(Roles = ("Admin,Moderator"))]
        public async Task<IActionResult> Edit(int? id)
        {
            //Belirli bir kategorinin bilgilerini alabilmek için API'ya HTTP GET isteği yapılıyor
            var responseMessage = await httpClient.GetAsync("https://localhost:7075/api/Categories/" + id);
            //Başarılı bir şekilde alındıysa, JSON verisini oku ve Category objesine dönüştür
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<Category>(jsonString);
                return View(value);
            }

            return NotFound("Category not found");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            //Kategori objesini JSON formatına çevirme
            var jsonCategory = JsonConvert.SerializeObject(category);
            var stringContent = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

            //API'ye HTTP PUT isteği yaparak kategori güncelleme
            var responseMessage = await httpClient.PutAsync("https://localhost:7075/api/Categories", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(category);
        }
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Delete(int? id)
        {
            var responseMessage = await httpClient.GetAsync("https://localhost:7075/api/Categories/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<Category>(jsonString);
                return View(value);
            }

            return NotFound("Category not found");

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responseMessage = await httpClient.DeleteAsync("https://localhost:7075/api/Categories?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound("Category Not Found");
        }
    }
}
