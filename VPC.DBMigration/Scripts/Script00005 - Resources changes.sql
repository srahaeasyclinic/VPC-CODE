IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'UpdatedOn' AND Object_ID = Object_ID(N'[dbo].[Resource]'))
BEGIN
   ALTER TABLE [dbo].[Resource] ADD [UpdatedOn] DATETIME NULL
END
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'UpdatedBy' AND Object_ID = Object_ID(N'[dbo].[Resource]'))
BEGIN
   ALTER TABLE [dbo].[Resource] ADD [UpdatedBy] UNIQUEIDENTIFIER NULL
END
GO


/****** Object:  StoredProcedure [dbo].[Resource_Create]    Script Date: 26-Jul-19 20:02:00 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Create]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Create]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Resource_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
	@guidId UNIQUEIDENTIFIER,
    @strKey [dbo].[mediumText] = NULL,
    @strValue [dbo].[xLargeText] = NULL,
    @strLanguage [dbo].[xSmallText] = NULL,
	@guidUpdatedBy UNIQUEIDENTIFIER,
    @strMessage VARCHAR(100) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM [dbo].[Resource]
        WHERE TenantId = @guidTenantId
              AND [Key] = @strKey
              AND [Language] = @strLanguage
    )
    BEGIN
        SET @strMessage = 'Resource already exits';
    END;
    ELSE
    BEGIN
        INSERT INTO [dbo].[Resource]
        (
			TenantId,
			Id,            
            [Key],
            [Value],
            [Language],
			[UpdatedOn],
			[UpdatedBy]
        )
        VALUES
        (@guidTenantId, @guidId, @strKey, @strValue, @strLanguage, GETUTCDATE(), @guidUpdatedBy);
    END;




END;


GO

/****** Object:  StoredProcedure [dbo].[Resource_Update]    Script Date: 26-Jul-19 20:02:49 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Update]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Update]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Resource_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strKey [dbo].[mediumText],
    @strValue [dbo].[xLargeText],
    @strLanguage [dbo].[xSmallText],
    @guidId UNIQUEIDENTIFIER,
	@guidUpdatedBy UNIQUEIDENTIFIER,
    @strMessage VARCHAR(100) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM [dbo].[Resource]
        WHERE TenantId = @guidTenantId
              AND [Key] = @strKey
              AND [Language] = @strLanguage
              AND Id <> @guidId
    )
    BEGIN
        SET @strMessage = 'Resource already exits';
    END;
    ELSE
    BEGIN

        UPDATE [dbo].[Resource]
        SET [Key] = @strKey,
            [Value] = @strValue,
            [Language] = @strLanguage,
			[UpdatedOn] = GETUTCDATE(),
			[UpdatedBy] = @guidUpdatedBy
        WHERE TenantId = @guidTenantId
              AND Id = @guidId;

    END;



END;


GO

/****** Object:  StoredProcedure [dbo].[Resource_Create_Xml_1]    Script Date: 26-Jul-19 20:03:42 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Create_Xml_1]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Create_Xml_1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Resource_Create_Xml_1]
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
		,[IsStatic] BIT NULL
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
		,[UpdatedOn]
    )
    SELECT TenantId,
           NEWID(),
           [Key],
           [Value],
           @strDefaultLanguage
		   ,[EntityCode]
		   ,[IsStatic]
		   ,GETUTCDATE()
    FROM @DATA;



    IF @@ERROR <> 0
    BEGIN
        RETURN 1;
    END;
    RETURN 0;
END;

GO
