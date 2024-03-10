using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Class
{
    public class DataNow : INotifyPropertyChanged 
    {
        private DateTime _currentDate = DateTime.Now;
        public string CurrentDate { get { return _currentDate.ToString("dddd, d", CultureInfo.InvariantCulture); } }

        public DataNow()
        {
            _ = UpdateCurrentDate();
        }

        public async Task UpdateCurrentDate()
        {
            while (true)
            {
                _currentDate = DateTime.Now;
                OnPropertyChanged(nameof(CurrentDate));
                await Task.Delay(1000);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}