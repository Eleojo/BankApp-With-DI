namespace consoleTaskBreakDown
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WELCOME TO MY BANK APP");
            Console.WriteLine("What would you like to do today");

       
            Console.WriteLine("Press 1 to Register \n Press 2 to Exit.");
            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Please input 1 or 2");
            }
            else if (userInput == "1")
            {
                var userMethod = new UserMethods();
                Console.WriteLine("Enter your FirstName");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter your LastName");
                string lastName = Console.ReadLine();

                Console.WriteLine("Enter your email");
                string email = Console.ReadLine();

                Console.WriteLine("Enter your password");
                string password = Console.ReadLine();

                userMethod.Register(firstName, lastName, email, password);
                Console.WriteLine("Congratulations. You have been registered successfully");
                Console.ReadKey();
                Console.Clear();    
                Main(args);
            }
            else if(userInput == "2")
            {
                Console.WriteLine("Thank you for banking with us.");
                Environment.Exit(0);
            }
        }



    }
}
