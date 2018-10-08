using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.OaManage;
using HuRongClub.Application.Entity.OaManage.ViewModel;
using HuRongClub.Application.IService.OaManage;
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

namespace HuRongClub.Application.Service.OaManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-26 16:05
    /// 描 述：工作周记
    /// </summary>
    public class WeekplanService : RepositoryFactory, WeekplanIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<WeekplanModel> GetPageList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<WeekplanModel> repository = new RepositoryFactory<WeekplanModel>();
            string UserName = OperatorProvider.Provider.Current().UserName; //获取当前用户名 根据用户名去查询是否在部门是管理身份
            string checkdep = OperatorProvider.Provider.Current().ManageDepartment; //Checkmanager(UserName); //是某个部门的管理者身份
            var strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();

            if (OperatorProvider.Provider.Current().IsSystem || OperatorProvider.Provider.Current().IsLeader == 1)
            {
                // 是管理者身份，全部
                strSql.Append(@"SELECT ow.*,u.TrueName AS TrueName,ow.ifcheck AS checks FROM  dbo.oa_weekplan AS ow
                                INNER JOIN dbo.Accounts_Users AS u  ON ow.userid=u.UserID 
                                WHERE 1=1 ");
            }
            else if (!string.IsNullOrEmpty(checkdep))
            {
                string[] checkdeps = checkdep.Split(',');
                string dep = "";
                foreach (var item in checkdeps)
                {
                    dep += "'" + item + "',";
                }
                if (!string.IsNullOrEmpty(dep))
                {
                    dep = dep.Substring(0, (dep.Length - 1));
                }

                // 部门的管理者身份
                string OldSystemUserID = OperatorProvider.Provider.Current().OldSystemUserID;
                strSql.Append(@" SELECT ow.*,u.TrueName AS TrueName,ow.ifcheck AS checks FROM dbo.oa_weekplan AS ow
                                INNER JOIN  dbo.Accounts_Users AS u ON ow.userid=u.UserID 
                                WHERE u.DepartmentID IN (" + dep + ") ");
            }
            else
            {
                //只显示自己的周报
                string UserId = OperatorProvider.Provider.Current().OldSystemUserID;
                strSql.Append(@"SELECT o.*,u.TrueName AS TrueName,o.ifcheck AS checks  
                                FROM  oa_weekplan o 
                                LEFT JOIN dbo.Accounts_Users u ON o.userid=u.UserID 
                                where o.userid='" + UserId + "'");
            }


            //查询条件 供应商名称、购买物品
            if (!queryParam["owner_name"].IsEmpty())
            {
                string name = queryParam["owner_name"].ToString();
                strSql.Append(" and TrueName like '%" + name + "%' ");
            }
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and ow.inputtime>='" + StartDate + "' ");
            }
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and ow.inputtime <='" + EndDate + "' ");
            }
            return repository.BaseRepository().FindList(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 判断是否管理员
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string Checkmanager(string username)
        {
            RepositoryFactory<WeekplanEntity> repository = new RepositoryFactory<WeekplanEntity>();
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT * FROM dbo.hr_department WHERE manager ='" + username + "' ");
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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<WeekplanEntity> GetList(string queryJson)
        {
            return null;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WeekplanEntity GetEntity(string keyValue)
        {
            RepositoryFactory<WeekplanEntity> repository = new RepositoryFactory<WeekplanEntity>();
            return repository.BaseRepository().FindEntity(keyValue);
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            RepositoryFactory<WeekplanEntity> repository = new RepositoryFactory<WeekplanEntity>();
            repository.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, WeekplanEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);

                entity.checktime = DateTime.Parse(DateTime.Now.ToString());
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.id = modifyid();//主键
                entity.ifcheck = "0"; //新增默认未批注
                entity.inputtime = DateTime.Parse(DateTime.Now.ToString()); //新增创建添加时间
                entity.userid = OperatorProvider.Provider.Current().OldSystemUserID; //获取当前登陆用户的id
                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// 获取主键id
        /// </summary>
        /// <returns></returns>
        public string modifyid()
        {
            var strSql = new StringBuilder();
            strSql.Append(@" select isnull(max(cast(id as int)),0)+1 from oa_weekplan ");
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