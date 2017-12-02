CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](128) NOT NULL,
	[FirstName] [nvarchar](128) NULL,
	[LastName] [nvarchar](128) NULL,
	[UserName] [nvarchar](64) NOT NULL,
	[NormalizedUserName] [nvarchar](64) NOT NULL,
	[Email] [nvarchar](64) NOT NULL,
	[NormalizedEmail] [nvarchar](64) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](128) NULL,
	[PhoneNumber] [nvarchar](32) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[PhotoUrl] [nvarchar](128) NULL,
	[Address] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](128) NULL,
	[SecurityStamp] [nvarchar](128) NULL,
	[RegistrationDate] [datetime] NULL,
	[LastLoginDate] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEndDateTimeUtc] [datetime] NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetTotalNumberOfUsers]
AS
SELECT COUNT(*) AS TotalNumberOfUsers
FROM     dbo.Users


GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[NormalizedName] [nvarchar](32) NOT NULL,
	[ConcurrencyStamp] [nvarchar](128) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersClaims](
	[Id] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](65) NOT NULL,
	[ClaimValue] [nvarchar](65) NOT NULL,
 CONSTRAINT [PK_UsersClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersLogins](
	[LoginProvider] [nvarchar](32) NOT NULL,
	[ProviderKey] [nvarchar](64) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_UsersLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_UsersRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Normalized_UserName_Email] ON [dbo].[Users]
(
	[NormalizedUserName] ASC,
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_UserName_Email] ON [dbo].[Users]
(
	[UserName] ASC,
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[UsersClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_EmailConfirmed]  DEFAULT ((0)) FOR [EmailConfirmed]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_PhoneNumberConfirmed]  DEFAULT ((0)) FOR [PhoneNumberConfirmed]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_LockoutEnabled]  DEFAULT ((0)) FOR [LockoutEnabled]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_TwoFactorEnabled]  DEFAULT ((0)) FOR [TwoFactorEnabled]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_AccessFailedCount]  DEFAULT ((0)) FOR [AccessFailedCount]
GO
ALTER TABLE [dbo].[UsersClaims]  WITH CHECK ADD  CONSTRAINT [FK_UsersClaims_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersClaims] CHECK CONSTRAINT [FK_UsersClaims_Users]
GO
ALTER TABLE [dbo].[UsersLogins]  WITH CHECK ADD  CONSTRAINT [FK_UsersLogins_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersLogins] CHECK CONSTRAINT [FK_UsersLogins_Users]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Roles]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Users]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetsUsers]
	@PageNumber INT = 1,
	@PageSize   INT = 100,
	@SortExpression INT = 0,
	@SortDirection VARCHAR(4) = 'ASC',
	@SearchPhrase VARCHAR(32) = ''
AS
BEGIN
	SET NOCOUNT ON;
	-- SortExpression = 0 => Email
	-- SortExpression = 1 => EmailConfirmed
	-- SortExpression = 2 => PhoneNumber
	-- SortExpression = 3 => LockoutEnabled
	-- SortExpression = 3 => LockoutEndDateTimeUtc
    SELECT Id, Email, EmailConfirmed, PhoneNumber, LockoutEnabled, LockoutEndDateTimeUtc
    FROM dbo.Users
	WHERE Email LIKE '%' + @SearchPhrase + '%' OR
	      PhoneNumber LIKE '%' + @SearchPhrase + '%'
    ORDER BY
		CASE WHEN @SortExpression = 0 AND @SortDirection = 'asc' THEN Email END ASC, 
		CASE WHEN @SortExpression = 0 AND @SortDirection = 'desc' THEN Email END DESC,
		CASE WHEN @SortExpression = 1 AND @SortDirection = 'asc' THEN EmailConfirmed END ASC, 
		CASE WHEN @SortExpression = 1 AND @SortDirection = 'desc' THEN EmailConfirmed END DESC,
		CASE WHEN @SortExpression = 2 AND @SortDirection = 'asc' THEN PhoneNumber END ASC, 
		CASE WHEN @SortExpression = 2 AND @SortDirection = 'desc' THEN PhoneNumber END DESC,
		CASE WHEN @SortExpression = 3 AND @SortDirection = 'asc' THEN LockoutEnabled END ASC, 
		CASE WHEN @SortExpression = 3 AND @SortDirection = 'desc' THEN LockoutEnabled END DESC,
		CASE WHEN @SortExpression = 4 AND @SortDirection = 'asc' THEN LockoutEndDateTimeUtc END ASC, 
		CASE WHEN @SortExpression = 4 AND @SortDirection = 'desc' THEN LockoutEndDateTimeUtc END DESC
    OFFSET @PageSize * (@PageNumber - 1) ROWS
    FETCH NEXT @PageSize ROWS ONLY OPTION (RECOMPILE);
END
GO