
/****** Object:  StoredProcedure [dbo].[Resource_Reset]    Script Date: 30-07-2019 18:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ===================================================
-- Author:		Soma Ghosh Chattopadhyay
-- Create date: 30.07.2019
-- Description:	Delete all and Insert Resources for a Tenant (Reset)
-- ==================================================

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Reset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Reset]
GO

CREATE PROCEDURE [dbo].[Resource_Reset]
(
    @guidTenantId UNIQUEIDENTIFIER,
	@strDefaultLanguage [dbo].[xSmallText],
    @XmlForResources AS XML
)
AS
BEGIN
    SET NOCOUNT ON;
    

    DECLARE @DATA TABLE
    (
        Id INT IDENTITY(1, 1),
        TenantId UNIQUEIDENTIFIER NOT NULL,
        [Key] [dbo].[mediumText] NOT NULL,
        [Value] [dbo].[xLargeText] NOT NULL,
		[EntityCode] [dbo].[xSmallText] NULL,
		[IsStatic] bit NULL
    );

	INSERT INTO @DATA
    SELECT ref.value('./@TenantId', 'uniqueidentifier') AS TenantId,
           ref.value('./@Key', '[dbo].[mediumText]') AS [Key],
           ref.value('./@Value', '[dbo].[xLargeText]') AS [Value]
		  ,ref.value('./@EntityCode', '[dbo].[xSmallText]') AS [EntityCode]
		  ,ref.value('./@IsStatic', 'bit') AS [IsStatic]
    FROM @XmlForResources.nodes('/Resources/Resource') AS T(ref);
     
	 BEGIN TRY
	 BEGIN TRANSACTION
	

   DELETE FROM Resource
   WHERE TenantId = @guidTenantId

-- Rollback the transaction if there were any errors
    IF @@ERROR <> 0
     BEGIN
       ROLLBACK
       RETURN 0
    END
   
    
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

	-- Rollback the transaction if there were any errors
    IF @@ERROR <> 0
     BEGIN
    -- Rollback the transaction
     ROLLBACK
       RETURN 0
    END

 COMMIT
 END TRY

 BEGIN CATCH
        ROLLBACK TRANSACTION
		Return 1
 END CATCH

  IF @@ERROR <> 0
    BEGIN
        RETURN 1;
  END;
    RETURN 0;
END;
