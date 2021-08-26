using System;

namespace PokemonAPI.Model
{
    public class CatchedPokemon
    {
        public CatchedPokemon(int masterId, int pokemonId)
        {
            this.id = $"M{masterId}P{pokemonId}";
            this.masterId = masterId;
            this.pokemonId = pokemonId;
        }

        public string id { get; private set; }
        public int masterId { get; set; }
        public int pokemonId { get; set; }
    }
}