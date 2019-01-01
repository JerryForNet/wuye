using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Application.IService.RepostryManage;
using HuRongClub.Application.Service.RepostryManage;
using HuRongClub.Util.Offices;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HuRongClub.Util;

namespace HuRongClub.Application.Busines.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-25 15:16
    /// 描 述：物品info信息
    /// </summary>
    public class GoodsinfoBLL
    {
        private GoodsinfoIService service = new GoodsinfoService();
        private InbillIService inbillIService = new InbillService();
        private OutbillIService outbillIService = new OutbillService();

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<GoodsinfoModel> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<GoodsinfoEntity> GetList(string fgoodsid)
        {
            return service.GetList(fgoodsid);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public GoodsinfoEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        /// <summary>
        /// 获取列表--根据领用主表编号
        /// </summary>
        /// <param name="foutbillid">领用主表编号</param>
        /// <returns>返回列表</returns>
        public IEnumerable<GoodsinfoEntity> GetLists(string fgoodsids)
        {
            return service.GetLists(fgoodsids);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public int ExportGoodsInfo(string queryJson)
        {
            try
            {
                IEnumerable<GoodsinfoModel> Ienum = service.GetPageList(null, queryJson);
                if (Ienum == null && Ienum.Count() == 0)
                {
                    return 0;
                }
                DataTable dt = new DataTable();
                //添加列
                dt.Columns.Add("fgoodsid", typeof(string));//材料编号
                dt.Columns.Add("funit", typeof(string));//单位
                dt.Columns.Add("fname1", typeof(string));//名称规格
                dt.Columns.Add("fcount", typeof(string));//库存数量
                dt.Columns.Add("fprice", typeof(string));//平均价格
                dt.Columns.Add("fmoney", typeof(string));//总金额
                dt.Columns.Add("fplace", typeof(string));//存放位置

                double fcount = 0;
                decimal fmoney = 0;
                foreach (GoodsinfoModel item in Ienum)
                {
                    DataRow dr = dt.NewRow();

                    dr["fgoodsid"] = item.fgoodsid;
                    dr["funit"] = item.funit;
                    dr["fname1"] = item.fname1;
                    dr["fcount"] = item.fcount;
                    dr["fprice"] = string.Format("{0:0.##}", item.fprice);
                    dr["fmoney"] = string.Format("{0:0.##}", item.fmoney);
                    dr["fplace"] = item.fplace;

                    dt.Rows.Add(dr);

                    fcount += item.fcount;
                    fmoney += item.fmoney;
                }

                DataRow drs = dt.NewRow();
                drs["fgoodsid"] = "";
                drs["funit"] = "";
                drs["fname1"] = "合计";
                drs["fcount"] = fcount;
                drs["fprice"] = "";
                drs["fmoney"] = string.Format("{0:0.##}", fmoney);
                drs["fplace"] = "";
                dt.Rows.Add(drs);

                //设置导出格式
                ExcelConfig excelconfig = new ExcelConfig();
                excelconfig.Title = string.Format("{0:yyyy-MM-dd}", DateTime.Now) + " 物资库存记录";
                excelconfig.TitleFont = "";
                excelconfig.TitlePoint = 25;
                excelconfig.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                excelconfig.IsAllSizeColumn = true;
                //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                excelconfig.ColumnEntity = listColumnEntity;
                ColumnEntity columnentity = new ColumnEntity();

                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fgoodsid", ExcelColumn = "材料编号", Width = 15 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "funit", ExcelColumn = "单位", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fname1", ExcelColumn = "名称规格", Width = 25 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fcount", ExcelColumn = "库存数量", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fprice", ExcelColumn = "平均价格", Width = 15 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fmoney", ExcelColumn = "总金额", Width = 15 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fplace", ExcelColumn = "存放位置", Width = 25 });

                //调用导出方法
                ExcelHelper.ExcelDownload(dt, excelconfig);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 物质库存统计报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<GoodsinfoReportModel> GetReportGoods(Pagination pagination, string queryJson)
        {
            List<GoodsinfoReportModel> result = new List<GoodsinfoReportModel>();
            IEnumerable<GoodsinfoModel> goods = service.GetPageList(pagination, queryJson);
            if (goods != null && goods.Count() > 0)
            {
                List<string> goodsIds = new List<string>();
                foreach (GoodsinfoModel item in goods)
                {
                    GoodsinfoReportModel mod = new GoodsinfoReportModel();
                    mod.fgoodsid = item.fgoodsid;
                    mod.fname = item.fname;
                    result.Add(mod);

                    goodsIds.Add(item.fgoodsid);
                }

                var queryParam = queryJson.ToJObject();

                // 入库数据
                var inbills = inbillIService.GetMonthInbill(goodsIds, Convert.ToInt32(queryParam["year"]));
                if (inbills != null && inbills.Count() > 0)
                {
                    foreach (var good in result)
                    {
                        foreach (var inbill in inbills)
                        {
                            if (inbill.GoodsId.ToString() == good.fgoodsid)
                            {
                                
                            }
                        }
                    }
                }


                // 出库数据
                var outbills = outbillIService.GetMonthInbill(goodsIds, Convert.ToInt32(queryParam["year"]));
            }

            return result;
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="ftypecode">类型编号</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, string ftypecode, GoodsinfoEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, ftypecode, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 提交数据
    }
}