using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CakeShop_WPfApp.ViewModels;

namespace CakeShop_WPfApp.Commands
{
    class SearchCommand : ICommand
    {
        private HomePageViewModel _viewModel;
        public HomePageViewModel ViewModel { get => _viewModel; set => _viewModel = value; }

        public SearchCommand(HomePageViewModel viewModel)
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
            if(parameter ==null)
            {
                return true;
            }
            string searchText = parameter as string;
            return true;
        }

        public void Execute(object parameter)
        {
            string searchText = parameter as string;
            _viewModel.ApplySearch(searchText);
            Debug.WriteLine(searchText); 
        }
    }
}
