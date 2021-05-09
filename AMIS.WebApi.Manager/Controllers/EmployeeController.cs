using AMIS.Business.Interfaces;
using AMIS.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        //protected readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeBL baseBL, ILogger<EmployeeController> logger) :base(baseBL, logger)
        {

        }


        /// <summary>
        /// Lấy ra danh sách theo phân trang
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi/Trang</param>
        /// <returns></returns>
        [HttpGet("paging")]
        public IActionResult GetPaging(int pageIndex, int pageSize)
        {
            try
            {
                // Lấy ra danh sách theo phân trang
                var entity = _baseBL.GetPaging(pageIndex, pageSize);
                if (entity != null)
                    return StatusCode(200, entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("exception", pageIndex, pageSize);
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(500, msg);
            }
        }
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var entity = _baseBL.GetBiggestCode();
                if(entity!=null)
                {
                    var item = entity.EmployeeCode.ToString();
                    var a = item.Split('N','V','-',' ');
                    string res = string.Join("", a.Skip(1));
                    int b = Int32.Parse(res);
                    b = b + 1;
                    item = ("NV-" + b).ToString();
                    return StatusCode(200, item);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("exception");
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(500, msg);
            }
        }
    }
}
