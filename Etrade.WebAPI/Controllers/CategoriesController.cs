using Etrade.DAL.Abstract;
using Etrade.Data.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Etrade.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryDAL _categoryDAL;
        //CategoriesController sınıfının constructor metodu
        public CategoriesController(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        //Tüm kategotileri getiren HTTP GET isteği
        [HttpGet]
        public IActionResult Get()
        {
            //Status Code - 200 OK
            //İsteğin başarılı olduğunu gösterir.
            return Ok(_categoryDAL.GetAll());
        }

        //Belirli bir kategoriyi getiren HTTP GET isteği
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null || _categoryDAL.GetAll() == null)
            {
                //400 Durum Kodu ( Bad Request )
                //Yapılan isteğin hatalı bir biçimde olduğunu belirtir
                return BadRequest();
            }
            var category = _categoryDAL.Get(Convert.ToInt32(id));
            if (category == null)
            {
                return NotFound("Category not found!!");
            }

            return Ok(category);
        }
        //Yeni bir kategori eklemek için HTTP POST isteği
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedDate = DateTime.Now;
                _categoryDAL.Add(category);
                return CreatedAtAction("Get", new { id = category.Id }, category);
            }
            return BadRequest();
        }
        //Var olan bir kategoriyi güncelleyn HTTP PUT metodu
        [HttpPut]
        public IActionResult Put([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                category.UpdatedDate = DateTime.Now;
                _categoryDAL.Update(category);
                return Ok(category);
            }
            return BadRequest();
        }
        //Belirli bit kategori ID'sine sahip kategoriy isilen HTTP DELETE metodu
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _categoryDAL.Get(x => x.Id == id);
            if (category == null)
                return BadRequest();
            _categoryDAL.Delete(id);
            return Ok();
        }
    }
}
