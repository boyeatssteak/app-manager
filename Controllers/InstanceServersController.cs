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
    public class InstanceServersController : Controller
    {
        private readonly AppManagerContext _context;

        public InstanceServersController(AppManagerContext context)
        {
            _context = context;
        }

        // GET api/instanceservers
        // [HttpGet, Authorize]
        [HttpGet]
        public IEnumerable<InstanceServer> GetInstanceServers()
        {
            return _context.InstanceServers;
        }

        // GET api/instanceservers/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstanceServer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instanceserver = await _context.InstanceServers.SingleOrDefaultAsync(m => m.Id == id);

            if (instanceserver == null)
            {
                return NotFound();
            }

            return Ok(instanceserver);
        }

        // PUT api/instanceservers/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstanceServer([FromRoute] int id, [FromBody] InstanceServer instanceserver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instanceserver.Id)
            {
                return BadRequest();
            }

            _context.Entry(instanceserver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstanceServerExists(id))
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

        // POST api/instanceservers
        [HttpPost]
        public async Task<IActionResult> PostInstanceServer([FromBody] InstanceServer instanceserver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InstanceServers.Add(instanceserver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstanceServer", new InstanceServer { Id = instanceserver.Id }, instanceserver);
        }

        // DELETE api/instanceservers/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstanceServer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instanceserver = await _context.InstanceServers.SingleOrDefaultAsync(m => m.Id == id);
            if (instanceserver ==  null)
            {
                return NotFound();
            }

            _context.InstanceServers.Remove(instanceserver);
            await _context.SaveChangesAsync();

            return Ok(instanceserver);
        }

        private bool InstanceServerExists(int id)
        {
            return _context.InstanceServers.Any(e => e.Id == id);
        }
    }
}