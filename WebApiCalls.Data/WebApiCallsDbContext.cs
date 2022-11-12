using Microsoft.EntityFrameworkCore;
using System;

namespace WebApiCalls.Data
{
    public class WebApiCallsDbContext : DbContext
    {
        public DbSet<ContactSession> ContactSessions { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public WebApiCallsDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
