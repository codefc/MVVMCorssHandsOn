using HOMvvmCross.Core.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMvvmCross.Core.Service.Interfaces
{
    public interface IDogApiService
    {
        Task<List<string>> ListBreeds();

        Task<List<string>> GetImagesOfBreed(string breedName);
    }
}
