﻿using AMIS.Business.Exceptions;
using AMIS.Business.Interfaces;
using AMIS.Common.Attributes;
using AMIS.Data;
using AMIS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AMIS.Business
{
    public class BaseBL<MISAEntity> : IBaseBL<MISAEntity>
    {
        ///// <summary>
        ///// Khởi tạo interface
        ///// Created by CMChau 6/5/2021
        ///// </summary>
        IBaseDL _baseDL;
        public BaseBL(IBaseDL baseDL)
        {
            _baseDL = baseDL;
        }
        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MISAEntity> GetAll()
        {
            var entities = _baseDL.GetAll<MISAEntity>();
            return entities;
        }
        /// <summary>
        /// Lấy thông tin của 1 bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Thông tin 1 bản ghi</returns>
        public MISAEntity GetById(Guid id)
        {
            var entity = _baseDL.GetById<MISAEntity>(id);
            return entity;
        }
        /// <summary>
        /// Phân trang danh sách bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>Danh sách bản ghi</returns>
        public IEnumerable<MISAEntity> GetPaging(int pageIndex, int pageSize)
        {
            var entities = _baseDL.GetPaging<MISAEntity>(pageIndex, pageSize);
            return entities;
        }
        /// <summary>
        /// Thêm mới bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số dòng đã thêm được</returns>
        public int Insert(MISAEntity entity)
        {
            Validate(entity);
            var rowAffect = _baseDL.Insert<MISAEntity>(entity);
            return rowAffect;
        }
        /// <summary>
        /// Update thông tin của 1 bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns>Số dòng đã thêm được</returns>
        public int Update(MISAEntity entity, Guid id)
        {
            var rowAffect = _baseDL.Update<MISAEntity>(entity, id);
            return rowAffect;
        }
        /// <summary>
        /// Xóa 1 bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Số dòng đã xóa được</returns>
        public int DeleteById(Guid id)
        {
            var rowAffect = _baseDL.DeleteById<MISAEntity>(id);
            return rowAffect;
        }
        /// <summary>
        /// Validate dữ liệu
        /// Created by CMChau 7/5/2021
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void Validate(MISAEntity entity)
        {
            var properties = typeof(MISAEntity).GetProperties();
            foreach (var property in properties)
            {
                // Lấy ra thuộc tính băt bắt buộc
                var attributeRequired = property.GetCustomAttributes(typeof(MISARequired), true);
                var attributeRegexEmail = property.GetCustomAttributes(typeof(MISAValidateEmail), true);
                var attributeDuplicate = property.GetCustomAttributes(typeof(MISADuplicate), true);
                // Kiểm tra nhập dữ liệu hay không
                if (attributeRequired.Length > 0)
                {
                    // Lấy ra giá trị của property
                    var propertyValue = property.GetValue(entity);
                    // Lấy ra kiểu của property
                    var propertyType = property.PropertyType;
                    // Kiểm tra giá trị và kiểu property
                    if (propertyType == typeof(string) && string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        // Lấy ra câu thông báo lỗi
                        var msgError = (attributeRequired[0] as MISARequired).MsgError;
                        throw new GuardException<MISAEntity>(msgError, entity);
                    }

                }
                // Kiểm tra độ dài chuỗi
                if (attributeRegexEmail.Length > 0)
                {
                    // Lấy giá trị của property
                    var propertyValue = property.GetValue(entity);
                    // Lấy ra kiểu của property
                    var propertyType = property.PropertyType;
                    // Lấy ra regex Email
                    var regexEmail = (attributeRegexEmail[0] as MISAValidateEmail).RegexEmail;
                    // Kiểm tra giá trị
                    if (!Regex.IsMatch(propertyValue.ToString(), regexEmail))
                    {
                        // Lấy ra câu thông báo lỗi
                        var msgError = (attributeRegexEmail[0] as MISAValidateEmail).MsgError;
                        throw new GuardException<MISAEntity>(msgError, entity);
                    }
                }
                // Kiểm tra trùng mã
                if (attributeDuplicate.Length > 0)
                {
                    // Lấy giá trị của property
                    var propertyValue = property.GetValue(entity);
                    // Lấy ra kiểu của property
                    var propertyType = property.PropertyType;
                    // Kiểm tra giá trị
                    var entities = GetAll();
                    foreach (var item in entities)
                    {
                       
                    }
                }
            }
        }
    }
}
