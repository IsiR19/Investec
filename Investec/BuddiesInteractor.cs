using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investec
{
    public interface IBuddiesInteractor
    {
        Task<List<Buddy>> GetBuddies(List<Actors> actors);
    }
    public class BuddiesInteractor : IBuddiesInteractor
    {
        public Task<List<Buddy>> GetBuddies(List<Actors> actors)
        {
            var friendsByMovie = new Dictionary<string,List<string>>();
            List<Buddy> buddyList = new List<Buddy>();
           
            foreach (var actor in actors) {
                var friendList = new List<string>();
                var actorList = new List<string>();

                //get actors movies
                var filmList = actor.Films.GroupBy(x => x.Name).Select(f => f.First());
                                
                //Go through movie list to see if friends have been extracted for movie
                //If not get friends of movies not in list
                foreach (var film in filmList) {
                    //If movie exist in list, friends have already been extracted no need to duplicate process
                    if (!friendsByMovie.ContainsKey(film.Name)) {
                        var actorsByfilm = (from a in actors
                                            from f in a.Films
                                            where f.Name == film.Name
                                            select a.Name).ToList();
                        
                        friendsByMovie.Add(film.Name, actorsByfilm);
                        }
                    };

                //extract list and concatanate 
                var friends = (from f in friendsByMovie
                               where f.Value.Contains(actor.Name)
                               select f.Value).ToList();

                //add friends to single list
                foreach (var friend in friends) {
                    friendList.AddRange(friend);
                }
                //create buddy object for each actor store in memory
                Buddy buddy = new Buddy {
                    ActorName = actor.Name,
                    FriendName = friendList.Distinct().ToList()   
                };

                buddyList.Add(buddy);
            }
            return Task.Run(() => buddyList.ToList());  
            } 
    }
}

