using HuRongClub.Application.Busines.RepostryManage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace HuRongClub.Test.BLL
{
    [TestClass]
    public class FeereportBLLTest
    {
        private FeereportBLL bll = new FeereportBLL();

        [TestMethod]
        public void GetqianfeiDistrictListJson()
        {
            string queryJson = "{'YearNum': '2016','MonthNum': '12','FeeName': '100,110','propertyID': '0008'}";

            var data = bll.GetqianfeiDistrictListJson(queryJson);
        }
    }
}