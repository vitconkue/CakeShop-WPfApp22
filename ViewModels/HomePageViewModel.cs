using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public CategoryModel SelectedCategory { get; set; }
        public ICommand UpdateView { get; set; }
        public ICommand GotoDetailPage { get; set; }

        public Paging PagingVar { get; set; }
        public int tripPerPage { get; set; }

        private int _currentFilter;

        public void CalculatePaging()
        {
            int cakeCount = CakeServices.GetCount();
            int newTotalPage = cakeCount / _cakePerPage +
                    (((cakeCount % _cakePerPage) == 0) ? 0 : 1);
            PagingVar = new Paging
            {
                CurrentPage = 1,
                CakePerPage = _cakePerPage,
                TotalPages = newTotalPage,


            };
            OnPropertyChanged("PagingVar");
            CakeList = CakeServices.GetCakeWithPageInfo(PagingVar.CurrentPage, PagingVar.CakePerPage, -1);

        }
        public HomePageViewModel(MainViewModel param)
        {
            
            _cakePerPage = 2;
            mainViewModel = param;
            UpdateView = new UpdateMainViewCommand(mainViewModel);
            CalculatePaging();
            GotoDetailPage = new RelayCommand(o => ShowCakeDetailPage(o));
            CategoryList = CategoryServices.LoadAll();
            SelectedCategory = new CategoryModel();
            SelectedCategory.Name = "Tất cả";
            CategoryList.Add(SelectedCategory);
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
            CakeList = CakeServices.GetCakeWithPageInfo(PagingVar.CurrentPage, PagingVar.CakePerPage, -1);
            OnPropertyChanged("CakeList");

        }

        public void GoToPreviousPage()
        {
            PagingVar.CurrentPage--;
            OnPropertyChanged("PagingVar");
            CakeList = CakeServices.GetCakeWithPageInfo(PagingVar.CurrentPage, PagingVar.CakePerPage, -1);
            OnPropertyChanged("CakeList");

        }
    }
}
