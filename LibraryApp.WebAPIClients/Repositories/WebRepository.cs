using LibraryApp.Interfaces.Base.Entities;
using LibraryApp.Interfaces.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApp.WebAPIClients.Repositories
{
    public class WebRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly HttpClient _client;

        public WebRepository(HttpClient client)
        {
            _client = client;
        }

        public Task<T> Add(T item, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(T item, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteById(int id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(T item, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistId(int Id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get(int Skip, int Count, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount(CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPage<T>> GetPage(int PageIndex, int PageSize, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T item, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
