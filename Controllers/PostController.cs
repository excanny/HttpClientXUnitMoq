using HttpClientXUnitMoq.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClientXUnitMoq.Controllers
{
    [Route("api/[controller]")]
    public class PostController
    {
        [Route("posts")]
        public async Task<List<Post>> Index()
        {

            List<Post> reservationList = new List<Post>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Post>>(apiResponse);
                }
            }
            

            return reservationList;

        }
    }

}

   

