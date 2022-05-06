using Investec;
namespace Investec
{
    class Program
    {
      public static async Task Main(string[] args)
        {
            SwapiApi Swapi = new SwapiApi();
            BuddiesInteractor interactor = new BuddiesInteractor();

            var buddies = await Swapi.GetPeopleList(new List<Actors>(),"https://swapi.dev/api/people");

            var friendsList = await interactor.GetBuddies(buddies);

            foreach (var friend in friendsList) {
                friend.FriendName.Remove(friend.ActorName);
                Console.WriteLine($"Actor: {friend.ActorName} " + $"buddies: {string.Join(",",friend.FriendName.ToArray())}");
            }
        }
    }
}