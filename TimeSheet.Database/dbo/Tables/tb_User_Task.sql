CREATE TABLE [dbo].[tb_User_Task] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [UserId]      NVARCHAR (128) NOT NULL,
    [TaskDate]    DATETIME       NOT NULL,
    [DateCreated] DATETIME       CONSTRAINT [DF_tb_User_Task_DateCreated] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_tb_User_Task] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tb_User_Task_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

