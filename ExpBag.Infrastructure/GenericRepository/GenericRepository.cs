using ExpBag.Application.Interfaces;
using ExpBag.EFData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Infrastructure.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext context;
        private readonly DbSet<TEntity> set;

        public GenericRepository(DataContext _context)
        {
            context = _context;
            set = context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            set.Add(entity);
            Save();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await set.AddAsync(entity);
            await SaveAsync();
        }


        private void Save()
        {
            context.SaveChanges();
        }

        private async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            if (entity != null)
            {
                set.Remove(entity);
                Save();
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity != null)
            {
                set.Remove(entity);
                await SaveAsync();
            }
        }

        public TEntity Find(int id)
        {
            return set.Find(id);
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await set.FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return set.AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            if (entity != null && set.Find(entity) != null)
            {
                set.Update(entity);
                //context.Entry(entity).State = EntityState.Modified;
                Save();
                return;
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity != null && set.Find(entity) != null)
            {
                set.Update(entity);
                //context.Entry(entity).State = EntityState.Modified;
                await SaveAsync();
                return;
            }
        }
    }
}
