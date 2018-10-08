using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.IService.RepostryManage;
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

namespace HuRongClub.Application.Service.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-25 15:16
    /// 描 述：物品info信息
    /// </summary>
    public class GoodsinfoService : RepositoryFactory, GoodsinfoIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<GoodsinfoModel> GetPageList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<GoodsinfoModel> repository = new RepositoryFactory<GoodsinfoModel>();
            var strSql = new StringBuilder();
            strSql.Append(" select a.fgoodsid,a.funit,a.fprice,a.fcount,a.fmoney,a.fplace,a.fname,  ");
            strSql.Append(" fname1=b.ftypename+'/'+c.ftypename+'/'+a.fname from (select * from tb_wh_goodsinfo  ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();

            #region 查询

            //物品
            if (!queryParam["fee_good"].IsEmpty())
            {
                string fgoodsid = queryParam["fee_good"].ToString();
                strSql.Append("  where fgoodsid='" + fgoodsid + "' ");
            }
            //小类
            else if (!queryParam["fee_code"].IsEmpty())
            {
                string fgoodsid = queryParam["fee_code"].ToString();
                strSql.Append(" where left(fgoodsid,6)='" + fgoodsid + "' ");
            }
            //大类
            else if (!queryParam["building_id"].IsEmpty())
            {
                string fgoodsid = queryParam["building_id"].ToString();
                strSql.Append(" where  left(fgoodsid,3)='" + fgoodsid + "' ");
            }
            else
            {
                strSql.Append(" where  fgoodsid<>''  ");
            }


            if (!queryParam["keyword"].IsEmpty())
            {
                string fname = queryParam["keyword"].ToString().Trim();
                if (!string.IsNullOrEmpty(fname)) 
                {
                    strSql.Append("   and (fname like '%" + fname + "%' OR fgoodsid LIKE '%" + fname + "%')");
                }
            }

            #endregion 查询

            strSql.Append(" ) a left join tb_wh_goodstype b on b.ftypecode=left(a.fgoodsid,3)  left join tb_wh_goodstype c on c.ftypecode=left(a.fgoodsid,6)  ");
            if (pagination == null)
            {
                return repository.BaseRepository().FindList(strSql.ToString()).OrderBy(t => t.fgoodsid);
            }
            else
            {
                return repository.BaseRepository().FindList(strSql.ToString(), pagination);
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<GoodsinfoEntity> GetList(string queryJson)
        {
            return this.BaseRepository().FindList<GoodsinfoEntity>("select fgoodsid,fname from tb_wh_goodsinfo where fgoodsid like '%" + queryJson + "%' ");
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public GoodsinfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<GoodsinfoEntity>(keyValue);
        }
        /// <summary>
        /// 获取列表--根据领用主表编号
        /// </summary>
        /// <param name="fgoodsids">领用主表编号</param>
        /// <returns>返回列表</returns>
        public IEnumerable<GoodsinfoEntity> GetLists(string fgoodsids)
        {
            var expression = LinqExtensions.True<GoodsinfoEntity>();
            string[] fg = fgoodsids.Split(',');
            expression = expression.And(t => fg.Contains(t.fgoodsid));
            return this.BaseRepository().FindList<GoodsinfoEntity>(expression);
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="pos">位数 不够前面补0</param>
        /// <param name="ftypecode">类型编号</param>
        /// <returns></returns>
        public string GetMaxID(int pos, string ftypecode)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(right(fgoodsid,3))+1 from tb_wh_goodsinfo where fgoodsid LIKE '"+ ftypecode + "%'");
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
        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            RepositoryFactory<GoodsinfoEntity> repository = new RepositoryFactory<GoodsinfoEntity>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                string str = "delete tb_wh_goodsinfo where fcount=0 and fgoodsid='" + keyValue + "'";
                repository.BaseRepository().ExecuteBySql(str);
            }
            
            //repository.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="ftypecode">类型编号</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, string ftypecode, GoodsinfoEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                GoodsinfoEntity ent = GetEntity(keyValue);
                ent.fmincount = entity.fmincount;
                ent.fplace = entity.fplace;
                ent.funit = entity.funit;
                ent.fmaxcount = entity.fmaxcount;
                ent.fname = entity.fname;
                ent.fgoodsid = keyValue;

                this.BaseRepository().Update(ent);
            }
            else
            {
                entity.fcount = 0;
                entity.fprice = 0;
                entity.fmoney = 0;
                entity.fuserid = OperatorProvider.Provider.Current().OldSystemUserID.ToInt();
                entity.finputdate = DateTime.Now;
                entity.fgoodsid = ftypecode + "-" + GetMaxID(3, ftypecode);

                this.BaseRepository().Insert(entity);
            }
        }

        #endregion 提交数据
    }
}