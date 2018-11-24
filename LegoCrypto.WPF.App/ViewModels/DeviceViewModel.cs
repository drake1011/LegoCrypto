using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using LegoCrypto.IO;

namespace LegoCrypto.WPF.App
{
    public class DeviceViewModel : ViewModelBase
    {
        private ObservableCollection<COMPortInfo> _comPorts;
        public ObservableCollection<COMPortInfo> ComPorts { get => _comPorts; set => SetProperty(ref _comPorts, value); }

        public string DisplayMember { get; set; } = "Description";

        private string _connectStatus = string.Empty;
        public string ConnectStatus { get => _connectStatus; set => SetProperty(ref _connectStatus, value); }

        public RelayCommand<object> SelectionChangedCmd { get; set; }
        public RelayCommand RefreshPortsCmd { get; set; }
        public RelayCommand ConnectCmd { get; set; }

        private ICollectionView _collectionView;

        private INfcDevice _nfcDevice;
        public INfcDevice NfCDevice { get => _nfcDevice; set => SetProperty(ref _nfcDevice, value); }

        public DeviceViewModel()
        {
            SelectionChangedCmd = new RelayCommand<object>(SelectionChanged);
            RefreshPortsCmd = new RelayCommand(RefreshPorts);
            ConnectCmd = new RelayCommand(Connect);
            RefreshPorts();
            
        }

        public void SelectionChanged(object sender)
        {

        }

        public void RefreshPorts()
        {
            ComPorts = new ObservableCollection<COMPortInfo>(COMPortInfo.GetCOMPortsInfo());

            // move the current index to arduino
            _collectionView = CollectionViewSource.GetDefaultView(ComPorts);
            if (_collectionView != null)
            {
                var arduino = ComPorts.FirstOrDefault(item => item.Description.Contains("Arduino"));
                if (arduino != null)
                    _collectionView.MoveCurrentTo(arduino);
            }
        }

        public void Connect()
        {
            ConnectStatus = "...";
            using (var arduino = new IO.Arduino.ArduinoNFC(((COMPortInfo)_collectionView.CurrentItem).Name, 9600, 800))
            {
                ConnectStatus = arduino.CheckDevice() ? "Success" : "Fail";
            }
        }
    }
}
