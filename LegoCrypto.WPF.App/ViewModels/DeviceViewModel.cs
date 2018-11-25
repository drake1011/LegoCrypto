using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using LegoCrypto.Data.Model;
using LegoCrypto.IO;

namespace LegoCrypto.WPF.App.ViewModels
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
        public RelayCommand ReadCmd { get; set; }
        public RelayCommand CancelCmd { get; set; }

        private ICollectionView _collectionView;

        private INfcDevice _nfcDevice;
        public INfcDevice NfcDevice { get => _nfcDevice; set => SetProperty(ref _nfcDevice, value); }

        private ITagData _tagRead;
        public ITagData TagRead { get => _tagRead;
            set {
                SetProperty(ref _tagRead, value);
                UID = TagRead.UID;
                TokenID = TagRead.ID;
                DataPage35 = TagRead.Pages[DataRegister.Page35];
                DataPage36 = TagRead.Pages[DataRegister.Page36];
                DataPage37 = TagRead.Pages[DataRegister.Page37];
                DataPage38 = TagRead.Pages[DataRegister.Page38];
                DataPage43 = TagRead.Pages[DataRegister.Page43];
            } }

        private string _uid;
        public string UID { get => _uid; set => SetProperty(ref _uid, value); }

        private uint? _tokenID;
        public uint? TokenID { get => _tokenID; set => SetProperty(ref _tokenID, value); }

        private string _dataPage35;
        public string DataPage35 { get => _dataPage35; set => SetProperty(ref _dataPage35, value); }

        private string _dataPage36;
        public string DataPage36 { get => _dataPage36; set => SetProperty(ref _dataPage36, value); }

        private string _dataPage37;
        public string DataPage37 { get => _dataPage37; set => SetProperty(ref _dataPage37, value); }

        private string _dataPage38;
        public string DataPage38 { get => _dataPage38; set => SetProperty(ref _dataPage38, value); }

        private string _dataPage43;
        public string DataPage43 { get => _dataPage43; set => SetProperty(ref _dataPage43, value); }

        private bool _IOButtonEnabled;
        /// <summary>
        /// Enabled property for IO commands
        /// </summary>
        public bool IOButtonEnabled { get => _IOButtonEnabled; set => SetProperty(ref _IOButtonEnabled, value); }

        private bool _IOInProgress;
        /// <summary>
        /// Enabled property for IO command in progress
        /// </summary>
        public bool IOInProgress { get => _IOInProgress; set => SetProperty(ref _IOInProgress, value); }

        private bool _IONoProgress = true;
        /// <summary>
        /// Enabled property for IO command not in progress
        /// </summary>
        public bool IONoProgress { get => _IONoProgress; set => SetProperty(ref _IONoProgress, value); }

        private BackgroundWorker Worker;

        public DeviceViewModel()
        {
            DisplayName = "IO";
            SelectionChangedCmd = new RelayCommand<object>(SelectionChanged);
            RefreshPortsCmd = new RelayCommand(RefreshPorts);
            ConnectCmd = new RelayCommand(Connect);
            ReadCmd = new RelayCommand(Read);
            CancelCmd = new RelayCommand(Cancel);
            RefreshPorts();

            Worker = new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
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
            Worker.RunWorkerAsync(NfcCommand.CheckDevice);

        }

        private void CheckDevice()
        {
            using (var arduino = new IO.Arduino.ArduinoNFC(((COMPortInfo)_collectionView.CurrentItem).Name, 9600, 800))
            {
                ConnectStatus = arduino.CheckDevice() ? "Arduino Verified" : "Unverified!";
            }

            if (ConnectStatus == "Arduino Verified")
            {
                NfcDevice = new IO.Arduino.ArduinoNFC(((COMPortInfo)_collectionView.CurrentItem).Name, 9600, 3000);
            }
            else
            {
                NfcDevice = null;
            }
        }

        private void IOButtonControl(bool Enable) => IOButtonEnabled = NfcDevice == null ? false : Enable;

        public void Read()
        {
            if (NfcDevice != null)
                Worker.RunWorkerAsync(NfcCommand.ReadNtag);
        }

        public void Cancel()
        {
            if (Worker.IsBusy)
                NfcDevice.CancelCommand();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            IOButtonControl(false);
            IOInProgress = !(IONoProgress = false);

            switch ((NfcCommand)e.Argument)
            {
                case NfcCommand.ReadNtag:
                    TagRead = NfcDevice.ReadNtag();
                    break;
                case NfcCommand.CheckDevice:
                    CheckDevice();
                    break;
                default:
                    break;
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IOButtonControl(true);
            IOInProgress = !(IONoProgress = true);
        }
    }
}
