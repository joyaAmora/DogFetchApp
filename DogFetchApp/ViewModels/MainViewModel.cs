using ApiHelper;
using DogFetchApp.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private int nbrImg;

        public int NbrImg
        {
            get => nbrImg;
            set
            {
                nbrImg = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> nbrImgList;
        public ObservableCollection<string> NbrImgList
        {
            get => nbrImgList;
            set
            {
                nbrImgList = value;
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

        private string selectedNbr;
        public string SelectedNbr
        {
            get => selectedNbr;
            set
            {
                selectedNbr = value;
                OnPropertyChanged();
                LoadImagesCommand.RaiseCanExecuteChanged();
            }
        }

        private string selectedBreed;
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
        public DelegateCommand<string> ChangeLanguageCommand { get; private set; }


        public MainViewModel()
        {
            LoadImagesCommand = new AsyncCommand<string>(LoadImages, CanExecuteLoadImage);
            NextImagesCommand = new AsyncCommand<string>(LoadImages, CanExecuteNextImage);
            ChangeLanguageCommand = new DelegateCommand<string>(ChangeLanguage);
            LoadBreeds();
            nbImages();
        }

        private void ChangeLanguage(string arg)
        {
            Properties.Settings.Default.Language = arg;
            Properties.Settings.Default.Save();
            MessageBox.Show($"{Properties.Resources.RestartMessage}");
            var filename = Application.ResourceAssembly.Location;
            var newFile = Path.ChangeExtension(filename, ".exe");
            Process.Start(newFile);
            Application.Current.Shutdown();
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

        private bool CanExecuteNextImage(string T)
        {
            bool isFetchable;
            if ((SelectedBreed != null) && (SelectedNbr != null) && DogsListCollection.Count > 0)
                isFetchable = true;
            else
                isFetchable = false;

            return isFetchable;
        }
        private void nbImages()
        {
            NbrImgList = new ObservableCollection<string> { "1", "3", "5", "10" };
        }
        private async Task LoadImages(string T)
        {
            nbrImg = int.Parse(SelectedNbr);
            var dog = await DogApiProcessor.GetImageUrl(SelectedBreed, nbrImg);

            DogsListCollection.Clear(); // vide la liste pour n'avoir que la nouvelle requête

            if (nbrImg > dog.Message.Count)
            {
                nbrImg = dog.Message.Count;
                MessageBox.Show("Nombre d'images disponible inférieure au nombre demandé");
                for (int i = 0; i < nbrImg; i++)
                {
                    dogsListCollection.Add(dog.Message[i]);
                }
            }
            else
            {
                for (int i = 0; i < nbrImg; i++)
                {
                    dogsListCollection.Add(dog.Message[i]);
                }
                NextImagesCommand.RaiseCanExecuteChanged();
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
