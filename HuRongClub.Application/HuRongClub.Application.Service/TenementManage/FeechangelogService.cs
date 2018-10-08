using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:19
    /// 描 述：费用调整日志
    /// </summary>
    public class FeechangelogService : RepositoryFactory<FeechangelogEntity>, FeechangelogIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeechangelogEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeechangelogEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeechangelogEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="pos">位数 不够前面补0</param>
        /// <returns></returns>
        public string GetMaxID(int pos)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(right(itemid,8))+1 from wy_feechangelog");
            string str = "1";
            object obj = this.BaseRepository().FindObject(strSql.ToString());
            if (obj != null)
            {
                str = obj.ToString();
            }
            if (str.Length < pos)
            {
                int leng = str.Length;
                for (int i = 0; i < (pos - leng); i++)
                {
                    str = "0" + str;
                }
            }
            return str;
        }

        /// <summary>
        /// 减免查询
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">参数</param>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        public IEnumerable<PaymentSerachEntity> GetList(Pagination pagination, string queryJson, string property_id)
        {
            var queryParam = queryJson.ToJObject();
            var strSql = new StringBuilder();
            var parameter = new List<DbParameter>();
            int type = 0;
            if (!queryParam["type"].IsEmpty())
            {
                type = queryParam["type"].ToInt();
            }

            strSql.Append("select itemid,contract_id,");
            if (type == 2)
            {
                strSql.Append("(wp.property_name+'/'+(select top 1 customername from wy_rentcontract where contractid=@contractid)) as room_name, ");
                if (!queryParam["contractid"].IsEmpty())
                {
                    string contractid = queryParam["contractid"].ToString();
                    parameter.Add(DbParameters.CreateDbParameter("@contractid", contractid));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@contractid", " "));
                }
            }
            else if (type == 1)
            {
                strSql.Append("(wp.property_name+'/'+(select top 1 room_name from wy_room where room_id=wy_feechangelog.room_id)+'/'+(select top 1 owner_name from wy_owner where owner_id=wy_feechangelog.owner_id)) as room_name,  ");
            }
            else
            {
                strSql.Append(@"(CASE WHEN ISNULL(contract_id,'')='' THEN
                                wp.property_name+'/'+(select top 1 room_name from wy_room where room_id=wy_feechangelog.room_id)
                                +'/'+(select top 1 owner_name from wy_owner where owner_id=wy_feechangelog.owner_id)
                                ELSE
                                (wp.property_name+'/'+(select top 1 customername from wy_rentcontract wrt where wrt.contractid=wy_feechangelog.contract_id))
                                END) as room_name,");
            }

            strSql.Append("source_money,new_money,");
            strSql.Append("(select top 1 building_name from wy_building where building_id=(select top 1 building_id from wy_room where room_id=wy_feechangelog.room_id)) as bname,");
            strSql.Append("(select top 1 feeitem_name from wy_feeitem where feeitem_id=wy_feechangelog.feeitem_id ) as  feename,operatetime,operatername,");
            strSql.Append("income_date,memo from wy_feechangelog");
            strSql.Append(" LEFT JOIN dbo.wy_property wp ON wy_feechangelog.property_id=wp.property_id ");
            strSql.Append("  where wy_feechangelog.property_id=@property_id  ");

            parameter.Add(DbParameters.CreateDbParameter("@property_id", property_id));

            if (!queryParam["room_id"].IsEmpty())
            {
                string room_id = queryParam["room_id"].ToString();
                strSql.Append("   and room_id=@room_id  ");
                parameter.Add(DbParameters.CreateDbParameter("@room_id", room_id));
            }
            if (!queryParam["contract_id"].IsEmpty())
            {
                string contract_id = queryParam["contract_id"].ToString();
                strSql.Append("   and contract_id=@contract_id  ");
                parameter.Add(DbParameters.CreateDbParameter("@contract_id", contract_id));
            }
            if (!queryParam["feeitem_id"].IsEmpty())
            {
                string feeitem_id = queryParam["feeitem_id"].ToString();
                strSql.Append("   and feeitem_id=@feeitem_id  ");
                parameter.Add(DbParameters.CreateDbParameter("@feeitem_id", feeitem_id));
            }
            if (!queryParam["year"].IsEmpty())
            {
                string year = queryParam["year"].ToString();
                strSql.Append("   and DATENAME(YEAR,income_date)=@year  ");
                parameter.Add(DbParameters.CreateDbParameter("@year", year));
            }
            if (!queryParam["mouth"].IsEmpty())
            {
                string mouth = queryParam["mouth"].ToString();
                if (mouth.Length == 1)
                {
                    mouth = "0" + mouth;
                }
                strSql.Append("   and DATENAME(MONTH,income_date)=@mouth  ");
                parameter.Add(DbParameters.CreateDbParameter("@mouth", mouth));
            }
            if (!queryParam["isdel"].IsEmpty())
            {
                string isdel = queryParam["isdel"].ToString();

                if (isdel == "1")
                {
                    // 删除查询
                    strSql.Append(" and memo='删除' ");
                }
                else
                {
                    // 减免查询
                    strSql.Append(" and memo<>'删除' ");
                }
            }
            if (!queryParam["state"].IsEmpty() && !queryParam["end"].IsEmpty())
            {
                string state = queryParam["state"].ToString();
                string end = queryParam["end"].ToString();
                strSql.Append(" AND CONVERT(VARCHAR(10),operatetime,120) BETWEEN @state AND @end ");
                parameter.Add(DbParameters.CreateDbParameter("@state", state));
                parameter.Add(DbParameters.CreateDbParameter("@end", end));
            }

            RepositoryFactory<PaymentSerachEntity> repository = new RepositoryFactory<PaymentSerachEntity>();

            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
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
        public void SaveForm(string keyValue, FeechangelogEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }

        #endregion
    }
}