using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-08 18:58
    /// 描 述：Inbill
    /// </summary>
    public class InbillMap : EntityTypeConfiguration<InbillEntity>
    {
        public InbillMap()
        {
            #region 表、主键
            //表
            this.ToTable("tb_wh_inbill");
            //主键
            this.HasKey(t => t.finbillid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
