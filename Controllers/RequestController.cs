using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using System.Collections.Generic;
using PokemonAPI.Model;

namespace PokemonAPI.Controllers
{
    public class RequestController : ControllerBase
    {
        public Pokemon returnPokemon(string ID = "")
        {
            int value;
            int.TryParse(ID, out value);

            if (string.IsNullOrWhiteSpace(ID))
            {
                value = new Random().Next(848);
            }

            dynamic pokemonSearch = value == 0 ? ID : value;
            var getPokemon = returnRequestResult($"https://pokeapi.co/api/v2/pokemon/{pokemonSearch}");
            var getEvolution = returnEvolution((Int32)getPokemon.id);

            var types = new List<string>();
            foreach (var item in getPokemon.types)
            {
                types.Add(item.type.name.ToString());
            }

            return new Pokemon(new
            {
                id = getPokemon.id,
                name = getPokemon.name,
                sprites = getPokemon.sprites.front_default,
                types = types,
                height = getPokemon.height,
                weight = getPokemon.weight,
                evolution = getEvolution
            });
        }
        private dynamic returnRequestResult(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var converter = new ExpandoObjectConverter();
                return JsonConvert.DeserializeObject<ExpandoObject>(response.Content.ReadAsStringAsync().Result, converter);
            }

            return null;
        }
        private List<dynamic> returnEvolution(int value)
        {
            var list = new List<dynamic>();
            var urlEvolution = returnRequestResult($"https://pokeapi.co/api/v2/pokemon-species/{value}").evolution_chain.url;
            var data = returnRequestResult(urlEvolution).chain;

            while (((IDictionary<string, object>)data).ContainsKey("evolves_to"))
            {
                var evoDetails = data.evolution_details;

                list.Add(new
                {
                    name = data.species.name,
                    min_level = evoDetails.Count != 0 ? data.evolution_details[0].min_level : 1,
                    gender = evoDetails.Count != 0 ? data.evolution_details[0].gender : null,
                });

                if (data.evolves_to.Count > 0 && ((IDictionary<string, object>)data.evolves_to[0]).ContainsKey("evolves_to"))
                {
                    data = data.evolves_to[0];
                }
                else
                {
                    break;
                }
            }

            return list;
        }
    }
}