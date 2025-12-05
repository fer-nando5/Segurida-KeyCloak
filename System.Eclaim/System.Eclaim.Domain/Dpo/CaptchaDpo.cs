namespace System.Eclaim.Domain.Dpo
{
    public class Redeem
    {
        public string token { get; set; } = string.Empty;
        public List<long> solutions { get; set; } = new();
    }
}
