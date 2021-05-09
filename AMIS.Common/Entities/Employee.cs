using AMIS.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Common.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        [MISARequired("Mã nhân viên không được phép để trống")]
        [MISADuplicate("Mã nhân viên đã tồn tại")]
        public string EmployeeCode { get; set; }
        [MISARequired("Tên nhân viên không được phép để trống")]
        public string FullName { get; set; }
        public string Address { get; set; }
        public int? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime IdentityDate { get; set; }
        public string IdentityPlace { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        [MISARequired("Email không được phép để trống")]
        [MISAValidateEmail(regexEmail: @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$","Email không hợp lệ")]
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Createdby { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string PositionName { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
