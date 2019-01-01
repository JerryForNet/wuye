using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Application.IService.RepostryManage;
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

namespace HuRongClub.Application.Service.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-24 18:47
    /// 描 述：物品领用
    /// </summary>
    public class OutbillService : RepositoryFactory, OutbillIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OutbillitemModel> GetPageList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<OutbillitemModel> repository = new RepositoryFactory<OutbillitemModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT  o.*,ISNULL(i.itemname,'') itemname,d.deptname,u.TrueName FROM	tb_wh_outbill o LEFT JOIN dbo.sys_dictitem i ON REPLACE(o.fman,'无',0)=i.itemid LEFT JOIN dbo.hr_department d ON o.fdeptid=d.deptid LEFT JOIN dbo.Accounts_Users u ON o.fuserid=u.UserID  where 1=1 ");
            var queryParam = queryJson.ToJObject();
            //查询条件 领用单编号、创建人 时间
            if (!queryParam["owner_name"].IsEmpty())
            {
                string owner_name = queryParam["owner_name"].ToString();
                strSql.Append(" and ( o.foutbillid LIKE '%" + owner_name + "%' OR u.TrueName LIKE '%" + owner_name + "%'  )");
            }
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and o.foutdate >='" + StartDate + "' ");
            }
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                EndDate = Convert.ToDateTime(EndDate).AddDays(1).ToShortDateString();
                strSql.Append(" and o.foutdate <='" + EndDate + "' ");
            }
            return repository.BaseRepository().FindList(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<OutbillEntity> GetList(string queryJson)
        {
            return null;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OutbillEntity GetEntity(string keyValue)
        {
            RepositoryFactory<OutbillEntity> repository = new RepositoryFactory<OutbillEntity>();
            return repository.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<OutbillgoodModel> GetDetails(string keyValue)
        {
            RepositoryFactory<OutbillgoodModel> repository = new RepositoryFactory<OutbillgoodModel>();
            var strSql = new StringBuilder();
            strSql.Append(" SELECT i.*,g.fname FROM  dbo.tb_wh_outbill_item  i LEFT JOIN dbo.tb_wh_goodsinfo g ON g.fgoodsid = i.fgoodsid WHERE 1=1 ");
            if (keyValue != "")
            {
                strSql.Append(" and  foutbillid='" + keyValue + "' ");
            }
            return repository.BaseRepository().FindList(strSql.ToString());
        }

        public IEnumerable<BillReportModel> GetMonthInbill(List<string> fgoodsid, int year)
        {
            if (fgoodsid == null || fgoodsid.Count == 0) return null;
            StringBuilder ids = new StringBuilder();
            foreach (var item in fgoodsid)
            {
                ids.AppendFormat("'{0}',", item);
            }
            string id = ids.ToString().Substring(0, ids.Length - 1);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"SELECT  ISNULL(SUM(item.fnumber), 0) tot_number ,
                                        ISNULL(SUM(item.fmoney), 0) tot_money ,
                                        item.fgoodsid ,
                                        DATEPART(MONTH, bill.foutdate) mon
                                FROM    dbo.tb_wh_outbill bill
                                        LEFT JOIN dbo.tb_wh_outbill_item item ON bill.foutbillid = item.foutbillid
                                WHERE   DATEPART(YEAR, bill.foutdate) = @year
		                                AND item.fgoodsid IN ({0})
                                GROUP BY item.fgoodsid ,
                                        bill.foutdate", id);

            List<DbParameter> param = new List<DbParameter>();
            param.Add(DbParameters.CreateDbParameter("@year", year));

            RepositoryFactory<BillReportModel> repository = new RepositoryFactory<BillReportModel>();

            return repository.BaseRepository().FindList(sql.ToString(), param.ToArray(), null);
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IEnumerable<OutbillitemEntity> list = null;
            if (!string.IsNullOrEmpty(keyValue))
            {
                //查询明细 先加库存再删除明细
                OutbillitemIService dal = new OutbillitemService();
                list = dal.GetListByFoutbillid(keyValue);
            }

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<OutbillEntity>(keyValue);
                db.Delete<OutbillitemEntity>(t => t.foutbillid.Equals(keyValue));
                if (list != null && list.Count() > 0)
                {
                    foreach (OutbillitemEntity item in list)
                    {
                        var strSqls = new StringBuilder();
                        strSqls.Append("UPDATE tb_wh_goodsinfo SET   ");
                        strSqls.Append(" fcount=ISNULL(fcount,0)+" + item.fnumber);
                        strSqls.Append(",fmoney=(CASE WHEN ISNULL(fmoney,0)<=0 THEN 0 ELSE (fmoney+" + item.fmoney + ") END) ");
                        strSqls.Append(",fprice=(CASE WHEN (fcount+" + item.fnumber + ")<=0 THEN 0 ELSE Round(convert(float,(ISNULL(fmoney,0)+" + item.fmoney + "))/convert(float,(fcount+" + item.fnumber + ")),2) END) ");
                        strSqls.Append("  WHERE fgoodsid='" + item.fgoodsid + "'  ");

                        db.ExecuteBySql(strSqls.ToString());
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="entryList">子表</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, OutbillEntity entity, List<OutbillitemEntity> entryList)
        {
            IEnumerable<OutbillitemEntity> list = null;
            if (!string.IsNullOrEmpty(keyValue))
            {
                //查询明细 先加库存再删除明细
                OutbillitemIService dal = new OutbillitemService();
                list = dal.GetListByFoutbillid(keyValue);
            }

            IRepository db = this.BaseRepository().BeginTrans();

            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    db.Update(entity);

                    if (list != null && list.Count() > 0)
                    {
                        // 加库存
                        foreach (OutbillitemEntity item in list)
                        {
                            var strSqls = AddGoodsRepertorySql(item);

                            db.ExecuteBySql(strSqls.ToString());
                        }

                        //明细
                        string deletesql = " delete tb_wh_outbill_item where foutbillid='" + keyValue + "' ";
                        db.ExecuteBySql(deletesql);
                    }

                    for (int i = 0; i < entryList.Count; i++)
                    {
                        entryList[i].foutbillid = entity.foutbillid;
                        entryList[i].fitemid = entity.foutbillid + "-" + Utils.SupplementZero((i + 1).ToString(), 3); //生成领用单物品信息编号

                        // 明细
                        db.Insert(entryList[i]);

                        //减库存
                        var strSql = RemoveGoodsRepertorySql(entryList[i]);
                        db.ExecuteBySql(strSql.ToString());
                    }
                }
                else
                {
                    //主表
                    entity.Create();
                    entity.finputdate = DateTime.Now;
                    entity.foutdate = DateTime.Now;
                    int userid = 0;
                    int.TryParse(OperatorProvider.Provider.Current().OldSystemUserID, out userid);
                    entity.fuserid = userid;
                    db.Insert(entity);

                    //明细
                    for (int i = 0; i < entryList.Count; i++)
                    {
                        entryList[i].foutbillid = entity.foutbillid;
                        entryList[i].fitemid = entity.foutbillid + "-" + Utils.SupplementZero((i + 1).ToString(), 3); //生成领用单物品信息编号

                        db.Insert(entryList[i]);

                        var strSql = RemoveGoodsRepertorySql(entryList[i]);

                        // 减库存
                        db.ExecuteBySql(strSql.ToString());
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 加库存的Sql
        /// </summary>
        /// <param name="entryList"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public StringBuilder AddGoodsRepertorySql(OutbillitemEntity item)
        {
            var strSqls = new StringBuilder();
            strSqls.AppendFormat(@" UPDATE  tb_wh_goodsinfo
                                    SET     fcount = ISNULL(fcount, 0) + {0} ,
                                            fmoney = ( ISNULL(fmoney, 0) + {1} ) ,
                                            fprice = ROUND(CONVERT(FLOAT, ( ISNULL(fmoney, 0) + {1} )) / ( ISNULL(fcount, 0) + {0} ), 2)
                                    WHERE   fgoodsid = '{2}'  ", item.fnumber, item.fmoney, item.fgoodsid);
            return strSqls;
        }

        /// <summary>
        /// 减库存的Sql
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public StringBuilder RemoveGoodsRepertorySql(OutbillitemEntity item)
        {
            var strSqls = new StringBuilder();

            strSqls.AppendFormat(@" UPDATE  tb_wh_goodsinfo
                                    SET     fcount = ( CASE WHEN ( ISNULL(fcount, 0) - {0} ) <= 0 THEN 0
                                                        ELSE ISNULL(fcount, 0) - {0}
                                                    END ) ,
                                            fmoney = ( CASE WHEN (( ISNULL(fmoney, 0) - {1} ) <= 0 OR ( ISNULL(fcount, 0) - {0} ) <= 0) THEN 0
                                                        ELSE ( fmoney - {1} )
                                                    END ) ,
                                            fprice = ( CASE WHEN (( ISNULL(fmoney, 0) - {1} ) <= 0 OR ( ISNULL(fcount, 0) - {0} ) <= 0) THEN 0
                                                        ELSE ROUND(CONVERT(FLOAT, ( ISNULL(fmoney, 0) - {1} )) / CONVERT(FLOAT, ( fcount - {0} )), 2)
                                                    END )
                                    WHERE   fgoodsid = '{2}'  ", item.fnumber, item.fmoney, item.fgoodsid);
            return strSqls;
        }

        #endregion 提交数据

        #region 验证数据

        /// <summary>
        /// 获取主键id
        /// </summary>
        /// <returns></returns>
        public string modifyid(string foutbillid)
        {
            var strSql = new StringBuilder();
            strSql.Append(@" select foutbillid from tb_wh_outbill where  foutbillid='" + foutbillid + "' ");
            object bj = this.BaseRepository().FindObject(strSql.ToString());
            if (bj != null)
            {
                return bj.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取主键id
        /// </summary>
        /// <returns></returns>
        public string Itemid(string foutbillid)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"    SELECt CONVERT(NVARCHAR(20),MAX(RIGHT(fitemid,1)+1))  FROM   dbo.tb_wh_outbill_item WHERE  fitemid LIKE '" + foutbillid + "%' ");
            object bj = this.BaseRepository().FindObject(strSql.ToString());
            if (bj.ToString() != "")
            {
                return bj.ToString();
            }
            else
            {
                return "1";
            }
        }

        /// <summary>
        /// 验证是否有子表数据
        /// </summary>
        /// <param name="foutid"></param>
        /// <returns></returns>
        public string CheckDel(string foutid)
        {
            RepositoryFactory<OutbillEntity> repository = new RepositoryFactory<OutbillEntity>();
            var strSql = new StringBuilder();
            strSql.Append("  SELECT COUNT(*) FROM dbo.tb_wh_outbill_item WHERE foutbillid='" + foutid + "' ");
            object bj = this.BaseRepository().FindObject(strSql.ToString());
            if (bj.ToString() != "" && bj.ToString() != "0")
            {
                return bj.ToString();
            }
            else
            {
                return null;
            }
        }

        #endregion 验证数据
    }
}