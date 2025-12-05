namespace System.Eclaim.Application.Dto.Captcha
{
    public record RedeemRequest(string token, List<long> solutions);
   
}
