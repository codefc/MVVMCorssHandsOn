using HOMvvmCross.Core.Service.Interfaces;
using HOMvvmCross.Core.Service.RestContract;
using Refit;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HOMvvmCross.Core.Service
{
    /// <summary>
    /// Classe de serviço responsável por implementar toda a lógica negocial necessária na aplicação.
    /// Abstrai toda as chamadas a fontes externas ao aplicativo, como o consumo de serviços REST e
    /// preparação dos dados retornados para a aplicação
    /// </summary>
    public class DogApiService : IDogApiService
    {
        /// <summary>
        /// Endereço base da API
        /// </summary>
        private const string BASE_URL = "https://dog.ceo";

        /// <summary>
        /// Contrato de serviço, no padrão REFIT, que contém os métodos da API que serão consumidos
        /// </summary>
        private IDogBreedApi _restService;

        public DogApiService()
        {
            _restService = RestService.For<IDogBreedApi>(BASE_URL);
        }
        
        public async Task<List<string>> GetImagesOfBreed(string breedName)
        {
            
            List<string> breedsImages = new List<string>();

            try
            {
                var breedsMessage = await _restService.GetImagesOfBreed(breedName);

                if (breedsMessage.Success && breedsMessage.Message != null)
                {
                    breedsImages = breedsMessage.Message;
                }
            }
            catch (Exception ex)
            {
                WriteConsole(ex);
            }

            return breedsImages;
        }

        public async Task<List<string>> ListBreeds()
        {
            List<string> breeds = new List<string>();

            try
            {
                var breedsMessage = await _restService.ListBreeds();

                if (breedsMessage.Success && breedsMessage.Message != null)
                {
                    breeds = breedsMessage.Message;
                }
            }
            catch(Exception ex)
            {
                WriteConsole(ex);
            }

            return breeds;
        }

        private void WriteConsole(Exception ex, [CallerMemberName]string methodName = "")
        {
            System.Diagnostics.Debug.WriteLine($"ERRO - {methodName} {ex.Message}");
        }
    }
}
