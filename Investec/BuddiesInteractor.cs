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
           
            foreach (var actor in actors) {
                
                var filmList = actor.Films.GroupBy(x => x.Name).Select(y => y.First());

                foreach (var film in filmList) {

                    var actorList = (from a in actors
                                     from f in a.Films
                                     where f.Name == film.Name
                                     select a.Name).ToList();
                };
            }
            return null;
        }
    }
}
