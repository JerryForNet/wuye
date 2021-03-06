﻿using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 09:28
    /// 描 述：Rentcontract
    /// </summary>
    public class RentcontractMap : EntityTypeConfiguration<RentcontractEntity>
    {
        public RentcontractMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_rentcontract");
            //主键
            this.HasKey(t => t.contractid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
