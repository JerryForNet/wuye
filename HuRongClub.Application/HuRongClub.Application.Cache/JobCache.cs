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
    /// 描 述：职位信息缓存
    /// </summary>
    public class JobCache
    {
        private JobBLL busines = new JobBLL();

        /// <summary>
        /// 职位列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<RoleEntity>>(busines.cacheKey);
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
        /// 职位列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList(string organizeId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(organizeId))
            {
                data = data.Where(t => t.OrganizeId == organizeId);
            }
            return data;
        }
    }
}