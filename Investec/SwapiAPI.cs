using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Investec
{
    public class SwapiApi
    {
        #region Fields
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructor
        public SwapiApi()
        {
            _httpClient = new HttpClient();
        }
        #endregion

        #region Methods
        public async Task<List<Actors>> GetPeopleList(List<Actors> actors, string url)
        {
                var response = await _httpClient.GetAsync(url);
                Actors people = new Actors();

                if (response.IsSuccessStatusCode) {
                    var personData = await response.Content.ReadAsStringAsync();
                    var personObject = JObject.Parse(personData);
                    url = personObject.SelectToken("next").ToString() ?? null;
                    foreach (var personResult in personObject) {
                        if (personResult.Key == "results") {
                            if (personResult.Value != null) {
                                foreach (var person in personResult.Value) {
                                    // get actor name
                                    var actor = new Actors {
                                        Name = person.SelectToken("name").ToString()
                                    };
                                    List<Films> movies = new List<Films>();
                                    // get list of films
                                    IList<string> films = person.SelectToken("films").Select(s => (string)s).ToList();
                                    //substring movies by Id
                                    foreach (var film in films) {
                                        Films movie = new Films {
                                            Name = film.Split('/')[5]
                                        };
                                        //add movie list to actors
                                        movies.Add(movie);  
                                    }
                                    actor.Films = movies;
                                    actors.Add(actor);
                                }
                            }
                        }
                    }
                    if (! String.IsNullOrEmpty(url)) {
                       await GetPeopleList(actors, url);
                    }
                }

            return actors;
        }

        #endregion
    }



}
