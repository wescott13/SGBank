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
        private string _filePath;

        public FileAccountRepository(string filePath)
        {
            _filePath = filePath;
        }
        public class Settings
        {
            public const string FilePath = @".\SGBank\Accounts.txt";
        }

        private static Account _account = new Account
        {
            //TODO
            //public string FilePath = @"\SGBank\Accounts.txt";
            

            Name = "Free Account",
            Balance = 100.00M,
            AccountNumber = "12345",
            Type = AccountType.Free
        };

        public Account LoadAccount(string AccountNumber)
        {
            if (AccountNumber == _account.AccountNumber)
            {
                return _account;
            }
            else
            {
                return null;
            }
        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }

    }
}
