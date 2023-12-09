class Wallet:
    def __init__(self, money, cin, driver_licence_id):
        self.money = money
        self.cin = CIN(cin)
        self.banque_card = []
        self.driver_licence = DriverLicence(driver_licence_id)
        self.visite_card = 0
        self.photos = []

    def launch(self):
        while True:
            print(self.inventary())
            print(
                "0. To throw the wallet away\n"
                "1. If you want to spend money\n"
                "2. If you want to add money to your wallet\n"
                "3. If you want to pull in/out your cin\n"
                "4. if you want to add a CIN (if there aren't any yet)\n"
                "5. If you want to pull in/out your driver licence\n"
                "6. if you want to add a driver licence(if there aren't any yet)\n"
                "7. If you want to pull in/out a bank card\n"
                "8. if you want to add bank card\n"
                "9. If you want to pull in/out a photo\n"
                "10. If you want to add some photo\n"
                "11. If you want to give some visit card\n"
                "12. if you want to add some visit card into your wallet\n"
            )

            try:
                select = int(input("Enter your choice: "))
            except ValueError:
                print("Invalid input. Please enter a number.")
                continue

            if select == 1:
                money_to_spend = int(input("How much you wanna spend? "))
                print(self.spend_money(money_to_spend))
            elif select == 2:
                money_to_add = int(input("How much you wanna add? "))
                print(self.add_money(money_to_add))
            elif select == 3:
                print(self.pull_out_or_pull_in_cin())
            elif select == 4:
                cin_to_add = input("Enter the id of your CIN: ")
                print(self.add_in_a_cin(cin_to_add))
            elif select == 5:
                print(self.pull_out_or_pull_in_driver_licence())
            elif select == 6:
                driver_licence_to_add = input("Enter the name you want to give the driver licence: ")
                print(self.add_driver_licence(driver_licence_to_add))
            elif select == 7:
                bank_card_name = input("Enter the name of the bank card: ")
                print(self.pull_out_or_pull_in_banque_card(bank_card_name))
            elif select == 8:
                bank_card_to_add = input("Enter the name of the bank card: ")
                print(self.add_bank_card(bank_card_to_add))
            elif select == 9:
                photo_name = input("Enter the name of the photo: ")
                print(self.pull_out_or_pull_in_photo(photo_name))
            elif select == 10:
                photo_to_add = input("Enter the name of the photo: ")
                print(self.add_photo(photo_to_add))
            elif select == 11:
                how_much = int(input("How much visit card you want to give? "))
                print(self.give_visite_card(how_much))
            elif select == 12:
                visit_card_to_add = int(input("How much visit card you want to add? "))
                print(self.add_visit_card(visit_card_to_add))
            elif select == 0:
                return
            else:
                print("Invalid selection. Please choose a valid option.")

    def inventary(self):
        result = f"Money: {self.money}\n"
        result += f"CIN: {'no CIN' if self.cin is None else f'{self.cin.id} {"in" if self.cin.is_in else "not in"} the wallet'}\n"
        result += "Bank cards:\n" + (
            "\n".join([f"   {card.name} {'is' if card.status else 'is not'} in the wallet" for card in self.banque_card])
            if self.banque_card
            else "   no bank cards"
        ) + "\n"
        result += f"Driver licence: {'no driver licence' if self.driver_licence is None else f'{self.driver_licence.id} {"in" if self.driver_licence.is_in else "not in"} the wallet'}\n"
        result += f"Visite card: {self.visite_card}\n"
        result += "Photos:\n" + (
            "\n".join([f"   {photo.name} {'is' if photo.status else 'is not'} in the wallet" for photo in self.photos])
            if self.photos
            else "   no photos"
        ) + "\n"

        return result

    def spend_money(self, amount):
        if amount <= 0:
            return "Invalid amount. Please enter a positive number."

        if self.money == 0:
            return "You're broke!"

        if amount > self.money:
            return "Insufficient funds."

        self.money -= amount
        return f"OK. Your current balance is now {self.money}."

    def add_money(self, amount):
        if amount <= 0:
            return "Invalid amount. Please enter a positive number."

        self.money += amount
        return f"OK. Your current balance is now {self.money}."

    def pull_out_or_pull_in_cin(self):
        if self.cin is None:
            return "No CIN available."

        self.cin.is_in = not self.cin.is_in
        return "Done."

    def add_in_a_cin(self, _id):
        if not _id:
            return "No CIN added."

        self.cin = CIN(_id)
        return "OK. You have a CIN now."

    def pull_out_or_pull_in_driver_licence(self):
        if self.driver_licence is None:
            return "No driver licence available."

        self.driver_licence.is_in = not self.driver_licence.is_in
        return "Done."

    def add_driver_licence(self, _id):
        if not _id:
            return "No driver licence added."

        self.driver_licence = DriverLicence(_id)
        return "OK. Driver licence added."

    def pull_out_or_pull_in_banque_card(self, name):
        if not self.banque_card:
            return "No bank cards available."

        card = next((c for c in self.banque_card if c.name == name), None)
        if card:
            card.status = not card.status
            return "Done."

        return "Bank card not found."

    def add_bank_card(self, name):
        if not name:
            return "Invalid bank card name."

        self.banque_card.append(BankCard(name, True))
        return "Done."

    def pull_out_or_pull_in_photo(self, name):
        if not self.photos:
            return "No photos available."

        photo = next((p for p in self.photos if p.name == name), None)
        if photo:
            photo.status = not photo.status
            return "Done."

        return "Photo not found."

    def add_photo(self, name):
        if not name:
            return "Invalid photo name."

        self.photos.append(Photo(name, True))
        return "Done."

    def give_visite_card(self, amount):
        if amount <= 0:
            return "Invalid amount. Please enter a positive number."

        self.visite_card += amount
        return "Done."

    def add_visit_card(self, amount):
        if amount < 0:
            return "Invalid amount. Please enter a non-negative number."

        self.visite_card += amount
        return "Done."


class BankCard:
    def __init__(self, name, status):
        self.name = name
        self.status = status


class DriverLicence:
    def __init__(self, _id):
        self.id = _id
        self.is_in = True


class CIN:
    def __init__(self, _id):
        self.id = _id
        self.is_in = True


class Photo:
    def __init__(self, name, status):
        self.name = name
        self.status = status


if __name__ == "__main__":
    wallet = Wallet(100, "123456", "driver123")
    wallet.launch()