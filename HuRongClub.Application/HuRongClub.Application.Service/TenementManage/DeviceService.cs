using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using System.Data.SqlClient;
using System.Data;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:47
    /// 描 述：Device
    /// </summary>
    public class DeviceService : RepositoryFactory<DeviceEntity>, DeviceIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DeviceModel> GetPageList(Pagination pagination, string queryJson)
        {

            RepositoryFactory<DeviceModel> repository = new RepositoryFactory<DeviceModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" 
select device.*,devicetype.typename,wy.property_name propertyname from wy_device device inner join wy_device_type devicetype
on device.d_typecode=devicetype.typecode
inner join wy_property wy
on wy.property_id=device.propertyid
where device.propertyid=@propertyid ");
            var parameter = new List<DbParameter>();

            string propertyid = Utils.GetCookie("property_id");
            if (!string.IsNullOrEmpty(propertyid))
            {
                parameter.Add(DbParameters.CreateDbParameter("@propertyid", propertyid));
            }
            var queryParam = queryJson.ToJObject();


            if (!queryParam["d_typecode"].IsEmpty())
            {
                string d_typecode = queryParam["d_typecode"].ToString();
                strSql.Append("  and d_typecode=@d_typecode");
                parameter.Add(DbParameters.CreateDbParameter("@d_typecode", d_typecode));
            }
            if (!queryParam["txt_Keyword"].IsEmpty())
            {
                string txt_Keyword = queryParam["txt_Keyword"].ToString();
                strSql.Append("  and (d_number like @keyword or d_name like @keyword or d_standard like @keyword or d_place like @keyword)");
                parameter.Add(DbParameters.CreateDbParameter("@keyword", "%" + txt_Keyword + "%"));
            }
            if (!queryParam["begindate"].IsEmpty())
            {
                string begindate = queryParam["begindate"].ToString();
                strSql.Append("  and d_usedate  >=@begindate ");
                parameter.Add(DbParameters.CreateDbParameter("@begindate", begindate));
            }
            if (!queryParam["enddate"].IsEmpty())
            {
                string enddate = queryParam["enddate"].ToString();
                strSql.Append("  and d_usedate  <=@enddate ");
                parameter.Add(DbParameters.CreateDbParameter("@enddate", enddate));
            }

            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DeviceModel> GetList(string queryJson)
        {
            RepositoryFactory<DeviceModel> repository = new RepositoryFactory<DeviceModel>();
            var strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();

            strSql.Append(@" 
select *,d_number +'|' + d_name + '|'+ d_place as mechinename from wy_device 
where propertyid=@propertyid ");
            var parameter = new List<DbParameter>();

            string propertyid = Utils.GetCookie("property_id");
            if (!string.IsNullOrEmpty(propertyid))
            {
                parameter.Add(DbParameters.CreateDbParameter("@propertyid", propertyid));
            }
            if (!queryParam["d_typecode"].IsEmpty())
            {
                string d_typecode = queryParam["d_typecode"].ToString();
                strSql.Append("  and d_typecode=@d_typecode");
                parameter.Add(DbParameters.CreateDbParameter("@d_typecode", d_typecode));
            }

            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DeviceModel GetEntity(string keyValue)
        {
            RepositoryFactory<DeviceModel> rep = new RepositoryFactory<DeviceModel>();
            var strSql = @"select device.*,wy.property_name propertyname,devicetype.typename from wy_device device 
inner join wy_property wy
on wy.property_id=device.propertyid
inner join wy_device_type devicetype
on device.d_typecode=devicetype.typecode
where device.d_number=@d_number ";
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@d_number", keyValue));
            return rep.BaseRepository().FindList(strSql,parameter.ToArray()).FirstOrDefault();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DeviceEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                string propertyid = Utils.GetCookie("property_id");
                entity.d_id = propertyid+GetMaxID_String("right(d_id,8)", "wy_device", 8);
                
                this.BaseRepository().Insert(entity);
               
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static string GetMaxID_String(string FieldName, string TableName, int pos)
        {
            RepositoryFactory<DeviceModel> rep = new RepositoryFactory<DeviceModel>();
            int num;
            object single = rep.BaseRepository().FindObject("select max(" + FieldName + ")+1 from " + TableName);
            if (single == null)
            {
                num = 1;
            }
            else
            {
                num = int.Parse(single.ToString());
            }
            string str2 = num.ToString();
            for (int i = 1; i <= pos; i++)
            {
                str2 = "0" + str2;
            }
            return str2.Substring(str2.Length - pos);
        }

        #endregion
    }
}
