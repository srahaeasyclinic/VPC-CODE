USE [1-VPC_Dev]
GO
/****** Object:  StoredProcedure [dbo].[Tenant_GetDefaultLanguageKeyTextBy_TenantId]    Script Date: 19-07-2019 16:52:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===================================================
-- Author:		Soma Ghosh Chattopadhyay
-- Create date: 19.07.2019
-- Description:	Get tenant default language Key, Text
-- ==================================================

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Tenant_GetDefaultLanguageKeyTextBy_TenantId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [Tenant_GetDefaultLanguageKeyTextBy_TenantId]
GO

CREATE PROCEDURE [Tenant_GetDefaultLanguageKeyTextBy_TenantId]
	@guidTenantId UNIQUEIDENTIFIER
	
AS
BEGIN

	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT  PickListValue.[Key], PickListValue.[Text]
	FROM   [dbo].[Tenant] 
	Inner JOIN  [dbo].[PickListValue] ON Tenant.Id = PickListValue.TenantId AND Tenant.PreferredLanguageId = PickListValue.Id
	WHERE Tenant.Id = @guidTenantId
END
