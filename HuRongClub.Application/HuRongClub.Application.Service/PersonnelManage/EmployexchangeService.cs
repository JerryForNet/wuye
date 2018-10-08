using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace HuRongClub.Application.Service.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 19:31
    /// 描 述：员工职位变动
    /// </summary>
    public class EmployexchangeService : RepositoryFactory<EmployexchangeEntity>, EmployexchangeIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EmployexchangeEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<EmployexchangeEntity>();
            var queryParam = queryJson.ToJObject();
            //员工id
            if (!queryParam["empid"].IsEmpty())
            {
                string empid = queryParam["empid"].ToString();
                expression = expression.And(t => t.empid.ToString() == empid);
            }
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "fromdept":         //原部门
                        expression = expression.And(t => t.fromdept.Contains(keyword));
                        break;

                    case "fromclass":          //原职级
                        expression = expression.And(t => t.fromclass.Contains(keyword));
                        break;

                    case "todept":         //现部门
                        expression = expression.And(t => t.todept.Contains(keyword));
                        break;

                    case "toclass":          //现职级
                        expression = expression.And(t => t.toclass.Contains(keyword));
                        break;

                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EmployexchangeEntity GetEntity(int keyValue)
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
        public void SaveForm(string keyValue, EmployexchangeEntity entity)
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