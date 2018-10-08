using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-04-27 16:55
    /// 描 述：设备类型
    /// </summary>
    public class wy_device_typeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// typecode
        /// </summary>
        /// <returns></returns>
        public string typecode { get; set; }
        /// <summary>
        /// typename
        /// </summary>
        /// <returns></returns>
        public string typename { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.typecode = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.typecode = keyValue;
                                            }
        #endregion
    }
}