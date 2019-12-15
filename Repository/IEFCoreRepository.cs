using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api {
    public interface IEFCoreRepository {
        void Add<T> (T entity) where T : class;
        void Update<T> (T entity) where T : class;
        void Delete<T> (T entity) where T : class;
        Task<bool> SaveChangeAsync ();
        Task<Client[]> GetAllClients ();
        Task<Client> GetClientById (int id);
        Task<Client[]> GetClientBynome (string nome);
        Task<Address> GetAddressById (int id);
    }
}