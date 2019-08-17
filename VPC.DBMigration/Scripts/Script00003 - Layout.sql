/****** Object:  StoredProcedure [dbo].[EntityLayout_Create]    Script Date: 17-Jul-19 15:39:56 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_Create]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntityLayout_Create]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EntityLayout_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @strEntityId xSmallText,
    @strName mediumText,
    @intType INT,
    @strSubType smallText = NULL,
    @intLayoutContext INT = NULL,
    @guidUpdatedBy UNIQUEIDENTIFIER,
	@defaultLayout BIT = 0,
	@layoutStr xLargeText = null
)
AS
BEGIN
    DECLARE @ErrorMessage NVARCHAR(4000),
            @ErrorNumber INT,
            @ErrorSeverity INT,
            @ErrorState INT,
            @ErrorLine INT,
            @ErrorProcedure NVARCHAR(200);

   

    BEGIN TRY
        BEGIN TRAN;

		IF @layoutStr IS NULL
		BEGIN
			IF @intType = 1
			BEGIN
				SET @layoutStr
					= '{"fields":[{"name":"InternalId","sequence":1,"hidden":true,"dataType":"Guid","refId":null,"defaultValue":null,"properties":null,"values":null,"clickable":false}],"defaultSortOrder":{"name":"","value":""},"actions":[]}';
			END;

			IF @intType = 3
			BEGIN
				SET @layoutStr
					= '{"fields":[{"name":"InternalId","sequence":1,"hidden":true,"dataType":"Guid","refId":null,"defaultValue":null,"properties":null,"values":null,"clickable":false,"defaultView":null},{"name":"SubType","sequence":2,"hidden":false,"dataType":"Text","refId":null,"defaultValue":null,"properties":null,"values":null,"clickable":false,"defaultView":null}],"defaultSortOrder":{"name":"","value":""},"defaultGroupBy":"","maxResult":0,"searchProperties":[{"name":"FreeTextSearch","properties":[]},{"name":"SimpleSearch","properties":[]},{"name":"AdvanceSearch","properties":[]}],"actions":[],"toolbar":[]}';
			END;


		END

        


        INSERT INTO [dbo].[EntityLayout]
        (
            [TenantId],
            [Id],
            [EntityId],
            [Name],
            [Type],
            [SubType],
            [LayoutContext],
            [Layout],
            [UpdatedOn],
            [UpdatedBy],
            [Default]
        )
        VALUES
        (@guidTenantId,
         @guidId,
         @strEntityId,
         @strName,
         @intType,
         @strSubType,
         @intLayoutContext,
         @layoutStr,
         GETUTCDATE(),
         @guidUpdatedBy,
         @defaultLayout
        );

        IF NOT EXISTS
        (
            SELECT *
            FROM [dbo].[EntityLayout]
            WHERE [EntityId] = @strEntityId
                  AND [Type] = @intType
                  AND ISNULL([SubType], 0) = ISNULL(@strSubType, 0)
                  AND ISNULL([LayoutContext], 0) = ISNULL(@intLayoutContext, 0)
                  AND [Default] = 1
				  AND TenantId = @guidTenantId
        )
        BEGIN
            UPDATE [dbo].[EntityLayout]
            SET [Default] = 1
            WHERE [TenantId] = @guidTenantId
                  AND [Id] = @guidId;
        END;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorNumber = ERROR_NUMBER(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE(),
               @ErrorLine = ERROR_LINE(),
               @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');

        ROLLBACK TRAN;
        RAISERROR(
                     @ErrorMessage,
                     @ErrorSeverity,
                     1,
                     @ErrorNumber,
                     @ErrorSeverity,
                     @ErrorState,
                     @ErrorProcedure,
                     @ErrorLine
                 );
    END CATCH;

END;
GO

/****** Object:  StoredProcedure [dbo].[PicklistLayout_Create]    Script Date: 17-Jul-19 15:41:55 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_Create]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistLayout_Create]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PicklistLayout_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @strPicklistId xSmallText,
    @strName mediumText,
    @intType INT,
    @intLayoutContext INT = NULL,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @defaultLayout xLargeText = NULL,
    @isDefault BIT = 0
)
AS
BEGIN

    DECLARE @ErrorMessage NVARCHAR(4000),
            @ErrorNumber INT,
            @ErrorSeverity INT,
            @ErrorState INT,
            @ErrorLine INT,
            @ErrorProcedure NVARCHAR(200);
    --,
    --@defaultLayout xLargeText; 
    BEGIN TRY
        BEGIN TRAN;

        IF @defaultLayout IS NULL
        BEGIN
            IF @intType = 1
            BEGIN
                SET @defaultLayout
                    = '{"fields":[{"name":"InternalId","sequence":1,"hidden":true,"dataType":"Guid","refId":null,"defaultValue":null,"properties":null,"values":null,"clickable":false}],"defaultSortOrder":{"name":"","value":""}}';
            END;

            IF @intType = 3
            BEGIN
                SET @defaultLayout
                    = '{"fields":[{"name":"InternalId","sequence":1,"hidden":true,"dataType":"Guid","refId":null,"defaultValue":null,"properties":null,"values":null,"clickable":false}],"defaultSortOrder":{"name":"","value":""},"defaultGroupBy":"","maxResult":0,"searchProperties":[{"name":"FreeTextSearch","properties":[]},{"name":"SimpleSearch","properties":[]},{"name":"AdvanceSearch","properties":[]}],"actions":[],"toolbar":[]}';
            END;
        END;



        INSERT INTO [dbo].[PicklistLayout]
        (
            [TenantId],
            [Id],
            [PicklistId],
            [Name],
            [Type],
            [LayoutContext],
            [UpdatedOn],
            [UpdatedBy],
            [Default],
            Layout
        )
        VALUES
        (@guidTenantId,
         @guidId,
         @strPicklistId,
         @strName,
         @intType,
         @intLayoutContext,
         GETDATE(),
         @guidUpdatedBy,
         @isDefault,
         @defaultLayout
        );

        IF NOT EXISTS
        (
            SELECT *
            FROM PicklistLayout
            WHERE [PicklistId] = @strPicklistId
                  AND [Type] = @intType
                  AND ISNULL(LayoutContext, 0) = ISNULL(@intLayoutContext, 0)
                  AND [Default] = 1
				  AND TenantId = @guidTenantId
        )
        BEGIN
            UPDATE dbo.PicklistLayout
            SET [Default] = 1
            WHERE [TenantId] = @guidTenantId
                  AND [Id] = @guidId;
        END;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorNumber = ERROR_NUMBER(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE(),
               @ErrorLine = ERROR_LINE(),
               @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');

        ROLLBACK TRAN;
        RAISERROR(
                     @ErrorMessage,
                     @ErrorSeverity,
                     1,
                     @ErrorNumber,
                     @ErrorSeverity,
                     @ErrorState,
                     @ErrorProcedure,
                     @ErrorLine
                 );
    END CATCH;



END;
GO