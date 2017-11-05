using System.Collections.Generic;
using System.Threading.Tasks;

namespace HOMvvmCross.Core.Service.Interfaces
{
    /// <summary>
    /// Interface que abstrai um contrato para as ViewModels de informações relacionadas a racas de cachorros
    /// </summary>
    public interface IDogApiService
    {
        /// <summary>
        /// Obtém todas as raças de cachorros em frma de uma lista de strings
        /// </summary>
        /// <returns>Una lista de strings contendo as raças dos cachorros</returns>
        Task<List<string>> ListBreeds();

        /// <summary>
        /// Obtém uma lista de imagens de acordo com uma raça definida do parâetro
        /// </summary>
        /// <param name="breedName">Raça desejada</param>
        /// <returns>Lista de endereço de imagem das raças</returns>
        Task<List<string>> GetImagesOfBreed(string breedName);
    }
}
