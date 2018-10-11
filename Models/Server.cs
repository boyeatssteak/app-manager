using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppManager.Models
{
  public class Server
  {
    [Key]
    public int Id { get; set; }
    public string Hostname { get; set; }
    public string IpAddress { get; set; }
    public string OpSystem { get; set; }
    public string Role { get; set; }
    public string Status { get; set; }
    public string Domain { get; set; }

    public virtual ICollection<InstanceServer> InstanceServers { get; set; }
  }
}