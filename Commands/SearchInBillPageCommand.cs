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
    class SearchInBillPageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private AddBillPageViewModel _viewModel;

        public AddBillPageViewModel ViewModel { get => _viewModel; set => _viewModel = value; }

        public SearchInBillPageCommand(AddBillPageViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
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
