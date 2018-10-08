using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    /// <summary>
    /// 根据入库单号视图model
    /// </summary>
    [NotMapped]
    public class InbillItemByfinidModel : InbillItemEntity
    {
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string providerName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName { get; set; }
    }
}