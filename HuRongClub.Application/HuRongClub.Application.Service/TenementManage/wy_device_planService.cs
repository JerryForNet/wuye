using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data.Repository;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using System.Text;
using HuRongClub.Data;
using System.Data.Common;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-05-02 13:04
    /// 描 述：wy_device_plan
    /// </summary>
    public class wy_device_planService : RepositoryFactory<wy_device_planEntity>, wy_device_planIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DevicePlanModel> GetList(string queryJson)
        {
            RepositoryFactory<DevicePlanModel> repository = new RepositoryFactory<DevicePlanModel>();
            var queryParam = queryJson.ToJObject();
            var strSql = new StringBuilder();
            var parameter = new List<DbParameter>();

            strSql.Append("select * from wy_device_plan deviceplan inner join wy_Device_maintence maintence on maintence.classid=deviceplan.classid");

            if (!queryParam["p_number"].IsEmpty())
            {
                string p_number = queryParam["p_number"].ToString();
                strSql.Append("  and deviceplan.p_number=@p_number");
                parameter.Add(DbParameters.CreateDbParameter("@p_number", p_number));
            }
            if (!queryParam["planid"].IsEmpty())
            {
                string planid = queryParam["planid"].ToString();
                strSql.Append("  and deviceplan.planid=@planid");
                parameter.Add(DbParameters.CreateDbParameter("@planid", planid));
            }
            if (!queryParam["fmonth"].IsEmpty())
            {
                string fmonth = queryParam["fmonth"].ToString();
                strSql.Append("  and deviceplan.fmonth=@fmonth");
                parameter.Add(DbParameters.CreateDbParameter("@fmonth", fmonth));
            }
            if (!queryParam["fyear"].IsEmpty())
            {
                string fyear = queryParam["fyear"].ToString();
                strSql.Append("  and deviceplan.fyear=@fyear");
                parameter.Add(DbParameters.CreateDbParameter("@fyear", fyear));
            }

            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());

        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public wy_device_planEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, wy_device_planEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                AddDevicePlan(entity);
            }
        }


        private void AddDevicePlan(wy_device_planEntity entity)
        {
            wy_device_planEntity nentity = null;
            try
            {                
                if (!string.IsNullOrEmpty(entity.classid))
                {
                    string[] classids = entity.classid.Split(',');
                    for (int i = 0; i < classids.Length; i++)
                    {
                        var sql = @"select * from wy_device_plan where p_number='" + entity.p_number + "' and fyear=" + entity.fyear + " and fmonth=" + (i + 1);
                        var model = this.BaseRepository().FindList(sql).FirstOrDefault();
                        
                        if (model == null)
                        {
                            if (!string.IsNullOrEmpty(classids[i]))
                            {
                                nentity = new wy_device_planEntity();
                                nentity.fyear = entity.fyear;
                                nentity.fmonth = (i + 1);
                                nentity.p_number = entity.p_number;
                                nentity.classid = classids[i];
                                nentity.fyear = entity.fyear;
                                nentity.fgroupid = entity.fgroupid;
                                nentity.fstatusid = 0;
                                nentity.planid = DeviceService.GetMaxID_String("right(planid,8)", "wy_device_plan", 8);
                                this.BaseRepository().Insert(nentity);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(classids[i]))
                            {
                                var sqldele = @"delete from wy_device_plan where p_number='" + entity.p_number + "' and fyear=" + entity.fyear + " and fmonth=" + (i + 1);

                                this.BaseRepository().ExecuteBySql(sqldele);
                            }
                            else
                            {
                                StringBuilder builder = new StringBuilder();
                                builder.Append("update wy_device_plan set ");
                                builder.Append("classid=@classid,");
                                builder.Append("finfo='',");
                                builder.Append("fstatusid=@fstatusid,");
                                builder.Append("fgroupid=@fgroupid");
                                builder.Append(" where p_number=@p_number and fyear=@fyear and fmonth=@fmonth");

                                DbParameter[] parameter ={
                                    DbParameters.CreateDbParameter("@classid",classids[i]),
                                    DbParameters.CreateDbParameter("@fstatusid",0),
                                    DbParameters.CreateDbParameter("@fgroupid",entity.fgroupid),
                                    DbParameters.CreateDbParameter("@p_number",entity.p_number),
                                    DbParameters.CreateDbParameter("@fyear",entity.fyear),
                                    DbParameters.CreateDbParameter("@fmonth",(i + 1))
                                };

                                this.BaseRepository().ExecuteBySql(builder.ToString(), parameter);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {                
                throw;
            }
        }
        #endregion
    }
}
