using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-10 15:27
    /// 描 述：wy_room
    /// </summary>
    public class RoomMap : EntityTypeConfiguration<RoomEntity>
    {
        public RoomMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_room");
            //主键
            this.HasKey(t => t.room_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
