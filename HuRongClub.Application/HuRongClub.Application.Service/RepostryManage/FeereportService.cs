using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.RepostryManage;
using HuRongClub.Application.Service.TenementManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.RepostryManage
{
    /// <summary>
    /// 统计
    /// </summary>
    public class FeereportService : RepositoryFactory, FeereportIService
    {
        /// <summary>
        /// 收费统计
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<ChargeModel> GetChargeListJson(string queryJson)
        {
            RepositoryFactory<ChargeModel> repository = new RepositoryFactory<ChargeModel>();
            var queryParam = queryJson.ToJObject();

            // 业主
            if (queryParam["UserStatues"].ToString() == "0")
            {
                #region 业主

                List<ChargeModel> chargelist = new List<ChargeModel>();
                StringBuilder strsql = new StringBuilder();

                // 判断是否选择了楼栋
                if (queryParam["FloorNum"].IsEmpty())
                {
                    strsql.Append("SELECT building_id FROM wy_building WHERE property_id=" + queryParam["propertyID"].ToString());
                    DataTable dt = this.BaseRepository().FindTable(strsql.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            StringBuilder strsql1 = new StringBuilder();
                            strsql1.Append(" exec tj_BuildingIncome @buildingid,@dtyear,@dtmonth,@feeitem ");
                            SqlParameter[] parameters = {
                            new SqlParameter("@buildingid", SqlDbType.VarChar),
                            new SqlParameter("@dtyear", SqlDbType.Int,4),
                            new SqlParameter("@dtmonth", SqlDbType.Int,4),
                            new SqlParameter("@feeitem", SqlDbType.VarChar)};

                            parameters[0].Value = dt.Rows[i]["building_id"];
                            parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
                            parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
                            parameters[3].Value = queryParam["feeitem"].ToString();

                            RepositoryFactory response = new RepositoryFactory();
                            ChargeModel item = response.BaseRepository().FindList<ChargeModel>(strsql1.ToString(), parameters).FirstOrDefault();
                            if (item != null)
                            {
                                chargelist.Add(item);
                            }
                        }
                    }
                    return chargelist;
                }
                else
                {
                    strsql.Append("SELECT room_id FROM wy_room WHERE building_id=" + queryParam["FloorNum"].ToString());
                    DataTable dt = this.BaseRepository().FindTable(strsql.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            StringBuilder strsql1 = new StringBuilder();
                            strsql1.Append(" exec tj_RoomIncome @roomid,@dtyear,@dtmonth,@feeitem ");
                            SqlParameter[] parameters = {
                            new SqlParameter("@roomid", SqlDbType.VarChar),
                            new SqlParameter("@dtyear", SqlDbType.Int,4),
                            new SqlParameter("@dtmonth", SqlDbType.Int,4),
                            new SqlParameter("@feeitem", SqlDbType.VarChar)};
                            parameters[0].Value = dt.Rows[i]["room_id"];
                            parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
                            parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
                            parameters[3].Value = queryParam["feeitem"].ToString();

                            RepositoryFactory response = new RepositoryFactory();
                            ChargeModel item = response.BaseRepository().FindList<ChargeModel>(strsql1.ToString(), parameters).FirstOrDefault();
                            if (item != null)
                            {
                                chargelist.Add(item);
                            }
                        }
                    }
                    return chargelist;
                }

                #endregion
            }
            else if (queryParam["UserStatues"].ToString() == "1") // 租客
            {
                #region 租客

                List<ChargeModel> chargelist = new List<ChargeModel>();
                StringBuilder strsql = new StringBuilder();
                strsql.Append("SELECT contractid FROM wy_rentcontract WHERE property_id=" + queryParam["propertyID"].ToString() + " and status=1 ");
                DataTable dt = this.BaseRepository().FindTable(strsql.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StringBuilder strsql1 = new StringBuilder();
                        strsql1.Append(" exec tj_ContractIncome @contractid,@dtyear,@dtmonth,@feeitem ");
                        SqlParameter[] parameters = {
                            new SqlParameter("@contractid", SqlDbType.VarChar),
                            new SqlParameter("@dtyear", SqlDbType.Int,4),
                            new SqlParameter("@dtmonth", SqlDbType.Int,4),
                            new SqlParameter("@feeitem", SqlDbType.VarChar)};
                        parameters[0].Value = dt.Rows[i]["contractid"];
                        parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
                        parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
                        parameters[3].Value = queryParam["feeitem"].ToString();

                        RepositoryFactory response = new RepositoryFactory();
                        ChargeModel item = response.BaseRepository().FindList<ChargeModel>(strsql1.ToString(), parameters).FirstOrDefault();
                        if (item != null)
                        {
                            chargelist.Add(item);
                        }
                    }
                }
                return chargelist;

                #endregion
            }
            else // 业主和租客
            {
                #region 业主和租客

                List<ChargeModel> list = new List<ChargeModel>();
                RepositoryFactory response = new RepositoryFactory();
                StringBuilder strSql = new StringBuilder();

                #region 业主

                // 判断是否选择了楼栋
                if (queryParam["FloorNum"].IsEmpty())
                {
                    strSql = new StringBuilder();
                    strSql.Append("SELECT building_id FROM wy_building WHERE property_id=" + queryParam["propertyID"].ToString());
                    DataTable dt = this.BaseRepository().FindTable(strSql.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            StringBuilder strsql1 = new StringBuilder();
                            strsql1.Append(" exec tj_BuildingIncome @buildingid,@dtyear,@dtmonth,@feeitem ");
                            SqlParameter[] parameters = {
                            new SqlParameter("@buildingid", SqlDbType.VarChar),
                            new SqlParameter("@dtyear", SqlDbType.Int,4),
                            new SqlParameter("@dtmonth", SqlDbType.Int,4),
                            new SqlParameter("@feeitem", SqlDbType.VarChar)};

                            parameters[0].Value = dt.Rows[i]["building_id"];
                            parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
                            parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
                            parameters[3].Value = queryParam["feeitem"].ToString();

                            ChargeModel item = response.BaseRepository().FindList<ChargeModel>(strsql1.ToString(), parameters).FirstOrDefault();
                            if (item != null)
                            {
                                list.Add(item);
                            }
                        }
                    }
                }
                else
                {
                    strSql = new StringBuilder();
                    strSql.Append("SELECT room_id FROM wy_room WHERE building_id=" + queryParam["FloorNum"].ToString());
                    DataTable dt = this.BaseRepository().FindTable(strSql.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            StringBuilder strsql1 = new StringBuilder();
                            strsql1.Append(" exec tj_RoomIncome @roomid,@dtyear,@dtmonth,@feeitem ");
                            SqlParameter[] parameters = {
                            new SqlParameter("@roomid", SqlDbType.VarChar),
                            new SqlParameter("@dtyear", SqlDbType.Int,4),
                            new SqlParameter("@dtmonth", SqlDbType.Int,4),
                            new SqlParameter("@feeitem", SqlDbType.VarChar)};
                            parameters[0].Value = dt.Rows[i]["room_id"];
                            parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
                            parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
                            parameters[3].Value = queryParam["feeitem"].ToString();

                            ChargeModel item = response.BaseRepository().FindList<ChargeModel>(strsql1.ToString(), parameters).FirstOrDefault();
                            if (item != null)
                            {
                                list.Add(item);
                            }
                        }
                    }
                }

                #endregion

                #region 租客

                strSql = new StringBuilder();
                strSql.Append("SELECT contractid FROM wy_rentcontract WHERE property_id=" + queryParam["propertyID"].ToString() + " and status=1 ");
                DataTable ren_dt = this.BaseRepository().FindTable(strSql.ToString());
                if (ren_dt != null && ren_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < ren_dt.Rows.Count; i++)
                    {
                        StringBuilder strsql1 = new StringBuilder();
                        strsql1.Append(" exec tj_ContractIncome @contractid,@dtyear,@dtmonth,@feeitem ");
                        SqlParameter[] parameters = {
                            new SqlParameter("@contractid", SqlDbType.VarChar),
                            new SqlParameter("@dtyear", SqlDbType.Int,4),
                            new SqlParameter("@dtmonth", SqlDbType.Int,4),
                            new SqlParameter("@feeitem", SqlDbType.VarChar)};
                        parameters[0].Value = ren_dt.Rows[i]["contractid"];
                        parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
                        parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
                        parameters[3].Value = queryParam["feeitem"].ToString();

                        ChargeModel item = response.BaseRepository().FindList<ChargeModel>(strsql1.ToString(), parameters).FirstOrDefault();
                        if (item != null)
                        {
                            item.building_name = item.customer;
                            item.room_name = item.customer;
                            item.customername = item.customer;
                            list.Add(item);
                        }
                    }
                }

                #endregion

                return list;

                #endregion
            }
        }

        /// <summary>
        /// 账龄
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<DelayanaModel> GetzhanglingListJson(string queryJson)
        {
            RepositoryFactory<DelayanaModel> repository = new RepositoryFactory<DelayanaModel>();
            var queryParam = queryJson.ToJObject();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" exec tj_arrears_detail @propertyID,@dtyear,@dtmonth,'month' ");
            SqlParameter[] parameters = {
                    new SqlParameter("@propertyID", SqlDbType.VarChar),
                    new SqlParameter("@dtyear", SqlDbType.Int,4),
                    new SqlParameter("@dtmonth", SqlDbType.Int,4)};
            parameters[0].Value = queryParam["propertyID"].ToString();
            parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
            parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
            RepositoryFactory response = new RepositoryFactory();
            IEnumerable<DelayanaModel> list = response.BaseRepository().FindList<DelayanaModel>(strSql.ToString(), parameters);
            return list.Where(d => queryParam["FeeName"].ToList().Contains(d.feeitem_id)).ToList();
        }

        /// <summary>
        /// 欠费统计
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<DistrictModel> GetqianfeiDistrictListJson(string queryJson)
        {
            StringBuilder strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            strSql.Append(@"SELECT  ISNULL(SUM(yinshou), 0) AS yinshou ,
                                    ISNULL(SUM(shishou), 0) AS shishou ,
                                    CASE WHEN ISNULL(SUM(yinshou), 0) = 0 THEN 0
                                         ELSE ISNULL(SUM(shishou), 0) / ISNULL(SUM(yinshou), 0)
                                    END AS zhuijiao ,
                                    ISNULL(SUM(yinshou), 0) - ISNULL(SUM(x1.shishou), 0) AS yue ,
                                    feeitem_id ,
                                    property_id ,
                                    property_name
                            FROM    ( SELECT    ISNULL(SUM(fee_income), 0) AS yinshou ,
                                                0 AS shishou ,
                                                fee.feeitem_id ,
                                                fee.property_id ,
                                                pro.property_name
                                      FROM      wy_feeincome fee
                                                INNER JOIN dbo.wy_property pro ON pro.property_id = fee.property_id
                                      WHERE     fee_year < @dtyear
                                                AND ( fee_date IS NULL
                                                      OR DATEPART(YEAR, fee_date) >= @dtyear
                                                    )
                                      GROUP BY  fee.feeitem_id ,
                                                fee.property_id ,
                                                pro.property_name
                                      UNION ALL
                                      SELECT    0 AS yinshou ,
                                                ISNULL(SUM(fee_income), 0) AS shishou ,
                                                fee.feeitem_id ,
                                                fee.property_id ,
                                                pro.property_name
                                      FROM      wy_feeincome fee
                                                INNER JOIN dbo.wy_property pro ON pro.property_id = fee.property_id
                                      WHERE     fee_year < 2017
                                                AND DATEPART(YEAR, fee_date) = @dtyear
                                                AND DATEPART(MONTH, fee_date) <= @dtmonth
                                      GROUP BY  fee.feeitem_id ,
                                                fee.property_id ,
                                                pro.property_name
                                    ) x1
                            GROUP BY feeitem_id ,
                                    property_id ,
                                    x1.property_name");
            RepositoryFactory response = new RepositoryFactory();
            SqlParameter[] parameters = {
                    new SqlParameter("@dtyear", SqlDbType.Int,4),
                    new SqlParameter("@dtmonth", SqlDbType.Int,4)};
            parameters[0].Value = Convert.ToInt32(queryParam["YearNum"]);
            parameters[1].Value = Convert.ToInt32(queryParam["MonthNum"]);
            IEnumerable<DistrictModel> list = response.BaseRepository().FindList<DistrictModel>(strSql.ToString(), parameters);
            return list.Where(d => queryParam["propertyID"].ToString().Split(',').Contains(d.property_id) && queryParam["FeeName"].ToString().Split(',').Contains(d.feeitem_id)).ToList();
        }

        /// <summary>
        /// 获取欠费 统计
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<DistrictModel> GetArrearageStatistics(string queryJson)
        {
            List<DistrictModel> list = new List<DistrictModel>();
            RepositoryFactory response = new RepositoryFactory();

            var queryParam = queryJson.ToJObject();
            string[] ParamFee = queryParam["FeeName"].ToString().Split(',');
            string[] Parampro = queryParam["propertyID"].ToString().Split(',');

            foreach (var feeitem in ParamFee)
            {
                foreach (var propertyid in Parampro)
                {
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append(" exec tj_calc_propertyArrears @propertyid,@year,@month,@feeitem ");
                    SqlParameter[] parameters = {
                            new SqlParameter("@propertyid", SqlDbType.VarChar),
                            new SqlParameter("@year", SqlDbType.Int,4),
                            new SqlParameter("@month", SqlDbType.Int,4),
                            new SqlParameter("@feeitem",SqlDbType.VarChar)
                    };

                    parameters[0].Value = propertyid;
                    parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
                    parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
                    parameters[3].Value = feeitem;

                    IEnumerable<DistrictModel> dm = response.BaseRepository().FindList<DistrictModel>(strSql.ToString(), parameters);
                    if (dm != null)
                    {
                        // 获取物业信息
                        PropertyEntity property = new PropertyService().GetEntity(propertyid);

                        foreach (var item in dm.ToList<DistrictModel>())
                        {
                            item.property_id = propertyid;
                            item.property_name = property == null ? "" : property.property_name;
                            item.feeitem_id = feeitem;
                            item.yue = item.yinshou - item.shishou;

                            if (item.shishou != 0)
                            {
                                item.zhuijiao = item.shishou / item.yinshou;
                            }

                            list.Add(item);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 获取应收费用列表（筛选）
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<DistrictModel> GetPercentListJson(string queryJson)
        {
            StringBuilder strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            strSql.Append(@"SELECT  ISNULL(SUM(fee_income), 0) AS yinshou ,
                                    ISNULL(SUM(fee_already), 0) AS shishou ,
                                    CASE WHEN ISNULL(SUM(fee_income), 0) = 0 THEN 0
                                         ELSE ISNULL(SUM(fee_already), 0) / ISNULL(SUM(fee_income), 0)
                                    END AS zhuijiao ,
                                    ISNULL(SUM(fee_income), 0) - ISNULL(SUM(x1.fee_already), 0) AS yue ,
                                    feeitem_id ,
                                    property_id ,
                                    property_name
                            FROM    ( SELECT    ISNULL(SUM(fee_income), 0) AS fee_income ,
                                                0 AS fee_already ,
                                                fee.property_id ,
                                                pro.property_name ,
                                                fee.feeitem_id
                                      FROM      wy_feeincome fee
                                                INNER JOIN dbo.wy_property pro ON pro.property_id = fee.property_id
                                      WHERE     fee_year = @dtyear
                                                AND fee_month <= @dtmonth
                                      GROUP BY  fee.property_id ,
                                                pro.property_name ,
                                                fee.feeitem_id
                                      UNION ALL
                                      SELECT    0 AS fee_income ,
                                                ISNULL(SUM(fee_income), 0) AS fee_already ,
                                                fee.property_id ,
                                                pro.property_name ,
                                                fee.feeitem_id
                                      FROM      wy_feeincome fee
                                                INNER JOIN dbo.wy_property pro ON pro.property_id = fee.property_id
                                      WHERE     fee_year = @dtyear
                                                AND fee_month <= @dtmonth
                                                AND DATEDIFF(MONTH, fee_date, @dttmp) >= 0
                                      GROUP BY  fee.property_id ,
                                                pro.property_name ,
                                                fee.feeitem_id
                                    ) x1
                            GROUP BY feeitem_id ,
                                    property_id ,
                                    x1.property_name");

            RepositoryFactory response = new RepositoryFactory();
            SqlParameter[] parameters = {
                    new SqlParameter("@dtyear", SqlDbType.Int,4),
                    new SqlParameter("@dtmonth", SqlDbType.Int,4),
                     new SqlParameter("@dttmp", SqlDbType.DateTime)};
            parameters[0].Value = Convert.ToInt32(queryParam["YearNum"]);
            parameters[1].Value = Convert.ToInt32(queryParam["MonthNum"]);
            parameters[2].Value = Convert.ToDateTime(Convert.ToInt32(queryParam["YearNum"]).ToString() + "-" + Convert.ToInt32(queryParam["MonthNum"]).ToString() + "-" + "01");
            IEnumerable<DistrictModel> list = response.BaseRepository().FindList<DistrictModel>(strSql.ToString(), parameters);
            return list.Where(d => queryParam["propertyID"].ToString().Split(',').Contains(d.property_id) && queryParam["FeeName"].ToString().Split(',').Contains(d.feeitem_id)).ToList();
        }

        /// <summary>
        /// 获取应收费用列表（筛选）
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<DelayDetail> GetDelayListJson(string queryJson)
        {
            StringBuilder strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            strSql.Append(@"SELECT  ISNULL(SUM(x1.fee_linian), 0) AS linian ,
                                    ISNULL(SUM(x1.fee_shangnian), 0) AS shangnian ,
                                    ISNULL(SUM(x1.fee_dangnian), 0) AS dangnian ,
                                    ISNULL(SUM(x1.fee_linian), 0) + ISNULL(SUM(x1.fee_shangnian), 0)
                                    + ISNULL(SUM(x1.fee_dangnian), 0) AS heji ,
                                    x1.feeitem_id ,
                                    x1.property_id ,
                                    x1.property_name
                            FROM    ( SELECT    fee.property_id ,
                                                pro.property_name ,
                                                fee.feeitem_id ,
                                                ISNULL(SUM(fee_income), 0) AS fee_linian ,
                                                0 AS fee_shangnian ,
                                                0 AS fee_dangnian
                                      FROM      wy_feeincome fee
                                                INNER JOIN wy_property pro ON pro.property_id = fee.property_id
                                      WHERE     fee.fee_income > 0
                                                AND DATEDIFF(MONTH, pay_enddate, @dttmp) >= 0
                                                AND ( fee_date IS NULL
                                                      OR DATEDIFF(MONTH, @dttmp, fee_date) > 0
                                                    )
                                                AND fee.fee_year < @dtyear - 1
                                      GROUP BY  fee.property_id ,
                                                pro.property_name ,
                                                fee.feeitem_id
                                      UNION ALL
                                      SELECT    fee.property_id ,
                                                pro.property_name ,
                                                fee.feeitem_id ,
                                                0 AS fee_linian ,
                                                ISNULL(SUM(fee_income), 0) AS fee_shangnian ,
                                                0 AS fee_dangnian
                                      FROM      wy_feeincome fee
                                                INNER JOIN wy_property pro ON pro.property_id = fee.property_id
                                      WHERE     fee.fee_income > 0
                                                AND DATEDIFF(MONTH, pay_enddate, @dttmp) >= 0
                                                AND ( fee_date IS NULL
                                                      OR DATEDIFF(MONTH, @dttmp, fee_date) > 0
                                                    )
                                                AND fee.fee_year = @dtyear - 1
                                      GROUP BY  fee.property_id ,
                                                pro.property_name ,
                                                fee.feeitem_id
                                      UNION ALL
                                      SELECT    fee.property_id ,
                                                pro.property_name ,
                                                fee.feeitem_id ,
                                                0 AS fee_linian ,
                                                0 AS fee_shangnian ,
                                                ISNULL(SUM(fee_income), 0) AS fee_dangnian
                                      FROM      wy_feeincome fee
                                                INNER JOIN wy_property pro ON pro.property_id = fee.property_id
                                      WHERE     fee.fee_income > 0
                                                AND DATEDIFF(MONTH, pay_enddate, @dttmp) >= 0
                                                AND ( fee_date IS NULL
                                                      OR DATEDIFF(MONTH, @dttmp, fee_date) > 0
                                                    )
                                                AND fee.fee_year = @dtyear
                                      GROUP BY  fee.property_id ,
                                                pro.property_name ,
                                                fee.feeitem_id
                                    ) x1
                            GROUP BY x1.feeitem_id ,
                                    x1.property_id ,
                                    x1.property_name");
            RepositoryFactory response = new RepositoryFactory();
            SqlParameter[] parameters = {
                    new SqlParameter("@dtyear", SqlDbType.Int,4),
                    new SqlParameter("@dtmonth", SqlDbType.Int,4),
                     new SqlParameter("@dttmp", SqlDbType.DateTime)};
            parameters[0].Value = Convert.ToInt32(queryParam["YearNum"]);
            parameters[1].Value = Convert.ToInt32(queryParam["MonthNum"]);
            parameters[2].Value = Convert.ToDateTime(Convert.ToInt32(queryParam["YearNum"]).ToString() + "-" + Convert.ToInt32(queryParam["MonthNum"]).ToString() + "-" + "01");
            IEnumerable<DelayDetail> list = response.BaseRepository().FindList<DelayDetail>(strSql.ToString(), parameters);
            return list.Where(d => queryParam["propertyID"].ToString().Split(',').Contains(d.property_id) && queryParam["FeeName"].ToString().Split(',').Contains(d.feeitem_id)).ToList();
        }

        /// <summary>
        /// 账龄统计
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<Arrears_detailModel> GetarrearsListJson(string queryJson)
        {
            RepositoryFactory<ChargeModel> repository = new RepositoryFactory<ChargeModel>();
            var queryParam = queryJson.ToJObject();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" EXEC tj_arrears_detail_dur_new @propertyID,@dtyear,@dtmonth,@groupfield,@durmonth,@feeitemId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@propertyID", SqlDbType.VarChar),
                    new SqlParameter("@dtyear", SqlDbType.Int,4),
                    new SqlParameter("@dtmonth", SqlDbType.Int,4),
                    new SqlParameter("@groupfield", SqlDbType.VarChar),
                    new SqlParameter("@durmonth", SqlDbType.Int,4),
                    new SqlParameter("@feeitemId", SqlDbType.VarChar)};
            parameters[0].Value = queryParam["propertyID"].ToString();
            parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
            parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
            parameters[3].Value = "room_id";
            parameters[4].Value = Convert.ToInt32(queryParam["DMonthNum"]);
            if (queryParam["feeitemId"] != null && !string.IsNullOrEmpty(queryParam["feeitemId"].ToString()))
            {
                parameters[5].Value = queryParam["feeitemId"].ToString();
            }
            else
            {
                parameters[5].Value = "";
            }
            RepositoryFactory response = new RepositoryFactory();
            return response.BaseRepository().FindList<Arrears_detailModel>(strSql.ToString(), parameters);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<Arrears_rentdetailModel> GetarrearsrentListJson(string queryJson)
        {
            RepositoryFactory<ChargeModel> repository = new RepositoryFactory<ChargeModel>();
            var queryParam = queryJson.ToJObject();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" EXEC tj_arrears_rentdetail_dur_new @propertyID,@dtyear,@dtmonth,@groupfield,@durmonth,@feeitemId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@propertyID", SqlDbType.VarChar),
                    new SqlParameter("@dtyear", SqlDbType.Int,4),
                    new SqlParameter("@dtmonth", SqlDbType.Int,4),
                    new SqlParameter("@groupfield", SqlDbType.VarChar),
                    new SqlParameter("@durmonth", SqlDbType.Int,4),
                    new SqlParameter("@feeitemId", SqlDbType.VarChar)};
            parameters[0].Value = queryParam["propertyID"].ToString();
            parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
            parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
            parameters[3].Value = "contract_id";
            parameters[4].Value = Convert.ToInt32(queryParam["DMonthNum"]);
            if (queryParam["feeitemId"] != null && !string.IsNullOrEmpty(queryParam["feeitemId"].ToString()))
            {
                parameters[5].Value = queryParam["feeitemId"].ToString();
            }
            else
            {
                parameters[5].Value = "";
            }

            RepositoryFactory response = new RepositoryFactory();
            return response.BaseRepository().FindList<Arrears_rentdetailModel>(strSql.ToString(), parameters);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="queryJson"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IEnumerable<ArraersDetailModel> GetArrearsDetailJson(string queryJson, Pagination pagination)
        {
            var queryParam = queryJson.ToJObject();
            if (queryParam["type"].ToString() == "1")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" EXEC tj_arrears_detail_dur_new @propertyID,@dtyear,@dtmonth,@groupfield,@durmonth,@feeitemId ");
                SqlParameter[] parameters = {
                    new SqlParameter("@propertyID", SqlDbType.VarChar),
                    new SqlParameter("@dtyear", SqlDbType.Int,4),
                    new SqlParameter("@dtmonth", SqlDbType.Int,4),
                    new SqlParameter("@groupfield", SqlDbType.VarChar),
                    new SqlParameter("@durmonth", SqlDbType.Int,4),
                    new SqlParameter("@feeitemId", SqlDbType.VarChar)};
                parameters[0].Value = queryParam["propertyID"].ToString();
                parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
                parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
                parameters[3].Value = "";
                parameters[4].Value = Convert.ToInt32(queryParam["DMonthNum"]);
                if (queryParam["feeitemId"] != null && !string.IsNullOrEmpty(queryParam["feeitemId"].ToString()))
                {
                    parameters[5].Value = queryParam["feeitemId"].ToString();
                }
                else
                {
                    parameters[5].Value = "";
                }

                RepositoryFactory response = new RepositoryFactory();
                IEnumerable<ArraersDetailModel> list = response.BaseRepository().FindList<ArraersDetailModel>(strSql.ToString(), parameters);
                if (!string.IsNullOrEmpty(pagination.sidx))
                {
                    if (pagination.sord == "desc")
                    {
                        return list.Where(d => queryParam["keyvalue"].ToString() == d.room_id).OrderByDescending(t => pagination.sidx == "feeitem_name" ? t.feeitem_name : (pagination.sidx == "fee_years" ? t.fee_years : t.fee_arrears)).ToList();
                    }
                    else
                    {
                        return list.Where(d => queryParam["keyvalue"].ToString() == d.room_id).OrderBy(t => pagination.sidx == "feeitem_name" ? t.feeitem_name : (pagination.sidx == "fee_years" ? t.fee_years : t.fee_arrears)).ToList();
                    }
                }
                else
                {
                    return list.Where(d => queryParam["keyvalue"].ToString() == d.room_id).ToList();
                }
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" EXEC tj_arrears_rentdetail_dur_new @propertyID,@dtyear,@dtmonth,@groupfield,@durmonth,@feeitemId ");
                SqlParameter[] parameters = {
                    new SqlParameter("@propertyID", SqlDbType.VarChar),
                    new SqlParameter("@dtyear", SqlDbType.Int,4),
                    new SqlParameter("@dtmonth", SqlDbType.Int,4),
                    new SqlParameter("@groupfield", SqlDbType.VarChar),
                    new SqlParameter("@durmonth", SqlDbType.Int,4),
                    new SqlParameter("@feeitemId", SqlDbType.VarChar)};
                parameters[0].Value = queryParam["propertyID"].ToString();
                parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
                parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
                parameters[3].Value = "";
                parameters[4].Value = Convert.ToInt32(queryParam["DMonthNum"]);
                if (queryParam["feeitemId"] != null && !string.IsNullOrEmpty(queryParam["feeitemId"].ToString()))
                {
                    parameters[5].Value = queryParam["feeitemId"].ToString();
                }
                else
                {
                    parameters[5].Value = "";
                }

                RepositoryFactory response = new RepositoryFactory();
                IEnumerable<ArraersDetailModel> list = response.BaseRepository().FindList<ArraersDetailModel>(strSql.ToString(), parameters);
                //return list.Where(d => queryParam["keyvalue"].ToString() == d.rentcontract_id).ToList();
                if (!string.IsNullOrEmpty(pagination.sidx))
                {
                    if (pagination.sord == "desc")
                    {
                        return list.Where(d => queryParam["keyvalue"].ToString() == d.rentcontract_id).OrderByDescending(t => pagination.sidx == "feeitem_name" ? t.feeitem_name : (pagination.sidx == "fee_years" ? t.fee_years : t.fee_arrears)).ToList();
                    }
                    else
                    {
                        return list.Where(d => queryParam["keyvalue"].ToString() == d.rentcontract_id).OrderBy(t => pagination.sidx == "feeitem_name" ? t.feeitem_name : (pagination.sidx == "fee_years" ? t.fee_years : t.fee_arrears)).ToList();
                    }
                }
                else
                {
                    return list.Where(d => queryParam["keyvalue"].ToString() == d.rentcontract_id).ToList();
                }
            }
        }

        /// <summary>
        /// 导出欠费统计--业主
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ArrearsExportEntity> ExportArrearsByYZ(string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" EXEC tj_arrears_detail_dur_new @propertyID,@dtyear,@dtmonth,@groupfield,@durmonth,@feeitemId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@propertyID", SqlDbType.VarChar),
                    new SqlParameter("@dtyear", SqlDbType.Int,4),
                    new SqlParameter("@dtmonth", SqlDbType.Int,4),
                    new SqlParameter("@groupfield", SqlDbType.VarChar),
                    new SqlParameter("@durmonth", SqlDbType.Int,4),
                    new SqlParameter("@feeitemId", SqlDbType.VarChar)};
            parameters[0].Value = queryParam["propertyID"].ToString();
            parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
            parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
            parameters[3].Value = "";
            parameters[4].Value = Convert.ToInt32(queryParam["DMonthNum"]);
            if (queryParam["feeitemId"] != null && !string.IsNullOrEmpty(queryParam["feeitemId"].ToString()))
            {
                parameters[5].Value = queryParam["feeitemId"].ToString();
            }
            else
            {
                parameters[5].Value = "";
            }

            RepositoryFactory response = new RepositoryFactory();

            return response.BaseRepository().FindList<ArrearsExportEntity>(strSql.ToString(), parameters).OrderBy(t => t.property_name).OrderBy(t => t.building_name).OrderBy(t => t.room_name).OrderBy(t => t.fee_years);
        }

        /// <summary>
        /// 导出欠费统计--租户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ArrearsExportByZHEntity> ExportArrearsByZH(string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" EXEC tj_arrears_rentdetail_dur_new @propertyID,@dtyear,@dtmonth,@groupfield,@durmonth,@feeitemId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@propertyID", SqlDbType.VarChar),
                    new SqlParameter("@dtyear", SqlDbType.Int,4),
                    new SqlParameter("@dtmonth", SqlDbType.Int,4),
                    new SqlParameter("@groupfield", SqlDbType.VarChar),
                    new SqlParameter("@durmonth", SqlDbType.Int,4),
                    new SqlParameter("@feeitemId", SqlDbType.VarChar)};
            parameters[0].Value = queryParam["propertyID"].ToString();
            parameters[1].Value = Convert.ToInt32(queryParam["YearNum"]);
            parameters[2].Value = Convert.ToInt32(queryParam["MonthNum"]);
            parameters[3].Value = "";
            parameters[4].Value = Convert.ToInt32(queryParam["DMonthNum"]);
            if (queryParam["feeitemId"] != null && !string.IsNullOrEmpty(queryParam["feeitemId"].ToString()))
            {
                parameters[5].Value = queryParam["feeitemId"].ToString();
            }
            else
            {
                parameters[5].Value = "";
            }

            RepositoryFactory response = new RepositoryFactory();
            return response.BaseRepository().FindList<ArrearsExportByZHEntity>(strSql.ToString(), parameters).OrderBy(t => t.property_name).OrderBy(t => t.customername).OrderBy(t => t.fee_years);
        }

        /// <summary>
        /// 获取租户月收费用户
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        public IEnumerable<RentreportEntity.RentreportListEntity> GetRentreportList(string property_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  wr.contractid ,
                                    wr.customername ,
                                    wr.rentarea ,
                                    ( SELECT    STUFF(( SELECT  ',' + room_name
                                                        FROM    wy_room
                                                        WHERE   room_id IN (
                                                                SELECT  b.rentcell
                                                                FROM    ( SELECT    rentcell = CONVERT(XML, '<root><v>'
                                                                                    + REPLACE(RTRIM(LTRIM(rentcell)),
                                                                                          ',', '</v><v>')
                                                                                    + '</v></root>')
                                                                          FROM      wy_rentcontract
                                                                          WHERE     contractid = wr.contractid
                                                                        ) a
                                                                        OUTER APPLY ( SELECT
                                                                                          rentcell = C.v.value('.',
                                                                                          'NVARCHAR(MAX)')
                                                                                      FROM
                                                                                          a.rentcell.nodes('/root/v') C ( v )
                                                                                    ) b )
                                                      FOR
                                                        XML PATH('')
                                                      ), 1, 1, '')
                                    ) room_name
                            FROM    wy_rentcontract wr
                            WHERE   wr.status = 1
                                    AND wr.property_id = @property_id ");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@property_id",property_id)
                };

            return this.BaseRepository().FindList<RentreportEntity.RentreportListEntity>(strSql.ToString(), parameter);
        }

        /// <summary>
        /// 获取租户月收费统计
        /// </summary>
        /// <param name="queryJson">参数</param>
        /// <param name="rentcontract_id">编号</param>
        /// <returns></returns>
        public IEnumerable<RentreportEntity.RentreportExtEntity> GetRentreportList(string queryJson, string rentcontract_id)
        {
            var queryParam = queryJson.ToJObject();
            string feeitem_id = "";
            if (!queryParam["FeeName"].IsEmpty())
            {
                List<JToken> FeeName = queryParam["FeeName"].ToList();
                foreach (JValue item in FeeName)
                {
                    feeitem_id += item.Value + ",";
                }
                if (!string.IsNullOrEmpty(feeitem_id))
                {
                    feeitem_id = feeitem_id.Substring(0, feeitem_id.Length - 1);
                }
            }

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"  SELECT  x2.* ,
                                            x3.feeitem_name
                                    FROM    ( SELECT    rentcontract_id ,
                                                        x1.feeitem_id ,
                                                        ISNULL(SUM(cshould), 0) AS cshould ,
                                                        ISNULL(SUM(creceive), 0) AS creceive
                                                FROM      ( SELECT    rentcontract_id ,
                                                                    feeitem_id ,
                                                                    fee_income AS cshould ,
                                                                    0 AS creceive
                                                            FROM      wy_feeincome
                                                            WHERE     rentcontract_id IN ( {0} )
                                                                    AND feeitem_id IN ( {1} )
                                                                    AND DATEPART(YEAR, pay_enddate) = @year
                                                                    AND DATEPART(MONTH, pay_enddate) = @month
                                                            UNION
                                                            SELECT    rentcontract_id ,
                                                                    feeitem_id ,
                                                                    0 AS cshould ,
                                                                    fee_income AS creceive
                                                            FROM      wy_feeincome
                                                            WHERE     rentcontract_id IN ( {2} )
                                                                    AND feeitem_id IN ( {3} )
                                                                    AND DATEPART(YEAR, fee_date) = @year
                                                                    AND DATEPART(MONTH, fee_date) = @month
                                                        ) x1
                                                GROUP BY  x1.rentcontract_id ,
                                                        x1.feeitem_id
                                            ) x2
                                            LEFT JOIN dbo.wy_feeitem x3 ON x2.feeitem_id = x3.feeitem_id "
                , rentcontract_id, feeitem_id, rentcontract_id, feeitem_id);

            int year = 0;
            if (!queryParam["YearNum"].IsEmpty())
            {
                year = queryParam["YearNum"].ToInt();
            }
            int month = 0;
            if (!queryParam["MonthNum"].IsEmpty())
            {
                month = queryParam["MonthNum"].ToInt();
            }

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@year",year),
                    DbParameters.CreateDbParameter("@month",month)
                };

            return this.BaseRepository().FindList<RentreportEntity.RentreportExtEntity>(strSql.ToString(), parameter);
        }
    }
}