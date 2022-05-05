using Investec;

//Get Characters and Film
//Filter out buddies i.e. people which have been in the same film
// print out list of buddies 

namespace Investec
{
    class Program
    {
      public static async Task Main(string[] args)
        {
            SwapiApi api = new SwapiApi();

            var buddies = await api.GetPeopleList(new List<People>(),"https://swapi.dev/api/people");
        }
    }
}