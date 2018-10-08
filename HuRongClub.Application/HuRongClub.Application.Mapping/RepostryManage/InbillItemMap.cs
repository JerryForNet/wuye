using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：彭长青
    /// 日 期：2017-04-26 13:42
    /// 描 述：入库汇总
    /// </summary>
    public class InbillItemMap : EntityTypeConfiguration<InbillItemEntity>
    {
        public InbillItemMap()
        {
            #region 表、主键

            //表
            this.ToTable("tb_wh_inbill_item");
            //主键
            this.HasKey(t => t.fitemid);

            #endregion 表、主键
        }
    }
}