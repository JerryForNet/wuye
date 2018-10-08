using HuRongClub.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-25 15:16
    /// 描 述：物品info信息

    [NotMapped]
    public class GoodsinfoModel : GoodsinfoEntity
    {
        /// <summary>
        /// 多规格名称
        /// </summary>
        /// <returns></returns>

        public string fname1 { get; set; }
    }
}