using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 14:30
    /// 描 述：保养单管理
    /// </summary>
    public class DeviceRecordService : RepositoryFactory<DeviceRecordEntity>, DeviceRecordIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DeviceRecordEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DeviceRecordModel> GetList(Pagination pagination, string queryJson)
        {
            var sql = new StringBuilder("select a.fnumber,a.fman,a.fdate,fyearmonth=ltrim(str(a.fplanyear))+'年'+ltrim(str(a.fplanmonth))+'月',a.fpartnumber,ostatus=case a.fstatusid when 1 then '完成' else '未输入' end,p.p_name,p.p_place,p.p_standard from wy_device_record a left join wy_device_part p on p.p_number=a.fpartnumber where a.propertyid=@propertyid");
            var parameter = new List<DbParameter>();
            string propertyid = Utils.GetCookie("property_id");
            parameter.Add(DbParameters.CreateDbParameter("@propertyid", propertyid));

            var queryParam = queryJson.ToJObject();
            string fplanyear = "";
            string fplanmonth = "";
            string fnumber = "";
            string fpartnumber = "";
            int fstatusid = 0;
            int fgroupid = 0;
            if (!queryParam["fplanyear"].IsEmpty())
            {
                sql.Append(" and a.fplanyear=@fplanyear");

                fplanyear = queryParam["fplanyear"].ToString();
                parameter.Add(DbParameters.CreateDbParameter("@fplanyear", fplanyear));
            }
            if (!queryParam["fplanmonth"].IsEmpty())
            {
                sql.Append(" and a.fplanmonth=@fplanmonth");

                fplanmonth = queryParam["fplanmonth"].ToString();
                parameter.Add(DbParameters.CreateDbParameter("@fplanmonth", fplanmonth));
            }
            if (!queryParam["fnumber"].IsEmpty())
            {
                sql.Append(" and a.fnumber=@fnumber");

                fnumber = queryParam["fnumber"].ToString();
                parameter.Add(DbParameters.CreateDbParameter("@fnumber", fnumber));
            }
            if (!queryParam["fpartnumber"].IsEmpty())
            {
                sql.Append(" and a.fpartnumber=@fpartnumber");

                fpartnumber = queryParam["fpartnumber"].ToString();
                parameter.Add(DbParameters.CreateDbParameter("@fpartnumber", fpartnumber));
            }
            if (!queryParam["fstatusid"].IsEmpty())
            {
                sql.Append(" and a.fstatusid=@fstatusid");

                fstatusid = Convert.ToInt32(queryParam["fstatusid"]);
                parameter.Add(DbParameters.CreateDbParameter("@fstatusid", fstatusid));
            }
            if (!queryParam["fgroupid"].IsEmpty())
            {
                sql.Append(" and a.fgroupid=@fgroupid");

                fgroupid = Convert.ToInt32(queryParam["fgroupid"]);
                parameter.Add(DbParameters.CreateDbParameter("@fgroupid", fgroupid));
            }
            if (string.IsNullOrEmpty(fplanyear) && string.IsNullOrEmpty(fplanmonth) && string.IsNullOrEmpty(fnumber) && string.IsNullOrEmpty(fpartnumber) && fstatusid == 0 && fgroupid == 0)
            {
                return null;
            }
            RepositoryFactory<DeviceRecordModel> repository = new RepositoryFactory<DeviceRecordModel>();
            if (string.IsNullOrEmpty(pagination.sidx))
            {
                pagination.sidx = "fdate";
            }
            if (string.IsNullOrEmpty(pagination.sord))
            {
                pagination.sord = "desc";
            }

            return repository.BaseRepository().FindList(sql.ToString(), parameter.ToArray(), pagination);
        }

        /// <summary>
        /// 获取打印的保养单列表
        /// </summary>
        /// <param name="fnumbers"></param>
        /// <returns></returns>
        public IEnumerable<DeviceRecordModel> GetPrintList(string fnumbers)
        {
            string sql = @"SELECT  deviceRecord.fnumber ,
                                    deviceRecord.fplanyear ,
                                    deviceRecord.fplanmonth ,
                                    device.d_number ,
                                    device.d_name ,
                                    device.d_standard ,
                                    device.d_place ,
                                    devicepart.p_number ,
                                    devicepart.p_name ,
                                    devicepart.p_standard ,
                                    devicepart.p_place ,
                                    lastrecord.fdate lastdate ,
                                    lastrecord.fman lastman ,
                                    lastrecord.finfo lastinfo ,
                                    maintence.maintencename ,
                                    maintence.maintence ,
                                    property.property_name ,
                                    item.itemname
                            FROM    wy_device_record deviceRecord
                                    LEFT JOIN wy_device_part devicepart ON devicepart.p_number = deviceRecord.fpartnumber
                                    LEFT JOIN dbo.wy_device device ON device.d_number = devicepart.d_devicenumber
                                    LEFT JOIN wy_device_plan deviceplan ON deviceplan.planid = deviceRecord.fplanid
                                    LEFT JOIN wy_Device_maintence maintence ON maintence.classid = deviceplan.classid
                                    LEFT JOIN ( SELECT  MAX(frecordid) frecordid ,
                                                        fpartnumber
                                                FROM    wy_device_record
                                                WHERE   fstatusid = 1
                                                GROUP BY fpartnumber
                                              ) temp_lastRecordID ON temp_lastRecordID.fpartnumber = deviceRecord.fpartnumber
                                    LEFT JOIN wy_device_record lastrecord ON lastrecord.frecordid = temp_lastRecordID.frecordid
                                    LEFT JOIN dbo.wy_property property ON property.property_id = deviceRecord.propertyid
                                    LEFT JOIN sys_dictitem item ON item.itemid = deviceRecord.fgroupid
                            WHERE   deviceRecord.propertyid = @propertyid and deviceRecord.fnumber in(" + fnumbers + @")
                            ORDER BY deviceRecord.fnumber ASC";

            var parameter = new List<DbParameter>();
            string propertyid = Utils.GetCookie("property_id");
            parameter.Add(DbParameters.CreateDbParameter("@propertyid", propertyid));

            RepositoryFactory<DeviceRecordModel> repository = new RepositoryFactory<DeviceRecordModel>();

            return repository.BaseRepository().FindList(sql.ToString(), parameter.ToArray());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<DeviceRecordModel> GetRecordCombobox(string queryJson)
        {
            var sql = new StringBuilder("select a.fnumber,fyearmonth=ltrim(rtrim(str(a.fplanyear)))+ltrim(rtrim(str(a.fplanmonth))),a.frecordid,a.fplanid,a.fdate,a.fman,a.finfo,b.* from wy_device_record a left join wy_device_part b on b.p_number=a.fpartnumber where 1=1 and a.propertyid=@propertyid");
            var parameter = new List<DbParameter>();
            string propertyid = Utils.GetCookie("property_id");
            parameter.Add(DbParameters.CreateDbParameter("@propertyid", propertyid));
            var queryParam = queryJson.ToJObject();
            string fnumber = "";
            RepositoryFactory<DeviceRecordModel> repository = new RepositoryFactory<DeviceRecordModel>();
            if (!queryParam["fnumber"].IsEmpty())
            {
                sql.Append(" and a.fnumber=@fnumber");

                fnumber = queryParam["fnumber"].ToString();
                parameter.Add(DbParameters.CreateDbParameter("@fnumber", fnumber));
            }
            if (!queryParam["fstatusid"].IsEmpty())
            {
                sql.Append(" and a.fstatusid=@fstatusid");

                int fstatusid = Convert.ToInt32(queryParam["fstatusid"]);
                parameter.Add(DbParameters.CreateDbParameter("@fstatusid", fstatusid));
            }

            sql.Append(" ORDER BY fnumber");
            return repository.BaseRepository().FindList(sql.ToString(), parameter.ToArray());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DeviceRecordEntity GetEntity(string keyValue)
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
        /// 更新保养单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int modifyDeviceRecord(DeviceRecordEntity entity)
        {
            string sql = @"update wy_device_record set fstatusid=1,fdate=@fdate,fman=@fman,finfo=@finfo where propertyid=@propertyid and fnumber=@fnumber";
            var parameter = new List<DbParameter>();
            string propertyid = Utils.GetCookie("property_id");
            parameter.Add(DbParameters.CreateDbParameter("@propertyid", propertyid));
            parameter.Add(DbParameters.CreateDbParameter("@fdate", entity.fdate));
            parameter.Add(DbParameters.CreateDbParameter("@fman", entity.fman));
            parameter.Add(DbParameters.CreateDbParameter("@finfo", entity.finfo));
            parameter.Add(DbParameters.CreateDbParameter("@fnumber", entity.fnumber));
            return this.BaseRepository().ExecuteBySql(sql, parameter.ToArray());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DeviceRecordEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                modifyDeviceRecord(entity);
            }
            else
            {
                Create(Convert.ToInt32(entity.fplanyear), Convert.ToInt32(entity.fplanmonth));
            }
        }

        /// <summary>
        /// 创建保养单
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void Create(int year, int month)
        {
            string propertyid = Utils.GetCookie("property_id");
            RepositoryFactory<wy_device_planEntity> repository = new RepositoryFactory<wy_device_planEntity>();
            var strsql = "select * from wy_device_plan where fyear=@year and fmonth=@month and p_number in(select p_number from wy_device_part where d_devicenumber in(select d_number from wy_device where propertyid=@propertyid)) order by fgroupid";
            var parameter = new List<DbParameter>();

            parameter.Add(DbParameters.CreateDbParameter("@year", year));
            parameter.Add(DbParameters.CreateDbParameter("@month", month));
            parameter.Add(DbParameters.CreateDbParameter("@propertyid", propertyid));
            var list = repository.BaseRepository().FindList(strsql.ToString(), parameter.ToArray());
            if (list == null)
            {
                return;
            }


            foreach (wy_device_planEntity item in list)
            {
                strsql = "select frecordid from wy_device_record where fplanyear=@year and fplanmonth=@month and fpartnumber=@number";
                parameter = new List<DbParameter>();

                parameter.Add(DbParameters.CreateDbParameter("@year", item.fyear));
                parameter.Add(DbParameters.CreateDbParameter("@month", item.fmonth));
                parameter.Add(DbParameters.CreateDbParameter("@number", item.p_number));
                RepositoryFactory<DeviceRecordEntity> repositoryRecord = new RepositoryFactory<DeviceRecordEntity>();
                var recordModel = repositoryRecord.BaseRepository().FindList(strsql.ToString(), parameter.ToArray()).FirstOrDefault();
                if (recordModel != null)
                {
                    continue;
                }

                DeviceRecordEntity entity = new DeviceRecordEntity();
                entity.fpartnumber = item.p_number;
                entity.frecordid = DeviceService.GetMaxID_String("right(planid,8)", "wy_device_plan", 8);
                entity.fplanyear = year;
                entity.fplanmonth = Convert.ToInt16(month);
                entity.fstatusid = 0;
                entity.propertyid = propertyid;
                entity.fnumber = GetMaxID_Where("fnumber", "wy_device_record", "propertyid='" + propertyid + "'");
                entity.fplanid = item.planid;
                entity.fgroupid = item.fgroupid;
                entity.Create();

                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <param name="sqlwhere"></param>
        /// <returns></returns>
        public static int GetMaxID_Where(string FieldName, string TableName, string sqlwhere)
        {
            RepositoryFactory<DeviceModel> rep = new RepositoryFactory<DeviceModel>();

            string sqlStr = "select ISNULL(max(" + FieldName + "),0)+1 from " + TableName;
            if (sqlwhere != string.Empty)
            {
                sqlStr = sqlStr + " where " + sqlwhere;
            }
            object single = rep.BaseRepository().FindObject(sqlStr);
            if (single == null)
            {
                return 1;
            }
            return int.Parse(single.ToString());
        }

        #endregion
    }
}