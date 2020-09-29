using ApiHelper;
using ApiHelper.Models;
using DogFetchApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DogFetchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel currentViewmodel;

        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.ApiHelper.InitializeClient();

            currentViewmodel = new MainViewModel();

            DataContext = currentViewmodel;
        }
        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            MenuItem param = sender as MenuItem;
            Properties.Settings.Default.Language = param.Name == "English_Selector" ? "en-CA" : "fr-CA";
            Properties.Settings.Default.Save();
            MessageBox.Show($"{Properties.Resources.RestartMessage}");
            var filename = Application.ResourceAssembly.Location;
            var newFile = Path.ChangeExtension(filename, ".exe");
            Process.Start(newFile);
            Application.Current.Shutdown();
        }
    }
}
