CREATE TABLE [dbo].[Contacts] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (20)  NULL,
    [LastName]     VARCHAR (50)  NULL,
    [PhoneNumber]  VARCHAR (50)  NULL,
    [Address]      VARCHAR (500) NULL,
        [City]      VARCHAR (50) NULL,
            [State]      VARCHAR (2) NULL,
                [Zip]      VARCHAR (10) NULL,

    [EmailAddress] VARCHAR (50)  NULL,
    [Company]      VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

