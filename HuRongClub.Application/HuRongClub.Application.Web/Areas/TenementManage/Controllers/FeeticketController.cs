using HuRongClub.Application.Busines.FinanceManage;
using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HuRongClub.Util.Log;
using HuRongClub.Application.Entity.TenementManage.ViewModel;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:08
    /// 描 述：Feeticket
    /// </summary>
    [HandlerOperateLog]
    public class FeeticketController : MvcControllerBase
    {
        /// <summary>
        /// 税控插件
        /// </summary>
        /// <param name="sInputInfo"></param>
        /// <param name="sOutputInfo"></param>
        /// <returns></returns>
       // [DllImport("C:\\Program Files (x86)\\税控盘组件接口\\NISEC_SKPC.dll", CharSet = CharSet.Ansi, EntryPoint = "OperateDisk")]
        //public extern static bool OperateDisk(byte[] sInputInfo, byte[] sOutputInfo);

        private FeeticketBLL feeticketbll = new FeeticketBLL();
        private PtInvoiceBLL ptinvoicebll = new PtInvoiceBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FeeticketIndex()
        {
            return View();
        }

        /// <summary>
        /// 费用跳转明细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FeeListView()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FeeticketForm()
        {
            return View();
        }

        #region 财务管理--发票管理

        /// <summary>
        /// 发票管理列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            //string username = OperatorProvider.Provider.Current().Account.ToLower();
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                ViewBag.Issystem = 1;
            }
            else
            {
                ViewBag.Issystem = 0;
            }
            return View();
        }

        /// <summary>
        /// 发票管理表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 打印发票
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintFrom(string keyValue)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            OptionBLL optionbll = new OptionBLL();
            var data = optionbll.GetEntity(property_id);
            if (data != null)
            {
                ViewBag.option_exchange = string.Format("{0:N2}", data.option_exchange);
            }

            DateTime time = DateTime.Now;
            ViewBag.year = time.Year.ToString();

            ViewBag.month = (time.Month < 10 ? ("0" + time.Month.ToString()) : time.Month.ToString());
            ViewBag.day = (time.Day < 10 ? ("0" + time.Day.ToString()) : time.Day.ToString());

            //取页面参数ticketid
            string ticketid = keyValue;

            ViewBag.fpregxml = GetFpregXml();
            ViewBag.fpkjxml = GetFpkjXml(ticketid);
            ViewBag.fpprintxml = GetFpprintXml();


            return View();
        }

        #endregion 财务管理--发票管理

        #endregion 视图功能

        #region 获取数据

        /// <summary>
        /// 获取票据管理列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = feeticketbll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = feeticketbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = feeticketbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 主键值
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCostByTicketId(string keyValue)
        {
            var data = feeticketbll.GetCostByTicketId(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetOherIncomeList(string keyValue, string queryJson)
        {
            var data = feeticketbll.GetOherIncomeList(keyValue, queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="status">发票状态</param>
        /// <param name="type">发票类别</param>
        /// <returns>返回列表</returns>
        [HttpGet]
        public ActionResult GetSelList(int status, string type)
        {
            var data = feeticketbll.GetSelList(OperatorProvider.Provider.Current().DepartmentId, type, status);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPrintListJson(string keyValue, string queryJson)
        {
            var data = feeticketbll.GetPrintListJson(keyValue, queryJson);
            return ToJsonResult(data);
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Dimission(string keyValue)
        {
            var data = feeticketbll.GetListByIds(keyValue);
            if (data != null && data.ToList().Count > 0)
            {
                foreach (FeeticketEntity ent in data)
                {
                    if (ent.ticket_status == 2)
                    {
                        return Error("发票已归档，不能进行启用操作！");
                    }
                }
                //发票状态为1是已使用
                feeticketbll.UpdateState(keyValue, 0);
                return Success("发票启用成功");
            }
            else
            {
                return Error("操作失败，请刷新页面再操作！");
            }
        }

        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Dimissireturn(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //发票状态为2是已归档
                feeticketbll.UpdateState(keyValue, 2);
                return Success("发票归档成功");
            }
            else
            {
                return Error("请选择归档发票！");
            }
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Dimissiout(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //发票状态为100是作废发票
                feeticketbll.UpdateState(keyValue, 100);
                return Success("发票作废成功");
            }
            else
            {
                return Error("请选择归档发票！");
            }
        }

        /// <summary>
        /// 退领数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            FeeticketEntity ent = feeticketbll.GetEntity(keyValue);
            if (ent != null)
            {
                if (ent.ticket_status == 2)
                {
                    return Error("发票已归档，不能进行退领操作！");
                }
                else
                {
                    //发票状态为1是已使用
                    feeticketbll.RemoveForm(keyValue);
                    return Success("发票退领成功");
                }
            }
            else
            {
                return Error("操作失败，请刷新页面再操作！");
            }
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="codeend"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, FeeticketEntity entity, int codeend)
        {
            int codebegin = Convert.ToInt32(entity.ticket_code);

            // 设置前面几个零
            string temp = string.Empty;
            if (entity.ticket_code.Length > codebegin.ToString().Length)
            {
                for (int i = 0; i < (entity.ticket_code.Length - codebegin.ToString().Length); i++)
                {
                    temp += "0";
                }
            }

            // 逐条根据编码区间, 循环添加每一个
            for (int i = codebegin; i <= codeend; i++)
            {
                entity.ticket_id = entity.ticket_type + i;   // 主键为发票类型拼当前输入code
                entity.ticket_code = temp + i.ToString();    // 本身编码是输入的值
                entity.ticket_status = 0;
                entity.lasttime = DateTime.Now;
                entity.lastoperate = OperatorProvider.Provider.Current().UserName;
                feeticketbll.SaveForm(keyValue, entity);
            }
            return Success("操作成功。");
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
            strXML.AppendFormat("<zcmxx>{0}</zcmxx>",Config.GetValue("zcmxx"));
            strXML.AppendFormat("</input>");
            strXML.AppendFormat("</body>");
            strXML.AppendFormat("</business>");

            return strXML.ToString();
        }

        /// <summary>
        /// 获取发票开具数据xml
        /// </summary>
        /// <param name="ticketid"></param>
        /// <returns></returns>
        private string GetFpkjXml(string ticketid)
        {
            StringBuilder strXML = new StringBuilder();

            strXML.AppendFormat("<?xml version=\"1.0\" encoding=\"gbk\"?>");
            strXML.AppendFormat("<business comment=\"发票开具\" id=\"FPKJ\">");
            strXML.AppendFormat("<body yylxdm=\"1\">");
            strXML.AppendFormat("<input>");
            strXML.AppendFormat("<skpbh>{0}</skpbh>", Config.GetValue("skpbh"));
            strXML.AppendFormat("<skpkl>{0}</skpkl>", Config.GetValue("skpkl"));
            strXML.AppendFormat("<keypwd>{0}</keypwd>", Config.GetValue("keypwd"));
            strXML.AppendFormat("<fplxdm>{0}</fplxdm>", Config.GetValue("fplxdm"));
            strXML.AppendFormat("<kplx>0</kplx>");
            strXML.AppendFormat("<tspz>00</tspz>");

            // 销货
            strXML.AppendFormat("<xhdwsbh>{0}</xhdwsbh>", Config.GetValue("xhdwsbh"));
            strXML.AppendFormat("<xhdwmc>{0}</xhdwmc>", Config.GetValue("xhdwmc"));
            strXML.AppendFormat("<xhdwdzdh>{0}</xhdwdzdh>", Config.GetValue("xhdwdzdh"));
            strXML.AppendFormat("<xhdwyhzh>{0}</xhdwyhzh>", Config.GetValue("xhdwyhzh"));

            // 购货数据
            strXML.AppendFormat("<ghdwsbh>{0}</ghdwsbh>", Config.GetValue("ghdwsbh"));
            strXML.AppendFormat("<ghdwmc>{0}</ghdwmc>", Config.GetValue("ghdwmc"));
            strXML.AppendFormat("<ghdwdzdh>{0}</ghdwdzdh>", Config.GetValue("ghdwdzdh"));
            strXML.AppendFormat("<ghdwyhzh>{0}</ghdwyhzh>", Config.GetValue("ghdwyhzh"));

            strXML.AppendFormat("<bmbbbh></bmbbbh>");
            strXML.AppendFormat("<hsslbs>0</hsslbs>");
            
           

            Decimal hjje = 0;
            Decimal hjse = 0;

            //行数据直接通过存储过程取，不允许编辑修改
            IEnumerable<TicketPrintEntity> data = feeticketbll.GetPrintListJson(ticketid, string.Empty);
            if (data != null && data.Count() > 0) {
                strXML.AppendFormat("<fyxm count=\"{0}\">", data.Count());

                int index = 1;
                foreach (TicketPrintEntity sku in data)
	            {
		            strXML.AppendFormat("<group xh=\"{0}\">", index);
                    strXML.AppendFormat("<fphxz>0</fphxz>");
                    strXML.AppendFormat("<spmc>{0}</spmc>", sku.feedispname);
                    strXML.AppendFormat("<spsm>{0}</spsm>",sku.taxtype);// 商品税目从返回数据字段取 
                    strXML.AppendFormat("<ggxh></ggxh>");
                    strXML.AppendFormat("<dw></dw>");
                    strXML.AppendFormat("<spsl></spsl>");
                    strXML.AppendFormat("<dj></dj>");
                    strXML.AppendFormat("<je>{0}</je>", sku.fmoney);
                    strXML.AppendFormat("<kcje></kcje>");
                    strXML.AppendFormat("<sl>{0}</sl>", sku.taxrate);
                    strXML.AppendFormat("<se>{0}</se>",  (Convert.ToDecimal(sku.taxrate.Replace("%", "")) * sku.fmoney / 100));
                    strXML.AppendFormat("<hsbz>1</hsbz>");
                    strXML.AppendFormat("<spbm>{0}</spbm>", sku.feeitem_id);
                    strXML.AppendFormat("<zxbm></zxbm>");
                    strXML.AppendFormat("<yhzcbs>0</yhzcbs>");
                    strXML.AppendFormat("<slbs></slbs>");
                    strXML.AppendFormat("<zzstsgl></zzstsgl>");


                    hjse = hjse + Convert.ToDecimal(sku.taxrate.Replace("%", "")) * sku.fmoney / 100;
                    hjje = hjje + sku.fmoney;

                    strXML.AppendFormat("</group>");
                    ++index;
	            }

                strXML.AppendFormat("</fyxm>");
            }

            
            strXML.AppendFormat("<zhsl></zhsl>");

            strXML.AppendFormat("<hjje>{0}</hjje>", hjje);
            strXML.AppendFormat("<hjse>{0}</hjse>", hjse);
            strXML.AppendFormat("<jshj>{0}</jshj>", (hjje + hjse));
            strXML.AppendFormat("<bz>$bz</bz>");
            strXML.AppendFormat("<skr>$skr</skr>");
            strXML.AppendFormat("<fhr>$fhr</fhr>");
            strXML.AppendFormat("<kpr>$kpr</kpr>");
            strXML.AppendFormat("<jmbbh></jmbbh>");
            strXML.Append("<zyspmc></zyspmc>");
            strXML.AppendFormat("<qdbz>0</qdbz>");
            strXML.Append("<ssyf></ssyf>");
            strXML.AppendFormat("<kpjh>{0}</kpjh>", Config.GetValue("kpjh"));
            strXML.Append("<tzdbh></tzdbh>");
            strXML.Append("<yfpdm></yfpdm>");
            strXML.Append("<yfphm></yfphm>");
            strXML.AppendFormat("<qmcs>{0}</qmcs>", Config.GetValue("qmcs"));
            strXML.AppendFormat("</input>");
            strXML.AppendFormat("</body>");
            strXML.AppendFormat("</business>");


            return strXML.ToString();
        }

        private string GetFpprintXml()
        {
            // 调用打印
            StringBuilder printXML = new StringBuilder();
            printXML.AppendFormat("<?xml version=\"1.0\" encoding=\"gbk\"?>");
            printXML.AppendFormat("<business comment=\"发票打印\" id=\"FPDY\">");
            printXML.AppendFormat("<body yylxdm=\"{0}\">", Config.GetValue("yylxdm"));
            printXML.AppendFormat("<input>");
            printXML.AppendFormat("   <nsrsbh>{0}</nsrsbh>", Config.GetValue("xhdwsbh"));
            printXML.AppendFormat("   <skpbh>{0}</skpbh>", Config.GetValue("skpbh"));
            printXML.AppendFormat("   <skpkl>{0}</skpkl>", Config.GetValue("skpkl"));
            printXML.AppendFormat("   <keypwd>{0}</keypwd>", Config.GetValue("keypwd"));
            printXML.AppendFormat("   <fplxdm>$fplxdm</fplxdm>"); // 发票类型代码
            printXML.AppendFormat("   <fpdm>$fpdm</fpdm>"); // 发票代码
            printXML.AppendFormat("   <fphm>$fphm</fphm>"); // 发票号码
            printXML.AppendFormat("   <dylx>0</dylx>"); // 0：发票打印，1：清单打印
            printXML.AppendFormat("   <dyfs>1</dyfs>");  // 0：批量打印  1：单张打印
            printXML.AppendFormat("   <dyjmc></dyjmc>");
            printXML.AppendFormat("</input>");
            printXML.AppendFormat("</body>");
            printXML.AppendFormat("</business>");

            return printXML.ToString();
        }


        /// <summary>
        /// 获取打印发票用的xml
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetPrintXML(string keyValue, string typecode, string queryJson, string skus)
        {
            ///typecode: 004增值税专用发票,007增值税普通发票
            ///
            string strReturnValue = "";
            var queryParam = HttpUtility.UrlDecode(queryJson).ToJObject();

            log4netHelper.Info("请求发票打印接口|" + keyValue + "|" + typecode + "|" + queryJson);


            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<?xml version=\"1.0\" encoding=\"gbk\"?>");
            strXML.AppendFormat("<business comment=\"发票开具\" id=\"FPKJ\">");
            strXML.AppendFormat("<body yylxdm=\"1\">");
            strXML.AppendFormat("<input>");
            strXML.AppendFormat("<skpbh>{0}</skpbh>", Config.GetValue("skpbh"));
            strXML.AppendFormat("<skpkl>{0}</skpkl>", Config.GetValue("skpkl"));
            strXML.AppendFormat("<keypwd>{0}</keypwd>", Config.GetValue("keypwd"));
            strXML.AppendFormat("<fplxdm>{0}</fplxdm>",typecode);
            strXML.AppendFormat("<kplx>0</kplx>");
            strXML.AppendFormat("<tspz>00</tspz>");

            strXML.AppendFormat("<xhdwsbh>{0}</xhdwsbh>", queryParam.GetValue("xhdwsbh"));
            strXML.AppendFormat("<xhdwmc>{0}</xhdwmc>", queryParam.GetValue("xhdwmc"));
            strXML.AppendFormat("<xhdwdzdh>{0}</xhdwdzdh>", queryParam.GetValue("xhdwdzdh"));
            strXML.AppendFormat("<xhdwyhzh>{0}</xhdwyhzh>", queryParam.GetValue("xhdwyhzh"));

            strXML.AppendFormat("<ghdwsbh>{0}</ghdwsbh>", queryParam.GetValue("ghdwsbh"));
            strXML.AppendFormat("<ghdwmc>{0}</ghdwmc>", queryParam.GetValue("ghdwmc"));
            strXML.AppendFormat("<ghdwdzdh>{0}</ghdwdzdh>", queryParam.GetValue("ghdwdzdh"));
            strXML.AppendFormat("<ghdwyhzh>{0}</ghdwyhzh>", queryParam.GetValue("ghdwyhzh"));

            strXML.AppendFormat("<bmbbbh></bmbbbh>");
            strXML.AppendFormat("<hsslbs>0</hsslbs>");
            

            Decimal hjje = 0;
            Decimal hjse = 0;

            // 参数
            if (!String.IsNullOrEmpty(skus))
            {
                skus = skus.Replace("},{", "}#{").Replace("[", "").Replace("]", "");
                string[] skulist = skus.Split('#');

                if (skulist != null && skulist.Count() > 0)
                {
                    strXML.AppendFormat("<fyxm count=\"{0}\">", skulist.Count());

                    for (int i = 0; i < skulist.Count(); i++)
                    {
                        var item = skulist[i];
                        strXML.AppendFormat("<group xh=\"{0}\">", (i + 1));

                        var sku = item.ToJObject();

                        strXML.AppendFormat("<fphxz>0</fphxz>");
                        strXML.AppendFormat("<spmc>{0}</spmc>", sku.GetValue("spmc"));
                        strXML.AppendFormat("<spsm></spsm>");
                        strXML.AppendFormat("<ggxh></ggxh>");
                        strXML.AppendFormat("<dw></dw>");
                        strXML.AppendFormat("<spsl></spsl>");
                        strXML.AppendFormat("<dj></dj>");
                        strXML.AppendFormat("<je>{0}</je>", sku.GetValue("je"));
                        strXML.AppendFormat("<kcje></kcje>");
                        strXML.AppendFormat("<sl>{0}</sl>", sku.GetValue("sl"));
                        strXML.AppendFormat("<se>{0}</se>", sku.GetValue("se"));
                        strXML.AppendFormat("<hsbz>1</hsbz>");
                        strXML.AppendFormat("<spbm>{0}</spbm>", sku.GetValue("spbm"));
                        strXML.AppendFormat("<zxbm></zxbm>");
                        strXML.AppendFormat("<yhzcbs>0</yhzcbs>");
                        strXML.AppendFormat("<slbs></slbs>");
                        strXML.AppendFormat("<zzstsgl></zzstsgl>");

                        hjse = hjse + Convert.ToDecimal(sku.GetValue("se"));
                        hjje = hjje + Convert.ToDecimal(sku.GetValue("je"));

                        strXML.AppendFormat("</group>");
                    }
                    
                    strXML.AppendFormat("</fyxm>");
                }
            }

            
            strXML.AppendFormat("<zhsl></zhsl>");

            strXML.AppendFormat("<hjje>{0}</hjje>", hjje);
            strXML.AppendFormat("<hjse>{0}</hjse>", hjse);
            strXML.AppendFormat("<jshj>{0}</jshj>", (hjje + hjse));
            strXML.AppendFormat("<bz>{0}</bz>", queryParam.GetValue("bz"));
            strXML.AppendFormat("<skr>{0}</skr>", queryParam.GetValue("skr"));
            strXML.AppendFormat("<fhr>{0}</fhr>", queryParam.GetValue("fhr"));
            strXML.AppendFormat("<kpr>{0}</kpr>", queryParam.GetValue("kpr"));
            strXML.AppendFormat("<jmbbh></jmbbh>");
            strXML.Append("<zyspmc></zyspmc>");
            strXML.Append("<spsm></spsm>");
            strXML.AppendFormat("<qdbz>0</qdbz>");
            strXML.Append("<ssyf></ssyf>");
            strXML.AppendFormat("<kpjh>{0}</kpjh>", Config.GetValue("kpjh"));
            strXML.Append("<tzdbh></tzdbh>");
            strXML.Append("<yfpdm></yfpdm>");
            strXML.Append("<yfphm></yfphm>");
            strXML.AppendFormat("<qmcs>{0}</qmcs>", Config.GetValue("qmcs"));
            strXML.AppendFormat("</input>");
            strXML.AppendFormat("</body>");
            strXML.AppendFormat("</business>");

            log4netHelper.Info("发票开具接口参数生成|" + strXML);

            return Success("操作成功！", strXML);
        }

        /// <summary>
        /// 发送xml请求，获取返回信息
        /// </summary>
        /// <param name="printXml"></param>
        /// <param name="typecode"></param>
        /// <returns></returns>
        public string GetPrintDoXml(string printXml, string typecode){
            
            string fpdm = String.Empty;
            string fphm = String.Empty;
            if (printXml.IndexOf("returnmsg") != -1 && printXml.IndexOf("成功") != -1)
            {
                fpdm = XmlHelper.XmlNodeFind(@"business/body/output/fpdm", printXml);
                fphm = XmlHelper.XmlAnalysis(@"business/body/output/fphm", printXml);

                // 调用打印
                StringBuilder printXML = new StringBuilder();
                printXML.AppendFormat("<?xml version=\"1.0\" encoding=\"gbk\"?>");
                printXML.AppendFormat("<business comment=\"发票打印\" id=\"FPDY\">");
                printXML.AppendFormat("<body yylxdm=\"{0}\">", Config.GetValue("yylxdm"));
                printXML.AppendFormat("<input>");
                printXML.AppendFormat("   <nsrsbh>{0}</nsrsbh>", Config.GetValue("nsrsbh"));
                printXML.AppendFormat("   <skpbh>{0}</skpbh>", Config.GetValue("skpbh"));
                printXML.AppendFormat("   <skpkl>{0}</skpkl>", Config.GetValue("skpkl"));
                printXML.AppendFormat("   <keypwd>{0}</keypwd>", Config.GetValue("keypwd"));
                printXML.AppendFormat("   <fplxdm>{0}</fplxdm>", typecode);
                printXML.AppendFormat("   <fpdm>{0}</fpdm>", fpdm);
                printXML.AppendFormat("   <fphm>{0}</fphm>", fphm);
                printXML.AppendFormat("   <dylx>0</dylx>"); // 0：发票打印，1：清单打印
                printXML.AppendFormat("   <dyfs>1</dyfs>");  // 0：批量打印  1：单张打印
                printXML.AppendFormat("   <dyjmc></dyjmc>");
                printXML.AppendFormat("</input>");
                printXML.AppendFormat("</body>");
                printXML.AppendFormat("</business>");

                Logger.Info("发票打印接口参数|" + printXML);

                return Success("操作成功！", printXML);
            }
            else
            {
                return Error(printXml);
            }
        }

        /// <summary>
        /// 发送xml请求，获取返回信息
        /// </summary>
        /// <param name="strXML"></param>
        /// <returns></returns>
        private static string sendXMLRequest(StringBuilder strXML)
        {
            string strReturnValue;
            byte[] bOutputInfo = new byte[1024 * 10];
            //OperateDisk(Encoding.Default.GetBytes(strXML.ToString()), bOutputInfo);

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
            return strReturnValue;
        }

        /// <summary>
        /// 保存页面提交数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SavePrintData(string queryJson,string skus)
        {
            if (String.IsNullOrEmpty(queryJson))
            {
                return Error("参数异常");
            }
            var queryParam = HttpUtility.UrlDecode(queryJson).ToJObject();

            PtInvoiceEntity entity = new PtInvoiceEntity();
            entity.inputtime = DateTime.Now;
            entity.inv_date = DateTime.Now;
            Decimal hjje = 0;
            Decimal hjse = 0;

            // 参数
            if (!String.IsNullOrEmpty(skus))
            {
                skus = skus.Replace("},{", "}#{").Replace("[", "").Replace("]", "");
                string[] skulist = skus.Split('#');

                if (skulist != null && skulist.Count() > 0)
                {

                    for (int i = 0; i < skulist.Count(); i++)
                    {
                        var item = skulist[i];
                        var sku = item.ToJObject();

                        hjse = hjse + Convert.ToDecimal(sku.GetValue("se"));
                        hjje = hjje + Convert.ToDecimal(sku.GetValue("je"));

                    }
                }
            }

            entity.inv_money = Convert.ToDouble(hjje + hjse);
            entity.inv_name = queryParam.GetValue("ghdwmc").ToString();
            entity.inv_notes = queryParam.GetValue("bz").ToString();
            entity.ticket_id = queryParam.GetValue("ticket_id").ToString();

            ptinvoicebll.SaveForm(null, entity);

            return Success("操作成功。");
        }

        #endregion 提交数据
    }
}