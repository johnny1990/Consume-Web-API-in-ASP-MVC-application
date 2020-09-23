using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IDataAccessRepository<TEntity, in TPrimaryKey> where TEntity : class
    {
        IEnumerable<TEntity> GetEmp();
        TEntity GetEmp(TPrimaryKey id);
        void PostEmp(TEntity entity);
        void PutEmp(TPrimaryKey id, TEntity entity);
        void DeleteEmp(TPrimaryKey id);
    }
}
