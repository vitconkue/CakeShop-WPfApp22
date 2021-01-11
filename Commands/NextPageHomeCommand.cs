using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CakeShop_WPfApp.Helper;
using CakeShop_WPfApp.ViewModels;

namespace CakeShop_WPfApp.Commands
{
    class NextPageHomeCommand : ICommand
    {
       
        private HomePageViewModel _viewModel;

        public HomePageViewModel ViewModel { get => _viewModel; set => _viewModel = value; }

        public NextPageHomeCommand(HomePageViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }



        public bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return true;
            }
            var pagingVar = parameter as Paging;
            bool result = true;

            if (pagingVar.CurrentPage == pagingVar.TotalPages)
            {
                result = false;
            }
            return result;
        }

        public void Execute(object parameter)
        {
            _viewModel.GoToNextPage();
        }
    }
}
