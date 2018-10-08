using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 业主列表
    /// </summary>
    public class OwnerListEntity
    {
        /// <summary>
        /// 业主编号
        /// </summary>
        public string owner_id { set; get; }
        /// <summary>
        /// 业主姓名
        /// </summary>
        public string owner_name { set; get; }
        /// <summary>
        /// 业主电话
        /// </summary>
        public string owner_tel { set; get; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string owner_cardno { set; get; }
        /// <summary>
        /// 房间编号
        /// </summary>
        public string room_id { set; get; }
        /// <summary>
        /// 入住日期
        /// </summary>
        public DateTime? in_date { set; get; }
        /// <summary>
        /// 操作用户
        /// </summary>
        public string sign_userid { set; get; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? sign_date { set; get; }
        /// <summary>
        /// 房间名称
        /// </summary>
        public string room_name { set; get; }
        /// <summary>
        /// 是否租赁
        /// </summary>
        public int? is_rent { set; get; }
        /// <summary>
        /// 是否业主入住
        /// </summary>
        public int? is_owner { set; get; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? building_dim { set; get; }
        /// <summary>
        /// 入住单元
        /// </summary>
        public string unitroom { set; get; }
    }
}
