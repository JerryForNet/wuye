using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-25 15:16
    /// 描 述：物品info信息
    /// </summary>
    public class GoodsinfoMap : EntityTypeConfiguration<GoodsinfoEntity>
    {
        public GoodsinfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("tb_wh_goodsinfo");
            //主键
            this.HasKey(t => t.fgoodsid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
