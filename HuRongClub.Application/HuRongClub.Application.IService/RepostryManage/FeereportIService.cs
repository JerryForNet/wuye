using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.RepostryManage
{
    /// <summary>
    /// 统计中心报表
    /// </summary>
    public interface FeereportIService
    {
        IEnumerable<ChargeModel> GetChargeListJson(string queryJson);

        IEnumerable<DelayanaModel> GetzhanglingListJson(string queryJson);

        IEnumerable<DistrictModel> GetqianfeiDistrictListJson(string queryJson);

        /// <summary>
        /// 获取欠费统计
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<DistrictModel> GetArrearageStatistics(string queryJson);

        IEnumerable<DistrictModel> GetPercentListJson(string queryJson);

        IEnumerable<DelayDetail> GetDelayListJson(string queryJson);

        IEnumerable<Arrears_rentdetailModel> GetarrearsrentListJson(string queryJson);

        IEnumerable<Arrears_detailModel> GetarrearsListJson(string queryJson);

        IEnumerable<ArraersDetailModel> GetArrearsDetailJson(string queryJson, Pagination pagination);

        /// <summary>
        /// 获取租户月收费用户
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        IEnumerable<RentreportEntity.RentreportListEntity> GetRentreportList(string property_id);

        /// <summary>
        /// 获取租户月收费统计
        /// </summary>
        /// <param name="queryJson">参数</param>
        /// <param name="rentcontract_id">编号</param>
        /// <returns></returns>
        IEnumerable<RentreportEntity.RentreportExtEntity> GetRentreportList(string queryJson, string rentcontract_id);
    }
}