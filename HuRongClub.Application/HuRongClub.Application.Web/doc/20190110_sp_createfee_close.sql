UPDATE	dbo.Base_User SET UserProperty = '0001,0002,0008,0009,0010,0011,0012,0013,0014,0015,0016,0018,0019,0021,0022,0023,0024,0025,0026,0027,0028,0029,0030,0031,0032' WHERE UserId = 'System'

USE [run_eproperty]
GO
/****** Object:  StoredProcedure [dbo].[CreateFeeClose]    Script Date: 2018/12/22 23:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jerry.Li
-- Create date: 2018/12/22
-- Description:	如果当月没有数据，存储过程生成数据
-- =============================================
CREATE PROCEDURE [dbo].[sp_create_ree_close] @fuser VARCHAR(10)
AS
    BEGIN
        SET NOCOUNT ON

        IF NOT EXISTS ( SELECT  property_id
                        FROM    dbo.wy_feeclose
                        WHERE   fyear = YEAR(GETDATE())
                                AND fmonth = MONTH(GETDATE()) )
            BEGIN
                DECLARE @year INT
                DECLARE @month INT
                DECLARE @property_id VARCHAR(10)

                DECLARE InsertFeeClose_Cursor CURSOR
                FOR
                    SELECT  property_id
                    FROM    wy_property
                OPEN InsertFeeClose_Cursor
                FETCH NEXT FROM InsertFeeClose_Cursor INTO @property_id
                WHILE @@fetch_status = 0
                    BEGIN
                        SET @year = DATEPART(YEAR, GETDATE())
                        SET @month = DATEPART(MONTH, GETDATE())

                        INSERT  INTO dbo.wy_feeclose
                                ( property_id ,
                                  fyear ,
                                  fmonth ,
                                  fstatus ,
                                  fuser ,
                                  flasttime
						        )
                        VALUES  ( @property_id , -- property_id - varchar(10)
                                  @year , -- fyear - smallint
                                  @month , -- fmonth - smallint
                                  0 , -- fstatus - varchar(5)
                                  @fuser , -- fuser - varchar(50)
                                  GETDATE()  -- flasttime - datetime
						        )

                        FETCH NEXT FROM InsertFeeClose_Cursor INTO @property_id
                    END

                CLOSE InsertFeeClose_Cursor
                DEALLOCATE InsertFeeClose_Cursor				
            END 
    END