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
    public class PlatformsController : Controller
    {
        private readonly AppManagerContext _context;

        public PlatformsController(AppManagerContext context)
        {
            _context = context;
        }

        // GET api/platforms
        // [HttpGet, Authorize]
        [HttpGet]
        public IEnumerable<Platform> GetPlatforms()
        {
            return _context.Platforms;
        }

        // GET api/platforms/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlatform([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var platform = await _context.Platforms.SingleOrDefaultAsync(m => m.Id == id);

            if (platform == null)
            {
                return NotFound();
            }

            return Ok(platform);
        }

        // PUT api/platforms/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlatform([FromRoute] int id, [FromBody] Platform platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != platform.Id)
            {
                return BadRequest();
            }

            _context.Entry(platform).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformExists(id))
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

        // POST api/platforms
        [HttpPost]
        public async Task<IActionResult> PostPlatform([FromBody] Platform platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkDuplicate = await _context.Platforms.SingleOrDefaultAsync(m => m.Name == platform.Name);
            if (checkDuplicate == null)
            {
                _context.Platforms.Add(platform);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPlatform", new Platform { Id = platform.Id }, platform);
            }
            else
            {
                // Duplicate platformname - return an HTTP 409 conflict error
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
        }

        // DELETE api/platforms/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatform([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var platform = await _context.Platforms.SingleOrDefaultAsync(m => m.Id == id);
            if (platform ==  null)
            {
                return NotFound();
            }

            _context.Platforms.Remove(platform);
            await _context.SaveChangesAsync();

            return Ok(platform);
        }

        private bool PlatformExists(int id)
        {
            return _context.Platforms.Any(e => e.Id == id);
        }
    }
}