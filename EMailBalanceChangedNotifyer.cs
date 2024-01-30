using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class EMailBalanceChangedNotifyer : INotifyer
{
    private string _email;

    public EMailBalanceChangedNotifyer(string email)
    {
        _email = email;
    }

    public void Notify(decimal balance)
    {
        Console.WriteLine($"EMail notification sent to {_email}: Balance changed. New balance: {balance}\n");
    }
}