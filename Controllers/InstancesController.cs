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
    public class InstancesController : Controller
    {
        private readonly AppManagerContext _context;

        public InstancesController(AppManagerContext context)
        {
            _context = context;
        }

        // GET api/instances
        // [HttpGet, Authorize]
        [HttpGet]
        public IEnumerable<Instance> GetInstances()
        {
            return _context.Instances;
        }

        // GET api/instances/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instance = await _context.Instances.SingleOrDefaultAsync(m => m.Id == id);

            var query =
                new
                {
                    // instances {Id, Env, Url, Name, Status}
                    instance,
                    // application {Id, Name, Platform, PlatformId, Owner, OwnerId}
                    application = 
                    from application in _context.Applications
                    join platform in _context.Platforms on application.PlatformId equals platform.Id
                    join users in _context.Users on application.OwnerId equals users.Id
                    where instance.AppId == application.Id
                    select new
                    {
                        id = application.Id,
                        name = application.Name,
                        platformId = platform.Id,
                        platform = platform.Name,
                        ownerId = users.Id,
                        owner = users.Name
                    },
                    // servers {Id, hostname, domain, ipAddress, opSystem, role}
                    servers = 
                    from _is in _context.InstanceServers
                    join server in _context.Servers on _is.ServerId equals server.Id 
                    where _is.InstanceId == instance.Id
                    select new
                    {
                        id = server.Id,
                        hostname = server.Hostname,
                        domain = server.Domain,
                        ipAddress = server.IpAddress,
                        opSystem = server.OpSystem,
                        role = server.Role
                    }
                };

            if (instance == null)
            {
                return NotFound();
            }

            return Ok(query);
        }

        // PUT api/instances/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstance([FromRoute] int id, [FromBody] Instance instance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instance.Id)
            {
                return BadRequest();
            }

            _context.Entry(instance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstanceExists(id))
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

        // POST api/instances
        [HttpPost]
        public async Task<IActionResult> PostInstance([FromBody] Instance instance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkDuplicate = await _context.Instances.SingleOrDefaultAsync(m => m.Url == instance.Url);
            if (checkDuplicate == null)
            {
                _context.Instances.Add(instance);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetInstance", new Instance { Id = instance.Id }, instance);
            }
            else
            {
                // Duplicate instancename - return an HTTP 409 conflict error
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
        }

        // DELETE api/instances/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instance = await _context.Instances.SingleOrDefaultAsync(m => m.Id == id);
            if (instance ==  null)
            {
                return NotFound();
            }

            _context.Instances.Remove(instance);
            await _context.SaveChangesAsync();

            return Ok(instance);
        }

        private bool InstanceExists(int id)
        {
            return _context.Instances.Any(e => e.Id == id);
        }
    }
}