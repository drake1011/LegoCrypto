using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegoCrypto.IO;

namespace LegoCrypto.WPF.App
{
    public class DeviceViewModel : ViewModelBase
    {
        private List<COMPortInfo> _comPorts;
        public List<COMPortInfo> ComPorts { get => _comPorts; set => SetProperty(ref _comPorts, value); }

        public string DisplayMember { get; set; } = "Description";

        public RelayCommand<object> SelectionChangedCmd { get; set; }

        public DeviceViewModel()
        {
            SelectionChangedCmd = new RelayCommand<object>(SelectionChanged);
            ComPorts = COMPortInfo.GetCOMPortsInfo();
        }

        public void SelectionChanged(object sender)
        {

        }
    }
}
