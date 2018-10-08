using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Cache.Factory;
using System.Collections.Generic;
using System.Linq;

namespace HuRongClub.Application.Cache
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2016.3.4 9:56
    /// 描 述：部门信息缓存
    /// </summary>
    public class GoodstypeCache
    {
        private GoodstypeBLL busines = new GoodstypeBLL();

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GoodstypeEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<GoodstypeEntity>>(busines.cacheKey);
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

        public GoodstypeEntity GetEntity(string ftypecode)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(ftypecode))
            {
                var d = data.Where(t => t.ftypecode == ftypecode).ToList<GoodstypeEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new GoodstypeEntity();
        }
    }
}