using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class YearModel :BaseEntity
    {
        /// <summary>
        /// ytext
        /// </summary>
        /// <returns></returns>
        public string ytext { get; set; }
        /// <summary>
        /// yvalue
        /// </summary>
        /// <returns></returns>
        public string yvalue { get; set; }

    }
}
