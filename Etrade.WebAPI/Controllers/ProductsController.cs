using Etrade.DAL.Abstract;
using Etrade.Data.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Etrade.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductDAL _IproductDAL;

        public ProductsController(IProductDAL ıproductDAL)
        {
            _IproductDAL = ıproductDAL;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_IproductDAL.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == null || _IproductDAL.GetAll() == null )
            {
                return BadRequest();
            }

            var product = _IproductDAL.Get(Convert.ToInt32(id));
            if (product == null)
            {
                return NotFound("Ürün bulunamadı");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _IproductDAL.Add(product);
                return CreatedAtAction("Get", new { id = product.Id }, product);
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _IproductDAL.Update(product);
                return Ok(product);
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _IproductDAL.Get(x => x.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            _IproductDAL.Delete(id);
            return Ok();
        }
    }
}
