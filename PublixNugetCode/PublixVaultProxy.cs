using System.Net.Http;
using System.Runtime.Remoting;
using DpapiCache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PublixVaultProxy
{
    public interface IVaultProxyService
    {
        Task<bool> RetrieveSecretFromRemoteVaultAsync();
        string VaultUrl { get; set; }
    }
    public class VaultProxyService : IVaultProxyService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public string VaultUrl { get; set; }    

        public VaultProxyService(string vaultUrl)
        {
           VaultUrl = vaultUrl;
        }


        /// <summary>
        /// Accessing the vault with the given address for secret using an http client instance
        /// Returns the secret (string) if vault is accessable or returns null
        /// Http client disposes once action completes or exception throws
        /// </summary>


        public async Task<bool> RetrieveSecretFromRemoteVaultAsync()
        {

            try
            {
                var response = await _httpClient.GetAsync(VaultUrl);
                await Task.Delay(500);
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
                    _httpClient.Dispose();
                    return false;
                }
            }
            catch (HttpRequestException)
            {
                _httpClient.Dispose();
                return false;

            }
        }


    }
}
