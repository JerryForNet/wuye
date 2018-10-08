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
    /// 版 本 6.1

    /// 创 建：王菲
    /// 日 期：2017-04-01 13:28
    /// 描 述：物品类别
    /// </summary>
    public class GoodstypeService : RepositoryFactory, GoodstypeIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<GoodstypeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<GoodstypeEntity> repository = new RepositoryFactory<GoodstypeEntity>();
            return repository.BaseRepository().FindList(pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<GoodstypeEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<GoodstypeEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();

                #region 多条件查询

                switch (condition)
                {
                    case "ftypename":    //名称
                        expression = expression.And(t => t.ftypename.Contains(keyword));
                        break;

                    case "ftypecode":    //名称
                        expression = expression.And(t => t.ftypecode.Contains(keyword));
                        break;

                    default:
                        break;
                }

                #endregion 多条件查询
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<GoodstypeEntity> GetList()
        {
            RepositoryFactory<GoodstypeEntity> repository = new RepositoryFactory<GoodstypeEntity>();
            return repository.BaseRepository().IQueryable().ToList();
        }

        public IEnumerable<GoodstypeEntity> GetListJson(string fparentcode)
        {
            return this.BaseRepository().FindList<GoodstypeEntity>("select ftypecode,ftypename,frootid from tb_wh_goodstype where fparentcode='" + fparentcode + "'");
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public GoodstypeEntity GetEntity(string keyValue)
        {
            RepositoryFactory<GoodstypeEntity> repository = new RepositoryFactory<GoodstypeEntity>();
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
            this.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, GoodstypeEntity entity)
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