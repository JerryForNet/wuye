using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-05-12 18:34
    /// 描 述：wy_device_contract
    /// </summary>
    public class wy_device_contractEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// pkeyid
        /// </summary>
        /// <returns></returns>
        public string pkeyid { get; set; }
        /// <summary>
        /// contractname
        /// </summary>
        /// <returns></returns>
        public string contractname { get; set; }
        /// <summary>
        /// fixcompany
        /// </summary>
        /// <returns></returns>
        public string fixcompany { get; set; }
        /// <summary>
        /// devicenumber
        /// </summary>
        /// <returns></returns>
        public string devicenumber { get; set; }
        /// <summary>
        /// linkman
        /// </summary>
        /// <returns></returns>
        public string linkman { get; set; }
        /// <summary>
        /// linkphone
        /// </summary>
        /// <returns></returns>
        public string linkphone { get; set; }
        /// <summary>
        /// begindate
        /// </summary>
        /// <returns></returns>
        public DateTime? begindate { get; set; }
        /// <summary>
        /// enddate
        /// </summary>
        /// <returns></returns>
        public DateTime? enddate { get; set; }
        /// <summary>
        /// signdate
        /// </summary>
        /// <returns></returns>
        public DateTime? signdate { get; set; }
        /// <summary>
        /// cmoney
        /// </summary>
        /// <returns></returns>
        public double? cmoney { get; set; }
        /// <summary>
        /// fnotes
        /// </summary>
        /// <returns></returns>
        public string fnotes { get; set; }
        /// <summary>
        /// attfile
        /// </summary>
        /// <returns></returns>
        public string attfile { get; set; }
        /// <summary>
        /// contractnumber
        /// </summary>
        /// <returns></returns>
        public string contractnumber { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.pkeyid = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.pkeyid = keyValue;
                                            }
        #endregion
    }
}