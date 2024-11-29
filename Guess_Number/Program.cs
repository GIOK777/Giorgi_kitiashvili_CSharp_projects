namespace Guess_Number
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 1000);
            //Console.WriteLine(randomNumber);
            Console.WriteLine("How many try you need to guess number between 0 - 1000 ?");
            int.TryParse(Console.ReadLine(), out int tryNumber);
            for (int i = 1; i <= tryNumber; i++)
            {
                Console.WriteLine($"Guess number:");
                int.TryParse(Console.ReadLine(), out int num);
                if (num < randomNumber) { Console.WriteLine($"Enter more then {num} - You have {tryNumber - i} try"); }
                else if (num > randomNumber) { Console.WriteLine($"Enter less then {num} - You have {tryNumber - i} try"); }
                else if (num == randomNumber) { Console.WriteLine($"You guessed it in {i} try"); }
                if (i == tryNumber) { Console.WriteLine("Tries expired"); }
            }
        }
    }
}

