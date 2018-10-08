using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using HuRongClub.Util.Extension;
using System;
using System.Data.SqlClient;
using System.Data;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:58
    /// 描 述：DevicePart
    /// </summary>
    public class DevicePartService : RepositoryFactory<DevicePartEntity>, DevicePartIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DevicePartModel> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string propertyid = Utils.GetCookie("property_id");
            if (string.IsNullOrEmpty(propertyid))
            {
                return null;
            }
            if (queryParam["d_typecode"].IsEmpty())
            {
                return null;
            }
            if (queryParam["year"].IsEmpty())
            {
                return null;
            }
            RepositoryFactory<DevicePartModel> repository = new RepositoryFactory<DevicePartModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" 
select devicepart.*,device.*,item.itemname,

(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=1) maintencename1,

(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=2) maintencename2,

(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=3) maintencename3,

(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=4) maintencename4,
(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=5) maintencename5,
(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=6) maintencename6,
(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=7) maintencename7,
(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=8) maintencename8,
(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=9) maintencename9,
(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=10) maintencename10,
(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=11) maintencename11,
(select top 1 (select top 1 maintencename from  wy_Device_maintence where classid=wy_device_plan.classid) as names from wy_device_plan where p_number=devicepart.p_number and fyear=@year
and fmonth=12) maintencename12

  from wy_device_part devicepart
 left join wy_device device
  on  devicepart.d_devicenumber=device.d_number 
  left join sys_dictitem item
  on item.itemid=devicepart.p_groupid
  
  where device.propertyid=@propertyid  and device.d_typecode=@d_typecode
  and devicepart.p_number in(select p_number from wy_device_plan where fyear=@year
  ");
            var parameter = new List<DbParameter>();
           


          

            string d_typecode = queryParam["d_typecode"].ToString();

    
             string   year = queryParam["year"].ToString();
    
            if (!queryParam["groupid"].IsEmpty())
            {
                strSql.Append("and fgroupid=@groupid");
                int groupid = Convert.ToInt32(queryParam["groupid"]);
                parameter.Add(DbParameters.CreateDbParameter("@groupid", groupid));
            }
            strSql.Append(")");

            parameter.Add(DbParameters.CreateDbParameter("@year", year));
            parameter.Add(DbParameters.CreateDbParameter("@propertyid", propertyid));
            parameter.Add(DbParameters.CreateDbParameter("@d_typecode", d_typecode));




            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DevicePartModel> GetList(string queryJson)
        {
            var sql = "select (p_number+'|'+p_name) as p_name,* from dbo.wy_device_part where d_devicenumber=@d_devicenumber";
            var parameter = new List<DbParameter>();
            string d_devicenumber = "";
            var queryParam = queryJson.ToJObject();

            if (!queryParam["d_devicenumber"].IsEmpty())
            {
                d_devicenumber = queryParam["d_devicenumber"].ToString();
            }
            RepositoryFactory<DevicePartModel> repository = new RepositoryFactory<DevicePartModel>();

            parameter.Add(DbParameters.CreateDbParameter("@d_devicenumber", d_devicenumber));
            return repository.BaseRepository().FindList(sql.ToString(), parameter.ToArray());

        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DevicePartEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
        public void SaveForm(string keyValue, DevicePartEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                this.BaseRepository().Insert(entity);
            }
        }

      /// <summary>
        /// 保养计划复制
      /// </summary>
      /// <param name="fromyear"></param>
      /// <param name="toyear"></param>
        public void CopyDevicePlan(int fromyear,int toyear)
        {
            if (fromyear==toyear)
            {
                return;
            }
            string propertyid = Utils.GetCookie("property_id");

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Fromyear", SqlDbType.Int), new SqlParameter("@Toyear", SqlDbType.Int), new SqlParameter("@propid", SqlDbType.VarChar, 4) };
            parameters[0].Value = fromyear;
            parameters[1].Value = toyear;
            parameters[2].Value =propertyid;
            this.BaseRepository().ExecuteByProc("DevicePlan_copy", parameters);
        }
        #endregion
    }
}
