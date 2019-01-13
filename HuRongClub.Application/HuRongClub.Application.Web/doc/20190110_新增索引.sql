USE [run_eproperty]
GO

/****** Object:  Index [IX_tb_wh_outbill_fgoodsid]    Script Date: 2019/1/1 15:56:12 ******/
CREATE NONCLUSTERED INDEX [IX_tb_wh_outbill_fgoodsid] ON [dbo].[tb_wh_outbill_item]
(
	[fgoodsid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


/****** Object:  Index [IX_tb_wh_outbill_foutbillid]    Script Date: 2019/1/1 15:56:17 ******/
CREATE NONCLUSTERED INDEX [IX_tb_wh_outbill_foutbillid] ON [dbo].[tb_wh_outbill_item]
(
	[foutbillid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

