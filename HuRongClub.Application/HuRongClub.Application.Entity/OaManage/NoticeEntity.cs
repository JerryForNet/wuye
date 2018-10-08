using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.OaManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-28 20:16
    /// 描 述：通知公告
    /// </summary>
    public class NoticeEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// id
        /// </summary>
        /// <returns></returns>
        public int? id { get; set; }

        /// <summary>
        /// 类型：通知，公告
        /// </summary>
        /// <returns></returns>
        public string notice_type { get; set; }

        /// <summary>
        /// title
        /// </summary>
        /// <returns></returns>
        public string title { get; set; }

        /// <summary>
        /// author
        /// </summary>
        /// <returns></returns>
        public string author { get; set; }

        /// <summary>
        /// create_time
        /// </summary>
        /// <returns></returns>
        public DateTime? create_time { get; set; }

        /// <summary>
        /// contents
        /// </summary>
        /// <returns></returns>
        public string contents { get; set; }

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
            int _id = 0;
            int.TryParse(keyValue, out _id);
            this.id = _id;
        }

        #endregion 扩展操作
    }
}