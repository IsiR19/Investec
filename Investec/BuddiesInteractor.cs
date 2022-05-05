using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investec
{
    public interface IBuddiesInteractor
    {
        Task<IEnumerable<Buddy>> GetBuddies(List<Actors> actors);
    }
    public class BuddiesInteractor : IBuddiesInteractor
    {
        public Task<IEnumerable<Buddy>> GetBuddies(List<Actors> actors)
        {
            var friendsByMovie = new Dictionary<string,List<string>>();
            var buddies = new List<Buddy>();
           
            foreach (var actor in actors) {
                //I have a list of actors who acted in movies 
                //need to select all the people who have acted in the same movie 
                var filmList = actor.Films.GroupBy(x => x.Name).Select(f => f.First());
                var actorList = new List<string>();
                foreach (var film in filmList) {
                    if (!friendsByMovie.ContainsKey(film.Name)) {
                        var actorsByfilm = (from a in actors
                                            from f in a.Films
                                            where f.Name == film.Name
                                            select a.Name).ToList();

                        friendsByMovie.Add(film.Name, actorsByfilm);
                        }
                    };
                
                }
                
            }
            //return null;
        }
    }
}
