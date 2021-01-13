using CakeShop_WPfApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Services;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using CakeShop_WPfApp.Services;

namespace CakeShop_WPfApp.ViewModels
{
    class UpdateCakePageViewModel: BaseViewModel
    {
        //Biến binding

        private string _imageSource;
        public string ImageSource
        {
            get { return this._imageSource; }
            set
            {
                if (this._imageSource != value)
                {
                    this._imageSource = value;
                    this.OnPropertyChanged(nameof(_imageSource));
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return this._name; }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this.OnPropertyChanged(nameof(_name));
                }
            }
        }

        private int _importPrice;
        public int ImportPrice
        {
            get { return this._importPrice; }
            set
            {
                if (this._importPrice != value)
                {
                    this._importPrice = value;
                    this.OnPropertyChanged(nameof(_importPrice));
                }
            }
        }

        private int _sellingPrice;
        public int SellingPrice
        {
            get { return this._sellingPrice; }
            set
            {
                if (this._sellingPrice != value)
                {
                    this._sellingPrice = value;
                    this.OnPropertyChanged(nameof(_sellingPrice));
                }
            }
        }

        private int _amount;
        public int Amount
        {
            get { return this._amount; }
            set
            {
                if (this._amount != value)
                {
                    this._amount = value;
                    this.OnPropertyChanged(nameof(_amount));
                }
            }
        }

        private int _categoryID;
        public int CategoryID
        {
            get { return this._categoryID; }
            set
            {
                if (this._categoryID != value)
                {
                    this._categoryID = value;
                    this.OnPropertyChanged(nameof(_categoryID));
                }
            }
        }

        private string _unit;
        public string Unit
        {
            get { return this._unit; }
            set
            {
                if (this._unit != value)
                {
                    this._unit = value;
                    this.OnPropertyChanged(nameof(_unit));
                }
            }
        }

        private string _information;
        public string Information
        {
            get { return this._information; }
            set
            {
                if (this._information != value)
                {
                    this._information = value;
                    this.OnPropertyChanged(nameof(_information));
                }
            }
        }

        private string _categoryNameData;
        public string CategoryNameData
        {
            get { return this._categoryNameData; }
            set
            {
                if (this._categoryNameData != value)
                {
                    this._categoryNameData = value;
                    this.OnPropertyChanged(nameof(_categoryNameData));
                }
            }
        }

        private ObservableCollection<CategoryModel> _allCategory;
        public ObservableCollection<CategoryModel> AllCategory
        {
            get
            {
                return _allCategory;
            }
            set
            {
                _allCategory = value;
                OnPropertyChanged(nameof(AllCategory));
            }
        }

        private int _oldAmount;
        public int OldAmount
        {
            get { return this._oldAmount; }
            set
            {
                if (this._oldAmount != value)
                {
                    this._oldAmount = value;
                    this.OnPropertyChanged(nameof(_oldAmount));
                }
            }
        }

        public CategoryServices categoryServices = new CategoryServices();
        public ICommand addImageButtonCommand { get; set; }

        public ICommand doneButtonCommand { get; set; }

        private MainViewModel mainViewModel;

        public CakeServices cakeServices = new CakeServices();
        public UpdateCakePageViewModel(int CakeID, MainViewModel param)
        {
            this.mainViewModel = param;
            AllCategory = new ObservableCollection<CategoryModel>();
            List<CategoryModel> tempList = new List<CategoryModel>();
            tempList = categoryServices.LoadAll();
            for (int i = 0; i < tempList.Count(); i++)
            {
                AllCategory.Add(tempList[i]);
            }
            addImageButtonCommand = new RelayCommand(o => updateImageButtonClick());
            doneButtonCommand = new RelayCommand(o => doneButtonClick(CakeID, param));
            CakeModel myCake = new CakeModel();
            myCake = cakeServices.loadSingleCake(CakeID);
            Name = myCake.Name;
            ImportPrice = myCake.ImportPrice;
            SellingPrice = myCake.SellingPrice;
            Amount = 0;
            Information = myCake.Information;
            Unit = myCake.Unit;
            OldAmount = myCake.Amount;
            for(int i=0;i<AllCategory.Count();i++)
            {
                if(AllCategory[i].Name == myCake.Category.Name)
                {
                    CategoryID = i;
                }
            }
            //Image
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            directory += "Database\\Images\\CakeImages\\";
            ImageSource = directory + myCake.ID + ".png";
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(ImportPrice));
            OnPropertyChanged(nameof(SellingPrice));
            OnPropertyChanged(nameof(Amount));
            OnPropertyChanged(nameof(Information));
            OnPropertyChanged(nameof(Unit));
            OnPropertyChanged(nameof(ImageSource));
        }

        private void doneButtonClick(int CakeID, MainViewModel param)
        {
            CakeModel newCake = new CakeModel();
            newCake.Name = Name;
            newCake.ImportPrice = ImportPrice;
            newCake.SellingPrice = SellingPrice;
            newCake.Amount = Amount + OldAmount;
            newCake.Information = Information;
            newCake.Unit = Unit;
            newCake.ID = CakeID;

            if (CategoryID == -1)
            {
                CategoryModel tempCategory = new CategoryModel();
                tempCategory.ID = -1;
                tempCategory.Name = CategoryNameData;
                newCake.Category = tempCategory;

            }
            else
            {
                newCake.Category = AllCategory[CategoryID];
            }
            int ID = CakeID;
            cakeServices.updateCakeInformationInDatabase(newCake);

            if (ImageSource == null)
            {
                ImageSource = "";
            }
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            directory += "Database\\Images\\CakeImages";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string fileName = ID.ToString() + ".png";
            string sourcePath = ImageSource;
            string targetPath = directory;
            string sourceFile = System.IO.Path.Combine(sourcePath, "");
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            if(sourceFile!=destFile)
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.IO.File.Copy(sourceFile, destFile, true);
            }
            MessageBox.Show("Cập nhật sản phẩm mới thành công!!!");
            mainViewModel.SelectedViewModel = new HomePageViewModel(param);
        }

        private void updateImageButtonClick()
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                File.ReadAllText(openFileDialog.FileName);
                ImageSource = openFileDialog.FileName;
            }
            OnPropertyChanged(nameof(ImageSource));
        }
    }
}
