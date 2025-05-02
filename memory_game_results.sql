SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[memory_game_results](
	[game_id] [int] IDENTITY(1,1) NOT NULL,
	[player1_id] [int] NOT NULL,
	[player2_id] [int] NOT NULL,
	[winner_id] [int] NOT NULL,
	[game_date] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[memory_game_results] ADD PRIMARY KEY CLUSTERED 
(
	[game_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[memory_game_results] ADD  DEFAULT (getdate()) FOR [game_date]
GO
ALTER TABLE [dbo].[memory_game_results]  WITH CHECK ADD FOREIGN KEY([player1_id])
REFERENCES [dbo].[players] ([player_id])
GO
ALTER TABLE [dbo].[memory_game_results]  WITH CHECK ADD FOREIGN KEY([player2_id])
REFERENCES [dbo].[players] ([player_id])
GO
ALTER TABLE [dbo].[memory_game_results]  WITH CHECK ADD FOREIGN KEY([winner_id])
REFERENCES [dbo].[players] ([player_id])
GO
