using System;
using System.Collections.Generic;

namespace PokemonAPI.Model
{
    public class Pokemon
    {
        public Pokemon(dynamic obj)
        {
            this.id = (Int32)obj.id;
            this.name = obj.name;
            this.sprites = obj.sprites;
            this.types = obj.types;
            this.height = (Int32)obj.height;
            this.weight = (Int32)obj.weight;
            this.evolution = obj.evolution;
        }

        public Int32 id { get; set; }
        public string name { get; set; }
        public string sprites { get; set; }
        public List<string> types { get; set; }
        public Int32? height { get; set; }
        public Int32? weight { get; set; }
        public List<dynamic> evolution { get; set; }
    }
}