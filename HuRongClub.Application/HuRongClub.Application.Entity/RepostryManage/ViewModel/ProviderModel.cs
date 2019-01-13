using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    /// <summary>
    /// 页面model
    /// </summary>
    [NotMapped]
    public class ProviderModel : ProviderEntity
    {
        /// <summary>
        /// 最后购买时间
        /// </summary>
        public DateTime? lastbuy { get; set; }

        /// <summary>
        /// 购买商品
        /// </summary>
        public string goodsinfo { get; set; }

        /// <summary>
        /// 最后评价
        /// </summary>
        public string pstar { get; set; }

        /// <summary>
        /// 最后评价人
        /// </summary>
        public string puser { get; set; }
    }
}