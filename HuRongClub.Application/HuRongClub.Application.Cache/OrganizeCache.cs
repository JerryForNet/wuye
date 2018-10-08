using HuRongClub.Application.Busines.BaseManage;
using HuRongClub.Application.Entity.BaseManage;
using HuRongClub.Cache.Factory;
using System.Collections.Generic;
using System.Linq;

namespace HuRongClub.Application.Cache
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2016.3.4 9:56
    /// 描 述：组织架构缓存
    /// </summary>
    public class OrganizeCache
    {
        private OrganizeBLL busines = new OrganizeBLL();

        /// <summary>
        /// 组织列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrganizeEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<OrganizeEntity>>(busines.cacheKey);
            if (cacheList == null)
            {
                var data = busines.GetList();
                CacheFactory.Cache().WriteCache(data, busines.cacheKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }

        /// <summary>
        /// 组织列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public OrganizeEntity GetEntity(string organizeId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(organizeId))
            {
                var d = data.Where(t => t.OrganizeId == organizeId).ToList<OrganizeEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new OrganizeEntity();
        }
    }
}