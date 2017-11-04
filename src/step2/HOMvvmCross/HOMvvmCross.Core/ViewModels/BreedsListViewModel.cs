using HOMvvmCross.Core.Service.Interfaces;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOMvvmCross.Core.ViewModels
{
    public class BreedsListViewModel : MvxViewModel
    {
        private MvxObservableCollection<string> _breeds;
        private IDogApiService _dogApiService;

        public MvxObservableCollection<string> Breeds
        {
            get { return _breeds; }
            set
            {
                SetProperty(ref _breeds, value);
            }
        }

        private IMvxCommand<string> _breedClick;

        public IMvxCommand<string> BreedClick
        {
            get
            {
                return _breedClick ?? (_breedClick = new MvxCommand<string>(ShowBreedImagesPage));
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                SetProperty(ref _isBusy, value);
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

            IsBusy = true;

            try
            {

                var models = await _dogApiService.ListBreeds();

                _breeds.AddRange(models);
            } catch
            {
                // Log
            }
            finally
            {
                IsBusy = false;
            }

           
        }

    }
}
