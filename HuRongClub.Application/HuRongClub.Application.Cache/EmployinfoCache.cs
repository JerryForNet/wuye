using HuRongClub.Application.Busines.PersonnelManage;
using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Cache.Factory;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HuRongClub.Application.Cache
{
    public class EmployinfoCache
    {
        private EmployinfoBLL busines = new EmployinfoBLL();

        /// <summary>
        /// 用户组列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployinfoEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<EmployinfoEntity>>(busines.cacheKey);
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
        /// 用户组列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public IEnumerable<EmployinfoEntity> GetList(int deptid)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(deptid.ToString()))
            {
                data = data.Where(t => t.deptid == deptid);
            }
            return data;
        }
    }
}