using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-10 15:27
    /// 描 述：wy_room
    /// </summary>
    public class RoomEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 房间编号
        /// </summary>
        /// <returns></returns>
        public string room_id { get; set; }
        /// <summary>
        /// 所属楼栋编号
        /// </summary>
        /// <returns></returns>
        public string building_id { get; set; }
        /// <summary>
        /// 业主编号
        /// </summary>
        /// <returns></returns>
        public string owner_id { get; set; }
        /// <summary>
        /// 所属楼层
        /// </summary>
        /// <returns></returns>
        public int? floor_number { get; set; }
        /// <summary>
        /// 房间名称
        /// </summary>
        /// <returns></returns>
        public string room_name { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        /// <returns></returns>
        public int? room_number { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        /// <returns></returns>
        public decimal? building_dim { get; set; }
        /// <summary>
        /// 房间使用面积
        /// </summary>
        /// <returns></returns>
        public decimal? room_dim { get; set; }
        /// <summary>
        /// 单元合并
        /// </summary>
        /// <returns></returns>
        public int? rowspan { get; set; }
        /// <summary>
        /// 楼层合并
        /// </summary>
        /// <returns></returns>
        public int? colspan { get; set; }
        /// <summary>
        /// 交房日期
        /// </summary>
        /// <returns></returns>
        public DateTime? jf_date { get; set; }
        /// <summary>
        /// 维修基金
        /// </summary>
        /// <returns></returns>
        public decimal? repair_price { get; set; }
        /// <summary>
        /// 是否业主入住
        /// </summary>
        /// <returns></returns>
        public Int16? is_owner { get; set; }
        /// <summary>
        /// 是否租赁
        /// </summary>
        /// <returns></returns>
        public Int16? is_rent { get; set; }
        /// <summary>
        /// 物业编号
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
        /// <summary>
        /// 房型
        /// </summary>
        /// <returns></returns>
        public string room_model { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.room_id = "";// Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.room_id = keyValue;
        }
        #endregion        
    }
}
