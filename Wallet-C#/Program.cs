// See https://aka.ms/new-console-template for more information
using System;
using WalletEhanced;
public class WalletMain
{
    
    public static void Main(String[] args)
    {
        Console.WriteLine("Inital money in your wallet\n");
        int initialValue;
        if (!int.TryParse(Console.ReadLine(), out initialValue)) { }
        Console.WriteLine("Your CIN if you have one\n");
        string cin = Console.ReadLine();
        Console.WriteLine("You driver licence if you have one\n");
        string driverLicence = Console.ReadLine();
        Wallet portefeuille = new(initialValue, cin, driverLicence);
        portefeuille.Launch();
    }
}