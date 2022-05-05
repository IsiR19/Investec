using Investec;
namespace Investec
{
    class Program
    {
      public static async Task Main(string[] args)
        {
            SwapiApi api = new SwapiApi();
            BuddiesInteractor interactor = new BuddiesInteractor();

            var buddies = await api.GetPeopleList(new List<Actors>(),"https://swapi.dev/api/people");

            var friendsList = await interactor.GetBuddies(buddies);
        }
    }
}