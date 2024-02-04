using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Account
{
    private decimal _balance;
    private List<INotifyer> _notifyers;

    public Account()
    {
        _balance = 0;
        _notifyers = new List<INotifyer>();
    }


    public Account(decimal balance)
    {
        _balance = balance;
        _notifyers = new List<INotifyer>();
    }

    public void AddNotifyer(INotifyer notifyer)
    {
        _notifyers.Add(notifyer);
    }

    public void ChangeBalance(decimal balance)
    {
        _balance = balance;
        Notification();
    }

    private void Notification()
    {
        foreach (var notifyer in _notifyers)
        {
            notifyer.Notify(_balance);
        }
    }

    public decimal GetBalance
    {
        get { return _balance; }
    }
}