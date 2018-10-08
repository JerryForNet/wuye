using HuRongClub.Application.Busines.PersonnelManage;
using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Cache.Factory;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HuRongClub.Application.Cache
{
    public class HrDepartmentCache
    {
        private HrDepartmentBLL busines = new HrDepartmentBLL();

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HrDepartmentEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<HrDepartmentEntity>>(busines.cacheKey);
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
        public IEnumerable<HrDepartmentEntity> GetList(string deptid)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(deptid.ToString()))
            {
                data = data.Where(t => t.deptid.ToString() == deptid);
            }
            return data;
        }

        public HrDepartmentEntity GetEntity(string deptid)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(deptid.ToString()))
            {
                var d = data.Where(t => t.deptid.ToString() == deptid).ToList<HrDepartmentEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new HrDepartmentEntity();
        }
    }
}