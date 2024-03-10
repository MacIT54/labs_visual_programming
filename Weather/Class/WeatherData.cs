using System;
using System.Collections.Generic;
using System.Text.Json;
using Avalonia.Media.Imaging;

namespace Weather.Class
{
    public class WeatherData
    {
        public static List<WeatherVariable> ParseWeatherForecast(string json)
        {
            List<WeatherVariable> weatherForecast = new List<WeatherVariable>();

            try
            {
                // Получение информации о часовом поясе Москвы
                TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");

                // Парсинг JSON
                JsonDocument doc = JsonDocument.Parse(json);
                JsonElement root = doc.RootElement;

                // Получение данных о погоде на несколько дней вперёд
                JsonElement list = root.GetProperty("list");

                foreach (JsonElement element in list.EnumerateArray())
                {
                    WeatherVariable weatherVariable = new WeatherVariable();

                    // Получение времени и даты в UTC
                    DateTime timeUtc = DateTimeOffset.FromUnixTimeSeconds(element.GetProperty("dt").GetInt64()).DateTime;

                    // Преобразование времени в местное время Москвы
                    DateTime timeMoscow = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, moscowTimeZone);

                    weatherVariable.Time = timeMoscow;
                    weatherVariable.Date = timeMoscow.Date;

                    // Получение данных о погоде
                    JsonElement main = element.GetProperty("main");
                    weatherVariable.Humidity = main.GetProperty("humidity").GetInt32();
                    weatherVariable.Pressure = main.GetProperty("pressure").GetDouble();
                    weatherVariable.UV = main.GetProperty("temp_kf").GetDouble();
                    double temperatureKelvin = main.GetProperty("temp").GetDouble();
                    weatherVariable.TemperatureCelsius = Math.Round(temperatureKelvin - 273.15);
                    double tmpmax = main.GetProperty("temp_max").GetDouble();
                    weatherVariable.TempMax = Math.Round(tmpmax - 273.15);
                    double tmpmin = main.GetProperty("temp_min").GetDouble();
                    weatherVariable.TempMin = Math.Round(tmpmin - 273.15);


                    JsonElement wind = element.GetProperty("wind");
                    weatherVariable.WindSpeed = wind.GetProperty("speed").GetDouble();

                    //JsonElement weather = element.GetProperty("weather");
                    //weatherVariable.Icon = new Bitmap($"https://openweathermap.org/img/wn/{element.GetProperty("weather")[0].GetProperty("icon").GetString()}.png");

                    weatherForecast.Add(weatherVariable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при парсинге прогноза погоды: {ex.Message}");
            }

            return weatherForecast;
        }




        public class WeatherVariable
        {
            public DateTime Date { get; set; }
            public DateTime Time { get; set; }
            public int Humidity { get; set; }
            public double WindSpeed { get; set; }
            public double Pressure { get; set; }
            public double UV { get; set; }
            public double TemperatureCelsius { get; set; }
            public Bitmap? Icon { get; set; }
            public double TempMax { get; set; }
            public double TempMin { get; set; }
        }
    }
}
