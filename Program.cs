using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Account acc1 = new Account(10);
        SMSLowBalanceNotifyer sms = new SMSLowBalanceNotifyer("8-800-555-35-35", 50);
        EMailBalanceChangedNotifyer email = new EMailBalanceChangedNotifyer("456@mail.ru");
        acc1.AddNotifyer(sms);
        acc1.AddNotifyer(email);
        acc1.ChangeBalance(13);
        acc1.ChangeBalance(60);
    }
}