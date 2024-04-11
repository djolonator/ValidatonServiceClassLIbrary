using Newtonsoft.Json;
using static ValidationService.Models;

namespace ValidationService
{
    public class TokenValidationService
    {
        public bool ValidateNitesTokenValue(string nitestToken)
        {
            return nitestToken == "trueValueOfToken";
        }

        public async Task<bool> ValidateToken(string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("_edoktorApiServiceUrl");
                client.DefaultRequestHeaders.Accept.Clear();
                try
                {
                    var validationResponse = await client.GetAsync("/api/oauth/ValidateToken?authKey=" + token);
                    if (validationResponse.IsSuccessStatusCode)
                    {
                        var tokenString = await validationResponse.Content.ReadAsStringAsync();
                        var validationModel = JsonConvert.DeserializeObject<TokenValidation>(tokenString);
                        return validationModel.Valid;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
