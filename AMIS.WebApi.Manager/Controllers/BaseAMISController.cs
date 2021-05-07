using AMIS.Business.Exceptions;
using AMIS.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMIS.WebApi.Manager.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BaseAMISController<MISAEntity> : ControllerBase
    {

        protected readonly ILogger<BaseAMISController<MISAEntity>> _logger;

        public BaseAMISController(ILogger<BaseAMISController<MISAEntity>> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Khởi tạo interface
        /// Created by CMChau 6/5/2021
        /// </summary>
        protected IBaseBL<MISAEntity> _baseBL;
        public BaseAMISController(IBaseBL<MISAEntity> baseBL)
        {
            _baseBL = baseBL;
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
                    return StatusCode(200, entities);
                return NoContent();
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"

                };
                return StatusCode(500, msg);
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
                    return StatusCode(200, entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("exception", id);
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"

                };
                return StatusCode(500, msg);
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
                _logger.LogError("exception", entity);
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = "Dữ liệu không hợp lệ",
                    field = "CustomerCode",
                    data = ex.Data
                };
                return StatusCode(400, mes);
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(500, msg);
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
                _logger.LogError("exception", entity);
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = "Dữ liệu không hợp lệ",
                    field = "CustomerCode",
                    data = ex.Data
                };
                return StatusCode(400, mes);
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA để được trợ giúp"
                };
                return StatusCode(500, msg);
            }
        }
        /// <summary>
        /// Xóa 1 bản ghi theo Id
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
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
                _logger.LogError("exception", id);
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
