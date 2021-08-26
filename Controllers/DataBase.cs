using MongoDB.Driver;
using PokemonAPI.Model;

namespace PokemonAPI.Controllers
{
    public class DataBase
    {
        public IMongoCollection<CatchedPokemon> connectMongo()
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://teste:teste@pokemonapi.fjwkl.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            IMongoDatabase database = dbClient.GetDatabase("Pokemon");
            return database.GetCollection<CatchedPokemon>("Catcheted");
        }
    }
}