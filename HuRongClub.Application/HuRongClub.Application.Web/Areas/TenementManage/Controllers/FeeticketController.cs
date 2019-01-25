using HuRongClub.Application.Busines.FinanceManage;
using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Entity.TenementManage.ViewModel;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.Log;
using HuRongClub.Util.WebControl;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Mvc;

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
        /// 发票批量打印
        /// </summary>
        /// <returns></returns>
        public ActionResult BatchPrint()
        {
            return View();
        }

        /// <summary>
        /// 打印发票
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintFrom(string keyValue)
        {
            // 票据日期
            DateTime time = DateTime.Now;
            ViewBag.year = time.Year.ToString();
            ViewBag.month = (time.Month < 10 ? ("0" + time.Month.ToString()) : time.Month.ToString());
            ViewBag.day = (time.Day < 10 ? ("0" + time.Day.ToString()) : time.Day.ToString());

            //取页面参数ticketid
            ViewBag.fpregxml = GetFpregXml();
            ViewBag.xhdwsbh = Config.GetValue("xhdwsbh");
            ViewBag.xhdwmc = Config.GetValue("xhdwmc");
            ViewBag.xhdwdzdh = Config.GetValue("xhdwdzdh");
            ViewBag.xhdwyhzh = Config.GetValue("xhdwyhzh");

            return View();
        }

        #endregion 财务管理--发票管理


        /// <summary>
        /// 发票批量打印
        /// </summary>
        /// <returns></returns>
        public ActionResult BatchPrintFrom() {
            return View();
        }

        /// <summary>
        /// 批量打印Items
        /// </summary>
        /// <returns></returns>
        public ActionResult BatchPrintListView() {
            return View();
        }

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
            PtInvoiceBLL ptinvoicebll = new PtInvoiceBLL();

            //step1 判断是否有收费数据判断pt_invoice表里面ticket_id是否存在
            PtInvoiceEntity entity = ptinvoicebll.GetEntity(keyValue);
            if (entity != null)
            {
                return Error("发票已经打印过不能重复打印");
            }

            //step2 data为空表示没有取到收费信息，发票号可能未使用或者作废等
            var data = feeticketbll.GetPrintListJson(keyValue, string.Empty);
            if (data == null || data.Count() == 0)
            {
                return Error("发票开票信息不存在！");
            }

            return Success("获取发票信息成功", data);
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
            strXML.AppendFormat("<zcmxx>{0}</zcmxx>", Config.GetValue("zcmxx"));
            strXML.AppendFormat("</input>");
            strXML.AppendFormat("</body>");
            strXML.AppendFormat("</business>");

            log4netHelper.Info("发票打印注册xml数据|" + strXML);

            return strXML.ToString();
        }

        /// <summary>
        /// 获取打印发票用的xml
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetFpkjDoXML(string keyValue, string typecode, string queryJson)
        {
            ///typecode: 004增值税专用发票,007增值税普通发票
            ///
            //string strReturnValue = "";
            var queryParam = HttpUtility.UrlDecode(queryJson).ToJObject();

            log4netHelper.Info("请求发票打印接口|" + keyValue + "|" + typecode + "|" + queryJson);

            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<?xml version='1.0' encoding='gbk' ?>");
            strXML.AppendFormat("<business comment='发票开具' id='FPKJ'>");
            strXML.AppendFormat("<body yylxdm='1'>");
            strXML.AppendFormat("<input>");
            strXML.AppendFormat("<skpbh>{0}</skpbh>", Config.GetValue("skpbh"));
            strXML.AppendFormat("<skpkl>{0}</skpkl>", Config.GetValue("skpkl"));
            strXML.AppendFormat("<keypwd>{0}</keypwd>", Config.GetValue("keypwd"));
            strXML.AppendFormat("<fplxdm>{0}</fplxdm>", typecode);
            strXML.AppendFormat("<kplx>0</kplx>");
            strXML.AppendFormat("<tspz>00</tspz>");

            // 销货
            strXML.AppendFormat("<xhdwsbh>{0}</xhdwsbh>", Config.GetValue("xhdwsbh"));
            strXML.AppendFormat("<xhdwmc>{0}</xhdwmc>", Config.GetValue("xhdwmc"));
            strXML.AppendFormat("<xhdwdzdh>{0}</xhdwdzdh>", Config.GetValue("xhdwdzdh"));
            strXML.AppendFormat("<xhdwyhzh>{0}</xhdwyhzh>", Config.GetValue("xhdwyhzh"));

            strXML.AppendFormat("<ghdwsbh>{0}</ghdwsbh>", queryParam.GetValue("ghdwsbh"));
            strXML.AppendFormat("<ghdwmc>{0}</ghdwmc>", queryParam.GetValue("ghdwmc"));
            strXML.AppendFormat("<ghdwdzdh>{0}</ghdwdzdh>", queryParam.GetValue("ghdwdzdh"));
            strXML.AppendFormat("<ghdwyhzh>{0}</ghdwyhzh>", queryParam.GetValue("ghdwyhzh"));

            strXML.AppendFormat("<bmbbbh>{0}</bmbbbh>", Config.GetValue("bmbbbh"));
            strXML.AppendFormat("<hsslbs>0</hsslbs>");

            Decimal hjje = 0;
            Decimal hjse = 0;

            // 参数
            IEnumerable<TicketPrintEntity> data = feeticketbll.GetPrintListJson(keyValue, string.Empty);
            if (data != null && data.Count() > 0)
            {
                strXML.AppendFormat("<fyxm count='{0}'>", data.Count());

                int index = 1;
                Decimal se = 0;

                foreach (TicketPrintEntity sku in data)
                {
                    //se = Math.Round(sku.fmoney / (1 + Convert.ToDecimal(sku.taxrate.Replace("%", "")) / 100),2);

                    strXML.AppendFormat("<group xh='{0}'>", index);
                    strXML.AppendFormat("<fphxz>0</fphxz>");
                    strXML.AppendFormat("<spmc>{0}</spmc>", sku.feedispname);
                    // strXML.AppendFormat("<spsm>{0}</spsm>", sku.taxtype);// 商品税目从返回数据字段取
                    strXML.AppendFormat("<ggxh></ggxh>");
                    strXML.AppendFormat("<dw></dw>");
                    strXML.AppendFormat("<spsl></spsl>");
                    strXML.AppendFormat("<dj></dj>");
                    strXML.AppendFormat("<je>{0}</je>", sku.fmoney);
                    strXML.AppendFormat("<kcje></kcje>");
                    strXML.AppendFormat("<sl>{0}</sl>", sku.sl);
                    strXML.AppendFormat("<se>{0}</se>", sku.se);
                    strXML.AppendFormat("<hsbz>1</hsbz>");
                    strXML.AppendFormat("<spbm>{0}</spbm>", sku.taxtype);
                    strXML.AppendFormat("<zxbm></zxbm>");
                    strXML.AppendFormat("<yhzcbs>0</yhzcbs>");
                    strXML.AppendFormat("<slbs></slbs>");
                    strXML.AppendFormat("<zzstsgl></zzstsgl>");

                    hjse = hjse + sku.se;
                    hjje = hjje + sku.fmoney - sku.se;

                    strXML.AppendFormat("</group>");
                    ++index;
                }

                strXML.AppendFormat("</fyxm>");
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
            //strXML.AppendFormat("<spsm>{0}</spsm>", spsm);
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

            log4netHelper.Info("发票开具接口xml数据生成|" + strXML);

            //return Success("操作成功！", strXML.ToString());
            return Content(strXML.ToString());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="typecode"></param>
        /// <param name="fpdm"></param>
        /// <param name="fphm"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetPrintDoXml(string typecode, string fpdm, string fphm)
        {
            // 调用打印
            StringBuilder printXML = new StringBuilder();
            printXML.AppendFormat("<?xml version='1.0' encoding='gbk'?>");
            printXML.AppendFormat("<business comment='发票打印' id='FPDY'>");
            printXML.AppendFormat("<body yylxdm='{0}'>", Config.GetValue("yylxdm"));
            printXML.AppendFormat("<input>");
            printXML.AppendFormat("   <nsrsbh>{0}</nsrsbh>", Config.GetValue("xhdwsbh"));
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

            log4netHelper.Info("发票打印接口xml数据生成|" + printXML);

            //return Success("操作成功！", printXML);
            return ToJsonResult(printXML.ToString());
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFpregDoXml()
        {
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<?xml version=\"1.0\" encoding=\"gbk\"?>");
            strXML.AppendFormat("<business comment=\"注册码信息导入\" id=\"ZCMDR\">");
            strXML.AppendFormat("<body yylxdm=\"1\">");
            strXML.AppendFormat("<input>");
            strXML.AppendFormat("<zcmxx>{0}</zcmxx>", Config.GetValue("zcmxx"));
            strXML.AppendFormat("</input>");
            strXML.AppendFormat("</body>");
            strXML.AppendFormat("</business>");

            Logger.Info("发票打印注册xml数据|" + strXML);

            return Success("操作成功！", strXML);
        }


        /// <summary>
        /// 获取选中的批量发票json数据
        /// </summary>
        /// <param name="ticketid">选中的ticketid用,连接组成的</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetBatchPrint(string ticketid)
        {
            String tickets = "'"+ticketid.Replace(",", "','") + "'";

            //获取全部发票明细数据
            DataTable dts = feeticketbll.GetBatchPrint(tickets);

            var data = new Newtonsoft.Json.Linq.JArray();

            //发票按照号码、税率分组
           DataTable dtgroup=dts.DefaultView.ToTable(true,"ticket_id","taxrate");
           for (int i = 0; i < dtgroup.Rows.Count; i++)
           {
               string tmpticketid = dtgroup.Rows[i][0].ToString() + "_s" + i.ToString();  //分组临时发票号
               string bz = "";   //发票备注
               decimal hjje = 0;  //合计金额
               decimal hjse = 0;  //合计税额
               decimal sl = Convert.ToDecimal(dtgroup.Rows[i][1].ToString().Replace("%", "")) / 100;   //税率
               decimal se=0;
               string khmc = "";   //客户名称
               string khdm = "";  //客户代码
               string khsh = "";   //客户税号
               string khdz = "";   //客户地址
               string khkhyhzh = "";  //客户开户银行账号

               var jarray = new Newtonsoft.Json.Linq.JArray();
             


               DataRow[] rows = dts.Select("ticket_id='" + dtgroup.Rows[i][0] + "' and taxrate='" + dtgroup.Rows[i][1] + "'");
               if (rows.Length > 0)
               {
                   foreach (DataRow row in rows)
                   {
                       //赋值及合计
                       khmc = row["khmc"].ToString();
                       khdm = row["khdm"].ToString();
                       khdz = row["khdz"].ToString();
                       khsh = row["khsh"].ToString();
                       khkhyhzh = row["khkhyhzh"].ToString();
                       hjje +=Convert.ToDecimal(row["check_money"]);
                       se=Math.Round(Convert.ToDecimal(row["check_money"]) * sl / (1 + sl), 2);
                       hjse += se;
                       if (bz.IndexOf(row["feeitem_name"].ToString()) > 0)
                           bz = bz + ","+row["fperiod"].ToString()+",";
                       else
                           bz = row["feeitem_name"].ToString()+":"+row["fperiod"].ToString()+",";
                          
                       //构建子json 行数据rowitems{feeitemname,dispname,taxtype,taxrate,fmoney,speriod,se}
                       var childjson=new Newtonsoft.Json.Linq.JObject();
                       childjson.Add("feeitemname",row["feeitem_name"].ToString());
                       childjson.Add("feedispname",row["feedispname"].ToString());
                       childjson.Add("taxtype",row["taxtype"].ToString());
                       childjson.Add("taxrate",row["taxrate"].ToString());
                       childjson.Add("fmoney", row["check_money"].ToString());
                       childjson.Add("fperiod",row["fperiod"].ToString());
                           
                       jarray.Add(childjson);
                   }
               }
               //构建主json 数据 {tmpticketid，khdm,khmc,khsh,khdz,khkhyhzh,hjje,hjse,bhsje,bz}
               var jsondata = new JObject();
               jsondata.Add("tmpticketid",tmpticketid);
               jsondata.Add("khdm",khdm);
               jsondata.Add("khmc",khmc);
               jsondata.Add("khsh",khsh);
               jsondata.Add("khdz",khdz);
               jsondata.Add("khkhyhzh",khkhyhzh);
               jsondata.Add("hjje",hjje);
               jsondata.Add("hjse",hjse);
               jsondata.Add("bhsje", hjje - hjse);
               jsondata.Add("bz",bz);
               jsondata.Add("feeitems",jarray);

               data.Add(jsondata);
           }
            //返回构造的json数据
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
        /// 保存页面提交数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SavePrintData(string keyValue, string queryJson)
        {
            if (String.IsNullOrEmpty(queryJson))
            {
                return Error("参数异常");
            }
            var queryParam = HttpUtility.UrlDecode(queryJson).ToJObject();

            //先保存购方客户信息，保存到表pt_kpkhxx，根据form里面gfkhdm是否为空判断，有值表示通过自动完成选择数据的已保存数据,不用保存，为空则需要pt_kpkhxx表插入一条记录
            if (queryParam.GetValue("ghkhdm") == null || queryParam.GetValue("ghkhdm").ToString() == "")
            {
                string khdm = feeticketbll.GetMaxID();//获取产生一个pt_kpkhxx最大khdm字段字符串

                //保存对应字段表pt_kpkhxx
                InvoiceInfoEntity saveEntity = new InvoiceInfoEntity();
                saveEntity.khdm = khdm;
                saveEntity.khmc = queryParam.GetValue("ghdwmc").ToString();
                saveEntity.khsh = queryParam.GetValue("ghdwsbh").ToString();
                saveEntity.khdz = queryParam.GetValue("ghdwdzdh").ToString();
                saveEntity.khkhyhzh = queryParam.GetValue("ghdwyhzh").ToString();

                new InvoiceInfoBLL().SaveForm(null, saveEntity);
            }

            PtInvoiceEntity entity = new PtInvoiceEntity();
            entity.inputtime = DateTime.Now;
            entity.inv_date = DateTime.Now;

            entity.inv_money = Convert.ToDecimal(queryParam.GetValue("fphjje"));
            entity.inv_name = queryParam.GetValue("kpr").ToString();
            entity.inv_notes = queryParam.GetValue("bz").ToString();
            entity.ticket_id = queryParam.GetValue("ticket_id").ToString();

            // 前端传参 需要注意
            entity.inv_num = queryParam.GetValue("fphm").ToString();
            entity.inv_fpdm = queryParam.GetValue("fpdm").ToString();
            entity.inv_lxdm = queryParam.GetValue("fplxdm").ToString();
            entity.khdm = queryParam.GetValue("ghkhdm").ToString();

            new PtInvoiceBLL().SaveForm(null, entity);

            return Success("操作成功。");
        }

        #endregion 提交数据
    }
}