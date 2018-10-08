using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    public class ChargeModel
    {
        /// <summary>
        /// 室号
        /// </summary>
        public string room_name { get; set; }
        /// <summary>
        ///  客户名
        /// </summary>
        public string customername { get; set; }

        /// <summary>
        /// 楼栋名
        /// </summary>
        public string building_name { get; set; }

        public string customer { get; set; }

        /// <summary>
        /// 历年欠收
        /// </summary>
        public decimal lnq { get; set; }
        /// <summary>
        /// 上年欠收
        /// </summary>
        public decimal shnq { get; set; }
        /// <summary>
        /// 当年欠收
        /// </summary>
        public decimal dnq { get; set; }
        /// <summary>
        /// 当月应收
        /// </summary>
        public decimal dyysh { get; set; }
        /// <summary>
        /// 应收合计
        /// </summary>
        public decimal yshhj { get; set; }
        /// <summary>
        /// 历年欠收（实收）
        /// </summary>
        public decimal shlnq { get; set; }
        /// <summary>
        /// 上年欠收（实收）
        /// </summary>
        public decimal shshnq { get; set; }
        /// <summary>
        /// 当年欠收（实收）
        /// </summary>
        public decimal shdnq { get; set; }
        /// <summary>
        /// 当月实收
        /// </summary>
        public decimal shdy { get; set; }
        /// <summary>
        /// 预收金额
        /// </summary>
        public decimal ysh { get; set; }
        /// <summary>
        /// 实收合计
        /// </summary>
        public decimal shshhj { get; set; }
        /// <summary>
        /// 预收转入
        /// </summary>
        public decimal yshzhr { get; set; }
        /// <summary>
        /// 预收余额
        /// </summary>
        public decimal yshye { get; set; }
        /// <summary>
        /// 欠收合计
        /// </summary>
        public decimal qshhj { get; set; }

        /// <summary>
        /// 收上年欠
        /// </summary>
        public decimal shshyq { get; set; }

    }
}
