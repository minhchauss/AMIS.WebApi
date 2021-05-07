using AMIS.Business.Exceptions;
using AMIS.Business.Interfaces;
using AMIS.Common.Attributes;
using AMIS.Common.Entities;
using AMIS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Business
{
   public class EmployeeBL:BaseBL<Employee>,IEmployeeBL
    {
        public EmployeeBL(IBaseDL baseDL):base(baseDL)
        {
           
        }

        protected override void Validate(Employee entity)
        {
            base.Validate(entity);
           
            //if (entity is Employee)
            //{

            //    //1. Kiểm tra đã nhập mã hay chưa
            //    if (string.IsNullOrEmpty(entity.EmployeeCode))
            //    {
            //        throw new GuardExceptions<Customer>("Mã khách hàng không để trống", entity);
            //    }
            //    //2. Kiểm tra đã nhập Email hay chưa
            //    if (string.IsNullOrEmpty(customer.Email))
            //    {
            //        throw new GuardExceptions<Customer>("Email không để trống", customer);
            //    }
            //    //3. Kiểm tra đã nhập Số điện thoại hay chưa
            //    if (string.IsNullOrEmpty(customer.PhoneNumber))
            //    {
            //        throw new GuardExceptions<Customer>("Số điện thoại không để trống", customer);
            //    }
            //    //4. Kiểm tra đã nhập Họ tên hay chưa
            //    if (string.IsNullOrEmpty(customer.FullName))
            //    {
            //        throw new GuardExceptions<Customer>("Tên khách hàng không để trống", customer);
            //    }
            //    //5. Kiểm tra định dạng EMail
            //    var regexEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            //    if (!Regex.IsMatch(customer.Email, regexEmail))
            //    {
            //        throw new GuardExceptions<Customer>("Email không đúng định dạng, kiểm tra lại", null);
            //    }
            //}

        }
    }
}
