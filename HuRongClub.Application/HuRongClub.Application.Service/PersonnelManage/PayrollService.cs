using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Data.Repository;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HuRongClub.Application.Service.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-04-27 18:40
    /// 描 述：Payroll
    /// </summary>
    public class PayrollService : RepositoryFactory<PayrollEntity>, PayrollIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PayrollEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<PayrollEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public PayrollEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
        public void SaveForm(string keyValue, PayrollEntity entity)
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

        /// <summary>
        /// 获取当前最大ID【待测试】
        /// </summary>
        /// <returns></returns>
        public int FindMaxID()
        {
            try
            {
                string safeSql = "select MAX(PayrollId) from hr_payroll";
                DataTable tb = this.BaseRepository().FindTable(safeSql);
                if (tb != null && tb.Rows.Count > 0 )
                {
                    if (tb.Rows[0][0] != null && tb.Rows[0][0].ToString() != "")
                    {
                        return Convert.ToInt32(tb.Rows[0][0]);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
            }

            return 0;
        }

        /// <summary>
        /// 保存上传数据
        /// </summary>
        /// <param name="parollEntity">薪资记录</param>
        /// <param name="detailList">所有的薪资信息</param>
        public void SaveUploadData(PayrollEntity parollEntity, List<PaydetailEntity> detailList)
        {
            try
            {
                this.BaseRepository().BeginTrans();

                this.BaseRepository().Insert(parollEntity);
                if (detailList.Count > 0)
                {
                    foreach (PaydetailEntity detail in detailList)
                    {
                        new RepositoryFactory<PaydetailEntity>().BaseRepository().Insert(detail);
                    }
                }

                this.BaseRepository().Commit();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}