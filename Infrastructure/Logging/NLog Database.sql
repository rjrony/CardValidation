SET ANSI_NULLS ON
  SET QUOTED_IDENTIFIER ON
  CREATE TABLE [dbo].[LogEntry] (
      [Id] [int] IDENTITY(1,1) NOT NULL,
      [Date] [datetime] NOT NULL,
	  [Exception] [nvarchar](max) NULL,
	  [Level] [nvarchar](50) NOT NULL,
	  [Logger] [nvarchar](250) NULL,
	  [MachineName] [nvarchar](100) NOT NULL,
	  [Message] [nvarchar](max) NOT NULL,
	  [StackTrace] [nvarchar](max) NULL,
	  [Thread] [int] NULL,
	  [UserName] [nvarchar](500) NULL,     
      
    CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED ([Id] ASC)
      WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
  ) ON [PRIMARY]