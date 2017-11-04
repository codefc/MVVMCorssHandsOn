using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMvvmCross.Core.Model
{
    /// <summary>
    /// Classe para abstrair um objeto de retorno obtido através da execução da API Pública <see cref="https://dog.ceo/dog-api/"/>
    /// </summary>
    /// <typeparam name="TMessage">Tipo que definirá a propriedade Message, representando o dado de retorno da API</typeparam>
    public class DogApiObjectReturn<TMessage>
    {
        /// <summary>
        /// Mapeando a propriedade status do objeto Json de retorno da API
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        public bool Success
        {
            get { return Status.ToLower() == "success"; }
        }

        /// <summary>
        /// Mapeando a propriedade Message do objeto Json de retorno da API
        /// </summary>
        [JsonProperty("message")]
        public TMessage Message
        {
            get; set;
        }
    }
}
