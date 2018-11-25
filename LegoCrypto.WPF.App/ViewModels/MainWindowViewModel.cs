using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LegoCrypto.WPF.App.ViewModels
{
    public class MainWindowViewModel
    {
        ObservableCollection<ViewModelBase> _workspaces;
        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        public ObservableCollection<ViewModelBase> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<ViewModelBase>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public MainWindowViewModel()
        {
            var tokenVm = new TokenSelectionViewModel();
            var deviceVm = new DeviceViewModel();
            var cryptoVm = new CryptoViewModel();

            Workspaces.Add(cryptoVm);
            Workspaces.Add(tokenVm);
            Workspaces.Add(deviceVm);
            SetActiveWorkspace(cryptoVm);
        }

        void SetActiveWorkspace(ViewModelBase workspace)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
    }
}
