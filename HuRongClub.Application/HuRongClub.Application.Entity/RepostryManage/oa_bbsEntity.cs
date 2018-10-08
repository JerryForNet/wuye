using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-01 18:57
    /// 描 述：oa_bbs
    /// </summary>
    public class oa_bbsEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// bbsid
        /// </summary>
        /// <returns></returns>
        public int? bbsid { get; set; }
        /// <summary>
        /// signname
        /// </summary>
        /// <returns></returns>
        public string signname { get; set; }
        /// <summary>
        /// subject
        /// </summary>
        /// <returns></returns>
        public string subject { get; set; }
        /// <summary>
        /// content
        /// </summary>
        /// <returns></returns>
        public string content { get; set; }
        /// <summary>
        /// pdate
        /// </summary>
        /// <returns></returns>
        public DateTime? pdate { get; set; }
        /// <summary>
        /// reid
        /// </summary>
        /// <returns></returns>
        public int? reid { get; set; }
        /// <summary>
        /// readnum
        /// </summary>
        /// <returns></returns>
        public int? readnum { get; set; }
        #endregion

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
            this.bbsid = _id;
                                            }
        #endregion
    }
}