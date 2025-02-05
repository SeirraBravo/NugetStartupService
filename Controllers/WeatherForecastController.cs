using Microsoft.AspNetCore.Mvc;
//using DpapiCache;
//using VaultManager;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet(Name ="EnableCache")]
        public async Task<Boolean> Get()
        {
            //DPAPIHelper.
            //VaultProxy.VaultHostName = "localhost";
            //VaultProxy.VaultHostPort = "8200";
            //VaultProxy.VaultUrl = "http://localhost:8200";
            //string secret = await VaultProxy.RetrieveSecretFromRemoteVaultAsync();
            //CacheHelper.CacheFilePath = @"D:\token\tokencache.txt";
            //CacheHelper.Encrypt(secret);

            return true;
        }
       
    }
}
