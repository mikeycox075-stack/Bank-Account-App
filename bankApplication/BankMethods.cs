using System;
using System.Collections.Generic;
using System.IO;

public class BankMethods
{
    public List<BankAccount> accounts = new List<BankAccount>();

    public void LoadAccounts()
    {
        accounts.Clear();

        if (!File.Exists("accounts.txt"))
        {
            Console.WriteLine("No accounts file found.");
            return;
        }

        foreach (var line in File.ReadAllLines("accounts.txt"))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var p = line.Split(',');

            accounts.Add(new BankAccount(
                p[0], p[1], p[2], p[3],
                p[4], decimal.Parse(p[5]),
                p[6], p[7]
            ));
        }
    }

    public void SaveAccounts()
    {
        using (StreamWriter sw = new StreamWriter("accounts.txt"))
        {
            foreach (var acc in accounts)
            {
                if (acc.IsValid())
                    sw.WriteLine(acc);
            }
        }
    }

    public BankAccount Login(string email, string password)
    {
        foreach (var acc in accounts)
        {
            if (acc.GetEmail() == email && acc.GetPassword() == password)
                return acc;
        }

        return null;
    }
    public void AddAccount(BankAccount acc)
    {
        if (!acc.IsValid())
        {
            Console.WriteLine("Invalid account.");
            return;
        }

        accounts.Add(acc);
        Console.WriteLine("Account added.");
    }

    public BankAccount FindAccount(string email)
    {
        foreach (var acc in accounts)
        {
            if (acc.GetEmail().Equals(email, StringComparison.OrdinalIgnoreCase))
                return acc;
        }

        return null;
    }

    public void RemoveAccount(string email)
    {
        var acc = FindAccount(email);

        if (acc != null)
        {
            accounts.Remove(acc);
            Console.WriteLine("Account removed.");
        }
        else
        {
            Console.WriteLine("Not found.");
        }
    }

    public void ListAccounts()
    {
        foreach (var acc in accounts)
        {
            Console.WriteLine($"{acc.GetName()} - {acc.GetEmail()}");
        }
    }

    public void Deposit(BankAccount acc, decimal amount)
    {
        acc.SetBalance(acc.GetBalance() + amount);
    }

    public void Withdraw(BankAccount acc, decimal amount)
    {
        if (amount > acc.GetBalance())
        {
            Console.WriteLine("Insufficient funds.");
            return;
        }

        acc.SetBalance(acc.GetBalance() - amount);
    }

    public void Transfer(BankAccount from, BankAccount to, decimal amount)
    {
        if (to == null)
        {
            Console.WriteLine("Target account not found.");
            return;
        }

        if (amount > from.GetBalance())
        {
            Console.WriteLine("Not enough money.");
            return;
        }

        from.SetBalance(from.GetBalance() - amount);
        to.SetBalance(to.GetBalance() + amount);
    }

    public void ApplyInterest(BankAccount acc)
    {
        if (acc.GetAccountType() == "Teen")
            acc.SetBalance(acc.GetBalance() * 1.01m);
        else
            acc.SetBalance(acc.GetBalance() * 1.03m);
    }
}
