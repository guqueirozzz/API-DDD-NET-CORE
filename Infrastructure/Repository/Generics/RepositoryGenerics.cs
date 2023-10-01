using Domain.Interfaces.Generics;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Generics
{
    public class RepositoryGenerics<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        public RepositoryGenerics() 
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        } 
        public async Task Add(T Objeto)
        {
            using(var data = new ContextBase(_optionsBuilder))
            {
                await data.Set<T>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T Objeto)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityById(int Id)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                return await data.Set<T>().FindAsync(Id);
              
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                return await data.Set<T>().ToListAsync();

            }
        }

        public async Task Update(T Objeto)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                data.Set<T>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }

      
    
        // To detect redundant calls
        private bool _disposedValue;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }
    
    }
}
