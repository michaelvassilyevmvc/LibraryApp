using LibraryApp.DAL.Context;
using LibraryApp.DAL.Entities.Base;
using LibraryApp.Interfaces.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {

        private readonly DataDB _db;
        protected DbSet<T> Set { get; }
        protected virtual IQueryable<T> Items => Set;

        public DbRepository(DataDB db)
        {
            _db = db;
            Set = _db.Set<T>();
        }

        public async Task<bool> Exist(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            return await Items.AnyAsync(i => i.Id == item.Id, Cancel).ConfigureAwait(false);
        }

        public async Task<bool> ExistId(int Id, CancellationToken Cancel = default)
        {
            return await Items.AnyAsync(item => item.Id == Id, Cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> Get(int Skip, int Count, CancellationToken Cancel = default)
        {
            if (Count <= 0)
                return Enumerable.Empty<T>();

            var query = Items;
            if (Skip > 0)
                query = query.Skip(Skip);
            return await query.Take(Count).ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default)
        {
            return await Items.ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<T> GetById(int id, CancellationToken Cancel = default)
        {
            switch (Items)
            {
                case DbSet<T> set:
                    return await set.FindAsync(new object[] { id }, Cancel).ConfigureAwait(false);
                case { } items:
                    return await items.FirstOrDefaultAsync(item => item.Id == id, Cancel).ConfigureAwait(false);
                default:
                    throw new InvalidOperationException("Ошибка в определении источника данных");
            }
        }

        public async Task<int> GetCount(CancellationToken Cancel = default)
        {
            return await Items.CountAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<IPage<T>> GetPage(int PageIndex, int PageSize, CancellationToken Cancel = default)
        {
            if (PageSize <= 0) return new Page(Enumerable.Empty<T>(), PageSize, PageIndex, PageSize);

            var query = Items;
            var total_count = await query.CountAsync(Cancel).ConfigureAwait(false);
            if (total_count == 0)
                return new Page(Enumerable.Empty<T>(), 0, PageIndex, PageSize);

            if (PageIndex > 0)
                query = query.Skip(PageIndex * PageSize);
            query = query.Take(PageSize);

            var items = await query.ToArrayAsync(Cancel).ConfigureAwait(false);

            return new Page(items, total_count, PageIndex, PageSize);
        }

        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>;

        public async Task<T> Add(T item, CancellationToken Cancel = default)
        {
            await _db.AddAsync(item, Cancel).ConfigureAwait(false);
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<T> Update(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Update(item);
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<T> Delete(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (!await ExistId(item.Id, Cancel)) return null;
            _db.Remove(item);
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<T> DeleteById(int id, CancellationToken Cancel = default)
        {
            var item = Set.Local.FirstOrDefault(i => i.Id == id);
            if (item is null)
            {
                item = await Set
                    .Select(i => new T { Id = i.Id })
                    .FirstOrDefaultAsync(i => i.Id == id, Cancel)
                    .ConfigureAwait(false);
            }

            if (item is null) return null;
            return await Delete(item, Cancel).ConfigureAwait(false);
        }
    }


}
