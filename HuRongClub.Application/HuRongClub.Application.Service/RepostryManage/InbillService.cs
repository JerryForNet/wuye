using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Application.Entity.SystemManage;
using HuRongClub.Application.IService.RepostryManage;
using HuRongClub.Application.IService.SystemManage;
using HuRongClub.Application.Service.SystemManage;
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
    /// 创 建：超级管理员
    /// 日 期：2017-04-08 18:58
    /// 描 述：Inbill
    /// </summary>
    public class InbillService : RepositoryFactory, InbillIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<InbillModel> GetPageList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<InbillModel> repository = new RepositoryFactory<InbillModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT  i.*,u.TrueName  FROM  dbo.tb_wh_inbill i LEFT JOIN dbo.Accounts_Users u  ON  i.fuserid=u.UserID WHERE 1=1 ");
            var queryParam = queryJson.ToJObject();
            //查询条件 入库单编号、创建人 时间
            if (!queryParam["owner_name"].IsEmpty())
            {
                string owner_name = queryParam["owner_name"].ToString();
                strSql.Append(" and ( i.finbillid LIKE '%" + owner_name + "%' OR u.TrueName LIKE '%" + owner_name + "%'  )");
            }
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and i.findate >='" + StartDate + "' ");
            }
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and i.findate <='" + EndDate + "' ");
            }
            return repository.BaseRepository().FindList(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public InbillEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<InbillEntity>(keyValue);
        }

        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<InbillItemByfinidModel> GetDetails(string keyValue)
        {
            RepositoryFactory<InbillItemByfinidModel> repository = new RepositoryFactory<InbillItemByfinidModel>();
            var strSql = new StringBuilder();
            strSql.Append(" SELECT i.*,p.fname AS providerName,g.fname AS GoodName FROM   dbo.tb_wh_inbill_item i LEFT JOIN dbo.tb_wh_provider p ON i.fproviderid=p.fproviderid LEFT JOIN dbo.tb_wh_goodsinfo g ON g.fgoodsid = i.fgoodsid WHERE 1=1 ");
            if (keyValue != "")
            {
                strSql.Append(" and  finbillid='" + keyValue + "' ");
            }
            return repository.BaseRepository().FindList(strSql.ToString());
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IEnumerable<InbillItemEntity> list = null;
            if (!string.IsNullOrEmpty(keyValue))
            {
                //查询明细 先减库存再删除明细
                InbillItemService dal = new InbillItemService();
                list = dal.GetListByfinbillid(keyValue);
            }

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                // 删主表
                db.Delete<InbillEntity>(keyValue);
                // 删明细
                db.Delete<InbillItemEntity>(t => t.finbillid.Equals(keyValue));

                // 删库存
                if (list != null && list.Count() > 0)
                {
                    foreach (InbillItemEntity item in list)
                    {
                        var strSqls = RemoveGoodsRepertorySql(item);

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
        /// <returns></returns>
        public void SaveForm(string keyValue, InbillEntity entity, List<InbillItemEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    db.Update(entity);

                    // 查询明细
                    InbillItemService dal = new InbillItemService();

                    IEnumerable<InbillItemEntity> list = dal.GetListByfinbillid(keyValue);
                    if (list != null && list.Count() > 0)
                    {
                        // 减历史库存
                        foreach (InbillItemEntity item in list)
                        {
                            var strSqls = RemoveGoodsRepertorySql(item);

                            db.ExecuteBySql(strSqls.ToString());
                        }

                        // 删明细
                        string deletesql = " delete tb_wh_inbill_item where finbillid='" + keyValue + "' ";
                        db.ExecuteBySql(deletesql);
                    }

                    int i = 0;
                    foreach (InbillItemEntity item in entryList)
                    {
                        // 加明细
                        item.Create();
                        item.finbillid = entity.finbillid;
                        item.fitemid = entity.finbillid + "-" + Utils.SupplementZero((i + 1).ToString(), 3); //生成领用单物品信息编号

                        db.Insert(item);

                        #region 编辑期间如果有出库动作的，要计算进来，否则会导致库存数量不对 Author:Jerry.Li Time:2017/10/25 17:31

                        InbillItemEntity inentity = new InbillItemEntity();
                        inentity.fnumber = item.fnumber;
                        inentity.fmoney = item.fmoney;
                        inentity.fgoodsid = item.fgoodsid;

                        string safeSql = string.Format(@" SELECT  *
                                            FROM    tb_wh_outbill_item i
                                                    LEFT JOIN dbo.tb_wh_outbill o ON o.foutbillid = i.foutbillid
                                            WHERE   i.fgoodsid = '{0}'
                                                    AND o.finputdate >= '{1}'", item.fgoodsid, entity.finputdate);

                        IEnumerable<OutbillitemEntity> olist = new RepositoryFactory().BaseRepository().FindList<OutbillitemEntity>(safeSql);
                        if (olist != null)
                        {
                            foreach (OutbillitemEntity obill in olist.ToList())
                            {
                                inentity.fnumber = Convert.ToInt32(inentity.fnumber) - Convert.ToInt32(obill.fnumber);
                                inentity.fmoney = inentity.fmoney - Convert.ToDecimal(obill.fmoney);
                            }
                        }
                        #endregion

                        //加库存
                        var strSqls = AddGoodsRepertorySql(inentity);

                        db.ExecuteBySql(strSqls.ToString());
                        i++;
                    }
                }
                else
                {
                    //主表
                    entity.Create();
                    entity.finputdate = DateTime.Now;
                    int userid = 0;
                    int.TryParse(OperatorProvider.Provider.Current().OldSystemUserID, out userid);
                    entity.fuserid = userid;
                    db.Insert(entity);

                    //明细
                    for (int i = 0; i < entryList.Count; i++)
                    {
                        entryList[i].finbillid = entity.finbillid;
                        entryList[i].fitemid = entity.finbillid + "-" + Utils.SupplementZero((i + 1).ToString(), 3); //生成领用单物品信息编号

                        db.Insert(entryList[i]);


                        //加库存
                        var strSqls = AddGoodsRepertorySql(entryList[i]);

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
        /// 加库存的Sql
        /// </summary>
        /// <param name="entryList"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public StringBuilder AddGoodsRepertorySql(InbillItemEntity item)
        {
            var strSqls = new StringBuilder();
            strSqls.AppendFormat(@" UPDATE  tb_wh_goodsinfo
                                    SET     fcount = ISNULL(fcount, 0) + {0} ,
                                            fmoney = ( ISNULL(fmoney, 0) + {1} ) ,
                                            fprice = (CASE WHEN (((ISNULL(fmoney, 0) + {1}) <= 0) or ((ISNULL(fcount, 0) + {0}) = 0)) THEN 0
				                                        ELSE ROUND(CONVERT(FLOAT, ( ISNULL(fmoney, 0) + {1} )) / ( ISNULL(fcount, 0) + {0} ), 2) 
                                                    END)
                                    WHERE   fgoodsid = '{2}'  ", item.fnumber, item.fmoney, item.fgoodsid);
            return strSqls;
        }
        /// <summary>
        /// 减库存的Sql
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public StringBuilder RemoveGoodsRepertorySql(InbillItemEntity item)
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
        public string modifyid(string finbillid)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select finbillid from tb_wh_inbill where finbillid='" + finbillid + "' ");
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
        public string Itemid(string finbillid)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"   SELECt CONVERT(NVARCHAR(20),MAX(RIGHT(fitemid,1)+1))  FROM   dbo.tb_wh_inbill_item WHERE  fitemid LIKE '" + finbillid + "%' ");
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
        /// 验证是否存在子表数据
        /// </summary>
        /// <param name="findid"></param>
        /// <returns></returns>
        public string CheckDel(string findid)
        {
            RepositoryFactory<InbillEntity> repository = new RepositoryFactory<InbillEntity>();
            var strSql = new StringBuilder();
            strSql.Append(" SELECT COUNT(*) FROM  dbo.tb_wh_inbill_item WHERE finbillid='" + findid + "' ");
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