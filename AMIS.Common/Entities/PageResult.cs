using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Common.Entities
{
    public class PageResult<MISAEntity>
    {
        public int CountList { set; get; }
        public IEnumerable<MISAEntity> Items { get; set; }
    }
}
