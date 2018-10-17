using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppManager.Models
{
  public class Platform
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int VendorId { get; set; }
    public string VendorDocs { get; set; }
    public string InternalDocs { get; set; }

    public virtual Vendor Vendor { get; set; }
  }
}