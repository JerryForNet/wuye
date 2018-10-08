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
    public class DeviceModel : DeviceEntity
    {
        /// <summary>
        /// 物业名称
        /// </summary>
        public string propertyname { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string typename { get; set; }  
        /// <summary>
        /// 设备名称
        /// </summary>
        public string mechinename { get; set; }

    }
}
