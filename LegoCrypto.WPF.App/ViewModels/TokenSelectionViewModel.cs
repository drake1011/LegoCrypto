using LegoCrypto.Data;
using LegoCrypto.Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LegoCrypto.WPF.App.ViewModels
{
    public class TokenSelectionViewModel : ViewModelBase
    {
        private string _statusMessage = string.Empty;
        public string StatusMessage { get => _statusMessage; set => SetProperty(ref _statusMessage, value); }

        private TokenRepo _tokenRepo;
        public ObservableCollection<ViewModelBase> Workspaces { get; set; }
        //private ObservableCollection<ViewModelBase> _workspaces;
        //public ObservableCollection<ViewModelBase> Workspaces
        //{
        //    get
        //    {
        //        if (_workspaces == null)
        //        {
        //            _workspaces = new ObservableCollection<ViewModelBase>();
        //        }
        //        return _workspaces;
        //    }
        //    set
        //    {
        //        _workspaces = value;
        //    }
        //}

        public TokenSelectionViewModel(ObservableCollection<ViewModelBase> tokenWorkspaces)
        {
            DisplayName = "Tokens";
            //SelectionChangedCmd = new RelayCommand<object>(SelectedTabChanged);
            //_tokenRepo = tokenRepo;

            StatusMessage += " | ";
            //StatusMessage += $"{_tokenRepo.Characters?.Count} Characters";
            //StatusMessage += " | ";
            //StatusMessage += $"{_tokenRepo.Vehicles?.Count} Vehicles";

            //var characterVm = new CharactersViewModel(_tokenRepo);
            //var vehicleVm = new VehiclesViewModel(_tokenRepo);

            //Workspaces.Add(characterVm);
            //Workspaces.Add(vehicleVm);
            Workspaces = tokenWorkspaces;
        }

        public void SelectedTabChanged(object tabName)
        {
            //switch(tabName)
            //{
            //    case "Character":
            //        _trs.SelectedToken = (IToken)CollectionViewSource.GetDefaultView(Characters).CurrentItem;
            //        break;
            //    case "Vehicle":
            //        _trs.SelectedToken = (IToken)CollectionViewSource.GetDefaultView(Vehicles);
            //        break;
            //}
        }
    }
}
