using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CakeShop_WPfApp.Commands;
using CakeShop_WPfApp.Helper;
using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Services;
using CakeShop_WPfApp.Views;

namespace CakeShop_WPfApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;

        private int _cakePerPage;
        public CakeServices CakeServices = new CakeServices();
        public List<CakeModel> CakeList { get; set; }
        public CategoryServices CategoryServices = new CategoryServices();
        public List<CategoryModel> CategoryList { get; set; }
        public ICommand NextPage { get; set; }
        public ICommand PreviousPage { get; set; }

        public ICommand SearchCommand { get; set; }

        public string CurrentSearchText { get; set; }

        private CategoryModel _selectedCategory; 
        public CategoryModel SelectedCategory {
            get
            {
                return _selectedCategory; 
            }
            set
            {
                _selectedCategory = value;
                CalculatePaging();
                Debug.Write(_selectedCategory.ID);
            }
        }
        public ICommand UpdateView { get; set; }
        public ICommand GotoDetailPage { get; set; }

        public Paging PagingVar { get; set; }
    

     

        public void CalculatePaging()
        {
            int cakeCount = CakeServices.GetCount(SelectedCategory.ID, CurrentSearchText);
            int newTotalPage = cakeCount / _cakePerPage +
                    (((cakeCount % _cakePerPage) == 0) ? 0 : 1);
            if (newTotalPage == 0)
                newTotalPage = 1;
            PagingVar = new Paging
            {
                CurrentPage = 1,
                CakePerPage = _cakePerPage,
                TotalPages = newTotalPage,


            };
            OnPropertyChanged("PagingVar");
            CakeList = CakeServices.GetCakeWithPageInfo(PagingVar.CurrentPage, PagingVar.CakePerPage, SelectedCategory.ID,CurrentSearchText);
            OnPropertyChanged("CakeList");
        }
        public ICommand GotoUpdatePage { get; set; }
        public HomePageViewModel(MainViewModel param)
        {
            //test = "Tất cả";
            //CurrentFilter = 0;
            CurrentSearchText = "";
            _cakePerPage = 8;
            mainViewModel = param;
            UpdateView = new UpdateMainViewCommand(mainViewModel);
            CakeList = CakeServices.GetAllCakes();
            GotoDetailPage = new RelayCommand(o => ShowCakeDetailPage(o));
            GotoUpdatePage = new RelayCommand(o => ShowUpdatePage(o));
            CategoryList = CategoryServices.LoadAll();
            SelectedCategory = new CategoryModel();
            SelectedCategory.Name = "Tất cả";
            SelectedCategory.ID = 0;
            CategoryList = CategoryServices.LoadAll();
            CategoryList.Insert(0, SelectedCategory);
            GotoDetailPage = new RelayCommand(o => ShowCakeDetailPage(o));
            
            CalculatePaging();

            SearchCommand = new SearchCommand(this); 
            NextPage = new NextPageHomeCommand(this);
            PreviousPage = new PreviousPageHomeCommand(this);

        }
        public void ShowCakeDetailPage(object parameter)
        {
            var CakeDetailPage = new CakeDetailPage(int.Parse(parameter.ToString()));
            CakeDetailPage.Show();
        }
        
        public void GoToNextPage()
        {
            PagingVar.CurrentPage++;
            OnPropertyChanged("PagingVar");
            CakeList = CakeServices.GetCakeWithPageInfo(PagingVar.CurrentPage, PagingVar.CakePerPage, SelectedCategory.ID, CurrentSearchText);
            OnPropertyChanged("CakeList");

        }

        public void GoToPreviousPage()
        {
            PagingVar.CurrentPage--;
            OnPropertyChanged("PagingVar");
            CakeList = CakeServices.GetCakeWithPageInfo(PagingVar.CurrentPage, PagingVar.CakePerPage, SelectedCategory.ID, CurrentSearchText);
            OnPropertyChanged("CakeList");

        }

        public void ApplySearch(string searchText)
        {
            CurrentSearchText = searchText;
            CalculatePaging();
        }

        public void ShowUpdatePage(object parameter)
        {
            mainViewModel.SelectedViewModel = new UpdateCakePageViewModel(int.Parse(parameter.ToString()), mainViewModel);
        }
    }
}
