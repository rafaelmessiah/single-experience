using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SingleExperience.Repositorio
{
    public interface ISingleExperienceRepository
    {

        public void Add<T>(T entity) where T : class;

        public void Update<T>(T entity) where T : class;

        public Task<bool> SaveChangesAsync();
    }
}
