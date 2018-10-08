using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.Entity.FinanceManage.ViewModel;
using HuRongClub.Application.IService.FinanceManage;
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

namespace HuRongClub.Application.Service.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-20 10:13
    /// 描 述：支出费用列表
    /// </summary>
    public class InnerbillService : RepositoryFactory, InnerbillIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<InnerbillModel> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            strSql.Append(@" select *,deptname=(select deptname from hr_department where deptid=wy_innerbill.deptid),(select sum(feemoney) from wy_innerbillitem where billid=wy_innerbill.billid) as feemoney from wy_innerbill where 1=1  ");
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "ticketnumber":            //票号
                        strSql.Append(" and ticketnumber like '%" + keyword + "%' ");
                        break;

                    case "operater":          //创建者
                        strSql.Append(" and operater like '%" + keyword + "%' ");
                        break;

                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList<InnerbillModel>(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<InnerbillEntity> GetList(string queryJson)
        {
            return null;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public InnerbillModel GetEntity(string keyValue)
        {
            string safeSql = @"SELECT TOP 1
                        [Extent1].[billid] AS [billid],
                        [Extent1].[deptid] AS [deptid],
                        dep.[deptname] AS [deptname],
                        item.[feemoney] AS [feemoney],
                        [Extent1].[ticketnumber] AS [ticketnumber],
                        [Extent1].[notes] AS [notes],
                        [Extent1].[operater] AS [operater],
                        [Extent1].[inputtime] AS [inputtime],
                        [Extent1].[moneydate] AS [moneydate],
                        [Extent1].[ifpay] AS [ifpay]
                        FROM [dbo].[wy_innerbill] AS [Extent1]
	                    LEFT JOIN dbo.hr_department AS dep ON [Extent1].deptid = dep.deptid
	                    LEFT JOIN wy_innerbillitem AS item ON item.billid = Extent1.billid
                        WHERE [Extent1].[billid] = @billid";

            DbParameter[] parameter =
            {
                DbParameters.CreateDbParameter("@billid",keyValue),
            };

            //获取详情字段
            InnerbillModel model = this.BaseRepository().FindList<InnerbillModel>(safeSql, parameter).FirstOrDefault();
            if (model != null)
            {
                // 查询子表
                IList<InnerbillitemEntity> list = this.BaseRepository().FindList<InnerbillitemEntity>(w => w.billid == model.billid).ToList();
                if (list != null && list.Count > 0)
                {
                    model.billitems = list;
                }
            }

            return model;
        }

        #endregion 获取数据

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
        public void SaveForm(string keyValue, InnerbillEntity entity)
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

        #endregion 提交数据
    }
}