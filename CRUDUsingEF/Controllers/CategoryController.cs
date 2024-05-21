using CRUDUsingEF.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace CRUDUsingEF.Controllers
{
    [Authorize]
    public class CategoryController : ApiController
    {
        ProductDBContext _db = new ProductDBContext();
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Category>))]
        public IHttpActionResult GetAll()
        {
            return Ok(_db.Categories.ToList());
        }

        [HttpGet]
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategoryById(int id)
        {
            return Ok(_db.Categories.Find(id));
        }

        [HttpPost]
        public IHttpActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return Created("DefaultApi", category);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPut]
        public IHttpActionResult Update(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                if (category == null && category.Id > 0)
                {
                    if (category.Id == id)
                    {
                        Category category1 = new Category();
                        category1.Id = id;
                        category1.Name = category.Name;

                        _db.Categories.Add(category1);
                        _db.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
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
        public IHttpActionResult Delete(int id)
        {
            if (id > 0)
            {
                Category _category = _db.Categories.Find(id);
                if (_category != null)
                {
                    _db.Categories.Remove(_category);
                    _db.SaveChanges();

                    return Ok();
                }
                else { return BadRequest(); }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
