using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Model;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomPokemonController : ControllerBase
    {
        [HttpGet("{quantity}")]
        public List<Pokemon> GetRandomPokemon(int quantity = 1)
        {
            var list = new List<Pokemon>();
            for (int i = 0; i < quantity; i++)
            {
                var response = new RequestController().returnPokemon();
                list.Add(response);
            }

            return list;
        }
    }
}