using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本 6.1
    /// 创 建：姜良福
    /// 日 期：2017-04-01 10:09
    /// 描 述：产业档案
    /// </summary>
    public class PropertyEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// 物业编号
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }

        /// <summary>
        /// 物业名称
        /// </summary>
        /// <returns></returns>
        public string property_name { get; set; }

        /// <summary>
        /// 物业地址
        /// </summary>
        /// <returns></returns>
        public string property_address { get; set; }

        /// <summary>
        /// 物业信息
        /// </summary>
        /// <returns></returns>
        public string property_info { get; set; }

        /// <summary>
        /// 物业平面图
        /// </summary>
        /// <returns></returns>
        public string image { get; set; }

        /// <summary>
        /// 图宽度
        /// </summary>
        /// <returns></returns>
        public decimal? image_width { get; set; }

        /// <summary>
        /// 图高度
        /// </summary>
        /// <returns></returns>
        public decimal? image_height { get; set; }

        #endregion 实体成员

        #region 扩展操作

        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.property_id = keyValue;
        }

        #endregion 扩展操作
    }
}