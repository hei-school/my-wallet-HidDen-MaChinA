import PromptSync from "prompt-sync";
import { wallet } from "./wallet.js";
let prompt = PromptSync();

const money = parseInt(prompt("your initial money: "))
const cin = prompt("you cin if there is: ")
const driver_licence = prompt("you driver licence: ")


const myWallet = new wallet(money,cin,driver_licence);
myWallet.launch()