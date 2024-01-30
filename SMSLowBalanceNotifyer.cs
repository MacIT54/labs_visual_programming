using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SMSLowBalanceNotifyer : INotifyer
{
    private string _phone;
    private decimal _lowBalanceValue;

    public SMSLowBalanceNotifyer(string phone, decimal lowBalanceValue)
    {
        _phone = phone;
        _lowBalanceValue = lowBalanceValue;
    }

    public void Notify(decimal balance)
    {
        if (balance < _lowBalanceValue)
        {
            Console.WriteLine($"SMS notification sent to {_phone}: Low balance alert! Current balance: {balance}\n");
        }
    }
}
