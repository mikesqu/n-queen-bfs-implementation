namespace QueenChess
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the size of the board");
            string input = Console.ReadLine();

            Console.WriteLine("Enter the amount of win states to find");
            string winStateAmount = Console.ReadLine();

            Console.WriteLine("Print all the states explored? 1 yes; or 0 for no");
            string printStates = Console.ReadLine();

            int sizeOfBoard = Int32.Parse(input);
            int winStateN = Int32.Parse(winStateAmount);
            int printStateChoice = Int32.Parse(printStates);

            Board board = new Board(sizeOfBoard);

            CollisionChecker cC = new CollisionChecker();

            PathFinder pathFinder = new PathFinder(board, cC, winStateN);

            PathFinderResult result = pathFinder.GetResultBFS();

            UI ui = new UI();

            if (printStateChoice == 1)
            {
                Console.WriteLine("StatesExplored: ");
                foreach (var state in result.States)
                {
                    ui.DrawTiles(state.Value);
                    Console.Write("\n");
                }
            }
            Console.WriteLine($"states explored number: {result.StatesChecked}");
            Console.WriteLine($"win states number: {result.WinStates.Count}");
            Console.WriteLine("WinStates: ");
            foreach (var state in result.TimeResults)
            {
                Console.WriteLine($"time took (seconds): {state.Key.TotalSeconds}");
                ui.DrawTiles(state.Value,removeSkips: true);
                Console.Write("\n");
            }

            Console.ReadLine();
        }
    }
}