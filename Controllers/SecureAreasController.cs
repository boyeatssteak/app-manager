using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using AppManager.Data;
using AppManager.Models;

namespace AppManager.Controllers
{
    [Route("api/[controller]")]
    public class SecureAreasController : Controller
    {
        private readonly AppManagerContext _context;

        public SecureAreasController(AppManagerContext context)
        {
            _context = context;
        }

        // GET api/secureareas
        // [HttpGet, Authorize]
        [HttpGet]
        public IEnumerable<SecureArea> GetSecureAreas()
        {
            return _context.SecureAreas;
        }

        // GET api/secureareas/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSecureArea([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var securearea = await _context.SecureAreas.SingleOrDefaultAsync(m => m.Id == id);

            if (securearea == null)
            {
                return NotFound();
            }

            return Ok(securearea);
        }

        // PUT api/secureareas/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecureArea([FromRoute] int id, [FromBody] SecureArea securearea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != securearea.Id)
            {
                return BadRequest();
            }

            _context.Entry(securearea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecureAreaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST api/secureareas
        [HttpPost]
        public async Task<IActionResult> PostSecureArea([FromBody] SecureArea securearea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkDuplicate = await _context.SecureAreas.SingleOrDefaultAsync(m => m.Url == securearea.Url);
            if (checkDuplicate == null)
            {
                _context.SecureAreas.Add(securearea);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSecureArea", new SecureArea { Id = securearea.Id }, securearea);
            }
            else
            {
                // Duplicate secureareaname - return an HTTP 409 conflict error
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
        }

        // DELETE api/secureareas/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecureArea([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var securearea = await _context.SecureAreas.SingleOrDefaultAsync(m => m.Id == id);
            if (securearea ==  null)
            {
                return NotFound();
            }

            _context.SecureAreas.Remove(securearea);
            await _context.SaveChangesAsync();

            return Ok(securearea);
        }

        private bool SecureAreaExists(int id)
        {
            return _context.SecureAreas.Any(e => e.Id == id);
        }
    }
}