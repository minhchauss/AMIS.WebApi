using AMIS.Data.Interfaces;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Data
{
    public class BaseDL : IBaseDL
    {

        /// <summary>
        /// Chuỗi kết nối đến database
        /// Created by CMChau 6/5/2021
        /// </summary>
        protected readonly string _connectionString = "" +
       "Host=47.241.69.179;" +
       "Port=3306;" +
       "User Id=dev;" +
       "Password=12345678;" +
       "Database= 15B_MS1_61_CukCuk_CMChau";
        protected IDbConnection _dbConnection;

        //protected readonly string _connectionString;

        //public BaseDL(string connectionString)
        //{
        //    this._connectionString = connectionString;
        //}

        //public BaseDL()
        //{
        //}

        //public BaseDL(IDbConnection dbConnection)
        //{
        //    _dbConnection = dbConnection;
        //}



        /// <summary>
        /// Lấy danh sách từ database
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <returns>danh sách tất cả bản ghi</returns>
        public IEnumerable<MISAEntity> GetAll<MISAEntity>()
        {

            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy tên bảng
                var tableName = typeof(MISAEntity).Name;
                // Gọi procedure
                var sqlCommand = $"Proc_Get{tableName}s";
                // Tiến hành lấy danh sách
                var entities = _dbConnection.Query<MISAEntity>(sqlCommand, commandType: CommandType.StoredProcedure);
                return entities;
            }
        }
        /// <summary>
        /// Lấy danh sách theo phân trang
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="pageIndex">số trang</param>
        /// <param name="pageSize">số bản ghi/trang</param>
        /// <returns></returns>
        public IEnumerable<MISAEntity> GetPaging<MISAEntity>(int pageIndex, int pageSize)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy tên bảng
                var tableName = typeof(MISAEntity).Name;
                // Gọi procedure
                var sqlCommand = $"Proc_{tableName}Paging";
                // Thêm param 
                var parameters = new DynamicParameters();
                parameters.Add($"m_PageIndex", pageIndex);
                parameters.Add($"m_PageSize", pageSize);
                // Lấy ra danh sách 
                var Entities = _dbConnection.Query<MISAEntity>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure); ;
                return Entities;
            }
        }
        /// <summary>
        /// Lấy thông tin của 1 bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="id">id của nhân viên</param>
        /// <returns>Thông tin của 1 bản ghi</returns>
        public MISAEntity GetById<MISAEntity>(Guid id)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy tên bảng
                var tableName = typeof(MISAEntity).Name;
                //Gọi procedure
                var sqlCommand = $"Proc_Get{tableName}ById";
                // Thêm param
                var parameters = new DynamicParameters();
                parameters.Add($"m_{tableName}Id", id);
                // Trả về thông tin của bản ghi
                var entity = _dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);
                return entity;
            }
        }
        /// <summary>
        /// Thêm mới 1 bản ghi
        /// Created by CMChau 6/5/2021
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số dòng đã thêm được</returns>
        public int Insert<MISAEntity>(MISAEntity entity)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy ra tên bảng
                var tableName = typeof(MISAEntity).Name;
                // Gọi procedure
                var sqlCommnad = $"Proc_Insert{tableName}";
                // Tiến hành thêm mới 
                var rowEffect = _dbConnection.Execute(sqlCommnad, param: entity, commandType: CommandType.StoredProcedure);
                return rowEffect;
            }
        }
        /// <summary>
        /// Update 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns>Số dòng đã update được</returns>
        public int Update<MISAEntity>(MISAEntity entity, Guid id)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy ra tên bảng
                var tableName = typeof(MISAEntity).Name;
                // Gọi procedure
                var sqlCommnad = $"Proc_Update{tableName}";
                // Kiểm tra id của bản ghi có tồn tại không
                var entityPropertyName = $"{tableName}Id";
                var entityPropertyId = typeof(MISAEntity).GetProperty(entityPropertyName);
                if (entityPropertyId != null)
                    entityPropertyId.SetValue(entity, id);
                // Tiến hành update thông tin
                var rowAffect = _dbConnection.Execute(sqlCommnad, param: entity, commandType: CommandType.StoredProcedure);
                return rowAffect;
            }
        }
        /// <summary>
        /// Xóa 1 bản ghi trong database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Số dòng đã xóa được</returns>
        public int DeleteById<MISAEntity>(Guid id)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy ra tên bảng
                var tableName = typeof(MISAEntity).Name;
                // Gọi procedure
                var sqlCommnad = $"Proc_Delete{tableName}";
                // Thêm param
                var parameters = new DynamicParameters();
                parameters.Add($"p_{tableName}Id", id);
                // Tiến hành xóa
                var rowEffect = _dbConnection.Execute(sqlCommnad, param: parameters, commandType: CommandType.StoredProcedure);
                return rowEffect;
            }
        }
        /// <summary>
        /// Lấy ra danh sách mã
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <returns>Danh sách mã</returns>
        public IEnumerable<MISAEntity> GetCode<MISAEntity>()
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy ra tên bảng
                var tableName = typeof(MISAEntity).Name;
                // Gọi procedure
                var sqlCommnad = $"Proc_Get{tableName}sCode";
                // Tiến hành xóa
                var entities = _dbConnection.Query<MISAEntity>(sqlCommnad, commandType: CommandType.StoredProcedure);
                return entities;
            }
        }
        /// <summary>
        /// Lấy ra mã Code lớn nhất
        /// CreatedBy CMChau 9/5/2021
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <returns></returns>
        public MISAEntity GetBiggestCode<MISAEntity>()
        {

            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy ra tên bảng
                var tableName = typeof(MISAEntity).Name;
                // Gọi procedure
                var sqlCommnad = $"Proc_GetBiggest{tableName}Code";
                // 
                var entities = _dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommnad, commandType: CommandType.StoredProcedure);
                return entities;
            }
        }
        /// <summary>
        /// Lấy ra tổng số bản ghi
        /// Created By CMChau 10/5/2021
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <returns></returns>
        public int GetCountByTableName<MISAEntity>()
        {

            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy ra tên bảng
                var tableName = typeof(MISAEntity).Name;
                // Gọi procedure
                var sqlCommnad = $"Proc_GetCount{tableName}s";
                // 
                var countNumber = _dbConnection.QueryFirstOrDefault<int>(sqlCommnad, commandType: CommandType.StoredProcedure);
                return countNumber;
            }
        }
        /// <summary>
        /// Lấy danh sách theo phân trang sau khi lọc
        /// Created By CMChau 13/5/2021
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trong 1 trang</param>
        /// <param name="textFilter">Chuỗi cần lọc</param>
        /// <returns>Danh sách đã lọc theo phân trang</returns>
        public IEnumerable<MISAEntity> GetPagingFilter<MISAEntity>(int pageIndex, int pageSize, string textFilter)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                if (textFilter.Length == 0)
                    return GetPaging<MISAEntity>(pageIndex, pageSize);
                else
                {
                    // Lấy ra tên bảng
                    var tableName = typeof(MISAEntity).Name;
                    // Gọi procedure
                    var sqlCommnad = $"Proc_Get{tableName}PagingFilter";
                    var parameters = new DynamicParameters();
                    parameters.Add($"m_PageIndex", pageIndex);
                    parameters.Add($"m_PageSize", pageSize);
                    parameters.Add($"fullName", textFilter);
                    parameters.Add($"{tableName}Code", textFilter);
                    parameters.Add($"phoneNumber", textFilter);
                    var entities = _dbConnection.Query<MISAEntity>(sqlCommnad,param:parameters, commandType: CommandType.StoredProcedure);
                    return entities;
                }
            }
        }

        public int GetCountFilter<MISAEntity>(string textFilter)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy ra tên bảng
                var tableName = typeof(MISAEntity).Name;
                // Gọi procedure
                var sqlCommnad = $"Proc_GetCount{tableName}sFilter";
                //Truyền param
                var parameters = new DynamicParameters();
                parameters.Add($"fullName", textFilter);
                parameters.Add($"phoneNumber", textFilter);
                parameters.Add($"{tableName}Code", textFilter);
                var countNumber = _dbConnection.QueryFirstOrDefault<int>(sqlCommnad,param:parameters, commandType: CommandType.StoredProcedure);
                return countNumber;
            }
        }


    }
}
