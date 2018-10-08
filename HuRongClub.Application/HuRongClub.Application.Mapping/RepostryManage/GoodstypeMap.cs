using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// 版 本
    
    /// 创 建：王菲
    /// 日 期：2017-04-01 13:28
    /// 描 述：物品类别
    /// </summary>
    public class GoodstypeMap : EntityTypeConfiguration<GoodstypeEntity>
    {
        public GoodstypeMap()
        {
            #region 表、主键
            //表
            this.ToTable("tb_wh_goodstype");
            //主键
            this.HasKey(t => t.ftypecode);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
