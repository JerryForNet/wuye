using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.RepostryManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-06-20 10:32
    /// 描 述：Lblist
    /// </summary>
    [HandlerOperateLog]
    public class LblistController : MvcControllerBase
    {
        private LblistBLL lblistbll = new LblistBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult LblistIndex()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult LblistForm()
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
            var data = lblistbll.GetPageList(pagination, queryJson);
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
            var data = lblistbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(int keyValue)
        {
            var data = lblistbll.GetEntity(keyValue);
            return ToJsonResult(data);
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(int keyValue)
        {
            LblistEntity entity = lblistbll.GetEntity(keyValue);
            if (entity != null)
            {
                if (entity.isnew == 0)
                {
                    return Error("已经过期领用不允许删除！");
                }
                else
                {
                    lblistbll.RemoveForm(keyValue);
                    return Success("删除成功。");
                }
            }
            else
            {
                return Error("删除数据不存在，请刷新页面操作。");
            }
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
        public ActionResult SaveForm(string keyValue, LblistEntity entity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                var data = lblistbll.GetList(entity.dictitemid, entity.empid);
                if (data != null && data.Count() > 0)
                {
                    lblistbll.UpdateByisnew(entity.dictitemid, entity.empid);
                }
                entity.isnew = 1;
                entity.inputtime = DateTime.Now;
                try
                {
                    if (!string.IsNullOrEmpty(OperatorProvider.Provider.Current().OldSystemUserID))
                    {
                        entity.inputuserid = Convert.ToInt16(OperatorProvider.Provider.Current().OldSystemUserID);
                    }
                    else
                    {
                        entity.inputuserid = 0;
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                LblistEntity ent = lblistbll.GetEntity(keyValue.ToInt());
                if (ent != null)
                {
                    if (ent.isnew == 0)
                    {
                        return Error("已经过期领用不允许编辑！");
                    }
                }
                else
                {
                    return Error("编辑数据不存在，请确定后再操作。");
                }
            }
            entity.lbenddate = entity.lbbegindate.AddMonths(entity.lbmonth);
            lblistbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        #endregion
    }
}