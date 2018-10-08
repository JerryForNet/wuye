using HuRongClub.Application.Entity.TenementManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-28 22:19
    /// 描 述：其他费用应收明细
    /// </summary>
    public class OtherincomeitemMap : EntityTypeConfiguration<OtherincomeitemEntity>
    {
        public OtherincomeitemMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_otherincomeitem");
            //主键
            this.HasKey(t => t.incomeitem_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
