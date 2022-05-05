using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Investec
{
    public class SwapiApi
    {
        #region Fields
        private readonly new IHttpClientFactory _httpClientFactory;
        #endregion

        #region Methods
        public async Task<People> GetPeopleList()
        {
            //get url
            string url = $"https://swapi.dev/api/people";
            HttpResponseMessage response = new HttpResponseMessage();

            // http client implementation
            try
            {
                var client = _httpClientFactory.CreateClient();
            
            var result = await client.GetAsync(url);
            var buddies = new Dictionary<string, string>();
            List<People> people = new List<People>();

                if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var contentObject = JObject.Parse(content);
                
                foreach (var item in contentObject)
                {
                      
                    if(item.Key == "results")
                    {
                            foreach(var person in item.Value)
                            {
                                 people.Name = person.SelectToken("name").ToString();
                            }
                    }

                    if(item.Key == "films")
                        {
                            foreach (var film in item.Value)
                            {
                                people.Films.Add(film.Value.T);

                                var filmArray = JArray.Parse(item.ToString());
                                foreach (var filmName in filmArray)
                                {
                                    people.Films.Add(filmName.ToString());
                                }
                            }

                        }
                }
                

                foreach (var buddy in people)
                    {
                       

                    }
            }

            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage();
            }


            return null;
            
           
        }

        #endregion
    }



}
