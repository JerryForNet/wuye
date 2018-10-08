using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 10:39
    /// 描 述：Feereceive
    /// </summary>
    public class FeereceiveService : RepositoryFactory<FeereceiveEntity>, FeereceiveIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeereceiveEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeereceiveEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeereceiveEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeereceiveEntity GetEntityByTicket(string keyValue)
        {
            return this.BaseRepository().FindEntity(w => w.ticket_id == keyValue);
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="pos">位数 不够前面补0</param>
        /// <returns></returns>
        public string GetMaxID(int pos)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(right(receive_id,8))+1 from wy_feereceive");
            string str = "1";
            object obj = this.BaseRepository().FindObject(strSql.ToString());
            if (obj != null)
            {
                str = obj.ToString();
            }
            if (str.Length < pos)
            {
                int leng = str.Length;
                for (int i = 0; i < (pos - leng); i++)
                {
                    str = "0" + str;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="ticket_id">发票编号</param>
        /// <returns></returns>
        public string Getreceive_id(string ticket_id)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 receive_id from wy_feereceive where ticket_id=@ticket_id ");
            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@ticket_id",ticket_id)
                };
            object obj = this.BaseRepository().FindObject(strSql.ToString(), parameter);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        #endregion

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
        public void SaveForm(string keyValue, FeereceiveEntity entity)
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

        /// <summary>
        /// 发票作废
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="ticketid"></param>
        /// <param name="operate"></param>
        /// <returns></returns>
        public int ToVoidForm(int flag, string ticketid, string operate)
        {
            string str = "DECLARE @p1 INT SET @p1=1 EXEC TicketHandle @flag=@p1 output,@ticketid=@ticket_id,@operate=@UserName SELECT @p1";
            DbParameter[] parameter ={
                     DbParameters.CreateDbParameter("@ticket_id",ticketid),
                     DbParameters.CreateDbParameter("@UserName",operate)
                 };
            return this.BaseRepository().ExecuteBySql(str, parameter);
        }

        #endregion
    }
}