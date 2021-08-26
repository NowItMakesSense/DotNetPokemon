using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Model;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchPokemonController : Controller
    {
        [HttpGet("{IDorName}")]
        public Pokemon SearchPokemon(string IDorName = "")
        {
            return new RequestController().returnPokemon(IDorName);
        }
    }
}