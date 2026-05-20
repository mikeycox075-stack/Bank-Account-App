public class BankAccount
{
    private string name;
    private string address;
    private string phone;
    private string email;
    private string password;
    private decimal balance;
    private string accountType;
    private string role;

    public BankAccount(string name, string address, string phone,
        string email, string password, decimal balance,
        string accountType, string role)
    {
        this.name = name;
        this.address = address;
        this.phone = phone;
        this.email = email;
        this.password = password;
        this.balance = balance;
        this.accountType = accountType;
        this.role = role;
    }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(name)
            && !string.IsNullOrWhiteSpace(email)
            && !string.IsNullOrWhiteSpace(password)
            && balance >= 0;
    }


    public string GetName() => name;
    public string GetAddress() => address;
    public string GetPhone() => phone;
    public string GetEmail() => email;
    public string GetPassword() => password;
    public decimal GetBalance() => balance;
    public string GetAccountType() => accountType;
    public string GetRole() => role;

    // Setters
    public void SetBalance(decimal value) => balance = value;

    public override string ToString()
    {
        return $"{name},{address},{phone},{email},{password},{balance},{accountType},{role}";
    }
}