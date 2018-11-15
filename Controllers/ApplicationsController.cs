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
    public class ApplicationsController : Controller
    {
        private readonly AppManagerContext _context;

        public ApplicationsController(AppManagerContext context)
        {
            _context = context;
        }

        // GET api/applications
        // [HttpGet, Authorize]
        [HttpGet]
        public IEnumerable<Application> GetApplications()
        {
            return _context.Applications;
        }

        // GET api/applications/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _context.Applications.SingleOrDefaultAsync(m => m.Id == id);

            var query =
                new
                {
                    // application {Id, Name, Description, RepoURL, Platform, PlatformId, Audience(Access), Owner, OwnerId, Status(Green Light)}
                    application =
                    from app in _context.Applications
                    join user in _context.Users on app.OwnerId equals user.Id
                    join platform in _context.Platforms on app.PlatformId equals platform.Id
                    where app.Id == application.Id
                    select new
                    {
                        id = app.Id,
                        name = app.Name,
                        desc = app.Description,
                        repo = app.Repo,
                        audience = app.Access,
                        status = app.Status,
                        platformId = app.PlatformId,
                        platform = platform.Name,
                        ownerId = app.OwnerId,
                        owner = user.Name
                    },
                    // instances {Id, Env, Url, Notes, {ServerId, hostname, domain, ipAddress}, Status}
                    instances = 
                    from instance in _context.Instances
                    where instance.AppId == application.Id
                    select new
                    {
                        id = instance.Id,
                        name = instance.Name,
                        env = instance.Environment,
                        status = instance.Status,
                        url = instance.Url,
                        servers = 
                        from _is in _context.InstanceServers
                        join server in _context.Servers on _is.ServerId equals server.Id 
                        where _is.InstanceId == instance.Id
                        select new
                        {
                            id = server.Id,
                            hostname = server.Hostname,
                            domain = server.Domain,
                            ipAddress = server.IpAddress
                        }
                    },
                    // secure areas {Id, Name, Url, Owner, OwnerId}
                    secureAreas =
                    from secArea in _context.SecureAreas
                    join user in _context.Users on secArea.OwnerId equals user.Id
                    where secArea.AppId == application.Id
                    select new
                    {
                        id = secArea.Id,
                        name = secArea.Description,
                        url = secArea.Url,
                        ownerId = user.Id,
                        owner = user.Name
                    }
                };

            if (application == null)
            {
                return NotFound();
            }

            return Ok(query);
        }

        // PUT api/applications/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication([FromRoute] int id, [FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != application.Id)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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

        // POST api/applications
        [HttpPost]
        public async Task<IActionResult> PostApplication([FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkDuplicate = await _context.Applications.SingleOrDefaultAsync(m => m.Name == application.Name);
            if (checkDuplicate == null)
            {
                _context.Applications.Add(application);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetApplication", new Application { Id = application.Id }, application);
            }
            else
            {
                // Duplicate applicationname - return an HTTP 409 conflict error
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
        }

        // DELETE api/applications/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _context.Applications.SingleOrDefaultAsync(m => m.Id == id);
            if (application ==  null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return Ok(application);
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}