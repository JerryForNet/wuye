using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

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
        public void TestForEach()
        {
            List<string> testList = new List<string>();
            foreach (var item in testList)
            {
                string t = item;
            }
        }

        /// <summary>
        /// 税控插件
        /// </summary>
        /// <param name="sInputInfo"></param>
        /// <param name="sOutputInfo"></param>
        /// <returns></returns>
        [DllImport("C:\\Program Files (x86)\\税控盘组件接口\\NISEC_SKPC.dll", CharSet = CharSet.Ansi, EntryPoint = "OperateDisk")]
        public extern static bool OperateDisk(byte[] sInputInfo, byte[] sOutputInfo);

        [TestMethod]
        public void testprint()
        {
            #region//购票信息查询

            string strReturnValue = "";

            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<?xml version=\"1.0\" encoding=\"gbk\"?>");
            strXML.AppendFormat("<business comment\"发票开具\" id=\"FPKJ\">");
            strXML.AppendFormat("<body yylxdm=\"1\">");
            strXML.AppendFormat("<input>");
            strXML.AppendFormat("<skpbh>税控盘编号</skpbh>");       
            strXML.AppendFormat("<skpkl>税控盘口令</skpkl>");      
            strXML.AppendFormat("<keypwd>数字证书密码</keypwd>"); 
            strXML.AppendFormat("<fplxdm>发票类型代码</fplxdm>"); 
            strXML.AppendFormat("<kplx>开票类型</kplx>");
            strXML.AppendFormat("<tspz>特殊票种标识<tspz>");
            strXML.AppendFormat("<xhdwsbh>销货单位识别号</xhdwsbh>");
            strXML.AppendFormat("<xhdwmc>销货单位名称</xhdwmc>");
            strXML.AppendFormat("<xhdwdzdh>销货单位地址电话</xhdwdzdh>");
            strXML.AppendFormat("<xhdwyhzh>销货单位银行帐号</xhdwyhzh>");
            strXML.AppendFormat("<ghdwsbh>购货单位识别号</ghdwsbh>");
            strXML.AppendFormat("<ghdwmc>购货单位名称</ghdwmc>");
            strXML.AppendFormat("<ghdwdzdh>购货单位地址电话</ghdwdzdh>");
            strXML.AppendFormat("<ghdwyhzh>购货单位银行帐号</ghdwyhzh>");
            strXML.AppendFormat("<bmbbbh>编码表版本号</bmbbbh>");
            strXML.AppendFormat("<hsslbs>含税税率标识</hsslbs>");
            strXML.AppendFormat("<fyxm count=\"1\">");
            strXML.AppendFormat("<group xh=\"1\">");
            strXML.AppendFormat("<fphxz>发票行性质</fphxz>");
            strXML.AppendFormat("<spmc>商品名称</spmc>");
            strXML.AppendFormat("<je>金额</je>");
            strXML.AppendFormat("<kcje>扣除金额</kcje>");
            strXML.AppendFormat("<sl>税率</sl>");
            strXML.AppendFormat("<se>税额</se>");
            strXML.AppendFormat("<hsbz>含税标志</hsbz>");
            strXML.AppendFormat("<spbm>商品编码</spbm>");
            strXML.AppendFormat("<zxbm>纳税人自行编码</zxbm>");
            strXML.AppendFormat("<yhzcbs>优惠政策标识</yhzcbs>");
            strXML.AppendFormat("<slbs>0税率标识</slbs>");
            strXML.AppendFormat("<zzstsgl>增值税特殊管理</zzstsgl>");
            strXML.AppendFormat("</group>");
            strXML.AppendFormat("</fyxm>");
            strXML.AppendFormat("<zhsl></zhsl>");
            strXML.AppendFormat("<hjje>合计金额</hjje>");
            strXML.AppendFormat("<hjse>合计税额</hjse>");
            strXML.AppendFormat("<jshj>价税合计</jshj>");
            strXML.AppendFormat("<bz>备注</bz>");
            strXML.AppendFormat("<skr>收款人</skr>");
            strXML.AppendFormat("<fhr>复核人</fhr>");
            strXML.AppendFormat("<kpr>开票人</kpr>");
            strXML.AppendFormat("<qdbz>清单标志</qdbz>");
            strXML.AppendFormat("<ssyf>所属月份</ssyf>");
            strXML.AppendFormat("<kpjh>开票机号</kpjh>");
            strXML.AppendFormat("<qmcs>签名参数</qmcs>");
            strXML.AppendFormat("</input>");
            strXML.AppendFormat("</body>");
            strXML.AppendFormat("</business>");

            byte[] bOutputInfo = new byte[1024 * 10];
            OperateDisk(Encoding.Default.GetBytes(strXML.ToString()), bOutputInfo);

            int intCount = 0;
            for (int i = 0; i < bOutputInfo.Length; i++)
            {
                if (bOutputInfo[i] == 0)
                {
                    intCount = i;
                    break;
                }
            }
            strReturnValue = Encoding.Default.GetString(bOutputInfo, 0, intCount);

            #endregion
        }


        
    }
}