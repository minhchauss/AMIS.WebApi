using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Business.Exceptions
{
   public class GuardException<MISAEntity>:Exception
    {
        public GuardException(string msg, MISAEntity entity) : base(msg)
        {
            this.Data.Add(1, entity);
        }
    }
}
