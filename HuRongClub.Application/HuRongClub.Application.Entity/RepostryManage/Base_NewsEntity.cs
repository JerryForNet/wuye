using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：李俊
    /// 日 期：2017-04-01 13:31
    /// 描 述：Base_News
    /// </summary>
    public class Base_NewsEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// NewsId
        /// </summary>
        /// <returns></returns>
        public string NewsId { get; set; }
        /// <summary>
        /// TypeId
        /// </summary>
        /// <returns></returns>
        public int? TypeId { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }
        /// <summary>
        /// FullHead
        /// </summary>
        /// <returns></returns>
        public string FullHead { get; set; }
        /// <summary>
        /// FullHeadColor
        /// </summary>
        /// <returns></returns>
        public string FullHeadColor { get; set; }
        /// <summary>
        /// BriefHead
        /// </summary>
        /// <returns></returns>
        public string BriefHead { get; set; }
        /// <summary>
        /// AuthorName
        /// </summary>
        /// <returns></returns>
        public string AuthorName { get; set; }
        /// <summary>
        /// CompileName
        /// </summary>
        /// <returns></returns>
        public string CompileName { get; set; }
        /// <summary>
        /// TagWord
        /// </summary>
        /// <returns></returns>
        public string TagWord { get; set; }
        /// <summary>
        /// Keyword
        /// </summary>
        /// <returns></returns>
        public string Keyword { get; set; }
        /// <summary>
        /// SourceName
        /// </summary>
        /// <returns></returns>
        public string SourceName { get; set; }
        /// <summary>
        /// SourceAddress
        /// </summary>
        /// <returns></returns>
        public string SourceAddress { get; set; }
        /// <summary>
        /// NewsContent
        /// </summary>
        /// <returns></returns>
        public string NewsContent { get; set; }
        /// <summary>
        /// PV
        /// </summary>
        /// <returns></returns>
        public int? PV { get; set; }
        /// <summary>
        /// ReleaseTime
        /// </summary>
        /// <returns></returns>
        public DateTime? ReleaseTime { get; set; }
        /// <summary>
        /// SortCode
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// DeleteMark
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// EnabledMark
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// ModifyUserId
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// ModifyUserName
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.NewsId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.NewsId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}