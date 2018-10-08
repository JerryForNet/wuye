using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Cache;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Data;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Management;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.RepostryManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-25 11:47
    /// 描 述：领用单对应物品信息
    /// </summary>
    [HandlerOperateLog]
    public class OutbillitemController : MvcControllerBase
    {
        private OutbillitemBLL outbillitembll = new OutbillitemBLL();

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
        /// 物品领用汇总
        /// </summary>
        /// <returns></returns>
        public ActionResult SummaryIndex()
        {
            return View();
        }

        /// <summary>
        /// 物品领用统计
        /// </summary>
        /// <returns></returns>
        public ActionResult StatisticsIndex()
        {
            return View();
        }

        /// <summary>
        /// 领用费用
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiveCost()
        {
            return View();
        }
        /// <summary>
        /// 物品领用统计打印预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult previewStatisticsIndex()
        {
            return View();
        }
        /// <summary>
        /// 物品领用汇总打印预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult previewSummaryIndex()
        {
            return View();
        }
        /// <summary>
        /// 领用费用打印预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult previewReceiveCost()
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
            var watch = CommonHelper.TimerStart();
            var data = outbillitembll.GetPageList(pagination, queryJson);
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
            var data = outbillitembll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取领用列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetOutListJson(Pagination pagination, string queryJson)
        {
            var data = outbillitembll.GetOutList(pagination, queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取领用统计
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetStatisticsJson(Pagination pagination, string queryJson)
        {
            var data = outbillitembll.GetStatisticsList(pagination, queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 领用费用
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetReceiveCostJson(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            GoodstypeBLL goodstypebll = new GoodstypeBLL();
            //获取商品类别List
            List<GoodstypeEntity> goodList = goodstypebll.GetListJson("0").ToList();
            DataTable dt = new DataTable();
            List<object> obj = new List<object>();
            if (goodList != null && goodList.Count > 0)
            {
                #region 1、创建列名

                //1、固定列名
                dt.Columns.Add("部门");//部门
                //2、动态创建列名
                foreach (var item in goodList)
                {
                    dt.Columns.Add(item.ftypename);
                }
                //3、固定列名
                dt.Columns.Add("小计");//小计

                #endregion 1、创建列名

                #region 2、添加行数据

                //获取部门
                HrDepartmentCache hrDepartmentCache = new HrDepartmentCache();
                List<HrDepartmentEntity> departList = hrDepartmentCache.GetList().ToList();

                //获取费用列表
                List<ReceiveCostModel> list = outbillitembll.GetReceiveCostList().ToList();
                List<ReceiveCostModel> costList = null;
                DataRow dr = dt.NewRow();
                //查询条件 领用时间
                DateTime? startDate = null;
                DateTime? endDate = null;
                if (!queryParam["StartDate"].IsEmpty())
                {
                    startDate = Convert.ToDateTime(queryParam["StartDate"]);
                }
                if (!queryParam["EndDate"].IsEmpty())
                {
                    endDate = Convert.ToDateTime(queryParam["EndDate"]);
                }
                if (departList != null && departList.Count > 0 && list != null && list.Count > 0)
                {
                    foreach (var dep in departList)
                    {
                        dr = dt.NewRow();
                        dr["部门"] = dep.deptname;
                        decimal amount = 0;
                        decimal money = 0;
                        foreach (var li in goodList)
                        {
                            costList = list.FindAll(o => o.fgoodsid == li.frootid && o.fdeptid == dep.deptid).ToList();
                            if (startDate != null)
                            {
                                costList = costList.FindAll(o => o.foutdate >= startDate).ToList();
                            }
                            if (endDate != null)
                            {
                                costList = costList.FindAll(o => o.foutdate <= endDate).ToList();
                            }
                            money = costList.Sum(o => o.fmoney);
                            dr[li.ftypename] = money.ToString("F");
                            amount += money;
                        }
                        dr["小计"] = amount.ToString("F");
                        dt.Rows.Add(dr);
                    }
                }

                #endregion 2、添加行数据
            }
            return ToJsonResult(dt);
        }

        /// <summary>
        /// 获取JQGrid动态列名
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGridRows()
        {
            List<object> obj = new List<object>();
            //获取商品类别List
            GoodstypeBLL goodstypebll = new GoodstypeBLL();
            List<GoodstypeEntity> goodList = goodstypebll.GetListJson("0").ToList();
            DataTable dt = new DataTable();

            if (goodList != null && goodList.Count > 0)
            {
                //1、固定列名
                dt.Columns.Add("部门");//部门
                //2、动态创建列名
                foreach (var item in goodList)
                {
                    dt.Columns.Add(item.ftypename);
                }
                //3、固定列名
                dt.Columns.Add("小计");//小计

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var colm = new
                    {
                        label = dt.Columns[i].ColumnName,
                        name = dt.Columns[i].ColumnName,
                        align = "center",
                        sortable = false
                    };
                    obj.Add(colm);
                }
            }
            return ToJsonResult(obj);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = outbillitembll.GetEntity(keyValue);
            var bigtype = "";
            var smalltype = "";
            if (keyValue != "" && keyValue != null)
            {
                string st = data.fgoodsid.ToString();
                int start = 1, length = 3;
                bigtype = st.Substring(start - 1, length);
                int beg = 1, all = 6;
                smalltype = st.Substring(beg - 1, all);
            }
            var jsonData = new
            {
                data = data,
                bigtype = bigtype,
                smalltype = smalltype
            };
            return ToJsonResult(jsonData);
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            outbillitembll.RemoveForm(keyValue);
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
        //     [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue, OutbillitemEntity entity)
        {
            string count = outbillitembll.NuCounts(entity.fgoodsid);
            if (double.Parse(count) < entity.fnumber)
            {
                return Error("数量不足。");
            }
            else
            {
                outbillitembll.SaveForm(keyValue, entity);
                return Success("操作成功。");
            }
        }

        #endregion 提交数据
    }
}