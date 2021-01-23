CREATE TABLE [dbo].[Inscricoes] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Nome]          NVARCHAR (MAX) NOT NULL,
    [Email]       NVARCHAR (MAX) NOT NULL,
    [Localidade]  NVARCHAR (MAX) NULL,
    [CartaoCidadao] NVARCHAR (MAX) NOT NULL,
    [DNasc]         DATETIME2 (7)  NOT NULL,
    [UrlImagem]     NVARCHAR (MAX) NULL,
    [UserId]        NVARCHAR (450) NULL,
    CONSTRAINT [PK_Inscricoes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Inscricoes_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])

);


GO
CREATE NONCLUSTERED INDEX [IX_Inscricoes_UserId]
    ON [dbo].[Inscricoes]([UserId] ASC);

