using HOMvvmCross.Core.Service.Interfaces;
using MvvmCross.Core.ViewModels;
using System;

namespace HOMvvmCross.Core.ViewModels
{
    /// <summary>
    /// View Model que representa uma listagem de imagens relacionadas a uma raça
    /// </summary>
    public class BreedImagesViewModel : MvxViewModel
    {
        private MvxObservableCollection<string> _images;
        private IDogApiService _dogApiService;

        public MvxObservableCollection<string> Images
        {
            get { return _images; }
        }

        private string _breedName;

        public void Init(string breedName)
        {
            _breedName = breedName;
        }

        public BreedImagesViewModel(IDogApiService dogApiService)
        {
            _images = new MvxObservableCollection<string>();
            _dogApiService = dogApiService;
        }

        public override async void Start()
        {
            base.Start();

            try
            {

                var models = await _dogApiService.GetImagesOfBreed(_breedName);

                Images.AddRange(models);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERRO - Carregar imagens - {ex.Message}");
            }

        }
    }
}
