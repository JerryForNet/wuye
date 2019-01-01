using System.ComponentModel.DataAnnotations.Schema;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    [NotMapped]
    public class billStatisticsModel : InbillItemEntity
    {
        /// <summary>
        /// 入库单编号
        /// </summary>
        /// public string finbillid { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public string findate { get; set; }

        /// <summary>
        /// 名称规格
        /// </summary>
        public string fname { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string fprovider { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string funit { get; set; }
    }
}