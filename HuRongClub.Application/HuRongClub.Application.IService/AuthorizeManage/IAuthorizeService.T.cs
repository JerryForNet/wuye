using HuRongClub.Application.Entity.AuthorizeManage;
using HuRongClub.Application.Entity.AuthorizeManage.ViewModel;
using HuRongClub.Application.Entity.BaseManage;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.IService.AuthorizeManage
{
    /// <summary>
    /// 版 本
    
    
    /// 日 期：2015.12.5 22:35
    /// 描 述：授权认证
    /// </summary>
    public interface IAuthorizeService<T>
    {
        IQueryable<T> IQueryable();
        IQueryable<T> IQueryable(Expression<Func<T, bool>> condition);
        IEnumerable<T> FindList(Pagination pagination);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination);
        IEnumerable<T> FindList(string strSql);
        IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter);
        IEnumerable<T> FindList(string strSql, Pagination pagination);
        IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination);
    }
}
