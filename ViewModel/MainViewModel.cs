using Aplikacja.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Aplikacja.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _selectedCityLatitude;
        private string _selectedCityLongitude;
        private string _data;
        private City _selectedCity;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }
        public List<City> Cities { get; set; } = new List<City>
        {
            new City { Name = "Warszawa", Latitude = "52.2297700", Longitude = "21.0117800" },
            new City { Name = "Kraków", Latitude = "50.064651", Longitude = "19.9449799" },
            new City { Name = "Łódź", Latitude = "49.6463", Longitude = "19.1310" },
        };

        public ICommand GetDataCommand { get; set; }

        public MainViewModel()
        {
            GetDataCommand = new RelayCommands(GetData);
        }

        public City SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));

                if (_selectedCity != null)
                {
                    _selectedCityLatitude = _selectedCity.Latitude;
                    _selectedCityLongitude = _selectedCity.Longitude;
                }
            }
        }


        private async void GetData()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("apikey", "NUKErlKWoUEFESwCqv9IP0SuM3r3SNSu");
            client.DefaultRequestHeaders.Add("Accept-Language", "pl");



            // Wykonanie żądania HTTP GET i odczytanie odpowiedzi
            var response = await client.GetAsync($"https://airapi.airly.eu/v2/measurements/nearest?{_selectedCityLatitude}&lng={_selectedCityLongitude}&maxDistanceKM=1000");

            string jsonResponse = await response.Content.ReadAsStringAsync();

            // Analiza odpowiedzi JSON i wyświetlenie danych
            if (response.IsSuccessStatusCode)
            {

                // Wyodrębnij informacje o jakości powietrza z odpowiedzi API:
                var content = await response.Content.ReadAsStringAsync();
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                string measurementsText = "";

                // Dodaj informacje o jakości powietrza
                measurementsText += $"{data.current.indexes[0].description}\n";

                // Dodaj informacje o poszczególnych zanieczyszczeniach
                foreach (var value in data.current.values)
                {
                    measurementsText += $"{value.name}: {value.value} {value.unit} ";
                    foreach (var standard in data.current.standards)
                    {
                        if (standard.pollutant == value.name)
                        {
                            measurementsText += $"Norma: {standard.limit}\n";
                            break;
                        }
                    }
                }

                // Dodaj czas pomiaru
                measurementsText += $"\nCzas pomiaru: {data.current.fromDateTime}";


                Data = measurementsText;




            }
            else
            {
                Console.WriteLine("Błąd podczas pobierania danych z API Airly");
            }

        }

    }
}
