package src.com.devoir.walletJava;

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Wallet {
    private int money;
    private CIN cin;
    private List<BankCard> banqueCard = new ArrayList<>();
    private DriverLicence driverLicence;
    private int visiteCard;
    private List<Photo> photos = new ArrayList<>();

    public Wallet(int money, String cin, String driverLicenceId) {
        this.money = money;
        this.cin = new CIN(cin);
        this.driverLicence = new DriverLicence(driverLicenceId);
    }

    public void launch() {
        Scanner scanner = new Scanner(System.in);

        while (true) {
            System.out.println(inventary());
            System.out.println(
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
            try {
                select = Integer.parseInt(scanner.nextLine());
            } catch (NumberFormatException e) {
                System.out.println("Invalid input. Please enter a number.");
                continue;
            }

            switch (select) {
                case 1:
                    System.out.print("How much you wanna spend? ");
                    int moneyToSpend;
                    try {
                        moneyToSpend = Integer.parseInt(scanner.nextLine());
                    } catch (NumberFormatException e) {
                        System.out.println("Invalid input. Please enter a number.");
                        continue;
                    }
                    System.out.println(spendMoney(moneyToSpend));
                    break;
                case 2:
                    System.out.print("How much you wanna add? ");
                    int moneyToAdd;
                    try {
                        moneyToAdd = Integer.parseInt(scanner.nextLine());
                    } catch (NumberFormatException e) {
                        System.out.println("Invalid input. Please enter a number.");
                        continue;
                    }
                    System.out.println(addMoney(moneyToAdd));
                    break;
                case 3:
                    System.out.println(pullOutOrPullInCin());
                    break;
                case 4:
                    System.out.print("Enter the id of your CIN: ");
                    String cinToAdd = scanner.nextLine();
                    System.out.println(addInACin(cinToAdd));
                    break;
                case 5:
                    System.out.println(pullOutOrPullInDriverLicence());
                    break;
                case 6:
                    System.out.print("Enter the name you want to give the driver licence: ");
                    String driverLicenceToAdd = scanner.nextLine();
                    System.out.println(addDriverLicence(driverLicenceToAdd));
                    break;
                case 7:
                    System.out.print("Enter the name of the bank card: ");
                    String bankCardName = scanner.nextLine();
                    System.out.println(pullOutOrPullInBanqueCard(bankCardName));
                    break;
                case 8:
                    System.out.print("Enter the name of the bank card: ");
                    String bankCardToAdd = scanner.nextLine();
                    System.out.println(addBankCard(bankCardToAdd));
                    break;
                case 9:
                    System.out.print("Enter the name of the photo: ");
                    String photoName = scanner.nextLine();
                    System.out.println(pullOutOrPullInPhoto(photoName));
                    break;
                case 10:
                    System.out.print("Enter the name of the photo: ");
                    String photoToAdd = scanner.nextLine();
                    System.out.println(addPhoto(photoToAdd));
                    break;
                case 11:
                    System.out.print("How much visit card you want to give? ");
                    int howMuch;
                    try {
                        howMuch = Integer.parseInt(scanner.nextLine());
                    } catch (NumberFormatException e) {
                        System.out.println("Invalid input. Please enter a number.");
                        continue;
                    }
                    System.out.println(giveVisiteCard(howMuch));
                    break;
                case 12:
                    System.out.print("How much visit card you want to add? ");
                    int visitCardToAdd;
                    try {
                        visitCardToAdd = Integer.parseInt(scanner.nextLine());
                    } catch (NumberFormatException e) {
                        System.out.println("Invalid input. Please enter a number.");
                        continue;
                    }
                    System.out.println(addVisitCard(visitCardToAdd));
                    break;
                case 0:
                    return;
                default:
                    System.out.println("Invalid selection. Please choose a valid option.");
                    break;
            }
        }
    }

    private String inventary() {
        StringBuilder result = new StringBuilder();
        result.append(String.format("Money: %d\n", money));
        result.append(String.format("CIN: %s\n", cin == null ? "no CIN" : String.format("%s %s the wallet", cin.id, cin.isIn ? "is in" : "is not in")));
        result.append("Bank cards:\n");
        if (!banqueCard.isEmpty()) {
            for (BankCard card : banqueCard) {
                result.append(String.format("   %s %s in the wallet\n", card.name, card.status ? "is" : "is not"));
            }
        } else {
            result.append("   no bank cards\n");
        }
        result.append(String.format("Driver licence: %s\n", driverLicence == null ? "no driver licence" : String.format("%s %s the wallet", driverLicence.id, driverLicence.isIn ? "is in" : "is not in")));
        result.append(String.format("Visite card: %d\n", visiteCard));
        result.append("Photos:\n");
        if (!photos.isEmpty()) {
            for (Photo photo : photos) {
                result.append(String.format("   %s %s in the wallet\n", photo.name, photo.status ? "is" : "is not"));
            }
        } else {
            result.append("   no photos\n");
        }

        return result.toString();
    }

    private String spendMoney(int amount) {
        if (amount <= 0) {
            return "Invalid amount. Please enter a positive number.";
        }

        if (money == 0) {
            return "You're broke!";
        }

        if (amount > money) {
            return "Insufficient funds.";
        }

        money -= amount;
        return String.format("OK. Your current balance is now %d.", money);
    }

    private String addMoney(int amount) {
        if (amount <= 0) {
            return "Invalid amount. Please enter a positive number.";
        }

        money += amount;
        return String.format("OK. Your current balance is now %d.", money);
    }

    private String pullOutOrPullInCin() {
        if (cin == null) {
            return "No CIN available.";
        }

        cin.isIn = !cin.isIn;
        return "Done.";
    }

    private String addInACin(String id) {
        if (id == null || id.isEmpty()) {
            return "No CIN added.";
        }

        cin = new CIN(id);
        return "OK. You have a CIN now.";
    }

    private String pullOutOrPullInDriverLicence() {
        if (driverLicence == null) {
            return "No driver licence available.";
        }

        driverLicence.isIn = !driverLicence.isIn;
        return "Done.";
    }

    private String addDriverLicence(String id) {
        if (id == null || id.isEmpty()) {
            return "No driver licence added.";
        }

        driverLicence = new DriverLicence(id);
        return "OK. Driver licence added.";
    }

    private String pullOutOrPullInBanqueCard(String name) {
        if (banqueCard.isEmpty()) {
            return "No bank cards available.";
        }

        for (BankCard card : banqueCard) {
            if (card.name.equals(name)) {
                card.status = !card.status;
                return "Done.";
            }
        }

        return "Bank card not found.";
    }

    private String addBankCard(String name) {
        if (name == null || name.isEmpty()) {
            return "Invalid bank card name.";
        }

        banqueCard.add(new BankCard(name, true));
        return "Done.";
    }

    private String pullOutOrPullInPhoto(String name) {
        if (photos.isEmpty()) {
            return "No photos available.";
        }

        for (Photo photo : photos) {
            if (photo.name.equals(name)) {
                photo.status = !photo.status;
                return "Done.";
            }
        }

        return "Photo not found.";
    }

    private String addPhoto(String name) {
        if (name == null || name.isEmpty()) {
            return "Invalid photo name.";
        }

        photos.add(new Photo(name, true));
        return "Done.";
    }

    private String giveVisiteCard(int amount) {
        if (amount <= 0) {
            return "Invalid amount. Please enter a positive number.";
        }

        visiteCard += amount;
        return "Done.";
    }

    private String addVisitCard(int amount) {
        if (amount < 0) {
            return "Invalid amount. Please enter a non-negative number.";
        }

        visiteCard += amount;
        return "Done.";
    }

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("your initial money: ");
        int money = scanner.nextInt();
        System.out.println("your cin number if you have one: ");
        String cin = scanner.nextLine();
        System.out.println("your driver licence if you have one: ");
        String driverLicence = scanner.next();
        Wallet wallet = new Wallet(money, cin, driverLicence);
        wallet.launch();
    }
}

