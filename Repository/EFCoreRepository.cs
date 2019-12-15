using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository {
    public class EFCoreRepository : IEFCoreRepository {

        private readonly ClientContext _context;

        public EFCoreRepository (ClientContext context) {
            _context = context;
        }
        public void Add<T> (T entity) where T : class {
            _context.Add (entity);
        }
        public void Update<T> (T entity) where T : class {
            _context.Update (entity);
        }
        public void Delete<T> (T entity) where T : class {
            _context.Remove (entity);
        }
        public async Task<bool> SaveChangeAsync () {
            return (await _context.SaveChangesAsync ()) > 0;
        }
        public async Task<Client[]> GetAllClients () {
            IQueryable<Client> query = _context.Clients
                .Include (h => h.Address);

            query = query.AsNoTracking ().OrderBy (h => h.Id);

            return await query.ToArrayAsync ();
        }
        public async Task<Client> GetClientById (int id) {
            IQueryable<Client> query = _context.Clients
                .Include (h => h.Address);

            query = query.AsNoTracking ().OrderBy (h => h.Id);

            return await query.FirstOrDefaultAsync (h => h.Id == id);
        }
        public async Task<Client[]> GetClientBynome (string name) {
            IQueryable<Client> query = _context.Clients
                .Include (h => h.Address);

            query = query.AsNoTracking ()
                .Where (h => h.Name.Contains (name))
                .OrderBy (h => h.Id);

            return await query.ToArrayAsync ();
        }

        public async Task<Address> GetAddressById (int id) {

            IQueryable<Address> query = _context.Address;

            return await query.FirstOrDefaultAsync (h => h.Id == id);
        }

    }
}