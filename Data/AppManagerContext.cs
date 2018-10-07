using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppManager.Models;

namespace AppManager.Data
{
  public class AppManagerContext : DbContext
  {
    public AppManagerContext()
    {
    }

    public AppManagerContext(DbContextOptions<AppManagerContext> options) : base(options)
    {
    }

    public DbSet<Server> Servers { get; set; }
  }
}