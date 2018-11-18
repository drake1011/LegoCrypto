using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LegoCrypto.IO;

namespace LegoCrypto.WPF.App
{
    public class DeviceViewModel : ViewModelBase
    {
        private ObservableCollection<COMPortInfo> _comPorts;
        public ObservableCollection<COMPortInfo> ComPorts { get => _comPorts; set => SetProperty(ref _comPorts, value); }

        public string DisplayMember { get; set; } = "Description";

        public RelayCommand<object> SelectionChangedCmd { get; set; }

        public DeviceViewModel()
        {
            SelectionChangedCmd = new RelayCommand<object>(SelectionChanged);
            ComPorts = new ObservableCollection<COMPortInfo>(COMPortInfo.GetCOMPortsInfo());

            // move the current index to arduino
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(ComPorts);
            if (collectionView != null)
            {
                var arduino = ComPorts.FirstOrDefault(item => item.Description.Contains("Arduino"));
                if(arduino != null)
                    collectionView.MoveCurrentTo(arduino);
            }
        }

        public void SelectionChanged(object sender)
        {

        }
    }
}
