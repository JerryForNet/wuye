USE [run_eproperty]
GO
/****** Object:  StoredProcedure [dbo].[sp_report_income]    Script Date: 2019/1/13 11:10:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jerry.Li
-- Create date: 2019-1-1
-- Description:	应收审计报表
-- =============================================
ALTER PROCEDURE [dbo].[sp_report_income]
    @dtyear INT ,
    @property_id VARCHAR(8),
	@glf_item_id VARCHAR(8)
AS
    BEGIN
        SET NOCOUNT ON;

        BEGIN
			
            SELECT  building_name ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = '100'
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                    ) wyf ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = '440'
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                    ) yxf ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id IN ( '440', '100' )
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                    ) hj ,
                    ( SELECT    ISNULL(SUM(fee_income), 0) price
                      FROM      wy_feeincome
                      WHERE     DATEPART(YEAR, pay_enddate) <=  (@dtyear-2)
                                AND fee_date IS NULL
                                AND building_id = bu.building_id
                    ) lnqf ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 1
                    ) glv_a ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 1
                    ) lnq_a ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 2
                    ) glv_b ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 2
                    ) lnq_b ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 3
                    ) glv_c ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 3
                    ) lnq_c ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 4
                    ) glv_d ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 4
                    ) lnq_d ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 5
                    ) glv_e ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 5
                    ) lnq_e ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 6
                    ) glv_f ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 6
                    ) lnq_f ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 7
                    ) glv_g ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 7
                    ) lnq_g ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 8
                    ) glv_h ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 8
                    ) lnq_h ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 9
                    ) glv_i ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 9
                    ) lnq_i ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 10
                    ) glv_j ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 10
                    ) lnq_j ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 11
                    ) glv_k ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 11
                    ) lnq_k ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     feeitem_id = @glf_item_id
                                AND building_id = bu.building_id
                                AND fee_year = @dtyear
                                AND fee_month = 12
                    ) glv_l ,
                    ( SELECT    ISNULL(SUM(fee_already), 0)
                      FROM      dbo.wy_feeincome
                      WHERE     building_id = bu.building_id
                                AND fee_year <= (@dtyear-2)
                                AND DATEPART(YEAR,fee_date) = @dtyear 
								AND DATEPART(MONTH,fee_date) = 12
                    ) lnq_l
            FROM    dbo.wy_building bu
            WHERE   property_id = @property_id
            ORDER BY bu.building_id

            RETURN    
        END
    
    END
