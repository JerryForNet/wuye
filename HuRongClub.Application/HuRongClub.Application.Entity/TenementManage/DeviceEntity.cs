using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:47
    /// 描 述：Device
    /// </summary>
    public class DeviceEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// d_number
        /// </summary>
        /// <returns></returns>
        public string d_number { get; set; }
        /// <summary>
        /// d_name
        /// </summary>
        /// <returns></returns>
        public string d_name { get; set; }
        /// <summary>
        /// d_standard
        /// </summary>
        /// <returns></returns>
        public string d_standard { get; set; }
        /// <summary>
        /// propertyid
        /// </summary>
        /// <returns></returns>
        public string propertyid { get; set; }
        /// <summary>
        /// d_typecode
        /// </summary>
        /// <returns></returns>
        public string d_typecode { get; set; }
        /// <summary>
        /// d_descript
        /// </summary>
        /// <returns></returns>
        public string d_descript { get; set; }
        /// <summary>
        /// d_place
        /// </summary>
        /// <returns></returns>
        public string d_place { get; set; }
        /// <summary>
        /// d_maker
        /// </summary>
        /// <returns></returns>
        public string d_maker { get; set; }
        /// <summary>
        /// d_usedate
        /// </summary>
        /// <returns></returns>
        public string d_usedate { get; set; }
        /// <summary>
        /// d_money
        /// </summary>
        /// <returns></returns>
        public string d_money { get; set; }
        /// <summary>
        /// d_origionnumber
        /// </summary>
        /// <returns></returns>
        public string d_origionnumber { get; set; }
        /// <summary>
        /// d_makeplace
        /// </summary>
        /// <returns></returns>
        public string d_makeplace { get; set; }
        /// <summary>
        /// d_contractman
        /// </summary>
        /// <returns></returns>
        public string d_contractman { get; set; }
        /// <summary>
        /// d_contractphone
        /// </summary>
        /// <returns></returns>
        public string d_contractphone { get; set; }
        /// <summary>
        /// d_contractaddress
        /// </summary>
        /// <returns></returns>
        public string d_contractaddress { get; set; }
        /// <summary>
        /// d_id
        /// </summary>
        /// <returns></returns>
        public string d_id { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.d_number = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.d_number = keyValue;
                                            }
        #endregion
    }
}