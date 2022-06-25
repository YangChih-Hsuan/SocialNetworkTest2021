USE [SocialNetworkTest2021]
GO

/****** Object:  Table [dbo].[VerificationCode]    Script Date: 2022/6/25 下午 10:57:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VerificationCode]') AND type in (N'U'))
BEGIN

	EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'VerificationDate'

	EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'CreateDate'

	EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'Status'

	EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'VCode'

	EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'Mail'

	EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'Key'

	DROP TABLE [dbo].[VerificationCode]

END
GO

/****** Object:  Table [dbo].[VerificationCode]    Script Date: 2022/6/25 下午 10:57:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VerificationCode](
	[Key] [int] IDENTITY(1,1) NOT NULL,
	[Mail] [nvarchar](100) NOT NULL,
	[VCode] [char](10) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[VerificationDate] [datetime] NULL,
 CONSTRAINT [PK_VerificationCode] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主Key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'Key'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'Mail'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'驗證碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'VCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'驗證狀態 (0=尚未驗證, 1=已驗證)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'驗證日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VerificationCode', @level2type=N'COLUMN',@level2name=N'VerificationDate'
GO

