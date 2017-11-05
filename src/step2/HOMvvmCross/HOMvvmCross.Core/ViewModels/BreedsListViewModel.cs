using HOMvvmCross.Core.Service.Interfaces;
using MvvmCross.Core.ViewModels;
using System;

namespace HOMvvmCross.Core.ViewModels
{
    /// <summary>
    /// View Model que representa a tela de listagem de raçcas
    /// </summary>
    public class BreedsListViewModel : MvxViewModel
    {
        private MvxObservableCollection<string> _breeds;
        private IDogApiService _dogApiService;

        public MvxObservableCollection<string> Breeds
        {
            get { return _breeds; }        
        }

        private IMvxCommand<string> _breedClick;

        public IMvxCommand<string> BreedClick
        {
            get
            {
                return _breedClick ?? (_breedClick = new MvxCommand<string>(ShowBreedImagesPage));
            }
        }

        private void ShowBreedImagesPage(string breedName)
        {
            ShowViewModel<BreedImagesViewModel>(breedName);
        }


        public BreedsListViewModel(IDogApiService dogApiService)
        {
            _breeds = new MvxObservableCollection<string>();
            _dogApiService = dogApiService;
        }

        public override async void Start()
        {
            base.Start();

            try
            {

                var models = await _dogApiService.ListBreeds();

                Breeds.AddRange(models);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERRO - Carregar listagem - {ex.Message}");
            }

        }

    }
}
