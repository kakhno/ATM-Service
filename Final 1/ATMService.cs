using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Final_1
{
    public class ATMService
    {
        private User user;

        public ATMService(string jsonFilePath)
        {
            LoadUserData(jsonFilePath);
        }

        private void LoadUserData(string jsonFilePath)
        {
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                user = JsonConvert.DeserializeObject<User>(json) ?? new User();

                // tranzakciis istoria iyos carieli listi
                user.TransactionHistory = new List<Transaction>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading user data: " + ex.Message);
            }
        }

        public bool ValidateCard(string cardNumber, string expirationDate)
        {
            return user.CardDetails.CardNumber == cardNumber &&
                   user.CardDetails.ExpirationDate == expirationDate;
        }

        public bool ValidatePin(string pin)
        {
            return user.PinCode == pin;
        }

        public void ViewBalance()
        {
            Console.WriteLine($"Current Balance: {user.CardDetails.Balance}");
            AddTransaction("Balance Check", 0);
        }

        public void Withdraw(double amount)
        {
            if (amount <= user.CardDetails.Balance)
            {
                user.CardDetails.Balance -= amount;
                AddTransaction("Withdrawal", amount);
                Console.WriteLine($"Successfully withdrew: {amount}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }

        public void Deposit(double amount)
        {
            user.CardDetails.Balance += amount;
            AddTransaction("Deposit", amount);
            Console.WriteLine($"Successfully deposited: {amount}");
        }

        public void ChangePin(string newPin)
        {
            user.PinCode = newPin;
            AddTransaction("Change PIN", 0);
            Console.WriteLine("PIN changed successfully.");
        }

        public void ConvertCurrency()
        {
            Console.WriteLine("Select conversion option:");
            Console.WriteLine("1. Convert Local Currency to USD");
            Console.WriteLine("2. Convert Local Currency to EUR");
            Console.WriteLine("3. Convert USD to Local Currency");
            Console.WriteLine("4. Convert EUR to Local Currency");

            string choice = Console.ReadLine();
            double amount;

            if (choice == "1")
            {
                Console.WriteLine("Enter amount in Local Currency:");
                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    double convertedAmount = amount * 0.37;
                    AddTransaction("Converted to USD", convertedAmount);
                    Console.WriteLine($"{amount} Local Currency is {convertedAmount} USD.");
                }
                else
                {
                    Console.WriteLine("Invalid amount entered.");
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine("Enter amount in Local Currency:");
                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    double convertedAmount = amount * 0.34;
                    AddTransaction("Converted to EUR", convertedAmount);
                    Console.WriteLine($"{amount} Local Currency is {convertedAmount} EUR.");
                }
                else
                {
                    Console.WriteLine("Invalid amount entered.");
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("Enter amount in USD:");
                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    double convertedAmount = amount / 0.37;
                    AddTransaction("Converted to Local Currency", convertedAmount);
                    Console.WriteLine($"{amount} USD is {convertedAmount} Local Currency.");
                }
                else
                {
                    Console.WriteLine("Invalid amount entered.");
                }
            }
            else if (choice == "4")
            {
                Console.WriteLine("Enter amount in EUR:");
                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    double convertedAmount = amount / 0.34;
                    AddTransaction("Converted to Local Currency", convertedAmount);
                    Console.WriteLine($"{amount} EUR is {convertedAmount} Local Currency.");
                }
                else
                {
                    Console.WriteLine("Invalid amount entered.");
                }
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        public void ShowLastTransactions()
        {
            var transactions = user.TransactionHistory.TakeLast(user.TransactionHistory.Count).ToList();

            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions available.");
                return;
            }

            foreach (var transaction in transactions)
            {
                string amountDisplay = transaction.TransactionType == "Balance Check" || transaction.TransactionType == "Change PIN" ? "" : $" of {transaction.Amount}";
                Console.WriteLine($"{transaction.TransactionDate}: {transaction.TransactionType}{amountDisplay}");
            }
        }

        private void AddTransaction(string type, double amount)
        {
            user.TransactionHistory.Add(new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                TransactionType = type,
                Amount = amount
            });

            if (user.TransactionHistory.Count > 5)
            {
                user.TransactionHistory.RemoveAt(0);
            }
        }

        public void SaveData(string jsonFilePath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving user data: " + ex.Message);
            }
        }
    }
}
