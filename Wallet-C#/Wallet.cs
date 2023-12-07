using WalletUtils;
using System.Collections.Generic;
using System.Xml.Linq;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Text;

namespace Wallet
{
    public class WalletMain
    {
        public List<History> Histories { get; set; }
        public List<Budget> Budgets { get; set; }
        public int Money { get; set; }
        public WalletMain(int money)
        {
            Budgets = new List<Budget>();
            this.Money = money;
            Histories = new List<History>();
        }

        public int SpendOrAddMoney(int value,String withDescription,bool weAreSpending) 
        {
            Console.WriteLine(Money);
            if (value > Money && weAreSpending == true) return -1;
            Console.WriteLine(Money);
            if (weAreSpending == true)
            {
                this.Money -= value;
                Console.WriteLine("asdklfalksdfjklsadfj");
            }
            else {
                {
                    this.Money += value;
                    Console.WriteLine("asdkjklsadfj");
                } 
            }
            this.Histories.Add(new History(value, weAreSpending ? "spent for: ":"add for: "+withDescription));
            Console.WriteLine(Money);
            return Money;
        }
        public Budget CreateBudget(int budgetValue, String description, String name)
        {
            Budget toBeAddedToList = new Budget(budgetValue,description,name);
            Budgets.Add(toBeAddedToList);
            this.Money -= budgetValue;
            this.Histories.Add(new History(budgetValue, description));
            return toBeAddedToList;
        }
        public Budget DeleteBudget(String name)
        {
            Budget? toReturn = this.Budgets.Find((el) => el.Name.Equals(name));
            if(toReturn == null)
            {
                return new Budget(0, "no such a budget", "not found");
            }
            Money += toReturn.Value;
            Budgets.Remove(toReturn);
            return toReturn;
        }
        public int SpendOrAddMoneyToOrFromABudget(String budgetName,int value,bool weAreSpending)
        {
            Budget? toReturn = this.Budgets.Find((el) =>el.Name.Equals(budgetName));
            if (toReturn == null) return -1;
            Budgets.Remove(toReturn);
            if (weAreSpending)
            {
                toReturn.Value = toReturn.Value + value;
            }
            else
            {
                toReturn.Value += toReturn.Value + value;
            }
            Budgets.Add(toReturn);
            Histories.Add(new History(value, toReturn.Description));
            return toReturn.Value;
        }
        public int HowMuchMoneyWhereAllocatedToBudgets()
        {
            return Budgets.Sum((budget) => budget.Value);
        }
        public String ListBudget()
        {
            StringBuilder builder = new();
            foreach (Budget budget in Budgets)
            {
                builder.Append("-------------")
                    .Append("\nName: ").Append(budget.Name)
                    .Append("\nValue: ").Append(budget.Value)
                    .Append("\nDescription: ").Append(budget.Description)
                    .Append("\n");
            }
            return builder.ToString();
        }
    }

    public class WalletConsoleInterface
    {
        private WalletMain? myWallet;
        public bool InitializeWallet()
        {
            CodeUtils.WriteIntoTerminal("\nWrite your initial money:");
            int inititalMoney = CodeUtils.ReadLineInt();
            myWallet = new WalletMain(inititalMoney);
            return true;
        }
        public int MainMenu()
        {
            CodeUtils.WriteIntoTerminal(
                "your current money is: " + myWallet.Money,
                myWallet?.HowMuchMoneyWhereAllocatedToBudgets() + " where allocated to budget",
                "if you want to spend money -> 1",
                "if you want to add money in your wallet -> 2",
                "if you want to manage budget -> 3",
                "if you want to look in your history -> 4"
                );
            return CodeUtils.ReadLineInt();
        }
        public void ManipulateWalletBudgets()
        {
            while (true)
            {
                CodeUtils.WriteIntoTerminal(
                    myWallet.ListBudget(),
                    "if you want to add a new Budget -> 1",
                    "if you want to remove a budget -> 2",
                    "if you want to return to the main meny -> 0"
                    );
                int choice = CodeUtils.ReadLineInt();
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        CodeUtils.WriteIntoTerminal("budget to allocate");
                        int initialBudget = CodeUtils.ReadLineInt();
                        CodeUtils.WriteIntoTerminal("budget name");
                        String name = Console.ReadLine();
                        CodeUtils.WriteIntoTerminal("budget description");
                        String description = Console.ReadLine();
                        myWallet.CreateBudget(initialBudget, description, name);
                        continue;
                    case 2:
                        CodeUtils.WriteIntoTerminal("budget to name");
                        String budgetName = Console.ReadLine();
                        myWallet.DeleteBudget(budgetName);
                        continue;
                }
            }
            
        }
        public void SpendMoneyInterface()
        {
            while (true)
            {
                CodeUtils.WriteIntoTerminal(
                    "if you want to spend money directly from your wallet -> 1",
                    "if you want to spend money from a budget -> 2",
                    "if you want to return to the main choice -> 0)"
                );
                int choice = CodeUtils.ReadLineInt();
                if (choice == 0) return;
                CodeUtils.WriteIntoTerminal("how much do you want to spend");
                int toSpend = CodeUtils.ReadLineInt();
                switch (choice)
                {
                    case 1:
                        CodeUtils.WriteIntoTerminal("transaction description");
                        String description = Console.ReadLine();
                        myWallet.SpendOrAddMoney(toSpend, description == null ? "no description" : description, true);
                        continue;
                    case 2:
                        CodeUtils.WriteIntoTerminal(
                            myWallet.ListBudget(),
                            "Budget name to take the money from"
                            );
                        String budgetName = Console.ReadLine();
                        myWallet.SpendOrAddMoneyToOrFromABudget(budgetName == null ? "no description" : budgetName, toSpend, true);
                        continue;
                }
            }
        }
        public void AddMoneyInterface()
        {
            while (true)
            {
                CodeUtils.WriteIntoTerminal(
                    "if you want to add money directly to your wallet -> 1",
                    "if you want to add money to a budget -> 2",
                    "if you want to return to the main choice -> 0)"
                );
                int choice = CodeUtils.ReadLineInt();
                if (choice == 0) return;
                CodeUtils.WriteIntoTerminal("how much do you want to add:");
                int toSpend = CodeUtils.ReadLineInt();
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        CodeUtils.WriteIntoTerminal("transaction description");
                        String description = Console.ReadLine();
                        myWallet.SpendOrAddMoney(toSpend, description, false);
                        continue;
                    case 2:
                        CodeUtils.WriteIntoTerminal(
                            myWallet.ListBudget(),
                            "Budget name to add the money in:"
                            );
                        String budgetName = Console.ReadLine();
                        myWallet.SpendOrAddMoneyToOrFromABudget(budgetName, toSpend, false);
                        continue;
                }
            }
        }
        public void History()
        {
            foreach (History item in myWallet.Histories)
            {
                CodeUtils.WriteIntoTerminal(item.ToString());
            }
        }
    }
}