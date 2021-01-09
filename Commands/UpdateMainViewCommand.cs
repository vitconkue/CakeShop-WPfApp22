using CakeShop_WPfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeShop_WPfApp.Commands
{
    public class UpdateMainViewCommand : ICommand
    {
        private MainViewModel viewModel;
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public UpdateMainViewCommand(MainViewModel param)
        {
            this.viewModel = param;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "AddBill")
            {
                viewModel.SelectedViewModel = new AddBillPageViewModel(viewModel);
            }
            if (parameter.ToString() == "HomePage")
            {
                viewModel.SelectedViewModel = new HomePageViewModel(viewModel);
            }

            if (parameter.ToString() == "BillPage")
            {
                viewModel.SelectedViewModel = new BillPageViewModel(viewModel);
            }

        }

    }
}
