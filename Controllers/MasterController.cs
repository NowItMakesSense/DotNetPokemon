using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PokemonAPI.Model;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MasterController
    {
        [HttpPost]
        public void createMaster([FromBody] MasterModel master)
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://teste:teste@pokemonapi.fjwkl.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

            IMongoDatabase database = dbClient.GetDatabase("Pokemon");
            IMongoCollection<MasterModel> colNews = database.GetCollection<MasterModel>("Masters");

            dynamic query = colNews.AsQueryable().Where(x => x.masterId == master.masterId).FirstOrDefault();

            if (query != null)
            {
                throw new Exception("This Master is already registered");
            }

            colNews.InsertOne(master);
        }

        [HttpGet("{id}")]
        public List<Pokemon> returnMasterProfile(int id)
        {
            var colNews = new DataBase().connectMongo();
            
            dynamic query = colNews.AsQueryable().ToList()
                                   .Where(x => x.masterId == id)
                                   .ToList();

            List<Pokemon> list = new List<Pokemon>();
            foreach (var item in query)
            {
                Pokemon pokemon = new RequestController().returnPokemon(item.pokemonId.ToString());
                list.Add(pokemon);
            }

            return list;
        }
    }
}