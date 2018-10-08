using HuRongClub.Application.Entity.BaseManage;
using HuRongClub.Application.Entity.SysManage;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HuRongClub.Test.EF
{
    [TestClass]
    public class EF
    {
        private RepositoryFactory dao = new RepositoryFactory();

        [TestMethod]
        public void GetRoomEntity()
        {
            var entity = dao.BaseRepository().FindList<RoomEntity>(w => true);
        }

        [TestMethod]
        public void InsertUserBatch()
        {
            for (int i = 0; i < 10000; i++)
            {
                dao.BaseRepository().Insert<UserEntity>(new UserEntity()
                {
                    UserId = Guid.NewGuid().ToString(),
                    EnCode = "1042",
                    Account = "李俊",
                    Password = "123456",
                    RealName = "李俊",
                    CreateDate = DateTime.Now
                });
            }
        }
    }
}