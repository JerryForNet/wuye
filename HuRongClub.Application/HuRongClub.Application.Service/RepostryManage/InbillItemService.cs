using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Application.IService.RepostryManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：彭长青
    /// 日 期：2017-04-26 13:42
    /// 描 述：入库汇总
    /// </summary>
    public class InbillItemService : RepositoryFactory, InbillItemIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<InbillItemByfinidModel> GetPageList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<InbillItemByfinidModel> repository = new RepositoryFactory<InbillItemByfinidModel>();
            var strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["condition"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                switch (condition)
                {
                    case "inid":            //入库
                        if (!queryParam["keyword"].IsEmpty())
                        {
                            string keyword = queryParam["keyword"].ToString();
                            strSql.Append(" SELECT i.*,p.fname AS providerName,g.fname AS GoodName FROM   dbo.tb_wh_inbill_item i LEFT JOIN dbo.tb_wh_provider p ON i.fproviderid=p.fproviderid LEFT JOIN dbo.tb_wh_goodsinfo g ON g.fgoodsid = i.fgoodsid WHERE 1=1 and finbillid='" + keyword + "' ");
                            return repository.BaseRepository().FindList(strSql.ToString());
                        }
                        break;

                    case "outid":            //出库
                        if (!queryParam["keyword"].IsEmpty())
                        {
                            string keyword = queryParam["keyword"].ToString();
                            strSql.Append("  SELECT i.*,g.fname  AS GoodName FROM  dbo.tb_wh_outbill_item  i LEFT JOIN dbo.tb_wh_goodsinfo g ON g.fgoodsid = i.fgoodsid WHERE 1=1 AND i.foutbillid='" + keyword + "'   ");
                            return repository.BaseRepository().FindList(strSql.ToString());
                        }
                        break;

                    default:
                        break;
                }
            }
            return repository.BaseRepository().FindList("");
        }

        /// <summary>
        /// 入库汇总列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<billItemModel> GetList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<billItemModel> repository = new RepositoryFactory<billItemModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" select a.fgoodsid as fgoodsid,fnumber=sum(a.fnumber),fmoney=sum(a.fmoney),  ");
            strSql.Append(@" fname=(select fname from tb_wh_goodsinfo where tb_wh_goodsinfo.fgoodsid=a.fgoodsid),  ");
            strSql.Append(@" funit=(select funit from tb_wh_goodsinfo where tb_wh_goodsinfo.fgoodsid=a.fgoodsid)   ");
            strSql.Append(@" FROM tb_wh_inbill_item a   ");
            strSql.Append(@" LEFT join tb_wh_inbill b on a.finbillid = b.finbillid where 1=1   ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            //查询条件 汇总时间
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and b.findate>=@StartDate ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
            }
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and b.findate<=@EndDate  ");
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }

            strSql.Append(@" GROUP by a.fgoodsid  ");
            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }

        /// <summary>
        /// 获取列表 --入库单主键
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<InbillItemByfinidModel> GetItemList(string queryJson)
        {
            RepositoryFactory<InbillItemByfinidModel> repository = new RepositoryFactory<InbillItemByfinidModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT i.*,p.fname AS providerName,g.fname AS GoodName FROM   dbo.tb_wh_inbill_item i LEFT JOIN dbo.tb_wh_provider p ON i.fproviderid=p.fproviderid LEFT JOIN dbo.tb_wh_goodsinfo g ON g.fgoodsid = i.fgoodsid WHERE 1=1 ");
            var queryParam = queryJson.ToJObject();
            //员工id
            if (!queryParam["finbillid"].IsEmpty())
            {
                string foutbillid = queryParam["finbillid"].ToString();
                strSql.Append(" and finbillid='" + foutbillid + "' ");
            }
            return repository.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 入库统计列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<billStatisticsModel> GetStatisticsList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<billStatisticsModel> repository = new RepositoryFactory<billStatisticsModel>();
            var strSql = new StringBuilder();
            strSql.Append(@"select * from (select a.*,CONVERT(NVARCHAR(10),b.findate,120) AS findate,c.fname,c.funit,isnull(d.fname,'') as fprovider from tb_wh_inbill_item a ");
            strSql.Append(@" LEFT join tb_wh_inbill b on a.finbillid = b.finbillid ");
            strSql.Append(@" LEFT join tb_wh_goodsinfo c on a.fgoodsid = c.fgoodsid ");
            strSql.Append(@" LEFT join tb_wh_provider d on a.fproviderid = d.fproviderid  where 1 = 1 ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            //查询条件 统计时间
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and b.findate>=@StartDate ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
            }
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and b.findate<=@EndDate  ");
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }
            //供应商
            if (!queryParam["Provider"].IsEmpty())
            {
                string Provider = queryParam["Provider"].ToString();
                strSql.Append(" and d.fproviderid=@Provider  ");
                parameter.Add(DbParameters.CreateDbParameter("@Provider", Provider));
            }
            //入库单编号
            if (!queryParam["Billid"].IsEmpty())
            {
                string Billid = queryParam["Billid"].ToString();
                strSql.Append(" and b.finbillid=@Billid  ");
                parameter.Add(DbParameters.CreateDbParameter("@Billid", Billid));
            }
            //查询方式 QueryType 1大类  2小类  3物品
            if (!queryParam["QueryType"].IsEmpty())
            {
                string type = queryParam["QueryType"].ToString();
                switch (type)
                {
                    case "1":
                        //大类
                        if (!queryParam["LargeClass"].IsEmpty())
                        {
                            string LargeClass = queryParam["LargeClass"].ToString();
                            strSql.Append(" AND left(a.fgoodsid, 3)= @LargeClass  ");
                            parameter.Add(DbParameters.CreateDbParameter("@LargeClass", LargeClass));
                        }
                        break;

                    case "2":
                        //小类
                        if (!queryParam["SmallClass"].IsEmpty())
                        {
                            string SmallClass = queryParam["SmallClass"].ToString();
                            strSql.Append(" AND left(a.fgoodsid, 6)= @SmallClass  ");
                            parameter.Add(DbParameters.CreateDbParameter("@SmallClass", SmallClass));
                        }
                        break;

                    case "3":
                        //物品
                        if (!queryParam["Goods"].IsEmpty())
                        {
                            string Goods = queryParam["Goods"].ToString();
                            strSql.Append(" AND a.fgoodsid = @Goods  ");
                            parameter.Add(DbParameters.CreateDbParameter("@Goods", Goods));
                        }
                        break;
                }
            }
            strSql.Append(")x1 ");
            //strSql.Append(@" order by b.findate desc   ");
            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }

        /// <summary>        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public InbillItemEntity GetEntity(string keyValue)
        {
            RepositoryFactory<InbillItemEntity> repository = new RepositoryFactory<InbillItemEntity>();
            return repository.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="finbillid"></param>
        /// <returns></returns>
        public IEnumerable<InbillItemEntity> GetListByfinbillid(string finbillid)
        {
            var expression = LinqExtensions.True<InbillItemEntity>();
            expression = expression.And(t => t.finbillid == finbillid);

            return this.BaseRepository().FindList(expression);
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="type">inid 入库单  outid 出库单</param>
        public void RemoveForm(string keyValue, string type)
        {
            IRepository db = this.BaseRepository().BeginTrans();

            try
            {
                if (type == "inid")
                {
                    #region 入库单删除
                    InbillItemEntity ent = GetEntity(keyValue);
                    if (ent != null)
                    {
                        GoodsinfoIService dal_g = new GoodsinfoService();
                        GoodsinfoEntity ent_g = dal_g.GetEntity(ent.fgoodsid);
                        if (ent_g != null)
                        {
                            double dbe = ent_g.fcount - ent.fnumber.ToDouble();
                            if (dbe < 0) {
                                dbe = 0;
                            }
                            ent_g.fcount = dbe;
                            decimal dl = ent_g.fmoney - ent.fmoney;
                            if (dl < 0)
                            {
                                dl = 0;
                            }
                            ent_g.fmoney = dl;
                            db.Update(ent_g);
                        }
                    }
                    db.Delete<InbillItemEntity>(keyValue);
                    #endregion
                }
                else if (type == "outid")
                {
                    #region 出库单删除
                    OutbillitemIService dal = new OutbillitemService();
                    OutbillitemEntity ent = dal.GetEntity(keyValue);
                    if (ent != null)
                    {
                        GoodsinfoIService dal_g = new GoodsinfoService();
                        GoodsinfoEntity ent_g = dal_g.GetEntity(ent.fgoodsid);
                        if (ent_g != null)
                        {
                            ent_g.fcount = ent_g.fcount + ent.fnumber.ToDouble();
                            decimal dl = ent_g.fmoney + ent.fmoney.ToDecimal();
                            ent_g.fmoney = dl;
                            db.Update(ent_g);
                        }
                    }

                    db.Delete<OutbillitemEntity>(keyValue);
                    #endregion
                }
                db.Commit();
            }
            catch (System.Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="type">inid 入库单  outid 出库单</param>
        public void RemoveFormAll(string keyValue, string type)
        {
            IRepository db = this.BaseRepository().BeginTrans();

            try
            {
                if (type == "inid")
                {
                    #region 入库单删除
                    IEnumerable<InbillItemEntity> list = GetListByfinbillid(keyValue);
                    if (list.Count() > 0)
                    {
                        foreach (InbillItemEntity item in list)
                        {
                            GoodsinfoIService dal_g = new GoodsinfoService();
                            GoodsinfoEntity ent_g = dal_g.GetEntity(item.fgoodsid);
                            if (ent_g != null)
                            {
                                double dbe = ent_g.fcount - item.fnumber.ToDouble();
                                if (dbe < 0)
                                {
                                    dbe = 0;
                                }
                                ent_g.fcount = dbe;
                                decimal dl = ent_g.fmoney - item.fmoney;
                                if (dl < 0)
                                {
                                    dl = 0;
                                }
                                ent_g.fmoney = dl;
                                db.Update(ent_g);
                            }
                        }
                    }
                    var expression = LinqExtensions.True<InbillItemEntity>();
                    expression = expression.And(t => t.finbillid == keyValue);
                    //子表
                    db.Delete(expression);
                    //主表
                    db.Delete<InbillEntity>(keyValue);
                    #endregion
                }
                else if (type == "outid")
                {
                    #region 出库单删除
                    OutbillitemIService dal = new OutbillitemService();
                    IEnumerable<OutbillitemEntity> list = dal.GetListByFoutbillid(keyValue);
                    if (list.Count() > 0)
                    {
                        foreach (OutbillitemEntity item in list)
                        {
                            GoodsinfoIService dal_g = new GoodsinfoService();
                            GoodsinfoEntity ent_g = dal_g.GetEntity(item.fgoodsid);
                            if (ent_g != null)
                            {
                                ent_g.fcount = ent_g.fcount + item.fnumber.ToDouble();
                                decimal dl = ent_g.fmoney + item.fmoney.ToDecimal();
                                ent_g.fmoney = dl;
                                db.Update(ent_g);
                            }
                        }
                    }

                    var expression = LinqExtensions.True<OutbillitemEntity>();
                    expression = expression.And(t => t.foutbillid == keyValue);
                    //子表
                    db.Delete(expression);
                    //主表
                    db.Delete<OutbillEntity>(keyValue);

                    #endregion
                }
                db.Commit();
            }
            catch (System.Exception)
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
        public void SaveForm(string keyValue, InbillItemEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                entity.fitemid = entity.finbillid + "00" + modifyid(entity.finbillid); //生成领用单物品信息编号
                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// 获取主键id
        /// </summary>
        /// <returns></returns>
        public string modifyid(string finbillid)
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

        #endregion 提交数据
    }
}