using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-24 18:47
    /// 描 述：物品领用
    /// </summary>
    public class OutbillMap : EntityTypeConfiguration<OutbillEntity>
    {
        public OutbillMap()
        {
            #region 表、主键

            //表
            this.ToTable("tb_wh_outbill");
            //主键
            this.HasKey(t => t.foutbillid);

            #endregion 表、主键
        }
    }
}