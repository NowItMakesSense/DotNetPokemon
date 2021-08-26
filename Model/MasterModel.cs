using System;

namespace PokemonAPI.Model
{
    public class MasterModel
    {
        #region Properties
        public string id { get; private set; }
        public int masterId { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string cpf { get; set; }

        #endregion

        public MasterModel(int masterId, string name, int age, string cpf)
        {
            this.masterId = masterId;
            this.name = name;
            this.age = age;

            if (!validadeCPF(cpf))
                throw new Exception("Invalid CPF");

            this.cpf = cpf;
            this.id = $"M{id}C{cpf}";
        }

        private bool validadeCPF(string cpf)
        {
            string value = cpf.Replace(".", "").Replace("-", "");

            bool same = true;
            for (int i = 1; i < 11 && same; i++)
                if (value[i] != value[0])
                    same = false;

            if (value.Length != 11 || same) { return false; }

            return true;
        }
    }
}