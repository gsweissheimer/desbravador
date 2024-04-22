using Newtonsoft.Json;
using v1.DTO;
using v1.Entities;

namespace v1.BLL
{
    public interface IRandomUserBLL
    {
        Task<RandomUserDTO> GetRandomUser();
    }
    public class RandomUserBLL : IRandomUserBLL
    {
        public RandomUserBLL()
        {
            
        }
        public async Task<RandomUserDTO> GetRandomUser()
        {       
            string randomUserString = await Main();
            RandomUserDTO response = RandomUserDTO.FromJson(randomUserString);
            return response;
        }
        private static async Task<string> Main()
        {
            string apiUrl = "https://randomuser.me/api/";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                        
                    }
                    else
                    {
                        Console.WriteLine($"Falha na solicitação. Código de status: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao fazer solicitação: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
