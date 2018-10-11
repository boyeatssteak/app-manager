using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppManager.Models
{
  public class SecureArea
  {
    [Key]
    public int Id { get; set; }
    public int AppId { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public string OwnerId { get; set; }

    public virtual Application Application { get; set; }
    public virtual User User { get; set; }
  }
}