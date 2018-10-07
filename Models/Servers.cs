using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppManager.Models
{
  public class Server
  {
    public int Id { get; set; }
    public string Hostname { get; set; }
  }
}