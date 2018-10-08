using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-05-02 18:41
    /// 描 述：Paydetail
    /// </summary>
    public class PaydetailService : RepositoryFactory<PaydetailEntity>, PaydetailIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PaydetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }

        /// <summary>
        /// 获取执行查询的sql'
        /// </summary>
        /// <param name="queryJson"></param>
        /// <param name="keyValue"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private string getListSql(string queryJson, string keyValue, List<DbParameter> parameter)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select hp.Id,hp.empid,he.empname,hd.deptname,hp.payrollid,hpi.dispName,hp.amount 
                            from hr_paydetail hp 
                            inner join hr_employinfo he on hp.empid = he.empid 
                            inner join hr_department hd  on he.deptid = hd.deptid 
                            left join hr_payitem hpi on hp.itemcode = hpi.itemcode 
                            where 1=1 ");

            if (queryJson != null)
            {
                var queryParam = queryJson.ToJObject();

                string seach = queryParam["keyword"] == null ? "" : queryParam["keyword"].ToString();
                if (seach != "")
                {
                    strSql.Append(" and empname like @keyword or deptname like @keyword");
                }
                parameter.Add(DbParameters.CreateDbParameter("@keyword", "%" + seach + "%"));
            }

            if (keyValue != null && keyValue != "")
            {
                strSql.Append(" and hp.payrollid = " + keyValue);
            }
            parameter.Add(DbParameters.CreateDbParameter("@keyValue", keyValue));
            

            return strSql.ToString();
        }

        /// <summary>
        /// 获取datatable
        /// </summary>
        /// <param name="queryJson"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public DataTable GetPageListToTable(string queryJson, string keyValue)
        {
            List<DbParameter> parameter = new List<DbParameter>();
            string strSql = getListSql(queryJson, keyValue, parameter);

            RepositoryFactory<PayDetailListEntity> repository = new RepositoryFactory<PayDetailListEntity>();

            return repository.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<PaydetailEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public PaydetailEntity GetEntity(string keyValue)
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
        /// 根据人员编号删除
        /// </summary>
        /// <param name="empid"></param>
        public void Delete(int empid, int payrollid)
        {
            this.BaseRepository().Delete(w => w.empid == empid && w.payrollid == payrollid);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, PaydetailEntity entity)
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