


using PublixVaultProxy;

namespace WebApplication1
{
    public class VaultStartUpService : IHostedService
    {
        private readonly ILogger<VaultStartUpService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IVaultProxyService _vaultProxy;

        public VaultStartUpService(ILogger<VaultStartUpService> logger, IConfiguration configuration, IVaultProxyService vaultProxyService)
        {
            _logger = logger;
            _configuration = configuration;
            _vaultProxy = vaultProxyService;

        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
          
            try
            {
                bool res = await _vaultProxy.RetrieveSecretFromRemoteVaultAsync();
                if (!res)
                {
                    _logger.LogError("Error occurd while caching the token ");
                }
                _logger.LogInformation("Vault token cached successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurd while caching the token ");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Vault startup service stopping.");
            return Task.CompletedTask;
        }
    }
}
