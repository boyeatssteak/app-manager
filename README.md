# app-manager
**A reference tool for keeping track of servers and applications, their environments and relationships.**
The idea behind this web application would be to help IT teams work more efficiently by having a simple resource that parties in different roles can reference when they need information.
 - Infrastructure team wants to do maintenance on a server, but needs to know what users or applications would be affected
 - Application team needs to know what a server's IP address is
 - Security team needs to know who authorizes access to a specific application
 - Support team needs documentation on application


## Function
Search must be useable in all areas of the app, and should index URLs, server names, IP addresses, and names. Applications should be surfaced by prod instance URL.

General browsing should be quick and user friendly showing broad but basic information about wherever the user is at in the application. As the user clicks further in to any area of the app, the information surfaced should be more detailed, but not overwhelming. Perhaps a simple icon and/or color that is associated with each structure item will help the user clearly recognize what information pertains to what.

 - Should be be able to inline add new structure items while creating different structure item (can create record of new server while creating record of new app instance)
 - New applications should point user to creating new app instance as part of app creation process.


## Structure

### Servers
The physical or virtual machines on which the applications run
(_localhost_)

**Potential columns**
 - hostname
 - IP Address
 - OS
 - Environment
 - Status (_Live_, _Deprecated_)

### Applications
Software peforming a specific role.
(_Company Website_)

**Potential Columns**
 - Name
 - Description
 - URL
 - Repo
 - Access (Public, auth only - if auth, by who?)
 - **Platform**
 - Status (_Live_, _Deprecated_)
 - Owner

### Instances
Copies of an application on different servers. A dev and production version of a company's website would be two instances of the same app since they are meant to represent the exact same content and functionality. However, just because there are similarities does not mean two things are instances. For example, a WordPress test site would not be an instance of the Company Website even if that was built on WordPress, and a second SharePoint farm that is just to test features would not be an instance of the or Production SharePoint application. They would be their own applications named as "test" or "dev".

An instance should be a _copy_ (same platform, content, and other characteristics) of other instances within the same application - not just applications that have similar platforms or features, but different content. Instances of an application should be able to relatively easily copy features, functions, and content between each other.
(_Development_, _Production_)

**Potential Columns**
 - **Application**
 - Environment
 - Name (defaults to "_[AppName]_ - _[Environment]_" if blank)
 - **Server(s)**
 - Status (_Live_, _Deprecated_)

### Platforms
A content management system or other software that may be used as the foundation for an application
(_WordPress_, _SharePoint_)

**Potential Columns**
 - Name
 - Description
 - Vendor
 - Vendor documentation
 - Internal documentation

### Secure Area
An area of a application that has unique access permissions compared to the main application
(_SharePoint Subsite_, _WordPress Dashboard_, _Central Admin_)

**Potential Columns**
 - Name
 - Description
 - URL
 - Secure Owner
 - **Application**

### Other
Comments/Notes, Vendors, Users

## Future Features
 - New applications created with a specific platform would inherit relevant secure areas, authentication methods, documentation, and auto-create appropriate default secure areas (SharePoint Central Admin, WordPRess Dahsboard) with app owner as secure area owner
 - Audit log and/or versioning
 - Store license information (single licenses area, users chooses whether license applies to app, instance, etc?)
 - Granular security controls
 - Reports such as server vs instance environment conflicts, recommended decoms, etc
 - Digital approval of system access requests
 - Track popular clicks in order to have homepage surface likely servers/apps/instances that users may be looking for
 - Add security, maybe initially just support basic LDAP authorization