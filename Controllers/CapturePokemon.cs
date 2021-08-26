using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PokemonAPI.Model;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CapturePokemon
    {
        [HttpPost]
        public void capturePokemon([FromBody] CatchedPokemon capt)
        {
            Pokemon pokemon = new RequestController().returnPokemon(capt.pokemonId.ToString());

            if (pokemon == null)
            {
                throw new ArgumentNullException("Pokemon Invalid");
            }

           var colNews = new DataBase().connectMongo();

            dynamic query = colNews.AsQueryable()
                                   .Where(x => x.masterId == capt.masterId && x.pokemonId == capt.pokemonId)
                                   .FirstOrDefault();

            if (query != null)
            {
                throw new Exception("This Master is already registered");
            }

            colNews.InsertOne(new CatchedPokemon(capt.masterId, capt.pokemonId));
        }
    }
}