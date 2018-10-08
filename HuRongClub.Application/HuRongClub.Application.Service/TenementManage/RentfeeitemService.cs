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
    /// 创 建：超级管理员
    /// 日 期：2017-04-21 09:28
    /// 描 述：Rentfeeitem
    /// </summary>
    public class RentfeeitemService : RepositoryFactory<RentfeeitemEntity>, RentfeeitemIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RentfeeitemEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RentfeeitemEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RentfeeitemEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="pos">位数 不够前面补0</param>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        public string GetMaxID(int pos,string property_id)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(right(itemid,8))+1 from wy_rentfeeitem");
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
            return property_id + str;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="contractid">租赁合同ID</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RentfeeitemListEntity> GetLists(string contractid)
        {
            RepositoryFactory<RentfeeitemListEntity> repository = new RepositoryFactory<RentfeeitemListEntity>();
            var StrSql = new StringBuilder();
            StrSql.Append("select rf.*,f.feeitem_name  ");
            StrSql.Append("FROM wy_rentfeeitem rf ");
            StrSql.Append("LEFT join wy_feeitem f on rf.feeitemid=f.feeitem_id  ");
            StrSql.Append("WHERE rf.contractid=@contractid ");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@contractid",contractid)
                };

            return repository.BaseRepository().FindList(StrSql.ToString(), parameter);
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
        /// <param name="property_id">物业编号</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public string SaveForm(string keyValue, string property_id, RentfeeitemEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                //entity.Create();
                entity.itemid = GetMaxID(8, property_id);
                this.BaseRepository().Insert(entity);
            }
            return entity.itemid;
        }
        #endregion
    }
}
