using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Application.Service.RepostryManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using HuRongClub.Application.Entity.RepostryManage;
using Newtonsoft.Json.Linq;
using HuRongClub.Util.Offices;
using HuRongClub.Cache.Factory;

namespace HuRongClub.Application.Busines.RepostryManage
{
    public class FeereportBLL
    {
        private FeereportService service = new FeereportService();
        /// <summary>
        /// 获取列表（缓存保存1个小时）
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ChargeModel> GetChargeListJson(string queryJson)
        {
            #region 使用缓存的方法提高查询速度 不建议使用
            var queryParam = queryJson.ToJObject();
            string userStatus = queryParam["UserStatues"].ToString();
            string floornum = queryParam["FloorNum"].ToString();
            string propertyId = queryParam["propertyID"].ToString();
            string year = queryParam["YearNum"].ToString();
            string month = queryParam["MonthNum"].ToString();
            string feeitem = queryParam["feeitem"].ToString();

            string cacheKey = CacheFactory.CacheKey().Charge(userStatus, floornum, propertyId, year, month, feeitem);

            IList<ChargeModel> cache = CacheFactory.Cache().GetCache<List<ChargeModel>>(cacheKey);
            if (cache == null)
            {
                IEnumerable<ChargeModel> list = service.GetChargeListJson(queryJson);
                if (list != null)
                {
                    CacheFactory.Cache().WriteCache<IList<ChargeModel>>(list.ToList(), cacheKey, DateTime.Now.AddHours(1));
                }
            }

            return CacheFactory.Cache().GetCache<List<ChargeModel>>(cacheKey);
            #endregion

            //return service.GetChargeListJson(queryJson);
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DelayanaModel> GetzhanglingListJson(string queryJson)
        {
            return service.GetzhanglingListJson(queryJson);
        }


        /// <summary>
        /// 获取欠费 统计 老
        /// Author:王伟
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetqianfeiDistrictListJson(string queryJson)
        {
            IEnumerable<DistrictModel> Newlist = service.GetArrearageStatistics(queryJson);

            var queryParam = queryJson.ToJObject();
            DataTable dt = new DataTable();
            dt.Columns.Add("property_name", typeof(string));
            //string[] ParamFee = JsonHelper.JSONToObject<string[]>(queryParam["FeeName"].ToString());
            //string[] Parampro = JsonHelper.JSONToObject<string[]>(queryParam["propertyID"].ToString());
            string[] ParamFee = queryParam["FeeName"].ToString().Split(',');
            string[] Parampro = queryParam["propertyID"].ToString().Split(',');

            if (ParamFee != null && ParamFee.Length > 0)
            {
                foreach (var Feeid in ParamFee)
                {
                    dt.Columns.Add("yinshou_" + Feeid, typeof(decimal));
                    dt.Columns.Add("shoushi_" + Feeid, typeof(decimal));
                    dt.Columns.Add("zhuijiao_" + Feeid, typeof(string));
                    dt.Columns.Add("yue_" + Feeid, typeof(decimal));
                }
            }
            if (Parampro != null && ParamFee.Length > 0)
            {
                foreach (var propertyID in Parampro)
                {
                    DataRow dr = dt.NewRow();
                    if (ParamFee != null && ParamFee.Length > 0)
                    {
                        foreach (var Feeid in ParamFee)
                        {
                            IEnumerable<DistrictModel> rowlist = Newlist.Where(d => d.feeitem_id == Feeid.ToString() && d.property_id == propertyID.ToString()).ToList();
                            if (rowlist != null && rowlist.Count() > 0)
                            {
                                foreach (var dd in rowlist)
                                {
                                    dr["yinshou_" + Feeid] = dd.yinshou;
                                    dr["shoushi_" + Feeid] = dd.shishou;
                                    string zj = (dd.zhuijiao * 10000).ToString();
                                    if (zj.IndexOf('.') != -1)
                                    {
                                        zj = zj.Substring(0, zj.IndexOf('.'));
                                    }
                                    if (zj.Length == 0) zj = "0";
                                    dr["zhuijiao_" + Feeid] = (Convert.ToInt32(zj) / (decimal)100).ToString() + "%";
                                    dr["yue_" + Feeid] = dd.yue;
                                    dr["property_name"] = dd.property_name;
                                }
                            }
                            else
                            {
                                dr["yinshou_" + Feeid] = 0;
                                dr["shoushi_" + Feeid] = 0;
                                dr["zhuijiao_" + Feeid] = "0%";
                                dr["yue_" + Feeid] = 0;
                            }
                        }
                    }
                    if (dr["property_name"] != null && dr["property_name"].ToString() != "")
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 获取应收费用列表（筛选）
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPercentListJson(string queryJson)
        {
            IEnumerable<DistrictModel> Newlist = service.GetPercentListJson(queryJson);

            var queryParam = queryJson.ToJObject();
            DataTable dt = new DataTable();
            dt.Columns.Add("property_name", typeof(string));
            //string[] ParamFee = JsonHelper.JSONToObject<string[]>(queryParam["FeeName"].ToString());
            //string[] Parampro = JsonHelper.JSONToObject<string[]>(queryParam["propertyID"].ToString());
            string[] ParamFee = queryParam["FeeName"].ToString().Split(',');
            string[] Parampro = queryParam["propertyID"].ToString().Split(',');
            if (Newlist != null && Newlist.Count() > 0)
            {
                if (ParamFee != null && ParamFee.Length > 0)
                {
                    foreach (var Feeid in ParamFee)
                    {
                        dt.Columns.Add("yinshou_" + Feeid, typeof(decimal));
                        dt.Columns.Add("shoushi_" + Feeid, typeof(decimal));
                        dt.Columns.Add("zhuijiao_" + Feeid, typeof(string));
                        dt.Columns.Add("yue_" + Feeid, typeof(decimal));
                    }
                }
                if (Parampro != null && ParamFee.Length > 0)
                {
                    foreach (var propertyID in Parampro)
                    {
                        DataRow dr = dt.NewRow();
                        if (ParamFee != null && ParamFee.Length > 0)
                        {
                            foreach (var Feeid in ParamFee)
                            {
                                IEnumerable<DistrictModel> rowlist = Newlist.Where(d => d.feeitem_id == Feeid.ToString() && d.property_id == propertyID.ToString()).ToList();
                                if (rowlist != null && rowlist.Count() > 0)
                                {
                                    foreach (var dd in rowlist)
                                    {
                                        dr["yinshou_" + Feeid] = dd.yinshou;
                                        dr["shoushi_" + Feeid] = dd.shishou;
                                        dr["zhuijiao_" + Feeid] = (Convert.ToInt32(dd.zhuijiao * 10000) / (decimal)100).ToString() + "%";
                                        dr["yue_" + Feeid] = dd.yue;
                                        dr["property_name"] = dd.property_name;
                                    }
                                }
                                else
                                {
                                    dr["yinshou_" + Feeid] = 0;
                                    dr["shoushi_" + Feeid] = 0;
                                    dr["zhuijiao_" + Feeid] = "0%";
                                    dr["yue_" + Feeid] = 0;
                                }
                            }
                        }
                        if (dr["property_name"] != null && dr["property_name"].ToString() != "")
                        {
                            dt.Rows.Add(dr);
                        }
                    }
                }

                return dt;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获取应收费用列表（筛选）
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetDelayListJson(string queryJson)
        {
            IEnumerable<DelayDetail> Newlist = service.GetDelayListJson(queryJson);

            var queryParam = queryJson.ToJObject();
            DataTable dt = new DataTable();
            dt.Columns.Add("property_name", typeof(string));
            //string[] ParamFee = JsonHelper.JSONToObject<string[]>(queryParam["FeeName"].ToString());
            //string[] Parampro = JsonHelper.JSONToObject<string[]>(queryParam["propertyID"].ToString());
            string[] ParamFee = queryParam["FeeName"].ToString().Split(',');
            string[] Parampro = queryParam["propertyID"].ToString().Split(',');
            if (ParamFee != null && ParamFee.Length > 0)
            {
                foreach (var Feeid in ParamFee)
                {
                    dt.Columns.Add("linian_" + Feeid, typeof(decimal));
                    dt.Columns.Add("shangnian_" + Feeid, typeof(decimal));
                    dt.Columns.Add("dangnian_" + Feeid, typeof(string));
                    dt.Columns.Add("heji_" + Feeid, typeof(decimal));
                }
                dt.Columns.Add("zonghe", typeof(decimal));
            }
            if (Parampro != null && ParamFee.Length > 0)
            {
                foreach (var propertyID in Parampro)
                {
                    DataRow dr = dt.NewRow();
                    if (ParamFee != null && ParamFee.Length > 0)
                    {
                        decimal zonghe = 0;
                        foreach (var Feeid in ParamFee)
                        {
                            IEnumerable<DelayDetail> rowlist = Newlist.Where(d => d.feeitem_id == Feeid.ToString() && d.property_id == propertyID.ToString()).ToList();
                            foreach (var dd in rowlist)
                            {
                                dr["linian_" + Feeid] = dd.linian;
                                dr["shangnian_" + Feeid] = dd.shangnian;
                                dr["dangnian_" + Feeid] = dd.dangnian;
                                dr["heji_" + Feeid] = dd.heji;
                                dr["property_name"] = dd.property_name;
                                zonghe += dd.heji;
                            }
                        }
                        dr["zonghe"] = zonghe;
                    }
                    if (dr["property_name"] != null && dr["property_name"].ToString() != "")
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 账龄统计
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<ArraersViewModel> GetArrearslistJson(string queryJson)
        {
            IEnumerable<Arrears_detailModel> YZList = service.GetarrearsListJson(queryJson);
            YZList = YZList.OrderBy(r => r.building_name);
            IEnumerable<Arrears_rentdetailModel> ZKList = service.GetarrearsrentListJson(queryJson);
            List<ArraersViewModel> ArraersViewModeList = new List<ArraersViewModel>();
            if (YZList != null && YZList.Count() > 0)
            {

                foreach (var YZModel in YZList)
                {
                    ArraersViewModel ArraersViewModel = new ArraersViewModel();
                    ArraersViewModel.Type_name = "业主";
                    ArraersViewModel.building_name = YZModel.building_name;
                    ArraersViewModel.CountNum = YZModel.hhh;
                    ArraersViewModel.room_name = YZModel.room_name;
                    ArraersViewModel.ArrearageAmount = YZModel.fee_roomarrears;
                    ArraersViewModel.owner_name = YZModel.owner_name;
                    ArraersViewModel.room_id = YZModel.room_id;
                    ArraersViewModeList.Add(ArraersViewModel);

                }
            }
            if (ZKList != null && ZKList.Count() > 0)
            {
                foreach (var ZKModel in ZKList)
                {
                    ArraersViewModel ArraersViewModel = new ArraersViewModel();
                    ArraersViewModel.Type_name = "租客";
                    ArraersViewModel.building_name = "租客";
                    ArraersViewModel.CountNum = ZKModel.hhh;
                    ArraersViewModel.ArrearageAmount = ZKModel.fee_roomarrears;
                    ArraersViewModel.CustomerName = ZKModel.customername;
                    ArraersViewModel.rentcontract_id = ZKModel.rentcontract_id;
                    ArraersViewModeList.Add(ArraersViewModel);
                }
            }
            return ArraersViewModeList;
        }

        public IEnumerable<ArraersDetailModel> GetArrearsDetailJson(string queryJson, Pagination pagination)
        {
            return service.GetArrearsDetailJson(queryJson, pagination);
        }

        /// <summary>
        /// 统计租户月收费中心数据
        /// </summary>
        /// <param name="queryJson">条件</param>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        public DataTable GetRentreportList(string queryJson, string property_id)
        {
            if (!string.IsNullOrEmpty(queryJson) && !string.IsNullOrEmpty(property_id))
            {
                IEnumerable<RentreportEntity.RentreportListEntity> list = service.GetRentreportList(property_id);
                if (list != null && list.Count() > 0)
                {
                    var queryParam = queryJson.ToJObject();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("room_name", typeof(string));
                    dt.Columns.Add("customername", typeof(string));
                    dt.Columns.Add("rentarea", typeof(string));

                    dt.Columns.Add("cshouldToalt", typeof(string));//应收合计
                    dt.Columns.Add("creceiveToalt", typeof(string));//实收合计
                    List<JToken> ParamFee = queryParam["FeeName"].ToList();
                    if (ParamFee != null && ParamFee.Count > 0)
                    {
                        foreach (JValue Feeid in ParamFee)
                        {
                            dt.Columns.Add("cshould_" + Feeid.Value, typeof(string));
                            dt.Columns.Add("creceive_" + Feeid.Value, typeof(string));
                        }
                    }
                    string contractid = "";
                    foreach (RentreportEntity.RentreportListEntity item in list)
                    {
                        contractid += item.contractid + ",";
                    }
                    if (!string.IsNullOrEmpty(contractid))
                    {
                        contractid = contractid.Substring(0, contractid.Length - 1);
                    }
                    IEnumerable<RentreportEntity.RentreportExtEntity> list_ext = service.GetRentreportList(queryJson, contractid);
                    foreach (RentreportEntity.RentreportListEntity item in list)
                    {
                        DataRow dr = dt.NewRow();
                        decimal cshouldToalt = 0;
                        decimal creceiveToalt = 0;

                        dr["room_name"] = item.room_name;
                        dr["customername"] = item.customername;
                        dr["rentarea"] = string.Format("{0:0.##}", item.rentarea);
                        foreach (JValue Feeid in ParamFee)
                        {
                            IEnumerable<RentreportEntity.RentreportExtEntity> rowlist = list_ext.Where(t => t.rentcontract_id == item.contractid && t.feeitem_id == Feeid.Value.ToString()).ToList();
                            if (rowlist != null && rowlist.Count() > 0)
                            {
                                dr["cshould_" + Feeid.Value] = string.Format("{0:0.##}", rowlist.First().cshould);
                                dr["creceive_" + Feeid.Value] = string.Format("{0:0.##}", rowlist.First().creceive);

                                cshouldToalt += rowlist.First().cshould;
                                creceiveToalt += rowlist.First().creceive;
                            }
                            else
                            {
                                dr["cshould_" + Feeid] = 0;
                                dr["creceive_" + Feeid] = 0;
                            }
                        }
                        dr["cshouldToalt"] = string.Format("{0:0.##}", cshouldToalt);
                        dr["creceiveToalt"] = string.Format("{0:0.##}", creceiveToalt);

                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 欠费导出
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public int ExportArrears(string queryJson)
        {
            DataTable dt = new DataTable();
            //添加列
            dt.Columns.Add("type", typeof(string));//类型
            dt.Columns.Add("building_name", typeof(string));//所属楼栋
            dt.Columns.Add("name", typeof(string));//业主/租客
            dt.Columns.Add("feeitem_name", typeof(string));//费用名称
            dt.Columns.Add("fee_years", typeof(string));//应收日期
            dt.Columns.Add("fee_arrears", typeof(string));//欠费金额

            #region 业主
            IEnumerable<ArrearsExportEntity> ienum_yz = service.ExportArrearsByYZ(queryJson);
            if (ienum_yz != null && ienum_yz.Count() > 0)
            {
                foreach (ArrearsExportEntity item in ienum_yz)
                {
                    DataRow dr = dt.NewRow();

                    dr["type"] = "业主";
                    dr["building_name"] = item.building_name + "/" + item.room_name;
                    dr["name"] = item.owner_name;
                    dr["feeitem_name"] = item.feeitem_name;
                    dr["fee_years"] = string.Format("{0:yyyy-MM}", item.fee_years);
                    dr["fee_arrears"] = string.Format("{0:0.##}", item.fee_arrears);

                    dt.Rows.Add(dr);
                }
            }
            #endregion

            #region 租户
            IEnumerable<ArrearsExportByZHEntity> ienum_zh = service.ExportArrearsByZH(queryJson);
            if (ienum_zh != null && ienum_zh.Count() > 0)
            {
                foreach (ArrearsExportByZHEntity item in ienum_zh)
                {
                    DataRow dr = dt.NewRow();

                    dr["type"] = "租客";
                    dr["building_name"] = "";
                    dr["name"] = item.customername;
                    dr["feeitem_name"] = item.feeitem_name;
                    dr["fee_years"] = string.Format("{0:yyyy-MM}", item.fee_years);
                    dr["fee_arrears"] = string.Format("{0:0.##}", item.fee_arrears);

                    dt.Rows.Add(dr);
                }
            }
            #endregion

            if (dt != null && dt.Rows.Count > 0)
            {
                //设置导出格式
                ExcelConfig excelconfig = new ExcelConfig();
                excelconfig.Title = null;
                excelconfig.TitleFont = "";
                excelconfig.TitlePoint = 25;
                excelconfig.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                excelconfig.IsAllSizeColumn = true;
                //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                excelconfig.ColumnEntity = listColumnEntity;
                ColumnEntity columnentity = new ColumnEntity();

                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "type", ExcelColumn = "类型", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "building_name", ExcelColumn = "所属楼栋", Width = 20 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "name", ExcelColumn = "业主/租客", Width = 20 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "feeitem_name", ExcelColumn = "费用名称", Width = 15 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fee_years", ExcelColumn = "应收日期", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fee_arrears", ExcelColumn = "欠费金额", Width = 10 });


                //调用导出方法
                ExcelHelper.ExcelDownload(dt, excelconfig);

                return 1;

            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 导出统计数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public bool GetExportList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            IEnumerable<ChargeModel> list = GetChargeListJson(queryJson);
            if (list != null && list.Count() > 0)
            {
                string userStatus = queryParam["UserStatues"].ToString();
                string floornum = queryParam["FloorNum"].ToString();

                //设置导出格式
                ExcelConfig excelconfig = new ExcelConfig();
                excelconfig.Title = null;
                excelconfig.TitleFont = "";
                excelconfig.TitlePoint = 25;
                excelconfig.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                excelconfig.IsAllSizeColumn = true;
                //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                excelconfig.ColumnEntity = listColumnEntity;
                ColumnEntity columnentity = new ColumnEntity();

                if (userStatus.Equals("1"))
                {
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "customer", ExcelColumn = "租户", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "lnq", ExcelColumn = "历年欠", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shnq", ExcelColumn = "上年欠", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "dnq", ExcelColumn = "当年欠", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "dyysh", ExcelColumn = "当月应收", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "yshhj", ExcelColumn = "应收合计", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shlnq", ExcelColumn = "收历年欠", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shshyq", ExcelColumn = "收上年欠", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shdnq", ExcelColumn = "收当年欠", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shdy", ExcelColumn = "收当月", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ysh", ExcelColumn = "预收", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shshhj", ExcelColumn = "实收合计", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "yshzhr", ExcelColumn = "预收转入", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "yshye", ExcelColumn = "预收余额", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "qshhj", ExcelColumn = "欠收合计", Width = 10 });
                }
                else
                {
                    if (String.IsNullOrEmpty(floornum))
                    {
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "building_name", ExcelColumn = "楼栋名称", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "lnq", ExcelColumn = "历年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shnq", ExcelColumn = "上年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "dnq", ExcelColumn = "当年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "dyysh", ExcelColumn = "当月应收", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "yshhj", ExcelColumn = "应收合计", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shlnq", ExcelColumn = "收历年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shshyq", ExcelColumn = "收上年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shdnq", ExcelColumn = "收当年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shdy", ExcelColumn = "收当月", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ysh", ExcelColumn = "预收", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shshhj", ExcelColumn = "实收合计", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "yshzhr", ExcelColumn = "预收转入", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "yshye", ExcelColumn = "预收余额", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "qshhj", ExcelColumn = "欠收合计", Width = 10 });
                    }
                    else
                    {
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "room_name", ExcelColumn = "室号", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "customername", ExcelColumn = "客户", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "lnq", ExcelColumn = "历年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shnq", ExcelColumn = "上年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "dnq", ExcelColumn = "当年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "dyysh", ExcelColumn = "当月应收", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "yshhj", ExcelColumn = "应收合计", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shlnq", ExcelColumn = "收历年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shshyq", ExcelColumn = "收上年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shdnq", ExcelColumn = "收当年欠", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shdy", ExcelColumn = "收当月", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ysh", ExcelColumn = "预收", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "shshhj", ExcelColumn = "实收合计", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "yshzhr", ExcelColumn = "预收转入", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "yshye", ExcelColumn = "预收余额", Width = 10 });
                        excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "qshhj", ExcelColumn = "欠收合计", Width = 10 });
                    }
                }


                DataTable dt = DataHelper.ListToDataTable<ChargeModel>((List<ChargeModel>)list);
                //调用导出方法
                ExcelHelper.ExcelDownload(dt, excelconfig);


                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
