using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Business.Interfaces
{
   public interface IBaseBL<MISAEntity>
    {
        IEnumerable<MISAEntity> GetAll();
        IEnumerable<MISAEntity> GetPaging(int pageIndex, int pageSize);
        MISAEntity GetById(Guid id);
        int Insert(MISAEntity entity);
        int Update(MISAEntity entity, Guid id);
        int DeleteById(Guid id);
        MISAEntity GetBiggestCode();
        IEnumerable<MISAEntity> GetCode();
    }
}
