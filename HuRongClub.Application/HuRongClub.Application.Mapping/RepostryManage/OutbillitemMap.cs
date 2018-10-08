using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-25 11:47
    /// 描 述：领用单对应物品信息
    /// </summary>
    public class OutbillitemMap : EntityTypeConfiguration<OutbillitemEntity>
    {
        public OutbillitemMap()
        {
            #region 表、主键

            //表
            this.ToTable("tb_wh_outbill_item");
            //主键
            this.HasKey(t => t.fitemid);

            #endregion 表、主键
        }
    }
}