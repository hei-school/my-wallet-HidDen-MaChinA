import PromptSync from 'prompt-sync'

const prompt = PromptSync();

export class wallet{
    money=0;
    cin;
    banque_card=[{
        name:"",
        status:true
    }]
    driver_licence={
        id:"asdfasdfadsf",
        isIn:true
    }
    visite_card=0;
    photos=[
        {name:"",status:true}
    ]
    constructor(money,cin,driver_licence){
        this.money=money
        this.cin={id:cin,isIn:true}
        this.driver_licence={id:driver_licence,isIn:true}
        this.pullOutOrPullInCin = () => {
            this.cin.isIn = !this.cin.isIn;
            return this.cin;
        }
        this.addInACin = (arg) => {
            if(arg!== undefined){
                this.cin = {id:arg,isIn:true};
            } else{
                return "oke no CIN then"
            }

            return "oke you have a CIN"
        }
        this.cinStatus = () => {
            return this.cin !== undefined ? this.cin.isIn : undefined;
        }
        this.getMoney = () => {
            return this.money == 0 ? "you broke" : this.money
        }
        this.spendMoney = (arg) => {
            if(arg===undefined){
                return "how much you wanna spend in there ?"
            }else if(arg> 0){
                return "we don't do that here"
            }else{
                this.money -=arg;
            }
            return "oke your current balance is now "+ this.money;
        }
        this.addMoney = (arg) => {
            if(arg===undefined){
                return "how much you wanna add in there ?"
            }else if(arg> 0){
                return "we don't do that here :v"
            }else{
                this.money +=arg 
            }
            return "oke your current balance is now "+ this.money;
        }
        this.pullOUtOrPullInBanqueCard = (arg) => {
            if(arg == undefined){
                return "what bank card ?"
            }
            for(let i of this.banque_card){
                if(i.name === arg){
                    i.status = !i.status;
                }
            }
            return "done"
        }
        this.addBankCard = (arg) => {
            if(arg== undefined){
                return "what is the bank card name bruh"
            }
            this.banque_card.push({name:arg,status:true})
            return "done";
        }
        this.pullOUtOrPullInDriverLicence = () => {
            if(this.driver_licence === undefined){
                return "there is no driver licence";
            }
            this.driver_licence.isIn = !this.driver_licence.isIn;
            return "done"
        }
        this.getDriverLicence = () => {
            return this.driver_licence;
        }
        this.addDriverLicence = (arg) => {
            if(arg== undefined){
                return "what's the name of that driver licence"
            }
            this.driver_licence={id:arg, isIn:true}
            return "done"
        }
        this.getVisiteCard = () => {
            return this.visite_card;
        }
        this.giveVisiteCard = (arg) => {
            if(arg==undefined){
                return "how much dude"
            }
            this.visite_card+=arg;
            return "done"
        }
        this.addVisiteCard = (arg) => {
            if(arg==undefined){
                return "how much visite card you wanna add ?"
            }else if(arg< 0){
                return "we don't do that here"
            }
            else{
                this.visite_card+=arg
            }
            return "done"
        }
        this.getPhotos = () => {
            let toReturn = "";
            for(let i of this.photos){
                toReturn += "\n----------------\n"
                toReturn += `photo: ${i.name} ${i.status ? "is in the wallet" : "is not in the wallet"}`
            }
            return toReturn;
        }
        this.addPhoto = (arg) => {
            if(arg==undefined){
                return "what photo you wanna add"
            }else if(arg=== ""){
                return "what you want me to do with no name"
            }
            this.photos.push({name:arg,status:true})
        }
        this.pullOUtOrPullInPhoto = (arg) => {
            if(arg === undefined){
                return "what photo ?"
            }
            for(let i of this.photos){
                if(i.name === arg){
                    i.status = !i.status
                }
            }
            return "done"
        }
        this.inventary = () => {
            let toReturn = ""
            toReturn +=`money: ${this.money}\n`
            toReturn += `CIN: ${this.cin == undefined || this.cin.id == "" ? "no cin": this.cin.isIn ? this.cin.id + "is inside the wallet" : this.cin.id + "is outside the wallet" }` + "\n"
            toReturn += `Bank card: \n`
            if(this.banque_card!=undefined){
                for(let i of this.banque_card){
                    toReturn += `   ${i.name} ${i.status ? "is in the wallet":"is not in the wallet"}\n`
                }
            }else{
                toReturn += "   no bank card\n"
            }
            toReturn += `Drive licence: ${this.driver_licence == undefined ? "no driver licence":`${this.driver_licence.id} ${this.driver_licence.isIn ? "is in the wallet" : "is not in the wallet"}`}\n`
            toReturn += `Visite card: ${this.visite_card}\n`
            toReturn += "Photos:\n"
            if(this.photos!=undefined){
                for(let i of this.photos){
                    toReturn += `   ${i.name} ${i.status ? "is in the wallet" : "is not in the wallet"}\n`
                }
            }
            else{
                toReturn +="    no photos\n"
            }
            return toReturn;
        }
        this.launch = ()=>{
            while(true){
                console.log(this.inventary() + "\n",
                    "0. To throw the wallet away",
                    "1. If you want to spend money\n",
                    "2. If you want to add money to your wallet\n",
                    "3. If you want to pull in/out your cin\n",
                    "4. if you want to add a CIN (if there aren't any yet)\n",
                    "5. If you want to pull in/out your driver licence\n",
                    "6. if you want to add a driver licence(if there aren't any yet)\n",
                    "7. If you want to pull in/out a bank card\n",
                    "8. if you want to add bank card\n",
                    "9. If you want to pull in/out a photo\n",
                    "10. If you want to add some photo\n",
                    "11. If you want to give some visit card\n",
                    "12. if you want add some visit card into your wallet\n"
                );
            
                const select = parseInt(prompt("Enter your choice: "));
                switch(select){
                    case 1:
                        const moneyToSpend = parseInt(prompt("How much you wanna spend ?  "))
                        console.log(this.spendMoney(moneyToSpend))
                        continue;
                    case 2:
                        const moneyToAdd = parseInt(prompt("How much you wanna add?  "))
                        console.log(this.addMoney(moneyToAdd))
                        continue;
                    case 3:
                        console.log(this.pullOutOrPullInCin())
                        continue;
                    case 4:
                        const cinToAdd= prompt("the id of your CIN then? ")
                        console.log(this.addInACin(cinToAdd))
                        continue;
                    case 5:
                        console.log(this.pullOUtOrPullInDriverLicence())
                        continue;
                    case 6:
                        const driverLicenceToAdd= prompt("the name you want to give the driver licence? ")
                        console.log(this.addDriverLicence(driverLicenceToAdd))
                        continue;
                    case 7:
                        const bankCardName= prompt("which one ? ")
                        console.log(this.pullOUtOrPullInBanqueCard(bankCardName))
                        continue;
                    case 8:
                        const bankCardToAdd= prompt("what's the name ? ")
                        console.log(this.addBankCard(bankCardToAdd))
                        continue;
                    case 9:
                        const photoName = prompt("which one ? ")
                        console.log(this.pullOUtOrPullInPhoto(photoName))
                        continue;
                    case 10:
                        const photoToAdd = prompt("what's the name of the photo ? ")
                        console.log(this.addPhoto(photoToAdd))
                        continue;
                    case 11:
                        const howMuch = parseInt(prompt("how much visit card you gave ? "))
                        console.log(this.giveVisiteCard(howMuch))
                        continue;
                    case 12:
                        const visitCardToAdd= parseInt(prompt("how much are those ? "))
                        console.log(this.addPhoto(visitCardToAdd))
                        continue;
                    case 0:
                        return;
                }
            }
        }
    }
}
