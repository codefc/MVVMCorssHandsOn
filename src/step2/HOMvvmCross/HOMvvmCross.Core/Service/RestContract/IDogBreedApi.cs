using HOMvvmCross.Core.Model;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HOMvvmCross.Core.Service.RestContract
{
    /// <summary>
    /// Interface contendo os métodos dos serviços REST a serem requisitados pela aplicação.
    /// </summary>
    public interface IDogBreedApi
    {
        /// <summary>
        /// Obtém todas as raças a API Dogs
        /// </summary>
        /// <returns></returns>
        [Get("/api/breeds/list")]
        Task<DogApiObjectReturn<List<string>>> ListBreeds();


        /// <summary>
        /// Obtém as imagens disponíveis de uma raça passada como parâmetro
        /// </summary>
        /// <param name="breedName"></param>
        /// <returns></returns>
        [Get("/api/breed/{breedName}/images")]
        Task<DogApiObjectReturn<List<string>>> GetImagesOfBreed(string breedName);
    }
}
