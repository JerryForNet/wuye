using HuRongClub.Application.Entity.BaseManage;
using HuRongClub.Application.Entity.FinanceManage.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Mapping.FinanceManage
{
    public class InnerbillitemMap : EntityTypeConfiguration<InnerbillitemEntity>
    {
        public InnerbillitemMap()
        {
            #region 表、主键

            //表
            this.ToTable("wy_innerbillitem");
            //主键
            this.HasKey(t => t.billitem_id);

            #endregion 表、主键
        }
    }
}