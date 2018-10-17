using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppManager.Models
{
  public class Application
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Repo { get; set; }
    public string Access { get; set; }
    public int PlatformId { get; set; }
    public string Status { get; set; }
    public int OwnerId { get; set; }
    public string Description { get; set; }

    public virtual Platform Platform { get; set; }
    public virtual User User { get; set; }
  }
}