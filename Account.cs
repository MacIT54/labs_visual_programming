using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 class Account
    {
        private decimal _balance;
        private List<INotifyer> _notifyers;

        public Account() {}

        public Account(decimal balance) : this()
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

        public decimal GetBalance()
        {
            return _balance;
        }

        private void Notification()
        {
            foreach (var notifyer in _notifyers)
            {
                notifyer.Notify(_balance);
            }
        }
    }