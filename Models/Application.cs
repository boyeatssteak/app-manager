using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppManager.Models
{
  public class Application
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Repo { get; set; }
    public string Access { get; set; }
    public string Platform { get; set; }
    public string Status { get; set; }
    public string Owner { get; set; }
    public string Description { get; set; }
  }
}