namespace System.Eclaim.Domain.OutPort.Secrets
{
    public interface IVaultSecretsProvider
    {
        Task<Dictionary<string, string>> GetSecretsAsync();
        Task<string?> GetSecretAsync(string key);
    }
}
