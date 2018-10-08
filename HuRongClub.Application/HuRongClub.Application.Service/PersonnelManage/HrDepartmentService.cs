using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.PersonnelManage
{
    /// <summary>
    /// 版 本 6.1

    /// 创 建：王菲
    /// 日 期：2017-04-01 10:41
    /// 描 述：部门信息
    /// </summary>
    public class HrDepartmentService : RepositoryFactory<HrDepartmentEntity>, HrDepartmentIService
    {
        #region 获取数据

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HrDepartmentEntity> GetList()
        {
            var expression = LinqExtensions.True<HrDepartmentEntity>();
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<HrDepartmentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<HrDepartmentEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "deptname":            //部门名称
                        expression = expression.And(t => t.deptname.Contains(keyword));
                        break;

                    case "manager":          //负责人
                        expression = expression.And(t => t.manager.Contains(keyword));
                        break;

                    case "phone":          //部门电话
                        expression = expression.And(t => t.phone.Contains(keyword));
                        break;

                    case "managerphone":          //负责人电话
                        expression = expression.And(t => t.managerphone.Contains(keyword));
                        break;

                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<HrDepartmentEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public HrDepartmentEntity GetEntity(int keyValue)
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
            BaseManage.DepartmentService dal = new BaseManage.DepartmentService();
            dal.RemoveForm(keyValue);

            this.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, HrDepartmentEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);

                Entity.BaseManage.DepartmentEntity dent = new Entity.BaseManage.DepartmentEntity();
                dent.DepartmentId = keyValue;
                dent.OrganizeId = "207fa1a9-160c-4943-a89b-8fa4db0547ce";
                dent.ParentId = "0";
                dent.EnCode = keyValue;
                dent.FullName = entity.deptname;
                dent.OuterPhone = entity.managerphone;
                dent.Manager = entity.manager;
                BaseManage.DepartmentService dal = new BaseManage.DepartmentService();

                dal.SavesForm(keyValue, dent);
            }
            else
            {
                //entity.Create();
                //this.BaseRepository().Insert(entity);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into hr_department(");
                strSql.Append("deptname,manager,phone,managerphone)");
                strSql.Append(" values (");
                strSql.Append("@deptname,@manager,@phone,@managerphone)");
                strSql.Append(";select @@IDENTITY");

                DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@deptname",entity.deptname),
                    DbParameters.CreateDbParameter("@manager",entity.manager),
                    DbParameters.CreateDbParameter("@phone",entity.phone),
                    DbParameters.CreateDbParameter("@managerphone",entity.managerphone)
                };

                object obj = this.BaseRepository().FindObject(strSql.ToString(), parameter);
                if (obj != null)
                {
                    Entity.BaseManage.DepartmentEntity dent = new Entity.BaseManage.DepartmentEntity();
                    dent.DepartmentId = obj.ToString();
                    dent.OrganizeId = "207fa1a9-160c-4943-a89b-8fa4db0547ce";
                    dent.ParentId = "0";
                    dent.EnCode= obj.ToString();
                    dent.FullName = entity.deptname;
                    dent.OuterPhone = entity.managerphone;
                    dent.Manager = entity.manager;
                    BaseManage.DepartmentService dal = new BaseManage.DepartmentService();

                    dal.SavesForm("", dent);
                }


            }
        }

        #endregion 提交数据
    }
}