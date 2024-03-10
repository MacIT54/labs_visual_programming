using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using static Weather.Class.WeatherData;

namespace Weather.Class
{
    public class WeatherAPI
    {
        private readonly RestClient _client;
        private readonly string _url = "https://api.openweathermap.org/data/2.5/forecast";
        private readonly string _apiKey = "38c8f2734fb5f69d974ba6edcb5db553";
        private readonly string _city = "Moscow";


        public WeatherAPI()
        {
            _client = new RestClient(_url);
        }

        public async Task<string> GetWeatherForecastAsync()
        {
            var request = new RestRequest();
            request.AddParameter("q", _city);
            request.AddParameter("appid", _apiKey);
            request.AddParameter("mode", "json");
            request.AddParameter("cnt", "40");

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
               
                return response.Content;
            }
            else
            {
                throw new Exception($"Не удалось получить прогноз погоды. Статус код: {response.StatusCode}. Содержимое ответа: {response.Content}");
            }
        }
    }
}
