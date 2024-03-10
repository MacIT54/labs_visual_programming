using System.Collections.Generic;
using System.ComponentModel;
using Weather.Class;

namespace Weather.ViewModels
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public DataNow CurrentDataNow { get; }
        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private List<WeatherData.WeatherVariable> _weatherForecast;
        public List<WeatherData.WeatherVariable> WeatherForecast
        {
            get { return _weatherForecast; }
            set
            {
                _weatherForecast = value;
                OnPropertyChanged(nameof(WeatherForecast));
            }
        }

        public MainViewModel()
        {
            CurrentDataNow = new DataNow();
            City = "Moscow";
            UpdateWeatherForecast();
        }

        private async void UpdateWeatherForecast()
        {
            WeatherAPI weatherAPI = new WeatherAPI();
            string jsonForecast = await weatherAPI.GetWeatherForecastAsync();
            WeatherForecast = WeatherData.ParseWeatherForecast(jsonForecast);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
