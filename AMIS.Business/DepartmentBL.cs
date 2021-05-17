using AMIS.Business.Exceptions;
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
        private readonly IDepartmentDL _departmentDL;
        public DepartmentBL(IDepartmentDL departmentDL) : base(departmentDL)
        {
            _departmentDL = departmentDL;
        }
        public override Department GetById(Guid id)
        {
            return base.GetById(id);
        }
        protected override void Validate(Department entity)
        {
            base.Validate(entity);
        }

    }
}
