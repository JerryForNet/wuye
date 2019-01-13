using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace HuRongClub.Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> list = new List<string>();
            list.Add("asdfasd");

            string jsonStr = JsonConvert.SerializeObject(list);

        }

        [TestMethod]
        public void TestForEach() {
            List<string> testList = new List<string>();
            foreach (var item in testList)
            {
                string t = item;
            }
        }
    }
}
