using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;
using System.Text;

namespace HuRongClub.Test
{
    [TestClass]
    public class PrintTest
    {

        [DllImport("C:\\Program Files (x86)\\税控盘组件接口\\NISEC_SKPC.dll", CharSet = CharSet.Ansi, EntryPoint = "OperateDisk")]
        public extern static bool OperateDisk(byte[] sInputInfo, byte[] sOutputInfo);


        [TestMethod]
        public void TestMethod1()
        {
            string res = GetFpregXml();
            string a = "";
        }

        /// <summary>
        /// 获取发票打印注册验证数据xml
        /// </summary>
        /// <returns></returns>
        private string GetFpregXml()
        {
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<?xml version=\"1.0\" encoding=\"gbk\"?>");
            strXML.AppendFormat("<business comment=\"注册码信息导入\" id=\"ZCMDR\">");
            strXML.AppendFormat("<body yylxdm=\"1\">");
            strXML.AppendFormat("<input>");
            strXML.AppendFormat("<zcmxx>{0}</zcmxx>", "1");
            strXML.AppendFormat("</input>");
            strXML.AppendFormat("</body>");
            strXML.AppendFormat("</business>");

            return strXML.ToString();
        }
    }
}
