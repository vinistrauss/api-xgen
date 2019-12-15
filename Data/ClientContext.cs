using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data {
    public class ClientContext : DbContext {

        public ClientContext (DbContextOptions<ClientContext> options) : base (options) {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Address { get; set; }

    }
}