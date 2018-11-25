using LegoCrypto.WPF.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LegoCrypto.WPF.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow window = new MainWindow();

            // Create the ViewModel to which 
            // the main window binds.
            //string path = "Data/customers.xml";
            var viewModel = new MainWindowViewModel();

            // When the ViewModel asks to be closed, 
            // close the window.
            ////EventHandler handler = null;
            ////handler = delegate
            ////{
            ////    viewModel.RequestClose -= handler;
            ////    window.Close();
            ////};
            ////viewModel.RequestClose += handler;

            // Allow all controls in the window to 
            // bind to the ViewModel by setting the 
            // DataContext, which propagates down 
            // the element tree.
            window.DataContext = viewModel;

            window.Show();
        }
    }
}
