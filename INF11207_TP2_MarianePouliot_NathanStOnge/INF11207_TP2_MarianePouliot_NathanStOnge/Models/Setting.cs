using KnnLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace INF11207_TP2_MarianePouliot_NathanStOnge.Models
{
    internal class Setting : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        Regex trainFileFormat = new Regex(".train_reduced.csv");
        Regex testFileFormat = new Regex(".test_reduced.csv");
        public KNN? knn;
        //public KNN? knn { get { return _knn; } }
        private bool _isValidPredict;

        public bool IsValidPredict
        {
            get { return _isValidPredict; }
        }

        private bool _isValidTrain;

        public bool IsValidTrain
        {
            get { return _isValidTrain; }
        }

        public int SettingId { get; set; }
        private bool _isValidEvaluate;
        public bool IsValidEvaluate
        {
            get { return _isValidEvaluate; }
        }



        private int _valueK;
        public int ValueK
        {
            get { return _valueK; }
            set
            {
                if (_valueK != value)
                {
                    _valueK = value;
                    SetIsValidTrain();
                    OnPropertyChanged();
                }
            }
        }

        private string? _sortAlgo;
        public string? SortAlgo
        {
            get { return _sortAlgo; }
            set
            {
                if (SortAlgo != value)
                {
                    _sortAlgo = value;
                    SetIsValidTrain();
                    OnPropertyChanged();
                }
            }
        }




        private float _alcool;
        public float Alcool
        {
            get { return _alcool; }
            set
            {
                if (_alcool != value)
                {
                    _alcool = value;
                    SetIsValidPredict();
                    OnPropertyChanged();
                }
            }
        }

        private float _sulfate;
        public float Sulfate
        {
            get { return _sulfate; }
            set
            {
                if (_sulfate != value)
                {
                    _sulfate = value;
                    SetIsValidPredict();
                    OnPropertyChanged();
                }
            }
        }

        private float _acidCitric;
        public float AcidCitric
        {
            get { return _acidCitric; }
            set
            {
                if (_acidCitric != value)
                {
                    _acidCitric = value;
                    SetIsValidPredict();
                    OnPropertyChanged();
                }
            }
        }

        private float _acidVolatil;
        public float AcidVolatil
        {
            get { return _acidVolatil; }
            set
            {
                if (_acidVolatil != value)
                {
                    _acidVolatil = value;
                    SetIsValidPredict();
                    OnPropertyChanged();
                }
            }
        }
        private string? _trainFilePath;
        [NotMapped]
        public string? TrainFilePath
        {
            get { return _trainFilePath; }
            set
            {
                if (_trainFilePath != value)
                {
                    _trainFilePath = value;
                    SetIsValidTrain();
                    OnPropertyChanged();
                }
            }
        }
        private string? _testFilePath;
        [NotMapped]
        public string? TestFilePath
        {
            get { return _testFilePath; }
            set
            {
                if (_testFilePath != value)
                {
                    _testFilePath = value;
                    SetIsValidEvaluate();
                    OnPropertyChanged();
                }
            }
        }
        
        


        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetIsValidPredict()
        {
            _isValidPredict = float.IsNormal(Alcool)
                    && float.IsNormal(Sulfate)
                    && float.IsNormal(AcidCitric)
                    && float.IsNormal(AcidVolatil)
                    && knn != null;
        }

        private void SetIsValidTrain()
        {
            _isValidTrain = int.TryParse(ValueK.ToString(), out _valueK)
                    && ValueK >= 1
                    && !string.IsNullOrEmpty(SortAlgo)
                    && !string.IsNullOrEmpty(_trainFilePath)
                    && trainFileFormat.IsMatch(_trainFilePath)
                    ;
                    
        }
        private void SetIsValidEvaluate()
        {
            _isValidEvaluate = !string.IsNullOrEmpty(_testFilePath)
                    && testFileFormat.IsMatch(_testFilePath)
                    && knn != null;
        }
    }
}
