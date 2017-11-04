using HOMvvmCross.Core.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOMvvmCross.Core.Model;
using Refit;
using HOMvvmCross.Core.Service.RestContract;
using System.Runtime.CompilerServices;

namespace HOMvvmCross.Core.Service
{
    public class DogApiService : IDogApiService
    {
        private const string BASE_URL = "https://dog.ceo";

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
