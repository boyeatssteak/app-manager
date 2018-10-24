using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppManager.Data;

namespace AppManager.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppManagerContext(
                    serviceProvider.GetRequiredService<DbContextOptions<AppManagerContext>>()))
            {
                // Check if any Applications, Instances, and Servers already exist
                if ( context.Applications.Any() && context.Instances.Any() && context.Servers.Any() )
                {
                    return;
                }

                var users = new User[]
                {
                    new User { Name = "John Doe", Email = "john@company.com", Title = "Communications Manager", Phone = "123-123-1000", Department = "Communications" },
                    new User { Name = "Jane Smith", Email = "jane@company.com", Title = "Collaboration Manager", Phone = "123-123-1001", Department = "IT" },
                    new User { Name = "Harold User", Email = "harold@company.com", Title = "Assistant", Phone = "123-123-1002", Department = "Human Resources" },
                    new User { Name = "Sally Boss", Email = "sally@company.com", Title = "President", Phone = "123-123-1004", Department = "Executive" }
                };
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();

                var vendors = new Vendor[]
                {
                    new Vendor { Name = "Automattic", Website = "https://automattic.com/" },
                    new Vendor { Name = "Microsoft", Website = "https://www.microsoft.com/" },
                    new Vendor { Name = "Local Shop", Website = "https://example.com", Contact = "Salesman Jack", ContactPhone = "123-456-7890", ContactEmail = "jack@example.com" }
                };
                foreach (Vendor v in vendors)
                {
                    context.Vendors.Add(v);
                }
                context.SaveChanges();

                var platforms = new Platform[]
                {
                    new Platform { Name = "WordPress", Description = "WordPress is open source software you can use to create a beautiful website, blog, or app.", VendorId = vendors.Single(v => v.Name == "Automattic" ).Id, VendorDocs = "https://codex.wordpress.org/", InternalDocs = "http://intranet.company.com/wiki/wordpress" },
                    new Platform { Name = "SharePoint 2019", Description = "A web-based collaborative platform that integrates with Microsoft Office", VendorId = vendors.Single(v => v.Name == "Microsoft").Id, VendorDocs = "https://docs.microsoft.com/en-us/sharepoint/", InternalDocs = "http://intranet.company.com/wiki/sharepoint" }
                };
                foreach (Platform p in platforms)
                {
                    context.Platforms.Add(p);
                }
                context.SaveChanges();

                var applications = new Application[]
                {
                    new Application { Name = "Company Website", Repo = "https://github.com/username/companywebsite", Access = "Public", PlatformId = platforms.Single(p => p.Name == "WordPress").Id, Status = "Development", OwnerId = users.Single(u => u.Name == "John Doe").Id, Description = "Public face of the company" },
                    new Application { Name = "Company SharePoint", Access = "AD Authenticated Users", PlatformId = platforms.Single(p => p.Name == "SharePoint 2019").Id, Status = "Live", OwnerId = users.Single(u => u.Name == "Jane Smith").Id, Description = "Resource for employees to find company documents"},
                    new Application { Name = "Project Website", Repo = "https://github.com/username/projectwebsite", Access = "Public", PlatformId = platforms.Single(p => p.Name == "WordPress").Id, Status = "Produciton", OwnerId = users.Single(u => u.Name == "Jane Smith").Id, Description = "Project website to feature special stuff" }
                };
                foreach (Application a in applications)
                {
                    context.Applications.Add(a);
                }
                context.SaveChanges();

                var instances = new Instance[]
                {
                    new Instance { Name = "Company Website - Dev", AppId = applications.Single(a => a.Name == "Company Website").Id, Environment = "Dev", Status = "Live", Url = "http://devwebsite.company.com/" },
                    new Instance { Name = "Company Website - Prod", AppId = applications.Single(a => a.Name == "Company Website").Id, Environment = "Prod", Status = "Planned", Url = "https://company.com" },
                    new Instance { Name = "Company Website - Test", AppId = applications.Single(a => a.Name == "Company Website").Id, Environment = "Test", Status = "Live", Url = "http://testwebsite.company.com/" },
                    new Instance { Name = "Primary Farm", AppId = applications.Single(a => a.Name == "Company SharePoint").Id, Environment = "Prod", Status = "Live", Url = "https://sharepoint.company.com" },
                    new Instance { Name = "Project Website - Prod", AppId = applications.Single(a => a.Name == "Project Website").Id, Environment = "Prod", Status = "Live", Url = "https://project.company.com" },
                    new Instance { Name = "Project Website - Dev", AppId = applications.Single(a => a.Name == "Project Website").Id, Environment = "Dev", Status = "Live", Url = "https://devproject.company.com" }
                };
                foreach (Instance i in instances)
                {
                    context.Instances.Add(i);
                }
                context.SaveChanges();

                var servers = new Server[]
                {
                    new Server { Hostname = "server-web", IpAddress = "10.1.10.20", OpSystem = "RHEL 7", Role = "Production", Status = "Deployment", Domain = "resource.add" },
                    new Server { Hostname = "server-devweb", IpAddress = "10.1.20.20", OpSystem = "RHEL 7", Role = "Development", Status = "Live", Domain = "resource.add" },
                    new Server { Hostname = "server-testweb", IpAddress = "10.1.30.20", OpSystem = "RHEL 7", Role = "Test", Status = "Live", Domain = "resource.add" },
                    new Server { Hostname = "server-oldtimes", IpAddress = "10.1.10.20", OpSystem = "RHEL 6", Role = "Production", Status = "Deprecated", Domain = "resource.add" },
                    new Server { Hostname = "server-spsql", IpAddress = "10.1.10.31", OpSystem = "Windows Server 2016", Role = "Production", Status = "Live", Domain = "company.corp" },
                    new Server { Hostname = "server-spapp", IpAddress = "10.1.10.32", OpSystem = "Windows Server 2016", Role = "Production", Status = "Live", Domain = "company.corp" },
                    new Server { Hostname = "server-spfe", IpAddress = "10.1.10.33", OpSystem = "Windows Server 2016", Role = "Production", Status = "Live", Domain = "company.corp" }
                };
                foreach (Server s in servers)
                {
                    context.Servers.Add(s);
                }
                context.SaveChanges();

                var instanceservers = new InstanceServer[]
                {
                    new InstanceServer { InstanceId = instances.Single(i => i.Name == "Company Website - Dev").Id, ServerId = servers.Single(s => s.Hostname == "server-devweb").Id },
                    new InstanceServer { InstanceId = instances.Single(i => i.Name == "Company Website - Prod").Id, ServerId = servers.Single(s => s.Hostname == "server-web").Id },
                    new InstanceServer { InstanceId = instances.Single(i => i.Name == "Company Website - Test").Id, ServerId = servers.Single(s => s.Hostname == "server-testweb").Id },
                    new InstanceServer { InstanceId = instances.Single(i => i.Name == "Primary Farm").Id, ServerId = servers.Single(s => s.Hostname == "server-spsql").Id },
                    new InstanceServer { InstanceId = instances.Single(i => i.Name == "Primary Farm").Id, ServerId = servers.Single(s => s.Hostname == "server-spapp").Id },
                    new InstanceServer { InstanceId = instances.Single(i => i.Name == "Primary Farm").Id, ServerId = servers.Single(s => s.Hostname == "server-spfe").Id },
                    new InstanceServer { InstanceId = instances.Single(i => i.Name == "Project Website - Dev").Id, ServerId = servers.Single(s => s.Hostname == "server-web").Id },
                    new InstanceServer { InstanceId = instances.Single(i => i.Name == "Project Website - Prod").Id, ServerId = servers.Single(s => s.Hostname == "server-web").Id }
                };
                foreach (InstanceServer _is in instanceservers)
                {
                    context.InstanceServers.Add(_is);
                }
                context.SaveChanges();

                var securearea = new SecureArea[]
                {
                    new SecureArea { AppId = applications.Single(a => a.Name == "Company Website").Id, Description = "WordPress Dashboard", Url = "http://devwebsite.company.com/wp-admin", OwnerId = users.Single(u => u.Name == "John Doe").Id },
                    new SecureArea { AppId = applications.Single(a => a.Name == "Company SharePoint").Id, Description = "Central Admin", Url = "https://server-spfe:12345", OwnerId = users.Single(u => u.Name == "Jane Smith").Id },
                    new SecureArea { AppId = applications.Single(a => a.Name == "Company SharePoint").Id, Description = "Executives Area", Url = "https://sharepoint.company.com/executives", OwnerId = users.Single(u => u.Name == "Sally Boss").Id }
                };
                foreach (SecureArea sa in securearea)
                {
                    context.SecureAreas.Add(sa);
                }
                context.SaveChanges();

            }
        }
    }
}