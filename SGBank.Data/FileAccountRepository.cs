using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;
using System.IO;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        string FilePath = @".\Accounts.txt";
        Dictionary<string, Account> accounts;
        
        public FileAccountRepository()
        {
            accounts = new Dictionary<string, Account>();  

            var fileAccounts = File.ReadAllLines(FilePath);
            foreach (var row in fileAccounts)
            {   
                    string[] columns = row.Split(',');

                try
                {
                    Account _account = new Account();
                    _account.AccountNumber = columns[0];
                    _account.Name = columns[1];
                    _account.Balance = Convert.ToDecimal(columns[2]);

                    _account.Code = columns[3];
                    _account.Type = parseAccountType(columns[3]);

                    accounts.Add(_account.AccountNumber, _account);
                }
                catch(Exception ex)
                {
                    //skipping that row.
                }
            }
        }
        private AccountType parseAccountType(string typeCode)
        {
            switch (typeCode)
            {
                case "F":
                    return AccountType.Free;
                case "B":
                    return AccountType.Basic;
                case "P":
                    return AccountType.Premium;
                default:
                    return AccountType.None;
            }
        }
        private static Account _account = new Account
        {
            Name = "Free Account",
            Balance = 100.00M,
            AccountNumber = "12345",
            Type = AccountType.Free
        };

        public Account LoadAccount(string AccountNumber)
        {
            return accounts[AccountNumber];  
        }
        public void SaveAccount(Account account)
        {
            accounts[account.AccountNumber] = account;  //set the value
            //File.Delete(FilePath);
            using (StreamWriter file = new StreamWriter(FilePath))
                foreach (var row in accounts.Values)
                    file.WriteLine("{0},{1},{2},{3}", row.AccountNumber, row.Name, row.Balance, row.Code);
            
            string data = File.ReadAllText(FilePath);
            string withHeader = "AccountNumber,Name,Balance,Type\n" + data;
            File.WriteAllText(FilePath, withHeader);

        }
    }
}
