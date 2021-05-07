using AMIS.Business.Interfaces;
using AMIS.Common.Entities;
using AMIS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Business
{
   public class DepartmentBL:BaseBL<Department>,IDepartmentBL
    {
        public DepartmentBL(IBaseDL baseDL) : base(baseDL)
        {

        }
        protected override void Validate(Department entity)
        {
            base.Validate(entity);
        }
    }
}
