using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Cache.Factory
{
    public class CacheKey
    {
        /// <summary>
        /// 收费统计
        /// </summary>
        /// <param name="userStatus"></param>
        /// <param name="floornum"></param>
        /// <param name="propertyId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="feeitem"></param>
        /// <returns></returns>
        public string Charge(string userStatus, string floornum, string propertyId, string year, string month, string feeitem)
        {
            return string.Format("chargelist_{0}_{1}_{2}_{3}_{4}_{5}", userStatus, floornum, propertyId, year, month, feeitem);
        }
    }
}
