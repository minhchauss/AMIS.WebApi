using AMIS.Business.Interfaces;
using AMIS.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMIS.WebApi.Manager.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : BaseAMISController<Employee>
    {
        public EmployeeController(IEmployeeBL baseBL):base(baseBL)
        {

        }
        [HttpGet("Paging")]
        public IActionResult GetPaging(int pageIndex , int pageSize)
        {
            return Ok();
        }
    }
}
