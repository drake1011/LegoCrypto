using LegoCrypto.Data.Model;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;

namespace LegoCrypto.WPF.App
{
    public class CryptoViewModel : ViewModelBase
    {
        private ITagData _tagData;
        public ITagData TagData { get => _tagData;
            set {
                SetProperty(ref _tagData, value);
                UID = TagData.UID;
                TokenID = TagData.ID;
                DataPage35 = TagData.Pages[DataRegister.Page35];
                DataPage36 = TagData.Pages[DataRegister.Page36];
                DataPage37 = TagData.Pages[DataRegister.Page37];
                DataPage38 = TagData.Pages[DataRegister.Page38];
                DataPage43 = TagData.Pages[DataRegister.Page43];
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

        private EditMode _editModeProp = EditMode.FullEdit;
        public EditMode EditModeProp { get => _editModeProp; set => SetProperty(ref _editModeProp, value); }

        private bool _encrypt = true;
        public bool Encrypt { get => _encrypt; set => SetProperty(ref _encrypt, value); }

        public RelayCommand RadioChangeCmd { get; set; }
        public RelayCommand CalculateCmd { get; set; }

        public CryptoViewModel()
        {
            RadioChangeCmd = new RelayCommand(HandleRadioChange);
            CalculateCmd = new RelayCommand(Calculate);
            HandleRadioChange();
        }

        public void HandleRadioChange()
        {
            EditModeProp = Encrypt ? EditMode.Encrypt : EditMode.Decrypt;
            TagData = TagFactory.CreateTag();
        }

        public async void Calculate()
        {
            try
            {
                ITag tag;
                if (Encrypt)
                {
                    tag = TagFactory.CreateTag(TokenID ?? 0, UID);
                    tag.Encrypt();
                }
                else
                {
                    tag = TagFactory.CreateTag(UID, DataPage35, DataPage36, DataPage37, DataPage38);
                    tag.Decrypt();
                }

                TagData = tag;
            }
            catch (Exception ex)
            {
                await ((MahApps.Metro.Controls.MetroWindow)Application.Current.MainWindow).ShowMessageAsync("Error", ex.Message);
            }
        }
    }
}
