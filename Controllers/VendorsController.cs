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
    public class VendorsController : Controller
    {
        private readonly AppManagerContext _context;

        public VendorsController(AppManagerContext context)
        {
            _context = context;
        }

        // GET api/vendors
        // [HttpGet, Authorize]
        [HttpGet]
        public IEnumerable<Vendor> GetVendors()
        {
            return _context.Vendors;
        }

        // GET api/vendors/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = await _context.Vendors.SingleOrDefaultAsync(m => m.Id == id);

            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        // PUT api/vendors/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor([FromRoute] int id, [FromBody] Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST api/vendors
        [HttpPost]
        public async Task<IActionResult> PostVendor([FromBody] Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkDuplicate = await _context.Vendors.SingleOrDefaultAsync(m => m.Name == vendor.Name);
            if (checkDuplicate == null)
            {
                _context.Vendors.Add(vendor);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetVendor", new Vendor { Id = vendor.Id }, vendor);
            }
            else
            {
                // Duplicate vendorname - return an HTTP 409 conflict error
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
        }

        // DELETE api/vendors/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = await _context.Vendors.SingleOrDefaultAsync(m => m.Id == id);
            if (vendor ==  null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return Ok(vendor);
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}