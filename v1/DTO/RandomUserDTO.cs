using Newtonsoft.Json;
using v1.Entities;

namespace v1.DTO
{
    public class RandomUserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        

        public static RandomUserDTO FromJson(string json)
        {
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(json);
            // Use explicitamente o JsonConvert do Newtonsoft.Json
            RandomUserDTO response =  new RandomUserDTO {
                FirstName = jsonObject.results[0].name.first,
                LastName = jsonObject.results[0].name.last,
                Email = jsonObject.results[0].email,
            };

            return response;
        }
    }
}
