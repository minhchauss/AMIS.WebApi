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
        public DepartmentController(IDepartmentBL baseBL, ILogger<DepartmentController> logger) : base(baseBL, logger)
        {

        }
        //public DepartmentController(IDepartmentBL baseBL, ILogger<DepartmentController> logger) : base(baseBL, logger)
        //{

        //}
    }
}
