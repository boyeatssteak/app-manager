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
    public class ServersController : Controller
    {
        private readonly AppManagerContext _context;

        public ServersController(AppManagerContext context)
        {
            _context = context;
        }

        // GET api/servers
        // [HttpGet, Authorize]
        [HttpGet]
        public IEnumerable<Server> GetServers()
        {
            return _context.Servers;
        }

        // GET api/servers/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var server = await _context.Servers.SingleOrDefaultAsync(m => m.Id == id);

            var query =
                new
                {
                    server,
                    instances = 
                    from _is in _context.InstanceServers
                    join instance in _context.Instances on _is.InstanceId equals instance.Id
                    join app in _context.Applications on instance.AppId equals app.Id
                    join user in _context.Users on app.OwnerId equals user.Id
                    where _is.ServerId == server.Id
                    select new
                    {
                        id = instance.Id,
                        name = instance.Name,
                        env = instance.Environment,
                        status = instance.Status,
                        url = instance.Url,
                        appId = app.Id,
                        appName = app.Name,
                        appStatus = app.Status,
                        ownerId = user.Id,
                        ownerName = user.Name
                    }
                };

            if (server == null)
            {
                return NotFound();
            }

            return Ok(query);

        }

        // PUT api/servers/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServer([FromRoute] int id, [FromBody] Server server)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != server.Id)
            {
                return BadRequest();
            }

            _context.Entry(server).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerExists(id))
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

        // POST api/servers
        [HttpPost]
        public async Task<IActionResult> PostServer([FromBody] Server server)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkHostname = await _context.Servers.SingleOrDefaultAsync(m => m.Hostname == server.Hostname);
            if (checkHostname == null)
            {
                _context.Servers.Add(server);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetServer", new Server { Id = server.Id }, server);
            }
            else
            {
                // Duplicate servername - return an HTTP 409 conflict error
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
        }

        // DELETE api/servers/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var server = await _context.Servers.SingleOrDefaultAsync(m => m.Id == id);
            if (server ==  null)
            {
                return NotFound();
            }

            _context.Servers.Remove(server);
            await _context.SaveChangesAsync();

            return Ok(server);
        }

        private bool ServerExists(int id)
        {
            return _context.Servers.Any(e => e.Id == id);
        }
    }
}