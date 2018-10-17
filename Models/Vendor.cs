using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppManager.Models
{
  public class Vendor
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Website { get; set; }
    public string Contact { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
  }
}