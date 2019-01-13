using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System;
using System.Linq;
using System.Runtime.InteropServices;
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
        [DllImport("C:\\Program Files (x86)\\税控盘组件接口\\NISEC_SKPC.dll", CharSet = CharSet.Ansi, EntryPoint = "OperateDisk")]
        public extern static bool OperateDisk(byte[] sInputInfo, byte[] sOutputInfo);

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
        /// 打印发票
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintFrom()
        {
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
        /// 获取打印发票用的xml
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPrintXML(string keyValue, int type, string queryJson)
        {
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

            return Content(queryJson);
        }

        #endregion 提交数据
    }
}