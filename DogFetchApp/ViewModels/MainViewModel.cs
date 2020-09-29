using ApiHelper;
using DogFetchApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private int nbrImg = 5;

        public int NbrImg
        {
            get => nbrImg;
            set
            {
                nbrImg = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> currentList = new ObservableCollection<string>();
        public ObservableCollection<string> CurrentList
        {
            get => currentList;
            set
            {
                currentList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> breedsListCollection = new ObservableCollection<string>();

        public ObservableCollection<string> BreedsListCollection
        {
            get => breedsListCollection;
            set
            {
                breedsListCollection = value;
                OnPropertyChanged();
            }
        } 
        
        private ObservableCollection<string> dogsListCollection = new ObservableCollection<string>();

        public ObservableCollection<string> DogsListCollection
        {
            get => dogsListCollection;
            set
            {
                dogsListCollection = value;
                OnPropertyChanged();
            }
        }

        private string selectedBreed;
        private string SelectedNbr = "1";

        public string SelectedBreed
        {
            get => selectedBreed;
            set
            {
                selectedBreed = value;
                OnPropertyChanged();
                LoadImagesCommand.RaiseCanExecuteChanged();
            }
        }

        /*Les commandes*/
        public AsyncCommand<string> NextImagesCommand { get; private set; }
        public AsyncCommand<string> LoadImagesCommand { get; private set; }


        public MainViewModel()
        {
            LoadImagesCommand = new AsyncCommand<string>(LoadImages, CanExecuteLoadImage);
            LoadBreeds();
        }

        private bool CanExecuteLoadImage(string T)
        {
            bool isFetchable;
            if ((SelectedBreed != null) && (SelectedNbr != null))
                isFetchable = true;
            else
                isFetchable = false;

            return isFetchable;
        }

        private async Task LoadImages(string T)
        {
            var dog = await DogApiProcessor.GetImageUrl(SelectedBreed, nbrImg);

            for (int i = 0; i < nbrImg; i++)
            {
                dogsListCollection.Add(dog.Message[i]);
                CurrentList.Add(dog.Message[i]);
            }
        }

        private async void LoadBreeds()
        {
            List<string> list = await DogApiProcessor.LoadBreedList();
            foreach (string item in list)
            {
                breedsListCollection.Add(item);
            }

        }


    }
}
