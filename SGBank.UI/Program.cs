using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SGBank.BLL;

namespace SGBank.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            AccountManager manager = AccountManagerFactory.Create();
            Menu.Start(manager);
        }
    }
}
