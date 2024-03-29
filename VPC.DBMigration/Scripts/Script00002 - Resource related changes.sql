/****** Object:  StoredProcedure [dbo].[Resource_CheckDuplicateKey]    Script Date: 16-Jul-19 15:39:56 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_CheckDuplicateKey]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_CheckDuplicateKey]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Resource_CheckDuplicateKey]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strKey [dbo].[mediumText],
    @strLanguage [dbo].[xSmallText]
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT Id,
           [Key],
           [Value],
           [Language]
    FROM [dbo].[Resource]
    WHERE TenantId = @guidTenantId
          AND LOWER([Key]) = LOWER(@strKey)
          AND LOWER([Language]) != LOWER(@strLanguage)
    ORDER BY Id;


END;

GO

/****** Object:  StoredProcedure [dbo].[Resource_Clone]    Script Date: 16-Jul-19 15:41:55 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Clone]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Clone]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Resource_Clone]
(
    @guidRootTenantId UNIQUEIDENTIFIER,
    @guidToTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [dbo].[Resource]
    (
        TenantId,
        Id,
        [Key],
        [Value],
        [Language]
    )
    SELECT @guidToTenantId,
           NEWID(),
           [Key],
           [Value],
           [Language]
    FROM [dbo].[Resource]
    WHERE TenantId = @guidRootTenantId;

END;

GO

/****** Object:  StoredProcedure [dbo].[Resource_Create]    Script Date: 16-Jul-19 15:43:16 PM ******/
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
            [Language]
        )
        VALUES
        (@guidTenantId, @guidId, @strKey, @strValue, @strLanguage);
    END;




END;

GO

/****** Object:  StoredProcedure [dbo].[Resource_Create_Xml]    Script Date: 16-Jul-19 15:43:56 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Create_Xml]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Create_Xml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Resource_Create_Xml]
(
    @guidrootTenantId UNIQUEIDENTIFIER,
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
    );

    DECLARE @language [dbo].[xSmallText];
    SET @language =
    (
        SELECT TOP 1
            PickListValue.[Key]
        FROM Tenant
            LEFT JOIN PickListValue
                ON Tenant.Id = PickListValue.TenantId
                   AND Tenant.PreferredLanguageId = PickListValue.Id
        WHERE Tenant.Id = @guidrootTenantId
    );
    IF @language IS NULL
        SET @language = 'en-US';
    INSERT INTO @DATA
    SELECT ref.value('./@TenantId', 'uniqueidentifier') AS TenantId,
           ref.value('./@Key', '[dbo].[mediumText]') AS [Key],
           ref.value('./@Value', '[dbo].[xLargeText]') AS [Value]
    FROM @XmlForResources.nodes('/Resources/Resource') AS T(ref);

    INSERT INTO [dbo].[Resource]
    (
        TenantId,
        Id,
        [Key],
        [Value],
        [Language]
    )
    SELECT TenantId,
           NEWID(),
           [Key],
           [Value],
           @language
    FROM @DATA;



    IF @@ERROR <> 0
    BEGIN
        RETURN 1;
    END;
    RETURN 0;
END;
GO

/****** Object:  StoredProcedure [dbo].[Resource_Delete]    Script Date: 16-Jul-19 15:44:38 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Delete]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Delete]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
 
CREATE PROCEDURE [dbo].[Resource_Delete]  
(  
	@guidTenantId UNIQUEIDENTIFIER,  
	@guidId UNIQUEIDENTIFIER
	
)  
AS   
    SET NOCOUNT ON   
     
BEGIN  

			DELETE FROM 	[dbo].[Resource]		
			WHERE TenantId = @guidTenantId AND Id = @guidId
	


END  

GO

/****** Object:  StoredProcedure [dbo].[Resource_DeleteByKey]    Script Date: 16-Jul-19 15:45:10 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_DeleteByKey]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_DeleteByKey]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
 
CREATE PROCEDURE [dbo].[Resource_DeleteByKey]  
(  
	@guidTenantId UNIQUEIDENTIFIER,  
	@strKey [dbo].[mediumText]
	
)  
AS   
    SET NOCOUNT ON   
     
BEGIN  

			DELETE FROM 	[dbo].[Resource]		
			WHERE TenantId = @guidTenantId AND [Key] = @strKey
	


END  

GO

/****** Object:  StoredProcedure [dbo].[Resource_Get]    Script Date: 16-Jul-19 15:45:41 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Get]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Get]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Resource_Get]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strLanguage [dbo].[xSmallText] = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @defaultLanguageTable AS TABLE
    (
        Tenant_Code UNIQUEIDENTIFIER NOT NULL,
        Tenent_Id UNIQUEIDENTIFIER NOT NULL,
        OrgNo VARCHAR(50) NULL,
        PickListValue_Id UNIQUEIDENTIFIER NULL,
        PickListId INT NULL,
        [Key] VARCHAR(50) NULL,
        [Text] VARCHAR(50) NULL,
        PreferredLanguageId UNIQUEIDENTIFIER NULL
    );

    INSERT INTO @defaultLanguageTable
    (
        Tenant_Code,
        Tenent_Id,
        OrgNo,
        PickListValue_Id,
        PickListId,
        [Key],
        [Text],
        PreferredLanguageId
    )
    EXEC [dbo].[Tenant_GetDefaultLanguageDetails] @guidTenantId;



    IF @strLanguage IS NOT NULL
    BEGIN
        DECLARE @strLanguageKey VARCHAR(50);

        SET @strLanguageKey =
        (
            SELECT TOP 1
                [Key]
            FROM [dbo].[PickListValue]
            WHERE (
                      [Text] LIKE @strLanguage + '%'
                      OR [Key] LIKE @strLanguage + '%'
                  )
                  AND [TenantId] = @guidTenantId
        );

        IF @strLanguageKey IS NOT NULL
            SELECT Id,
                   [Key],
                   [Value],
                   [Language]
            FROM dbo.[Resource]
            WHERE TenantId = @guidTenantId
                  AND [Language] = @strLanguageKey
            ORDER BY Id;

        ELSE
        BEGIN
            --SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
            SET @strLanguage =
            (
                SELECT TOP 1 [Key] FROM @defaultLanguageTable
            );
            --END SET DEFAULT LANGUAGE FROM TENANT TABLE--

            IF @strLanguage IS NOT NULL
                SELECT Id,
                       [Key],
                       [Value],
                       [Language]
                FROM dbo.[Resource]
                WHERE TenantId = @guidTenantId
                      AND [Language] = @strLanguage
                ORDER BY Id;

            ELSE
                SELECT Id,
                       [Key],
                       [Value],
                       [Language]
                FROM dbo.[Resource]
                WHERE TenantId = @guidTenantId
                ORDER BY Id;
        END;

    END;
    ELSE
    BEGIN
        --SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
        SET @strLanguage =
        (
            SELECT TOP 1 [Key] FROM @defaultLanguageTable
        );
        --END SET DEFAULT LANGUAGE FROM TENANT TABLE--

        IF @strLanguage IS NOT NULL
            SELECT Id,
                   [Key],
                   [Value],
                   [Language]
            FROM dbo.[Resource]
            WHERE TenantId = @guidTenantId
                  AND [Language] = @strLanguage
            ORDER BY Id;

        ELSE
            SELECT Id,
                   [Key],
                   [Value],
                   [Language]
            FROM dbo.[Resource]
            WHERE TenantId = @guidTenantId
            ORDER BY Id;

    END;
END;

GO

/****** Object:  StoredProcedure [dbo].[Resource_GetALL]    Script Date: 16-Jul-19 15:46:17 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetALL]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_GetALL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Resource_GetALL]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intPageIndex INT = NULL,
    @intPageSize INT = NULL,
    @strOrderBy VARCHAR(50) = NULL,
    @strLanguage [dbo].[xSmallText] = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @strLanguageKey VARCHAR(50);
    DECLARE @defaultLanguageTable AS TABLE
    (
        Tenant_Code UNIQUEIDENTIFIER NOT NULL,
        Tenent_Id UNIQUEIDENTIFIER NOT NULL,
        OrgNo VARCHAR(50) NULL,
        PickListValue_Id UNIQUEIDENTIFIER NULL,
        PickListId INT NULL,
        [Key] VARCHAR(50) NULL,
        [Text] VARCHAR(50) NULL,
        PreferredLanguageId UNIQUEIDENTIFIER NULL
    );

    INSERT INTO @defaultLanguageTable
    (
        Tenant_Code,
        Tenent_Id,
        OrgNo,
        PickListValue_Id,
        PickListId,
        [Key],
        [Text],
        PreferredLanguageId
    )
    EXEC [dbo].[Tenant_GetDefaultLanguageDetails] @guidTenantId;

    IF @strOrderBy IS NOT NULL
    BEGIN

        IF @strOrderBy = ''
            SET @strOrderBy = 'Id';

        IF TRIM(LOWER(@strOrderBy)) = 'id'
            SET @strOrderBy = 'Id';

        IF TRIM(LOWER(@strOrderBy)) = 'key'
            SET @strOrderBy = '[Key]';

        IF TRIM(LOWER(@strOrderBy)) = 'value'
            SET @strOrderBy = '[Value]';

        IF TRIM(LOWER(@strOrderBy)) = 'language'
            SET @strOrderBy = '[Language]';


    END;
    IF @strOrderBy IS NULL
    BEGIN
        SET @strOrderBy = 'Id';
    END;

    IF @strLanguage = ''
        SET @strLanguage = NULL;

    IF @intPageIndex IS NOT NULL
       AND @intPageSize IS NOT NULL
    BEGIN
        IF @intPageIndex = 0
           AND @intPageSize = 0
        BEGIN
            SET @intPageIndex = NULL;
            SET @intPageSize = NULL;
        END;
    END;

    IF @intPageIndex IS NOT NULL
       AND @intPageSize IS NOT NULL
    BEGIN


        IF @strLanguage IS NOT NULL
        BEGIN
            SET @strLanguageKey =
            (
                SELECT TOP 1
                    [Key]
                FROM [dbo].[PickListValue]
                WHERE (
                          [Text] LIKE @strLanguage + '%'
                          OR [Key] LIKE @strLanguage + '%'
                      )
                      AND [TenantId] = @guidTenantId
            );

            IF @strLanguageKey IS NOT NULL
            BEGIN
                --SELECT       Id, [Key], [Value], [Language]
                --FROM          dbo.[Resource]
                --WHERE         TenantId=@guidTenantId AND [Language] = @strLanguageKey
                --ORDER BY Id OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                --OPTION (RECOMPILE);

                IF @strOrderBy = 'Id'
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
                          AND Resrc.[Language] = @strLanguageKey
                    ORDER BY Resrc.Id OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);

                ELSE IF @strOrderBy = '[Key]'
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
                          AND Resrc.[Language] = @strLanguageKey
                    ORDER BY Resrc.[Key] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);

                ELSE IF @strOrderBy = '[Value]'
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
                          AND Resrc.[Language] = @strLanguageKey
                    ORDER BY Resrc.[Value] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);

                ELSE IF @strOrderBy = '[Language]'
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
                          AND Resrc.[Language] = @strLanguageKey
                    ORDER BY Resrc.[Language] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);


            END;
            ELSE
            BEGIN
                --SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
                SET @strLanguage =
                (
                    SELECT TOP 1 [Key] FROM @defaultLanguageTable
                );
                --END SET DEFAULT LANGUAGE FROM TENANT TABLE--

                IF @strLanguage IS NOT NULL
                BEGIN

                    IF @strOrderBy = 'Id'
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
                              AND Resrc.[Language] = @strLanguage
                        ORDER BY Resrc.Id OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                        OPTION (RECOMPILE);


                    ELSE IF @strOrderBy = '[Key]'
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
                              AND Resrc.[Language] = @strLanguage
                        ORDER BY Resrc.[Key] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                        OPTION (RECOMPILE);

                    ELSE IF @strOrderBy = '[Value]'
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
                              AND Resrc.[Language] = @strLanguage
                        ORDER BY Resrc.[Value] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                        OPTION (RECOMPILE);

                    ELSE IF @strOrderBy = '[Language]'
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
                              AND Resrc.[Language] = @strLanguage
                        ORDER BY Resrc.[Language] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                        OPTION (RECOMPILE);



                END;
                ELSE
                BEGIN

                    IF @strOrderBy = 'Id'
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
                        ORDER BY Resrc.Id OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                        OPTION (RECOMPILE);

                    ELSE IF @strOrderBy = '[Key]'
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
                        ORDER BY Resrc.[Key] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                        OPTION (RECOMPILE);

                    ELSE IF @strOrderBy = '[Value]'
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
                        ORDER BY Resrc.[Value] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                        OPTION (RECOMPILE);

                    ELSE IF @strOrderBy = '[Language]'
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
                        ORDER BY Resrc.[Language] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                        OPTION (RECOMPILE);

                END;
            END;

        END;
        ELSE
        BEGIN
            --SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
            SET @strLanguage =
            (
                SELECT TOP 1 [Key] FROM @defaultLanguageTable
            );
            --END SET DEFAULT LANGUAGE FROM TENANT TABLE--

            IF @strLanguage IS NOT NULL
            BEGIN
                IF @strOrderBy = 'Id'
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
                          AND Resrc.[Language] = @strLanguage
                    ORDER BY Resrc.Id OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);


                ELSE IF @strOrderBy = '[Key]'
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
                          AND Resrc.[Language] = @strLanguage
                    ORDER BY Resrc.[Key] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);

                ELSE IF @strOrderBy = '[Value]'
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
                          AND Resrc.[Language] = @strLanguage
                    ORDER BY Resrc.[Value] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);

                ELSE IF @strOrderBy = '[Language]'
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
                          AND Resrc.[Language] = @strLanguage
                    ORDER BY Resrc.[Language] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);

            END;
            ELSE
            BEGIN
                IF @strOrderBy = 'Id'
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
                    ORDER BY Resrc.Id OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);

                ELSE IF @strOrderBy = '[Key]'
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
                    ORDER BY Resrc.[Key] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);

                ELSE IF @strOrderBy = '[Value]'
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
                    ORDER BY Resrc.[Value] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);

                ELSE IF @strOrderBy = '[Language]'
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
                    ORDER BY Resrc.[Language] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
                    OPTION (RECOMPILE);
            END;
        END;


    END;
    ELSE
    BEGIN

        IF @strLanguage IS NOT NULL
        BEGIN
            SET @strLanguageKey =
            (
                SELECT TOP 1
                    [Key]
                FROM [dbo].[PickListValue]
                WHERE (
                          [Text] LIKE @strLanguage + '%'
                          OR [Key] LIKE @strLanguage + '%'
                      )
                      AND [TenantId] = @guidTenantId
            );

            IF @strLanguageKey IS NOT NULL
            BEGIN
                --SELECT       Id, [Key], [Value], [Language]
                --FROM          dbo.[Resource]
                --WHERE         TenantId=@guidTenantId AND [Language] = @strLanguageKey
                --ORDER BY Id

                IF @strOrderBy = 'Id'
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
                          AND Resrc.[Language] = @strLanguageKey
                    ORDER BY Resrc.Id;

                ELSE IF @strOrderBy = '[Key]'
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
                          AND Resrc.[Language] = @strLanguageKey
                    ORDER BY Resrc.[Key];

                ELSE IF @strOrderBy = '[Value]'
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
                          AND Resrc.[Language] = @strLanguageKey
                    ORDER BY Resrc.[Value];

                ELSE IF @strOrderBy = '[Language]'
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
                          AND Resrc.[Language] = @strLanguageKey
                    ORDER BY Resrc.[Language];


            END;
            ELSE
            BEGIN
                --SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
                SET @strLanguage =
                (
                    SELECT TOP 1 [Key] FROM @defaultLanguageTable
                );
                --END SET DEFAULT LANGUAGE FROM TENANT TABLE--

                IF @strLanguage IS NOT NULL
                BEGIN
                    IF @strOrderBy = 'Id'
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
                              AND Resrc.[Language] = @strLanguage
                        ORDER BY Resrc.Id;

                    ELSE IF @strOrderBy = '[Key]'
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
                              AND Resrc.[Language] = @strLanguage
                        ORDER BY Resrc.[Key];

                    ELSE IF @strOrderBy = '[Value]'
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
                              AND Resrc.[Language] = @strLanguage
                        ORDER BY Resrc.[Value];

                    ELSE IF @strOrderBy = '[Language]'
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
                              AND Resrc.[Language] = @strLanguage
                        ORDER BY Resrc.[Language];
                END;
                ELSE
                BEGIN

                    IF @strOrderBy = 'Id'
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
                        ORDER BY Resrc.Id;

                    ELSE IF @strOrderBy = '[Key]'
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
                        ORDER BY Resrc.[Key];

                    ELSE IF @strOrderBy = '[Value]'
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
                        ORDER BY Resrc.[Value];

                    ELSE IF @strOrderBy = '[Language]'
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
                        ORDER BY Resrc.[Language];

                END;


            END;

        END;
        ELSE
        BEGIN
            --SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
            SET @strLanguage =
            (
                SELECT TOP 1 [Key] FROM @defaultLanguageTable
            );
            --END SET DEFAULT LANGUAGE FROM TENANT TABLE--

            IF @strLanguage IS NOT NULL
            BEGIN
                IF @strOrderBy = 'Id'
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
                          AND Resrc.[Language] = @strLanguage
                    ORDER BY Resrc.Id;

                ELSE IF @strOrderBy = '[Key]'
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
                          AND Resrc.[Language] = @strLanguage
                    ORDER BY Resrc.[Key];

                ELSE IF @strOrderBy = '[Value]'
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
                          AND Resrc.[Language] = @strLanguage
                    ORDER BY Resrc.[Value];

                ELSE IF @strOrderBy = '[Language]'
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
                          AND Resrc.[Language] = @strLanguage
                    ORDER BY Resrc.[Language];
            END;
            ELSE
            BEGIN

                IF @strOrderBy = 'Id'
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
                    ORDER BY Resrc.Id;

                ELSE IF @strOrderBy = '[Key]'
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
                    ORDER BY Resrc.[Key];

                ELSE IF @strOrderBy = '[Value]'
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
                    ORDER BY Resrc.[Value];

                ELSE IF @strOrderBy = '[Language]'
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
                    ORDER BY Resrc.[Language];

            END;


        END;

    END;
END;

GO

/****** Object:  StoredProcedure [dbo].[Resource_GetByKeyAndLanguage]    Script Date: 16-Jul-19 15:47:01 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetByKeyAndLanguage]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_GetByKeyAndLanguage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Resource_GetByKeyAndLanguage]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strKey [dbo].[mediumText],
    @strLanguage [dbo].[xSmallText]
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
          AND Resrc.[Language] = @strLanguage
    ORDER BY Id;



END;

GO

/****** Object:  StoredProcedure [dbo].[Resource_GetKeyFromLanguage]    Script Date: 16-Jul-19 15:47:33 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetKeyFromLanguage]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_GetKeyFromLanguage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Resource_GetKeyFromLanguage]
    @guidTenantId UNIQUEIDENTIFIER,
    @strLanguage [dbo].[xSmallText] = NULL
AS
BEGIN
    SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @strLanguage IS NOT NULL
        SELECT TOP 1
            [Key],
            [Text]
        FROM [dbo].[PickListValue]
        WHERE (
                  [Text] LIKE @strLanguage + '%'
                  OR [Key] LIKE @strLanguage + '%'
              )
              AND [TenantId] = @guidTenantId;
    ELSE
    BEGIN

        DECLARE @defaultLanguageTable AS TABLE
        (
            Tenant_Code UNIQUEIDENTIFIER NOT NULL,
            Tenent_Id UNIQUEIDENTIFIER NOT NULL,
            OrgNo VARCHAR(50) NULL,
            PickListValue_Id UNIQUEIDENTIFIER NULL,
            PickListId INT NULL,
            [Key] VARCHAR(50) NULL,
            [Text] VARCHAR(50) NULL,
            PreferredLanguageId UNIQUEIDENTIFIER NULL
        );

        INSERT INTO @defaultLanguageTable
        (
            Tenant_Code,
            Tenent_Id,
            OrgNo,
            PickListValue_Id,
            PickListId,
            [Key],
            [Text],
            PreferredLanguageId
        )
        EXEC [dbo].[Tenant_GetDefaultLanguageDetails] @guidTenantId;


        SELECT TOP 1
            [Key],
            [Text]
        FROM @defaultLanguageTable;


    END;

END;

GO

/****** Object:  StoredProcedure [dbo].[Resource_SaveorUpdateorDelete]    Script Date: 16-Jul-19 15:48:05 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_SaveorUpdateorDelete]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_SaveorUpdateorDelete]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
 
CREATE PROCEDURE [dbo].[Resource_SaveorUpdateorDelete]  
(  
	@guidTenantId UNIQUEIDENTIFIER, 
    @strKey [dbo].[mediumText] = NULL,
	@strValue [dbo].[xLargeText] = NULL,
	@strLanguage [dbo].[xSmallText] = NULL,
	@guidId UNIQUEIDENTIFIER = NULL,
	@mode CHAR(1) = NULL
	,@strMessage VARCHAR(100) OUTPUT
)  
AS   
    SET NOCOUNT ON   
     
BEGIN  

	IF @mode = 'S'
	BEGIN

			IF EXISTS (SELECT * FROM [dbo].[Resource] WHERE TenantId = @guidTenantId AND [Key] = @strKey AND [Language] = @strLanguage)
			BEGIN
					SET @strMessage = 'Resource already exits'
			END
			ELSE
			BEGIN
					INSERT INTO [dbo].[Resource] (TenantId, [Key], [Value], [Language])
					VALUES (@guidTenantId,@strKey,@strValue,@strLanguage)
			END
	END
	ELSE IF @mode = 'U'
	BEGIN

			IF EXISTS (SELECT * FROM [dbo].[Resource] WHERE TenantId = @guidTenantId AND [Key] = @strKey AND [Language] = @strLanguage AND Id <> @guidId)
			BEGIN
					SET @strMessage = 'Resource already exits'
			END
			ELSE
			BEGIN

					UPDATE 	[dbo].[Resource]
					SET [Key] = @strKey,
					[Value] = @strValue,
					[Language] = @strLanguage
					WHERE TenantId = @guidTenantId AND Id = @guidId

			END

	END
	ELSE IF @mode = 'D'
	BEGIN
			DELETE FROM 	[dbo].[Resource]		
			WHERE TenantId = @guidTenantId AND Id = @guidId

	END


END  

GO

/****** Object:  StoredProcedure [dbo].[Resource_Update]    Script Date: 16-Jul-19 15:48:41 PM ******/
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
            [Language] = @strLanguage
        WHERE TenantId = @guidTenantId
              AND Id = @guidId;

    END;



END;

GO