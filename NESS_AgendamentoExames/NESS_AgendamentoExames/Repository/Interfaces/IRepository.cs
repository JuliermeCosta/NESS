using System.Collections.Generic;

namespace NESS_AgendamentoExames.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();

        T GetById(int id);

        void Insert(T entity);

        void Delete(int id);

        void Update(T entity);
    }
}
