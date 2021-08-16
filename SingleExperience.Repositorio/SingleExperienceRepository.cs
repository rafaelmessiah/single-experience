using SingleExperience.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SingleExperience.Repositorio
{
    public class SingleExperienceRepository : ISingleExperienceRepository
    {
        protected readonly SeContext _context;

        public SingleExperienceRepository(SeContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
