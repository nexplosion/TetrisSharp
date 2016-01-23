namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameField();

            game.Init();
            game.Start();
            System.Console.WriteLine("\nGame Over!");
            System.Console.ReadLine();
        }
    }
}
