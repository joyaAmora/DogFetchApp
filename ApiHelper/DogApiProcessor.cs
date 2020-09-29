using ApiHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiHelper
{
    public class DogApiProcessor
    {

        public static async Task<List<string>> LoadBreedList()
        {
            string url = $"https://dog.ceo/api/breeds/list/all";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    BreedModel breed = await response.Content.ReadAsAsync<BreedModel>();
                    var child = breed.Breeds.Values.ToList();
                    var families = breed.Breeds.Keys.ToList();
                    return families;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }


            }
        }

        public static async Task<DogModel> GetImageUrl(string breed, int nbrImg)
        {
            string url = $"https://dog.ceo/api/breed/{breed}/images/random/{nbrImg}";


            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DogModel dog = await response.Content.ReadAsAsync<DogModel>();
                    return dog;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
