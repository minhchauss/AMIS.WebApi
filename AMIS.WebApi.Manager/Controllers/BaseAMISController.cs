using AMIS.Business.Exceptions;
using AMIS.Business.Interfaces;
using AMIS.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AMIS.WebApi.Manager.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BaseAMISController<MISAEntity> : ControllerBase
    {

        protected readonly ILogger<BaseAMISController<MISAEntity>> _logger;

        /// <summary>
        /// Khởi tạo interface
        /// Created by CMChau 6/5/2021
        /// </summary>
        protected IBaseBL<MISAEntity> _baseBL;

        public BaseAMISController(IBaseBL<MISAEntity> baseBL, ILogger<BaseAMISController<MISAEntity>> logger)
        {
            _baseBL = baseBL;
            _logger = logger;
        }
        /// <summary>
        /// Lấy ra danh sách toàn bộ bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                // Lấy ra toàn bộ danh sách
                var entities = _baseBL.GetAll();
                if (entities.Count() > 0)
                    return Ok(entities);
                return NoContent();
            }
            catch (WebException ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"

                };

                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }
        /// <summary>
        /// Lấy ra thông tin của 1 bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Thông tin của 1 bản ghi</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                // Lấy ra thông tin 1 bản ghi
                var entity = _baseBL.GetById(id);
                if (entity != null)
                    return Ok(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Function GetById has exception!");
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"

                };
                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }
        /// <summary>
        /// Lấy tổng số bản ghi
        /// Created by CMChau 10/5/2021
        /// </summary>
        /// <returns>Tổng số bản ghi</returns>
        [HttpGet("total")]
        public IActionResult GetTableCountData()
        {
            try
            {
                var countNumber = _baseBL.GetCountByTableName();
                return Ok(countNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exception");
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }
        /// <summary>
        /// Thêm mới 1 bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số dòng thêm mới được</returns>
        [HttpPost]
        public IActionResult Insert([FromBody] MISAEntity entity)
        {
            try
            {
                // Lấy ra số dòng đã thêm được
                var rowAffect = _baseBL.Insert(entity);
                if (rowAffect > 0)
                    return Ok();
                return NoContent();
            }
            catch (GuardException<MISAEntity> ex)
            {
                _logger.LogError(ex, "exception");
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = "Dữ liệu không hợp lệ",
                    field = "CustomerCode",
                    data = ex.Data
                };
                return StatusCode(StatusCodes.Status400BadRequest, mes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exception");
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }
        /// <summary>
        /// Sửa 1 bản ghi theo Id
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="entity">Nội dung</param>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Số dòng đã sửa được</returns>
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] MISAEntity entity, Guid id)
        {
            try
            {
                // Lấy ra số dòng đã sửa
                var rowAffect = _baseBL.Update(entity, id);
                if (rowAffect > 0)
                    return Ok();
                return NoContent();
            }
            catch (GuardException<MISAEntity> ex)
            {
                _logger.LogError(ex, "exception");
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = "Dữ liệu không hợp lệ",
                    field = "CustomerCode",
                    data = ex.Data
                };
                return StatusCode(StatusCodes.Status400BadRequest, mes);
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }
        /// <summary>
        /// Xóa 1 bản ghi theo Id
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Số dòng đã xóa được</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            try
            {
                // Lấy ra số dòng đã xóa 
                var rowAffect = _baseBL.DeleteById(id);
                if (rowAffect > 0)
                    return Ok();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exception");
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }
        /// <summary>
        /// Lấy danh sách lọc theo phân trang
        /// Created by CMChau 13/5/2021
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trong 1 trang</param>
        /// <param name="textFilter">Chuỗi cần lọc</param>
        /// <returns>Danh sách lọc theo phân trang</returns>
        [HttpGet("paging/filter")]
        public IActionResult GetPagingFilter(int pageIndex, int pageSize, string textFilter)
        {
            try
            {
                var entities = _baseBL.GetPagingFilter(pageIndex, pageSize, textFilter);
                if (entities.Count() > 0)
                    return Ok(entities);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exception");
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }

        [HttpGet("totalFilter")]
        public IActionResult GetTotalFilter(string textFilter)
        {
            try
            {
                if (textFilter == null || textFilter == "")
                {
                    var countNumber = _baseBL.GetCountByTableName();
                    return Ok(countNumber);
                }
                else
                if (textFilter != null || textFilter != "")
                {
                    var totalFilter = _baseBL.GetCountFilter(textFilter);
                    return Ok(totalFilter);
                }
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exception");
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }
        /// <summary>
        /// Lấy danh sách đã lọc và tổng số bản ghi
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số phần tử/trang</param>
        /// <param name="textFilter">Từ khóa cần lọc</param>
        /// <returns>Tổng số bản ghi đã lọc và danh sách bản ghi</returns>
        [HttpGet("paging")]
        public virtual IActionResult GetResultPaging(int pageIndex, int pageSize, string textFilter)
        {
            try
            {
                var pageResult = new PageResult<MISAEntity>();
                if (textFilter == null || textFilter == "")
                {
                    pageResult.CountList = _baseBL.GetCountByTableName();
                    pageResult.Items = _baseBL.GetPaging(pageIndex, pageSize);
                    return Ok(pageResult);
                }
                else
                {
                    pageResult.CountList = _baseBL.GetCountFilter(textFilter);
                    pageResult.Items = _baseBL.GetPagingFilter(pageIndex, pageSize, textFilter);
                    return Ok(pageResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exception");
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, msg);
            }
        }
    }
}
