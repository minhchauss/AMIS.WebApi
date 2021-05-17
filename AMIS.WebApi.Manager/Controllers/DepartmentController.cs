using AMIS.Business.Interfaces;
using AMIS.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMIS.WebApi.Manager.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class DepartmentController : BaseAMISController<Department>
    {
        
        private readonly IDepartmentBL _departmentBL;
        public DepartmentController(IDepartmentBL departmentBL, ILogger<DepartmentController> logger) : base(departmentBL, logger)
        {
            _departmentBL = departmentBL;
        }

    }
}
