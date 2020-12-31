﻿using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using Conexa.Integration.Interface;
using Conexa.Integration.ViewModels;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Conexa.Integration
{
    public class ContractIntegrationWeathermap : IContractIntegrationWeathermap
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;


        private readonly string _remoteServiceBaseUrl;
        public ContractIntegrationWeathermap(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;

            _remoteServiceBaseUrl = $"http://api.openweathermap.org/data/2.5/";
        }

        public async Task<Climate> GetTemperature(string city)
        {
            var uri = API.Clima.GetAllTemperatura(_remoteServiceBaseUrl, city, _settings.Value.KeyWeathermap);
            //var responseString = await _httpClient.GetStringAsync(uri);
            var response = await _httpClient.GetAsync(uri);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Climate>(await response.Content.ReadAsStringAsync());
            }
           
                return null;
           
        }
        public Task<Climate> GetTemperature(double longitude, double latitude)
        {
            return null;
        }
    }
}
