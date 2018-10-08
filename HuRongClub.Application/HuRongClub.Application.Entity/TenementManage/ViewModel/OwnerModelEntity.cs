using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 业主扩展实体
    /// </summary>
    [NotMapped]
    public class OwnerModelEntity:OwnerEntity
    {
        /// <summary>
        /// 物业名称
        /// </summary>
        public string property_name { set; get; }
        /// <summary>
        /// 入住房间单元
        /// </summary>
        public string room_names { set; get; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? building_dim { set; get; }
        /// <summary>
        /// 房间使用面积
        /// </summary>
        public decimal? room_dim { set; get; }
    }
}
