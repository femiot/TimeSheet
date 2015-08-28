CREATE TABLE [dbo].[tb_Task] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [User_TaskId] INT            NOT NULL,
    [ClientName]  NVARCHAR (250) NOT NULL,
    [Description] NCHAR (500)    NOT NULL,
    [Hours]       NVARCHAR (5)   NOT NULL,
    [Type]        NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_tb_Task] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tb_Task_tb_User_Task] FOREIGN KEY ([User_TaskId]) REFERENCES [dbo].[tb_User_Task] ([Id])
);

