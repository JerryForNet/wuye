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
    /// 描 述：部门信息缓存
    /// </summary>
    public class DepartmentCache
    {
        private DepartmentBLL busines = new DepartmentBLL();

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<DepartmentEntity>>(busines.cacheKey);
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
        /// 部门列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetList(string organizeId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(organizeId))
            {
                data = data.Where(t => t.OrganizeId == organizeId);
            }
            return data;
        }

        public DepartmentEntity GetEntity(string departmentId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(departmentId))
            {
                var d = data.Where(t => t.DepartmentId == departmentId).ToList<DepartmentEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new DepartmentEntity();
        }
    }
}