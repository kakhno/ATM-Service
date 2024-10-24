using System;

namespace Final_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jsonFilePath = @"C:\Users\C\Desktop\Final 1\Final 1\userData.json"; 
            ATMService atmService = new ATMService(jsonFilePath);

            while (true) // თუ მონაცემები არასწორია თავიდან გაეშვება
            {
                Console.WriteLine("Enter Card Number:");
                string cardNumber = Console.ReadLine();

                Console.WriteLine("Enter Expiration Date (MM/YY):");
                string expirationDate = Console.ReadLine();

                if (!atmService.ValidateCard(cardNumber, expirationDate))
                {
                    Console.WriteLine("Invalid card details. Please try again.");
                    continue; 
                }

                Console.WriteLine("Enter PIN:");
                string pin = Console.ReadLine();

                if (!atmService.ValidatePin(pin))
                {
                    Console.WriteLine("Invalid PIN. Exiting.");
                    return;
                }

                while (true)
                {
                    Console.WriteLine("Select an action:");
                    Console.WriteLine("1. View Balance");
                    Console.WriteLine("2. Withdraw");
                    Console.WriteLine("3. Deposit");
                    Console.WriteLine("4. Change PIN");
                    Console.WriteLine("5. Last 5 Transactions");
                    Console.WriteLine("6. Currency Conversion"); 
                    Console.WriteLine("7. Exit");

                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        atmService.ViewBalance();
                    }
                    else if (choice == "2")
                    {
                        Console.WriteLine("Enter amount to withdraw:");
                        if (double.TryParse(Console.ReadLine(), out double withdrawAmount))
                        {
                            atmService.Withdraw(withdrawAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount entered.");
                        }
                    }
                    else if (choice == "3")
                    {
                        Console.WriteLine("Enter amount to deposit:");
                        if (double.TryParse(Console.ReadLine(), out double depositAmount))
                        {
                            atmService.Deposit(depositAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount entered.");
                        }
                    }
                    else if (choice == "4")
                    {
                        Console.WriteLine("Enter new PIN:");
                        string newPin = Console.ReadLine();
                        atmService.ChangePin(newPin);
                    }
                    else if (choice == "5")
                    {
                        atmService.ShowLastTransactions();
                    }
                    else if (choice == "6") 
                    {
                        atmService.ConvertCurrency();
                    }
                    else if (choice == "7")
                    {
                        atmService.SaveData(jsonFilePath);
                        Console.WriteLine("Goodbye!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Try again.");
                    }
                }
            }
        }
    }
}

