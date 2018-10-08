using System;
using System.Data.Entity.Infrastructure;

namespace HuRongClub.Data.EF
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2016.04.07
    /// 描 述：数据库连接接口
    /// </summary>
    public interface IDbContext : IDisposable, IObjectContextAdapter
    {
    }
}