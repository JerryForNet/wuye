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
    /// 创 建：王菲
    /// 日 期：2017-04-25 11:47
    /// 描 述：领用单对应物品信息
    /// </summary>
    public class OutbillitemService : RepositoryFactory, OutbillitemIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OutbillgoodModel> GetPageList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<OutbillgoodModel> repository = new RepositoryFactory<OutbillgoodModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT i.*,g.fname FROM  dbo.tb_wh_outbill_item  i LEFT JOIN dbo.tb_wh_goodsinfo g ON g.fgoodsid = i.fgoodsid ");
            return repository.BaseRepository().FindList(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<OutbillgoodModel> GetList(string queryJson)
        {
            RepositoryFactory<OutbillgoodModel> repository = new RepositoryFactory<OutbillgoodModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT i.*,g.fname FROM  dbo.tb_wh_outbill_item  i LEFT JOIN dbo.tb_wh_goodsinfo g ON g.fgoodsid = i.fgoodsid  where 1=1 ");
            var queryParam = queryJson.ToJObject();
            //员工id
            if (!queryParam["foutbillid"].IsEmpty())
            {
                string foutbillid = queryParam["foutbillid"].ToString();
                strSql.Append(" and foutbillid like '" + foutbillid + "%' ");
            }
            return repository.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<billItemModel> GetOutList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<billItemModel> repository = new RepositoryFactory<billItemModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" select a.fgoodsid as fgoodsid,fnumber=sum(a.fnumber),fmoney=sum(a.fmoney),  ");
            strSql.Append(@" fname=(select fname from tb_wh_goodsinfo where tb_wh_goodsinfo.fgoodsid=a.fgoodsid),  ");
            strSql.Append(@" funit=(select funit from tb_wh_goodsinfo where tb_wh_goodsinfo.fgoodsid=a.fgoodsid)   ");
            strSql.Append(@" FROM tb_wh_outbill_item a   ");
            strSql.Append(@" LEFT join tb_wh_outbill b on a.foutbillid = b.foutbillid ");
            strSql.Append(@" LEFT join hr_department c on b.fdeptid = c.deptid  where 1=1  ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            //查询条件 领用时间
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and b.foutdate>=@StartDate ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
            }
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and b.foutdate<=@EndDate  ");
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }
            if (!queryParam["DeptName"].IsEmpty())
            {
                string DeptName = queryParam["DeptName"].ToString();
                strSql.Append(" and c.deptid=@DeptName  ");
                parameter.Add(DbParameters.CreateDbParameter("@DeptName", DeptName));
            }

            strSql.Append(@" GROUP by a.fgoodsid    ");
            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }

        /// <summary>
        /// 领用统计
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<OutStatisticsModel> GetStatisticsList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<OutStatisticsModel> repository = new RepositoryFactory<OutStatisticsModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" select a.*,CONVERT(NVARCHAR(10),b.foutdate,120) AS foutdate,b.fdeptid,c.fname,c.funit,isnull(d.deptname,'') as fdeptname from tb_wh_outbill_item a ");
            strSql.Append(@" INNER join tb_wh_outbill b on a.foutbillid = b.foutbillid ");
            strSql.Append(@" INNER join tb_wh_goodsinfo c on a.fgoodsid = c.fgoodsid ");
            strSql.Append(@" INNER join hr_department d on b.fdeptid = d.deptid  where 1 = 1 ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            //查询条件 领用时间
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and b.foutdate>=@StartDate ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
            }
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and b.foutdate<=@EndDate  ");
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }
            //部门
            if (!queryParam["Provider"].IsEmpty())
            {
                string Provider = queryParam["Provider"].ToString();
                strSql.Append(" and  d.deptid=@Provider  ");
                parameter.Add(DbParameters.CreateDbParameter("@Provider", Provider));
            }
            //出库单号
            if (!queryParam["Billid"].IsEmpty())
            {
                string Billid = queryParam["Billid"].ToString();
                strSql.Append(" and a.foutbillid=@Billid  ");
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
            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }

        /// <summary>
        /// 领用费用
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ReceiveCostModel> GetReceiveCostList()
        {
            RepositoryFactory<ReceiveCostModel> repository = new RepositoryFactory<ReceiveCostModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT isnull(fmoney,0)AS fmoney,b.fdeptid,b.foutdate,left(ltrim(fgoodsid),3) AS fgoodsid  FROM tb_wh_outbill_item AS a ");
            strSql.Append(@" INNER JOIN tb_wh_outbill AS b ON a.foutbillid=b.foutbillid ");
            return repository.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OutbillitemEntity GetEntity(string keyValue)
        {
            RepositoryFactory<OutbillitemEntity> repository = new RepositoryFactory<OutbillitemEntity>();
            return repository.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 根据出库单编号查询 明细列表
        /// </summary>
        /// <param name="foutbillid"></param>
        /// <returns></returns>
        public IEnumerable<OutbillitemEntity> GetListByFoutbillid(string foutbillid)
        {
            var expression = LinqExtensions.True<OutbillitemEntity>();
            expression = expression.And(t => t.foutbillid == foutbillid);

            return this.BaseRepository().FindList(expression);
        }


        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            RepositoryFactory<OutbillgoodModel> repository = new RepositoryFactory<OutbillgoodModel>();
            repository.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, OutbillitemEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.fitemid = entity.foutbillid + "00" + modifyid(entity.foutbillid); //生成领用单物品信息编号
                entity.fprice = decimal.Parse(price(entity.fgoodsid));
                entity.fmoney = decimal.Parse(entity.fprice.ToString()) * decimal.Parse(entity.fnumber.ToString());
                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// 获取主键id
        /// </summary>
        /// <returns></returns>
        public string modifyid(string foutbillid)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  CONVERT(VARCHAR(20),max(RIGHT(fitemid,1))+1) from tb_wh_outbill_item WHERE fitemid LIKE '" + foutbillid + "%' ");
            object bj = this.BaseRepository().FindObject(strSql.ToString());
            if (bj != null)
            {
                return bj.ToString();
            }
            else
            {
                return "1";
            }
        }

        /// <summary>
        /// 获取价格
        /// </summary>
        /// <returns></returns>
        public string price(string fgoodsid)
        {
            var strSql = new StringBuilder();
            strSql.Append(@" select fprice from tb_wh_goodsinfo where fgoodsid='" + fgoodsid + "' ");
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
        /// 获取数量
        /// </summary>
        /// <returns></returns>
        public string NuCounts(string fnumber)
        {
            var strSql = new StringBuilder();
            strSql.Append(@" select fcount from tb_wh_goodsinfo where fgoodsid='" + fnumber + "' ");
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

        #endregion 提交数据
    }
}