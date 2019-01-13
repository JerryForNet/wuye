using HuRongClub.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Xml;

namespace HuRongClub.Test
{
    [TestClass]
    public class XmlHelp
    {
        [TestMethod]
        public void TestMethod1()
        {
            StringBuilder xmlHtml = new StringBuilder();
            xmlHtml.AppendFormat("<?xml version=\"1.0\" encoding=\"gbk\"?>");
            xmlHtml.AppendFormat("<business comment=\"发票开具\" id=\"FPKJ\" >");
            xmlHtml.AppendFormat("<body yylxdm=\"1\">");
            xmlHtml.AppendFormat("<output>");
            xmlHtml.AppendFormat("<fplxdm> 发票类型代码 </fplxdm>");
            xmlHtml.AppendFormat("<fpdm>发票代码</fpdm>");
            xmlHtml.AppendFormat("<fphm>发票号码</fphm>");
            xmlHtml.AppendFormat("<kprq>开票日期</kprq>");
            xmlHtml.AppendFormat("<hjje>合计金额</hjje>");
            xmlHtml.AppendFormat("<skm>税控码</skm>");
            xmlHtml.AppendFormat("<returncode>0</returncode>");
            xmlHtml.AppendFormat("<returnmsg>成功</returnmsg>");
            xmlHtml.AppendFormat("</output>");
            xmlHtml.AppendFormat("</body>");
            xmlHtml.AppendFormat("</business>");

            //string fpdm = XmlHelper.XmlAnalysis(@"business/body/output/fpdm", xmlHtml.ToString());


            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlHtml.ToString());


            foreach (XmlElement book in xmlDocument.SelectNodes(@"business/body/output/fpdm"))
            {
                // if you know attribute name simply use GetAttribute e.g.  
                //Console.WriteLine("id value: {0}.", book.GetAttribute("id"));
                //// if you don't know attribute names you can loop e.g.  
                //foreach (XmlAttribute attribute in book.Attributes)
                //{
                //    Console.WriteLine("attribute with name {0} has value {1}.", attribute.Name, attribute.Value);
                //}

                string a = book.ToString();
            }
        }
    }
}