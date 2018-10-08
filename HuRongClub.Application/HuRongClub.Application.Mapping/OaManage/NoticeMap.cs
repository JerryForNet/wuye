using HuRongClub.Application.Entity.OaManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.OaManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-28 20:16
    /// 描 述：通知公告
    /// </summary>
    public class NoticeMap : EntityTypeConfiguration<NoticeEntity>
    {
        public NoticeMap()
        {
            #region 表、主键
            //表
            this.ToTable("oa_notice");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
