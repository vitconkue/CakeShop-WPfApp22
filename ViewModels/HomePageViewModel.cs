using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CakeShop_WPfApp.Commands;
using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Services;
using CakeShop_WPfApp.Views;

namespace CakeShop_WPfApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        public CakeServices CakeServices = new CakeServices();
        public List<CakeModel> CakeList { get; set; }
        public CategoryServices CategoryServices = new CategoryServices();
        public List<CategoryModel> CategoryList { get; set; }
        public CategoryModel SelectedCategory { get; set; }
        public ICommand UpdateView { get; set; }
        public ICommand GotoDetailPage { get; set; }

        public ICommand GotoUpdatePage { get; set; }
        public HomePageViewModel(MainViewModel param)
        {
            mainViewModel = param;
            UpdateView = new UpdateMainViewCommand(mainViewModel);
            CakeList = CakeServices.GetAllCakes();
            GotoDetailPage = new RelayCommand(o => ShowCakeDetailPage(o));
            GotoUpdatePage = new RelayCommand(o => ShowUpdatePage(o));
            CategoryList = CategoryServices.LoadAll();
            SelectedCategory = new CategoryModel();
            SelectedCategory.Name = "Tất cả";
            CategoryList.Add(SelectedCategory);
        }
        public void ShowCakeDetailPage(object parameter)
        {
            var CakeDetailPage = new CakeDetailPage(int.Parse(parameter.ToString()));
            CakeDetailPage.Show();
        }

        public void ShowUpdatePage(object parameter)
        {
            mainViewModel.SelectedViewModel = new UpdateCakePageViewModel(int.Parse(parameter.ToString()), mainViewModel);
        }
    }
}
