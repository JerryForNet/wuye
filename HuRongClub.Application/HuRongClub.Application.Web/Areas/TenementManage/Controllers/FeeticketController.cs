using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System;
using System.Linq;
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

        #endregion 提交数据
    }
}