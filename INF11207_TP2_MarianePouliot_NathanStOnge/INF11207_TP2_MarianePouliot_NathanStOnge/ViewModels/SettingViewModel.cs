using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;



namespace INF11207_TP2_MarianePouliot_NathanStOnge.ViewModels
{
    internal class SettingViewModel
    {
        public Models.Setting? Setting { get; set; }
        public ObservableCollection<string> sortAlgorithms { get; set; }
        private int _currentUserId;
        private Models.WineQualityDbContext _db;
        public ObservableCollection<string> Cities { get; set; }
        public Models.User? User { get; set; }
        private Principale _principaleWindow;
        //public Visibility FirstPictureVisibility { get; set; } = Visibility.Hidden;
        //public Visibility SecondPictureVisibility { get; set; } = Visibility.Hidden;
        //public Visibility ThirdPictureVisibility { get; set; } = Visibility.Hidden;
        public ObservableCollection<Models.Prediction> Predictions { get; private set; }




        public SettingViewModel(int currentUserId, Principale principaleWindow)
        {
            _principaleWindow = principaleWindow;
            _currentUserId = currentUserId; 
            Setting = new Models.Setting();
            sortAlgorithms = new ObservableCollection<string>() { "Shell", "Sélection" };
            _db = new Models.WineQualityDbContext();
            User = _db.Users.Find(_currentUserId);
            Cities = new ObservableCollection<string>() { "Rimouski", "Québec", "Lévis" };
            Predictions = new ObservableCollection<Models.Prediction>();
            Refresh();

            
            UpdateUserInfosCommand = new RelayCommand(
                o => User.IsValid,
                o => UpdateUserInfos()
                );
            OpenTrainFileCommand = new RelayCommand(
                o => true,
                o => OpenTrainFile()
                );
            SetKnnCommand = new RelayCommand(
                o => Setting.IsValidTrain,
                o => SetKnn()
                );
            PredictCommand = new RelayCommand(
                o => Setting.IsValidPredict,
                o => Predict()
                );
            OpenTestFileCommand = new RelayCommand(
                o => true,
                o => OpenTestFile()
                );
            EvaluateKnnCommand = new RelayCommand(
                o => Setting.IsValidEvaluate,
                o => EvaluateKnn()
                );
            RefreshCommand = new RelayCommand(
                o => true,
                o => Refresh()
                );
            QuitWindowMenuCommand = new RelayCommand(
                o => true,
                o => QuitWindowMenu()
                );
           


        }

        
        public ICommand UpdateUserInfosCommand { get; private set; }
        private void UpdateUserInfos()
        {

            _db.Users.Find(_currentUserId).UserId = _currentUserId;
            _db.Users.Find(_currentUserId).Name = User.Name;
            _db.Users.Find(_currentUserId).FirstName = User.FirstName;
            _db.Users.Find(_currentUserId).City = User.City;
            _db.Users.Find(_currentUserId).Gender = User.Gender;
            _db.Users.Find(_currentUserId).Birthday = User.Birthday;
            _db.Users.Find(_currentUserId).Email = User.Email;

            _db.SaveChanges();

            MessageBox.Show("Utilisateur mis à jour");
        }
        public ICommand OpenTrainFileCommand { get; private set; }
        private void OpenTrainFile()
        {
            OpenFileDialog fileOpener = new OpenFileDialog();
            if(fileOpener.ShowDialog() == true)
                Setting.TrainFilePath = fileOpener.FileName;

        }
        public ICommand SetKnnCommand { get; private set; }
        private void SetKnn()
        {
            string _sortAlgo = "selection";
            if (Setting.SortAlgo == "Sélection")
                _sortAlgo = "selection";
            else if (Setting.SortAlgo == "Shell")
                _sortAlgo = "shell";


           Setting.knn = new KnnLibrary.KNN(); 
           Setting.knn.Train(Setting.TrainFilePath, Setting.ValueK, _sortAlgo);
           MessageBox.Show("Paramétrage réussi");
        }
        public ICommand PredictCommand { get; private set; }
        private void Predict()
        {
            
            int quality = Setting.knn.Predict(new KnnLibrary.Wine
            {
                Alcohol = Setting.Alcool,
                Sulphates = Setting.Sulfate,
                CitricAcid = Setting.AcidCitric,
                VolatileAcidity = Setting.AcidVolatil,
            });
            switch (quality)
            {
                case 3:
                    //FirstPictureVisibility = Visibility.Hidden;
                    //SecondPictureVisibility = Visibility.Hidden;
                    //ThirdPictureVisibility = Visibility.Visible;
                    _principaleWindow.first.Visibility = Visibility.Hidden;
                    _principaleWindow.second.Visibility = Visibility.Hidden;
                    _principaleWindow.third.Visibility = Visibility.Visible;
                    break;
                case 6:
                    //FirstPictureVisibility = Visibility.Hidden;
                    //SecondPictureVisibility = Visibility.Visible;
                    //ThirdPictureVisibility = Visibility.Hidden;
                    _principaleWindow.first.Visibility = Visibility.Hidden;
                    _principaleWindow.second.Visibility = Visibility.Visible;
                    _principaleWindow.third.Visibility = Visibility.Hidden;
                    break;
                case 9:
                    //FirstPictureVisibility = Visibility.Visible;
                    //SecondPictureVisibility = Visibility.Hidden;
                    //ThirdPictureVisibility = Visibility.Hidden;
                    _principaleWindow.first.Visibility = Visibility.Visible;
                    _principaleWindow.second.Visibility = Visibility.Hidden;
                    _principaleWindow.third.Visibility = Visibility.Hidden;
                    break;
            }

            _db.Add(new Models.Setting()
            {
                ValueK = Setting.ValueK,
                SortAlgo = Setting.SortAlgo,
                Alcool = Setting.Alcool,
                AcidCitric = Setting.AcidCitric,
                AcidVolatil = Setting.AcidVolatil,
                Sulfate = Setting.Sulfate,    
                

            });
            _db.SaveChanges();
            int maxId = _db.Settings.Max(s => s.SettingId);
            _db.Add(new Models.Prediction()
            {
                Quality = quality,
                SettingId = maxId,
                UserId = User.UserId,
                Date = DateTime.Now.ToString(),
            });
            _db.SaveChanges();
            
        }
        public ICommand OpenTestFileCommand { get; private set; }
        private void OpenTestFile()
        {
            OpenFileDialog fileOpener = new OpenFileDialog();
            if (fileOpener.ShowDialog() == true)
                Setting.TestFilePath = fileOpener.FileName;

        }
        public ICommand EvaluateKnnCommand { get; private set; }
        private void EvaluateKnn()
        {
            Setting.knn.Evaluate(Setting.TestFilePath);
            _principaleWindow.performance.Content = Setting.knn.Accuracy;
            List<string> confusionMatrix = new List<string>();

            foreach (KeyValuePair<int, int[]> line in Setting.knn.ConfusionMatrix)
            {
                foreach (int value in line.Value)
                {
                    confusionMatrix.Add(value.ToString());
                }

            }

            _principaleWindow.t_3.Content = confusionMatrix[0];
            _principaleWindow.t_6.Content = confusionMatrix[1];
            _principaleWindow.t_9.Content = confusionMatrix[2];
            _principaleWindow.s_3.Content = confusionMatrix[3];
            _principaleWindow.s_6.Content = confusionMatrix[4];
            _principaleWindow.s_9.Content = confusionMatrix[5];
            _principaleWindow.n_3.Content = confusionMatrix[6];
            _principaleWindow.n_6.Content = confusionMatrix[7];
            _principaleWindow.n_9.Content = confusionMatrix[8];

        }
        public ICommand RefreshCommand { get; private set; }
        private void Refresh()
        {
            try
            {

                Predictions.Clear();

                foreach (Models.Prediction prediction in _db.Predictions.Where(p => p.UserId == User.UserId).ToList())
                {
                    Models.Prediction displayPrediction = new Models.Prediction();

                    displayPrediction.Alcool = _db.Settings.Where(s => s.SettingId == prediction.SettingId).First().Alcool;
                    displayPrediction.AcidCitric = _db.Settings.Where(s => s.SettingId == prediction.SettingId).First().AcidCitric;
                    displayPrediction.AcidVolatil = _db.Settings.Where(s => s.SettingId == prediction.SettingId).First().AcidVolatil;
                    displayPrediction.Sulfate = _db.Settings.Where(s => s.SettingId == prediction.SettingId).First().Sulfate;
                    displayPrediction.ValueK = _db.Settings.Where(s => s.SettingId == prediction.SettingId).First().ValueK;
                    displayPrediction.SortAlgo = _db.Settings.Where(s => s.SettingId == prediction.SettingId).First().SortAlgo;
                    displayPrediction.Quality = _db.Predictions.Where(p => p.SettingId == prediction.SettingId).First().Quality;
                    displayPrediction.Date = _db.Predictions.Where(p => p.SettingId == prediction.SettingId).First().Date;

                    Predictions.Add(displayPrediction);

                }
            }
            catch (Exception ex) { }
            
            }
        public ICommand QuitWindowMenuCommand { get; private set; }
        private void QuitWindowMenu()
        {
            Application.Current.Shutdown();
        }
        



    }
}
