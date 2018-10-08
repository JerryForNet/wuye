using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 房间实体
    /// </summary>
    [NotMapped]
    public class RoomModelEntity : RoomEntity
    {    
        #region 扩展字段

        /// <summary>
        /// 楼栋名称
        /// </summary>
        /// <returns></returns>
        public string building_name { get; set; }
        /// <summary>
        /// 业主名称
        /// </summary>
        /// <returns></returns>
        public string owner_name { get; set; }
        #endregion
    }
}
