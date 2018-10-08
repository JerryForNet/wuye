using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.RepostryManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-24 18:47
    /// 描 述：物品领用
    /// </summary>
    [HandlerOperateLog]
    public class OutbillController : MvcControllerBase
    {
        private OutbillBLL outbillbll = new OutbillBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Info()
        {
            return View();
        }

        #endregion 视图功能

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
            if (string.IsNullOrEmpty(queryJson))
            {
                return null;
            }

            var watch = CommonHelper.TimerStart();
            var data = outbillbll.GetPageList(pagination, queryJson);
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
            var data = outbillbll.GetList(queryJson);
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
            var data = outbillbll.GetEntity(keyValue);
            var childData = outbillbll.GetDetails(keyValue);
            var jsonData = new { entity = data, childEntity = childData };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            var data = outbillbll.GetDetails(keyValue);
            return ToJsonResult(data);
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //  [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            outbillbll.RemoveForm(keyValue);
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
        // [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue, OutbillEntity entity, string strChildEntitys)
        {
            List<OutbillitemEntity> childEntitys = strChildEntitys.ToList<OutbillitemEntity>();

            if (string.IsNullOrEmpty(keyValue))
            {
                var childs = from c in childEntitys group c by c.fgoodsid into g where g.Count() > 1 select g;
                if (childs != null && childs.ToList().Count() > 0)
                {
                    return Error("不可以选择重复的物品");
                }
            }

            #region 判断库存是否充足
            if (!string.IsNullOrEmpty(keyValue))
            {
                #region 修改时判断，要加上修改前数量
                string fgoodsid = "";
                if (childEntitys != null && childEntitys.Count > 0)
                {
                    foreach (OutbillitemEntity item in childEntitys)
                    {
                        fgoodsid += item.fgoodsid + ",";
                    }
                    if (!string.IsNullOrEmpty(fgoodsid))
                    {
                        fgoodsid = fgoodsid.Substring(0, fgoodsid.Length - 1);
                    }
                    //当前库存
                    GoodsinfoBLL bll_g = new GoodsinfoBLL();
                    var data = bll_g.GetLists(fgoodsid);
                    //修改前出库
                    OutbillitemBLL bll_o = new OutbillitemBLL();
                    var data_out = bll_o.GetListByFoutbillid(keyValue);
                    bool bl = true;
                    if (data != null)
                    {
                        foreach (OutbillitemEntity item in childEntitys)
                        {
                            double fcount = 0;
                            var data_out_s = data_out.Where(t => t.fgoodsid == item.fgoodsid);
                            if (data_out_s != null && data_out_s.Count() > 0)
                            {
                                fcount = data_out_s.First().fnumber.ToDouble();
                            }
                            var data_s = data.Where(t => t.fgoodsid == item.fgoodsid);
                            if (data_s != null && data_s.Count() > 0)
                            {
                                if ((data_s.First().fcount + fcount) < item.fnumber)
                                {
                                    bl = false;
                                    continue;
                                }
                            }
                            else
                            {
                                bl = false;
                                continue;
                            }
                        }
                    }
                    else
                    {
                        bl = false;
                    }

                    if (!bl)
                    {
                        return Error("库存不足，请核实后重新填写！");
                    }
                }

                #endregion
            }
            else
            {
                #region 新增时判断，直接判断
                string fgoodsid = "";
                if (childEntitys != null && childEntitys.Count > 0)
                {
                    foreach (OutbillitemEntity item in childEntitys)
                    {
                        fgoodsid += item.fgoodsid + ",";
                    }
                    if (!string.IsNullOrEmpty(fgoodsid))
                    {
                        fgoodsid = fgoodsid.Substring(0, fgoodsid.Length - 1);
                    }
                    GoodsinfoBLL bll_g = new GoodsinfoBLL();
                    var data = bll_g.GetLists(fgoodsid);
                    bool bl = true;
                    if (data != null)
                    {
                        foreach (OutbillitemEntity item in childEntitys)
                        {
                            var dataWhere = data.Where(t => t.fgoodsid == item.fgoodsid);
                            if (dataWhere != null)
                            {
                                if (dataWhere.First().fcount < item.fnumber)
                                {
                                    bl = false;
                                    continue;
                                }
                            }
                            else
                            {
                                bl = false;
                                continue;
                            }
                        }
                    }
                    else
                    {
                        bl = false;
                    }

                    if (!bl)
                    {
                        return Error("库存不足，请核实后重新填写！");
                    }
                }
                #endregion
            }
            #endregion

            outbillbll.SaveForm(keyValue, entity, childEntitys);
            return Success("操作成功。");
        }

        #endregion 提交数据

        #region 验证数据

        public string CheckDel(string foutid)
        {
            string check = outbillbll.CheckDel(foutid);
            if (check != null && check != "")
            {
                return check;
            }
            else
            {
                return null;
            }
        }

        #endregion 验证数据
    }
}