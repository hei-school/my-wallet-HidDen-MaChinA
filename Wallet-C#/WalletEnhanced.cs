using System;
using System.Collections.Generic;

namespace WalletEhanced
{
    class Wallet
    {
        private int money;
        private CIN cin;
        private List<BankCard> banqueCard = new List<BankCard>();
        private DriverLicence driverLicence;
        private int visiteCard;
        private List<Photo> photos = new List<Photo>();

        public Wallet(int money, string cin, string driverLicenceId)
        {
            this.money = money;
            this.cin = new CIN(cin);
            this.driverLicence = new DriverLicence(driverLicenceId);

            // Initializing methods for various functionalities
        }

        public void Launch()
        {
            while (true)
            {
                Console.WriteLine(Inventary());
                Console.WriteLine(
                    "0. To throw the wallet away\n" +
                    "1. If you want to spend money\n" +
                    "2. If you want to add money to your wallet\n" +
                    "3. If you want to pull in/out your cin\n" +
                    "4. if you want to add a CIN (if there aren't any yet)\n" +
                    "5. If you want to pull in/out your driver licence\n" +
                    "6. if you want to add a driver licence(if there aren't any yet)\n" +
                    "7. If you want to pull in/out a bank card\n" +
                    "8. if you want to add bank card\n" +
                    "9. If you want to pull in/out a photo\n" +
                    "10. If you want to add some photo\n" +
                    "11. If you want to give some visit card\n" +
                    "12. if you want add some visit card into your wallet\n"
                );

                int select;
                if (!int.TryParse(Console.ReadLine(), out select))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (select)
                {
                    case 1:
                        Console.Write("How much you wanna spend? ");
                        int moneyToSpend;
                        if (!int.TryParse(Console.ReadLine(), out moneyToSpend))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            continue;
                        }
                        Console.WriteLine(SpendMoney(moneyToSpend));
                        break;
                    case 2:
                        Console.Write("How much you wanna add? ");
                        int moneyToAdd;
                        if (!int.TryParse(Console.ReadLine(), out moneyToAdd))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            continue;
                        }
                        Console.WriteLine(AddMoney(moneyToAdd));
                        break;
                    case 3:
                        Console.WriteLine(PullOutOrPullInCin());
                        break;
                    case 4:
                        Console.Write("Enter the id of your CIN: ");
                        string cinToAdd = Console.ReadLine();
                        Console.WriteLine(AddInACin(cinToAdd));
                        break;
                    case 5:
                        Console.WriteLine(PullOutOrPullInDriverLicence());
                        break;
                    case 6:
                        Console.Write("Enter the name you want to give the driver licence: ");
                        string driverLicenceToAdd = Console.ReadLine();
                        Console.WriteLine(AddDriverLicence(driverLicenceToAdd));
                        break;
                    case 7:
                        Console.Write("Enter the name of the bank card: ");
                        string bankCardName = Console.ReadLine();
                        Console.WriteLine(PullOutOrPullInBanqueCard(bankCardName));
                        break;
                    case 8:
                        Console.Write("Enter the name of the bank card: ");
                        string bankCardToAdd = Console.ReadLine();
                        Console.WriteLine(AddBankCard(bankCardToAdd));
                        break;
                    case 9:
                        Console.Write("Enter the name of the photo: ");
                        string photoName = Console.ReadLine();
                        Console.WriteLine(PullOutOrPullInPhoto(photoName));
                        break;
                    case 10:
                        Console.Write("Enter the name of the photo: ");
                        string photoToAdd = Console.ReadLine();
                        Console.WriteLine(AddPhoto(photoToAdd));
                        break;
                    case 11:
                        Console.Write("How much visit card you want to give? ");
                        int howMuch;
                        if (!int.TryParse(Console.ReadLine(), out howMuch))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            continue;
                        }
                        Console.WriteLine(GiveVisiteCard(howMuch));
                        break;
                    case 12:
                        Console.Write("How much visit card you want to add? ");
                        int visitCardToAdd;
                        if (!int.TryParse(Console.ReadLine(), out visitCardToAdd))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            continue;
                        }
                        Console.WriteLine(AddVisitCard(visitCardToAdd));
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Please choose a valid option.");
                        break;
                }
            }
        }

        private string Inventary()
        {
            string result = $"Money: {money}\n";
            result += $"CIN: {(cin == null ? "no CIN" : cin.Id + $"{(cin.IsIn ? "" : "not ")}in the wallet")}\n";
            result += "Bank cards:\n" + (banqueCard.Count > 0 ? string.Join("\n", banqueCard.Select(card => $"   {card.Name} {(card.Status ? "is" : "is not")} in the wallet")) : "   no bank cards\n");
            result += $"Driver licence: {(driverLicence == null ? "no driver licence" : $"{driverLicence.Id} {(driverLicence.IsIn ? "" : "not ")}in the wallet")}\n";
            result += $"Visite card: {visiteCard}\n";
            result += "Photos:\n" + (photos.Count > 0 ? string.Join("\n", photos.Select(photo => $"   {photo.Name} {(photo.Status ? "is" : "is not")} in the wallet")) : "   no photos\n");

            return result;
        }

        private string SpendMoney(int amount)
        {
            if (amount <= 0)
            {
                return "Invalid amount. Please enter a positive number.";
            }

            if (money == 0)
            {
                return "You're broke!";
            }

            if (amount > money)
            {
                return "Insufficient funds.";
            }

            money -= amount;
            return $"OK. Your current balance is now {money}.";
        }

        private string AddMoney(int amount)
        {
            if (amount <= 0)
            {
                return "Invalid amount. Please enter a positive number.";
            }

            money += amount;
            return $"OK. Your current balance is now {money}.";
        }

        private string PullOutOrPullInCin()
        {
            if (cin == null)
            {
                return "No CIN available.";
            }

            cin.IsIn = !cin.IsIn;
            return "Done.";
        }

        private string AddInACin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "No CIN added.";
            }

            cin = new CIN(id);
            return "OK. You have a CIN now.";
        }

        private string PullOutOrPullInDriverLicence()
        {
            if (driverLicence == null)
            {
                return "No driver licence available.";
            }

            driverLicence.IsIn = !driverLicence.IsIn;
            return "Done.";
        }

        private string AddDriverLicence(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "No driver licence added.";
            }

            driverLicence = new DriverLicence(id);
            return "OK. Driver licence added.";
        }

        private string PullOutOrPullInBanqueCard(string name)
        {
            if (banqueCard.Count == 0)
            {
                return "No bank cards available.";
            }

            var card = banqueCard.Find(c => c.Name == name);
            if (card != null)
            {
                card.Status = !card.Status;
                return "Done.";
            }

            return "Bank card not found.";
        }

        private string AddBankCard(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Invalid bank card name.";
            }

            banqueCard.Add(new BankCard(name, true));
            return "Done.";
        }

        private string PullOutOrPullInPhoto(string name)
        {
            if (photos.Count == 0)
            {
                return "No photos available.";
            }

            var photo = photos.Find(p => p.Name == name);
            if (photo != null)
            {
                photo.Status = !photo.Status;
                return "Done.";
            }

            return "Photo not found.";
        }

        private string AddPhoto(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Invalid photo name.";
            }

            photos.Add(new Photo(name, true));
            return "Done.";
        }

        private string GiveVisiteCard(int amount)
        {
            if (amount <= 0)
            {
                return "Invalid amount. Please enter a positive number.";
            }

            visiteCard += amount;
            return "Done.";
        }

        private string AddVisitCard(int amount)
        {
            if (amount < 0)
            {
                return "Invalid amount. Please enter a non-negative number.";
            }

            visiteCard += amount;
            return "Done.";
        }
    }

    class BankCard
    {
        public string Name { get; }
        public bool Status { get; set; }

        public BankCard(string name, bool status)
        {
            Name = name;
            Status = status;
        }
    }

    class DriverLicence
    {
        public string Id { get; }
        public bool IsIn { get; set; }

        public DriverLicence(string id)
        {
            Id = id;
            IsIn = true;
        }
    }

    class CIN
    {
        public string Id { get; }
        public bool IsIn { get; set; }

        public CIN(string id)
        {
            Id = id;
            IsIn = true;
        }
    }

    class Photo
    {
        public string Name { get; }
        public bool Status { get; set; }

        public Photo(string name, bool status)
        {
            Name = name;
            Status = status;
        }
    }

}
