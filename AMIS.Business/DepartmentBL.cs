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
        public DepartmentBL(IBaseDL baseDL) : base(baseDL)
        {

        }
        public override Department GetById(Guid id)
        {
            ValidateId(id);
            return base.GetById(id);
        }
        protected override void Validate(Department entity)
        {
            base.Validate(entity);
            if (entity.DepartmentName == "ndviet")
            {
                throw new GuardException<Department>("Khong duoc dat ten nguoi thuong cho Deparment", entity);
            }

        }
        protected void ValidateId(Guid id)
        {
            if(id.ToString()!=null)
            {
                //throw new GuardException<Department>("ok", );
            }    
        }
    }
}
