using LibraryApp.DAL.Context;
using LibraryApp.DAL.Entities.Base;
using LibraryApp.Interfaces.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Repositories
{
    public class DbNamedRepository<T> : DbRepository<T>, INamedRepository<T> where T : NamedEntity, new()
    {
        public DbNamedRepository(DataDB db):base(db)
        {
            
        }

        public async Task<T> DeleteByName(string name, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(i => i.Name == name);
            if (item is null)
                item = await Set
                    .Select(i => new T { Id = i.Id, Name = i.Name })
                    .FirstOrDefaultAsync(i => i.Name == name, cancel)
                    .ConfigureAwait(false);

            if(item is null)
                return null;

            return await Delete(item, cancel).ConfigureAwait(false);
        }

        public async Task<bool> ExistName(string name, CancellationToken cancel = default)
        {
            return await Items.AnyAsync(item =>  item.Name == name, cancel).ConfigureAwait(false);
        }

        public async Task<T> GetByName(string name, CancellationToken cancel = default)
        {
            return await Items.FirstOrDefaultAsync(item => item.Name == name, cancel).ConfigureAwait(false);
        }

    }
}
