using AMIS.Business.Exceptions;
using AMIS.Business.Interfaces;
using AMIS.Common.Entities;
using Microsoft.AspNetCore.Http;
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
        private readonly IEmployeeBL _employeeBL;
        //protected readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeBL employeeBL, ILogger<EmployeeController> logger) :base(employeeBL, logger)
        {
            _employeeBL = employeeBL;
        }


        /// <summary>
        /// Lấy ra danh sách theo phân trang
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi/Trang</param>
        /// <returns>danh sách lấy theo phân trang</returns>
        [HttpGet("paging")]
        public IActionResult GetPaging(int pageIndex, int pageSize)
        {
            try
            {
                if (pageIndex <= 0)
                    throw new GuardException<int>("pageIndex can lon hon 0", pageIndex);

                if (pageSize < 0)
                    throw new GuardException<int>("pageSize can lon hon 0", pageSize);

                pageSize = pageSize > 100 ? 100 : pageSize;

                // Lấy ra danh sách theo phân trang
                var entity = _employeeBL.GetPaging(pageIndex, pageSize);
                if (entity != null)
                    return StatusCode(200, entity);
                return NoContent();
            }
            catch (GuardException<int> ex)
            {
                _logger.LogError(ex, "exception");
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = "Dữ liệu không hợp lệ",
                    field = "PageInfo",
                    data = ex.Data
                };
                return StatusCode(StatusCodes.Status400BadRequest, mes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"exception",ex.Data);

                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }
        /// <summary>
        /// Lấy ra mã mới nhất
        /// </summary>
        /// <returns>Mã mới nhất tự động tăng</returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var entity = _employeeBL.GetBiggestCode();
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
                _logger.LogError("exception", ex);
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }
        /// <summary>
        /// Lấy ra danh sách nhân viên và số lượng theo phân trang
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="textFilter">Từ khóa lọc</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/v2/[controller]s/paging/filter")]
        public override IActionResult GetResultPaging(int pageIndex, int pageSize, string textFilter)
        {
            return base.GetResultPaging(pageIndex, pageSize, textFilter);
        }
    }
}
