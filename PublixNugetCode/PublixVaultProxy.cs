using System.Net.Http;
using System.Runtime.Remoting;
using DpapiCache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PublixVaultProxy
{
    public interface IVaultProxyService
    {
        Task<bool> RetrieveSecretFromRemoteVaultAsync(string VaultUrl);
    }
    public class VaultProxyService : IVaultProxyService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<VaultProxyService> _logger;
        private static readonly HttpClient _httpClient = new HttpClient();

        public VaultProxyService(IConfiguration configuration, ILogger<VaultProxyService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            var VaultConfig = _configuration.GetSection("VaultConfig");
            string vaultUrl = VaultConfig["VaultUrl"];
            //string vaultToken = VaultConfig["VaultToken"];
            _httpClient.DefaultRequestHeaders.Add("X-Vault-Token", VaultConfig["VaultToken"]);
        }



        /// <summary>
        /// Accessing the vault with the given address for secret using an http client instance
        /// Returns the secret (string) if vault is accessable or returns null
        /// Http client disposes once action completes or exception throws
        /// </summary>



        public async Task<bool> RetrieveSecretFromRemoteVaultAsync(string VaultUrl)
        {

            try
            {
                var response = await _httpClient.GetAsync(VaultUrl);
                response.EnsureSuccessStatusCode();
                string secret = await response.Content.ReadAsStringAsync();
                if (secret != null)
                {
                    string cacheFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "offlineToken.dat");
                    CacheHelper.CacheFilePath = cacheFilePath;
                    CacheHelper.Encrypt(secret);
                    return true;
                }
                else
                {
                    _logger.LogError("Error while accessing the vault");
                    _httpClient.Dispose();
                    return false;
                }
            }
            catch (HttpRequestException)
            {
                _logger.LogError("Error while accessing the vault");
                _httpClient.Dispose();
                return false;

            }
        }


    }
}
