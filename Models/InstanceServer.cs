using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppManager.Models
{
  public class InstanceServer
  { 
    [Key]
    public int Id { get; set; }
    public int InstanceId { get; set; }
    public int ServerId { get; set; }
    
    public Instance Instance { get; set; }
    public Server Server { get; set; }
  }
}