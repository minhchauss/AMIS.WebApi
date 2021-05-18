using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Data.Interfaces
{
    public interface IBaseDL
    {
        IEnumerable<MISAEntity> GetAll<MISAEntity>();
        IEnumerable<MISAEntity> GetPaging<MISAEntity>(int pageIndex, int pageSize);
        MISAEntity GetById<MISAEntity>(Guid id);
        int Insert<MISAEntity>(MISAEntity entity);
        int Update<MISAEntity>(MISAEntity entity, Guid id);
        int DeleteById<MISAEntity>(Guid id);
        MISAEntity GetBiggestCode<MISAEntity>();
        IEnumerable<MISAEntity> GetCode<MISAEntity>();
        int GetCountByTableName<MISAEntity>();
        IEnumerable<MISAEntity> GetPagingFilter<MISAEntity>(int paggIndex, int pageSize,string textFilter);
        int GetCountFilter<MISAEntity>(string textFilter);

    }
}
