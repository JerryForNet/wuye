using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 10:39
    /// 描 述：Feereceive
    /// </summary>
    [HandlerOperateLog]
    public class FeereceiveController : MvcControllerBase
    {
        private FeereceiveBLL feereceivebll = new FeereceiveBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FeereceiveIndex()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FeereceiveForm()
        {
            return View();
        }

        /// <summary>
        /// 发票作废
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ToVoidIndex()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = feereceivebll.GetPageList(pagination, queryJson);
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
            var data = feereceivebll.GetList(queryJson);
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
            var data = feereceivebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 查询发票信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetToVoidInfoJosn(string keyValue)
        {
            string receive_id = feereceivebll.Getreceive_id(keyValue);
            if (!string.IsNullOrEmpty(receive_id))
            {
                OtherincomeBLL otherincomebll = new OtherincomeBLL();
                var data = otherincomebll.GetEntitys(receive_id, 0);
                data.type = "0";
                return ToJsonResult(data);
            }
            else
            {
                OtherincomeBLL bll = new OtherincomeBLL();
                string incomeid = bll.Getincomeid(keyValue);
                if (!string.IsNullOrEmpty(incomeid))
                {
                    var data = bll.GetEntitys(incomeid, 1);
                    data.type = "1";
                    return ToJsonResult(data);
                }
                else
                {
                    return Error("发票输入不正确，请输入正确的发票号码！");
                }
            }
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            feereceivebll.RemoveForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, FeereceiveEntity entity)
        {
            feereceivebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 发票作废
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult ToVoidForm(string keyValue, int Type)
        {
            FeeticketBLL bll_f = new FeeticketBLL();
            FeeticketEntity Fent = bll_f.GetEntity(keyValue);
            string DepartmentId = OperatorProvider.Provider.Current().DepartmentId;
            if (Fent == null)
            {
                return Error("操作错误！");
            }
            else if (Fent.ticket_status == 2)
            {
                return Error("发票已经归档无法作废，请与财务部联系！");
            }
            else if ((Fent.ticket_status != 1) && (Fent.ticket_status != 10))
            {
                return Error("发票尚未使用无法作废，请与财务部联系！");
            }
            else if (Fent.dept_id != DepartmentId)
            {
                return Error("发票不属于本部门无法作废，请与财务部联系！！");
            }
            else
            {
                string name = OperatorProvider.Provider.Current().UserName;
                if (Type == 0)
                {
                    if (feereceivebll.ToVoidForm(1, keyValue, name) > 0)
                    {
                        return Success("操作成功。");
                    }
                    else
                    {
                        return Error("操作失败。");
                    }
                }
                else
                {
                    OtherincomeBLL bll_o = new OtherincomeBLL();
                    bll_o.ToVoidForm(name, keyValue);
                    return Success("操作成功。");
                }
            }
        }

        #endregion
    }
}