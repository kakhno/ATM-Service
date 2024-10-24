using System;
using System.Collections.Generic;

public class CardDetails
{
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string CVC { get; set; }
    public double Balance { get; set; }
}

public class Transaction
{
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; }
    public double Amount { get; set; }
    public double AmountUSD { get; set; } 
    public double AmountEUR { get; set; } 
}

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public CardDetails CardDetails { get; set; }
    public string PinCode { get; set; }
    public List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();
}
