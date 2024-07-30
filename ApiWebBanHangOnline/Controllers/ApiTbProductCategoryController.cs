using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiWebBanHangOnline.Data;
using Model.Domain;

namespace ApiWebBanHangOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TbProductCategoryController : ControllerBase
    {
        private readonly ApiDbContext _db;

        public TbProductCategoryController(ApiDbContext context)
        {
            _db = context;
        }

        // GET: api/TbProductCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbProductCategory>>> GetTbProductCategories()
        {
            return await _db.TbProductCategories.OrderBy(x=>x.Title).ToListAsync();
        }

        // GET: api/TbProductCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbProductCategory>> GetTbProductCategory(Guid id)
        {
            var tbProductCategory = await _db.TbProductCategories.FindAsync(id);

            if (tbProductCategory == null)
            {
                return NotFound();
            }

            return tbProductCategory;
        }

        // PUT: api/TbProductCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbProductCategory(Guid id, [FromBody] TbProductCategory? tbProductCategory)
        {
            var item = await _db.TbProductCategories.FindAsync(id);
            if (item == null) { return NotFound(); }

            if (tbProductCategory == null)
            {
                item.IsActive = !item.IsActive;
            }
            else
            {
                item.Title  = tbProductCategory.Title;
                item.IsActive = tbProductCategory.IsActive;
            }

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbProductCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(item);
        }

        // POST: api/TbProductCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TbProductCategory>> PostTbProductCategory([FromBody] TbProductCategory tbProductCategory)
        {
            _db.TbProductCategories.Add(tbProductCategory);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetTbProductCategory", new { id = tbProductCategory.Id }, tbProductCategory);
        }

        // DELETE: api/TbProductCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTbProductCategory(Guid id)
        {
            var tbProductCategory = await _db.TbProductCategories.FindAsync(id);
            if (tbProductCategory == null)
            {
                return NotFound();
            }

            _db.TbProductCategories.Remove(tbProductCategory);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool TbProductCategoryExists(Guid id)
        {
            return _db.TbProductCategories.Any(e => e.Id == id);
        }
    }
}
