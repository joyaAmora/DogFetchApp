using ApiHelper.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiHelper
{
    public class DogApiProcessor
    {

        public static async Task<List<BreedModel>> LoadBreedList()
        {
            ///TODO : À compléter LoadBreedList
            /// Attention le type de retour n'est pas nécessairement bon
            /// J'ai mis quelque chose pour avoir une base


            return new List<BreedModel>();
        }

        public static async Task<DogModel> GetImageUrl(string breed, int nbrImg)
        {
            string url = $"https://dog.ceo/api/breeds/{breed}/images/{nbrImg}";


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
