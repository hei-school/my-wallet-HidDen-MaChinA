// See https://aka.ms/new-console-template for more information
using System;
using Wallet;
public class WalletMain
{
    private static WalletConsoleInterface interfaces = new();
    public static void Main(String[] args)
    {
        interfaces.InitializeWallet();
        while (true)
        {
            switch (interfaces.MainMenu())
            {
                case 0:
                    interfaces.History();
                    return;
                case 1:
                    interfaces.SpendMoneyInterface();
                    continue;
                case 2:
                    interfaces.AddMoneyInterface();
                    continue;
                case 3:
                    interfaces.ManipulateWalletBudgets();
                    continue;
                case 4:
                    interfaces.History();
                    continue;
            }
        }
    }
}