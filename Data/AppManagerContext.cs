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

    public DbSet<Application> Applications { get; set; }
    public DbSet<Instance> Instances { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<SecureArea> SecureAreas { get; set; }
    public DbSet<Server> Servers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<InstanceServer> InstanceServers { get; set; }
  }
}