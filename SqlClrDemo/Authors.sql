CREATE TABLE [dbo].[Authors] ( [AuthorId] INT IDENTITY (1, 1) NOT NULL
,                              [Name]     NVARCHAR (MAX) NULL
,                              CONSTRAINT [PK_dbo.Authors] PRIMARY KEY CLUSTERED ([AuthorId] ASC) );
GO
CREATE TRIGGER tri_Insert_Authors
ON [dbo].[Authors]
FOR INSERT
AS EXTERNAL NAME SqlClrDemo.Triggers.InsertAuthorTrigger;