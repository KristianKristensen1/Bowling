using ConsoleTables;
using System.Collections.ObjectModel;

namespace BowlingGame
{
    internal class Program
    {
        private static ConsoleTable table = new ConsoleTable("Frame number", "Roll 1", "Roll 2", "Score");
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bowling!");

            var game = new BowlingGame();

            while (!game.IsFinished)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("How many pins did you knock down?");

                try
                {
                    var pins = Convert.ToInt32(Console.ReadLine());
                    game.AddRoll(pins);
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Something went wrong: {ex.Message}");
                    continue;
                }

                var scoreboard = game.GetScoreboard();
                DrawScoreboard(scoreboard);
            }
        }

        static void DrawScoreboard(ReadOnlyCollection<Frame> scoreBoard)
        {
            Console.Clear();
            table.Rows.Clear();

            for (int frameNumber = 0; frameNumber < scoreBoard.Count; frameNumber++)
            {
                var frame = scoreBoard.ElementAt(frameNumber);
                table.AddRow(frameNumber + 1, frame.Rolls.ElementAtOrDefault(0), frame.Rolls.ElementAtOrDefault(1), frame.Score);
            }
            table.Write();
        }
    }
}
