using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HuRongClub.Application.Busines.SystemManage;

namespace HuRongClub.Test.BLL
{
    [TestClass]
    public class CodeRuleTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            CodeRuleBLL bll = new CodeRuleBLL();
            string code = bll.GetBillCode("0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "10002");
        }
    }
}
