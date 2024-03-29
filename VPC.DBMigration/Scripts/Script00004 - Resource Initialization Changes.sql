
/****** Object:  StoredProcedure [dbo].[Resource_Create_Xml]    Script Date: 26-07-2019 16:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
	 DROP PROCEDURE [dbo].[Resource_Create_Xml] 
END
GO


CREATE PROCEDURE [dbo].[Resource_Create_Xml]
(
    @guidrootTenantId UNIQUEIDENTIFIER,
	@strDefaultLanguage [dbo].[xSmallText],
    @XmlForResources AS XML
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @DATA TABLE
    (
        Id INT IDENTITY(1, 1),
        TenantId UNIQUEIDENTIFIER NOT NULL,
        [Key] [dbo].[mediumText] NOT NULL,
        [Value] [dbo].[xLargeText] NOT NULL
		,[EntityCode] [dbo].[xSmallText] NULL
		,[IsStatic] bit NULL
    );

   
    INSERT INTO @DATA
    SELECT ref.value('./@TenantId', 'uniqueidentifier') AS TenantId,
           ref.value('./@Key', '[dbo].[mediumText]') AS [Key],
           ref.value('./@Value', '[dbo].[xLargeText]') AS [Value]
		  ,ref.value('./@EntityCode', '[dbo].[xSmallText]') AS [EntityCode]
		  ,ref.value('./@IsStatic', 'bit') AS [IsStatic]
    FROM @XmlForResources.nodes('/Resources/Resource') AS T(ref);

    INSERT INTO [dbo].[Resource]
    (
        TenantId,
        Id,
        [Key],
        [Value],
        [Language]
		,[EntityCode]
		,[IsStatic]
    )
    SELECT TenantId,
           NEWID(),
           [Key],
           [Value],
           @strDefaultLanguage
		   ,[EntityCode]
		   ,[IsStatic]
    FROM @DATA;



    IF @@ERROR <> 0
    BEGIN
        RETURN 1;
    END;
    RETURN 0;
END;
GO
