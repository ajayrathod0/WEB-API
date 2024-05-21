using CRUDUsingEF.Models;
using System.Linq;
using System.Web.Http;

namespace CRUDUsingEF.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        ProductDBContext _db = new ProductDBContext();

        [HttpGet]
        public IHttpActionResult GetAllProduct()
        {
            return Ok(_db.Products.ToList());

        }

        [HttpGet]
        public IHttpActionResult GetProductById([FromUri] int id)
        {
            return Ok(_db.Products.Find(id));
        }



        [HttpPost]
        public IHttpActionResult CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();

                return Created("DefaultApi", product);
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpPut]
        public IHttpActionResult UpdateProduct([FromUri] int id, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product != null && product.Id >= 0)
                {
                    if (product.Id == id)
                    {
                        Product dbProduct = _db.Products.Find(id);

                        dbProduct.Id = product.Id;
                        dbProduct.Name = product.Name;
                        dbProduct.Price = product.Price;
                        dbProduct.AvailableQuantity = product.AvailableQuantity;

                        _db.SaveChanges();

                        return Ok(dbProduct);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {

                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct([FromUri] int id)
        {
            if (id > 0)
            {
                Product dbProduct = _db.Products.Find(id);
                if (dbProduct != null)
                {
                    _db.Products.Remove(dbProduct);
                    _db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
