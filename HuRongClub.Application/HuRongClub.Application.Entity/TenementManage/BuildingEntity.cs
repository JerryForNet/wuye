using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-07 17:17
    /// 描 述：Building
    /// </summary>
    public class BuildingEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 楼栋编号
        /// </summary>
        /// <returns></returns>
        public string building_id { get; set; }
        /// <summary>
        /// 物业编号
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
        /// <summary>
        /// 楼栋名称
        /// </summary>
        /// <returns></returns>
        public string building_name { get; set; }
        /// <summary>
        /// 楼栋类型
        /// </summary>
        /// <returns></returns>
        public string building_type { get; set; }
        /// <summary>
        /// 楼层数
        /// </summary>
        /// <returns></returns>
        public int? floor_count { get; set; }
        /// <summary>
        /// 楼层名称
        /// </summary>
        /// <returns></returns>
        public string floor_list { get; set; }
        /// <summary>
        /// 房间编号
        /// </summary>
        /// <returns></returns>
        public string room_list { get; set; }
        /// <summary>
        /// X坐标
        /// </summary>
        /// <returns></returns>
        public int? map_x { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        /// <returns></returns>
        public int? map_y { get; set; }
        /// <summary>
        /// 效果图片
        /// </summary>
        /// <returns></returns>
        public string image { get; set; }
        /// <summary>
        /// 图片宽
        /// </summary>
        /// <returns></returns>
        public int? image_width { get; set; }
        /// <summary>
        /// 图片高
        /// </summary>
        /// <returns></returns>
        public int? image_high { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.building_id = ""; //Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.building_id = keyValue;
        }
        #endregion
    }
}