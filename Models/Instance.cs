using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppManager.Models
{
  public class Instance
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int AppId { get; set; }
    public string Environment { get; set; }
    public string Status { get; set; }
    public string Url { get; set; }

    public virtual ICollection<InstanceServer> InstanceServers { get; set; }
    public virtual Application Application { get; set; }
  }
}