﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.BLL.WithdrawRules
{
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if (account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: a non premium account hit the Premium Withdraw Rule.  Contact IT";
                return response;
            }

            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative!";
                return response;
            }

            response.OldBalance = account.Balance;
            account.Balance += amount;
            response.Account = account;
            response.Amount = amount;
            response.Success = true;
            if (account.Balance < -500)
            {
                account.Balance = account.Balance - 10;
                Console.WriteLine("Premium Overdraft Fee Applied $10 charge");
            }

            return response;

        }
    }
}
