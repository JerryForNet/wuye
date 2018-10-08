using HuRongClub.Application.Busines.SysManage;
using HuRongClub.Application.Entity.SysManage;
using HuRongClub.Cache.Factory;
using HuRongClub.Util.Extension;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HuRongClub.Application.Cache
{
    public class DictitemCache
    {
        private DictitemBLL busines = new DictitemBLL();

        /// <summary>
        /// 字典项中职级的列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DictitemEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<DictitemEntity>>(busines.cacheKey);
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
        /// 职级列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public IEnumerable<DictitemEntity> GetList(string dictid)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(dictid))
            {
                data = data.Where(t => t.dictid == dictid.ToInt());
            }
            return data;
        }

        public DictitemEntity GetEntity(string dictid)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(dictid.ToString()))
            {
                var d = data.Where(t => t.dictid.ToString() == dictid).ToList<DictitemEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new DictitemEntity();
        }
    }
}