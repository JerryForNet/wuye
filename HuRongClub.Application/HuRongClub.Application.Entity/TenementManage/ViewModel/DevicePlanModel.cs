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
    public class DevicePlanModel : wy_device_planEntity
    {
        /// <summary>
        /// maintencename
        /// </summary>
        /// <returns></returns>
        public string maintencename { get; set; }
        /// <summary>
        /// maintence
        /// </summary>
        /// <returns></returns>
        public string maintence { get; set; }

    }
}
