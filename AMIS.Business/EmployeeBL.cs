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
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        public EmployeeBL(IBaseDL baseDL) : base(baseDL)
        {

        }

        protected override void Validate(Employee entity)
        {
            base.Validate(entity);
           
        }
        /// <summary>
        /// Validate dữ liệu của employee
        /// Createdby CMChau 9/5/2021
        /// </summary>
        /// <param name="entity"></param>
        protected override void ValidateDuplicate(Employee entity)
        {
            base.ValidateDuplicate(entity);
            var properties = typeof(Employee).GetProperties();
            foreach (var property in properties)
            {
                // Lấy ra thuộc tính băt bắt buộc
                var attributeDuplicate = property.GetCustomAttributes(typeof(MISADuplicate), true);
                // Kiểm tra trùng mã
                if (attributeDuplicate.Length > 0)
                {
                    // Lấy ra giá trị của property
                    var propertyValue = property.GetValue(entity);
                    // Lấy ra kiểu của property
                    var propertyType = property.PropertyType;
                    // Kiểm tra giá trị đầu vào
                    var CodeList = GetCode();
                    foreach (var item in CodeList)
                    {
                        if (entity.EmployeeCode.ToString() == item.EmployeeCode.ToString())
                        {
                            var msgError = (attributeDuplicate[0] as MISADuplicate).MsgError;
                            throw new GuardException<Employee>(msgError, entity);
                        }
                    }
                }
            }
        }
    }
}
