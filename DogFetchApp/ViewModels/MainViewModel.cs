using ApiHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
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

        public MainViewModel()
        {
            LoadBreeds();
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
