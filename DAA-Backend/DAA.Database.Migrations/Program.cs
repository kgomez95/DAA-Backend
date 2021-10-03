using DAA.Database.Migrations.Contexts;
using DAA.Database.Views.VideoGames;
using DAA.Utils.Extensions;
using System;
using System.Linq;

namespace DAA.Database.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            DAADbContext dbContext = new DAADbContext();
            VideoGameScoreView[] test = dbContext.VideoGamesScoreView.ToArray();

            for (int i = 0; i < test.Length; i++)
            {
                Console.WriteLine(test[i].ToJson());
            }
        }
    }
}
