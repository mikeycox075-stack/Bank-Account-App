using System;

class Program
{
    static void Main()
    {
        BankMethods bank = new BankMethods();
        bank.LoadAccounts();

        Console.WriteLine("=== BANK LOGIN ===");

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        var user = bank.Login(email, password);

        if (user == null)
        {
            Console.WriteLine("Login failed.");
            return;
        }

        Console.WriteLine($"Welcome {user.GetName()} ({user.GetRole()})");

        int choice;

        do
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. Balance");
            Console.WriteLine("5. Apply Interest");
            Console.WriteLine("6. Add Account");

            if (user.GetRole() == "Admin")
                Console.WriteLine("7. View All Accounts");

            Console.WriteLine("0. Exit");

            Console.Write("Choice: ");

            try
            {
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Amount: ");
                        bank.Deposit(user, decimal.Parse(Console.ReadLine()));
                        break;

                    case 2:
                        Console.Write("Amount: ");
                        bank.Withdraw(user, decimal.Parse(Console.ReadLine()));
                        break;

                    case 3:
                        Console.Write("Send to email: ");
                        string targetEmail = Console.ReadLine();
                        var target = bank.FindAccount(targetEmail);

                        Console.Write("Amount: ");
                        decimal amount = decimal.Parse(Console.ReadLine());

                        bank.Transfer(user, target, amount);
                        break;

                    case 4:
                        Console.WriteLine($"Balance: {user.GetBalance()}");
                        break;

                    case 5:
                        bank.ApplyInterest(user);
                        Console.WriteLine("Interest applied.");
                        break;

                    case 6:
                        AddAccount(bank);
                        break;

                    case 7:
                        if (user.GetRole() == "Admin")
                            bank.ListAccounts();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
                choice = -1;
            }

        } while (choice != 0);

        bank.SaveAccounts();
    }

    static void AddAccount(BankMethods bank)
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Address: ");
        string address = Console.ReadLine();

        Console.Write("Phone: ");
        string phone = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        Console.Write("Balance: ");
        decimal balance = decimal.Parse(Console.ReadLine());

        Console.Write("Type (Adult/Teen): ");
        string type = Console.ReadLine();

        Console.Write("Role (Admin/User): ");
        string role = Console.ReadLine();

        bank.AddAccount(new BankAccount(name, address, phone, email, password, balance, type, role));
    }
}
