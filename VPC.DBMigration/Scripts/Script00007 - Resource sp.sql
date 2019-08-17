/****** Object:  StoredProcedure [dbo].[Resource_GetByKey]    Script Date: 16/08/2019 15:56:47 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetByKey]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_GetByKey]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Resource_GetByKey]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strKey [dbo].[mediumText]
)
AS
BEGIN
    SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT Resrc.Id,
           Resrc.[Key],
           Resrc.[Value],
           Resrc.[Language],
           (
               SELECT TOP 1
                   [Text]
               FROM [dbo].[PickListValue]
               WHERE [Key] = Resrc.[Language]
           ) AS LanguageName
    FROM dbo.[Resource] AS Resrc
    WHERE TenantId = @guidTenantId
          AND Resrc.[Key] = @strKey
    ORDER BY Id;



END;

GO

/****** Object:  StoredProcedure [dbo].[Resource_GetByMenuId]    Script Date: 16/08/2019 16:00:45 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetByMenuId]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_GetByMenuId]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Resource_GetByMenuId]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidMenuId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT R.Id, R.[Key], R.[Value], R.[Language] 
	FROM [Resource] R
	INNER JOIN MENU M ON TRIM(R.[Key]) = TRIM(M.MenuCode)
	WHERE M.Id = @guidMenuId
	AND R.TenantId = @guidTenantId

END;

GO