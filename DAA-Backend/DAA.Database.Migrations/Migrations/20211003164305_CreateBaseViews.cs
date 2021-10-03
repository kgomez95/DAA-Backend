using DAA.Constants.Databases;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAA.Database.Migrations.Migrations
{
    public partial class CreateBaseViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			// Vista: VW_VIDEOGAMESCORE
			migrationBuilder.Sql(@$"
                CREATE VIEW {DbViewsValues.VideoGameScores.VIEW_NAME} AS
                SELECT
	                vgs.VGS_Id as {DbViewsValues.VideoGameScores.ID}, 
	                vgs.VGS_Name as {DbViewsValues.VideoGameScores.VIDEO_GAME_NAME}, 
	                pts.PTS_Name as {DbViewsValues.VideoGameScores.PLATFORM_NAME}, 
	                vgs.VGS_Score as {DbViewsValues.VideoGameScores.SCORE}
                FROM
	                VIDEO_GAMES vgs
	                LEFT JOIN PLATFORMS pts ON vgs.PTS_Id = pts.PTS_Id;");

			// Vista: VW_VIDEOGAMES
			migrationBuilder.Sql(@$"
                CREATE VIEW {DbViewsValues.VideoGames.VIEW_NAME} AS
                SELECT
	                vgs.VGS_Id as {DbViewsValues.VideoGames.VIDEO_GAME_ID}, 
	                vgs.VGS_Name as {DbViewsValues.VideoGames.VIDEO_GAME_NAME}, 
	                vgs.VGS_Description as {DbViewsValues.VideoGames.VIDEO_GAME_DESCRIPTION}, 
	                vgs.VGS_Price as {DbViewsValues.VideoGames.VIDEO_GAME_PRICE}, 
	                vgs.VGS_Score as {DbViewsValues.VideoGames.VIDEO_GAME_SCORE}, 
	                vgs.VGS_ReleaseDate as {DbViewsValues.VideoGames.VIDEO_GAME_RELEASE_DATE}, 
	                pts.PTS_Id as {DbViewsValues.VideoGames.PLATFORM_ID}, 
	                pts.PTS_Name as {DbViewsValues.VideoGames.PLATFORM_NAME}, 
	                pts.PTS_Company as {DbViewsValues.VideoGames.PLATFORM_COMPANY}, 
	                pts.PTS_Price as {DbViewsValues.VideoGames.PLATFORM_PRICE}, 
	                pts.PTS_ReleaseDate as {DbViewsValues.VideoGames.PLATFORM_RELEASE_DATE}
                FROM
	                VIDEO_GAMES vgs
	                LEFT JOIN PLATFORMS pts ON vgs.PTS_Id = pts.PTS_Id;");

			// Vista: VW_PLATFORMS
			migrationBuilder.Sql(@$"
                CREATE VIEW {DbViewsValues.Platforms.VIEW_NAME} AS
                SELECT
	                pts.PTS_Id as {DbViewsValues.Platforms.ID}, 
	                pts.PTS_Name as {DbViewsValues.Platforms.NAME}, 
	                pts.PTS_Company as {DbViewsValues.Platforms.COMPANY}, 
	                pts.PTS_Price as {DbViewsValues.Platforms.PRICE}, 
	                pts.PTS_ReleaseDate as {DbViewsValues.Platforms.RELEASE_DATE}
                FROM
	                PLATFORMS pts;");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DROP VIEW { DbViewsValues.VideoGameScores.VIEW_NAME};");
            migrationBuilder.Sql($"DROP VIEW { DbViewsValues.VideoGames.VIEW_NAME};");
			migrationBuilder.Sql($"DROP VIEW { DbViewsValues.Platforms.VIEW_NAME};");
		}
    }
}
