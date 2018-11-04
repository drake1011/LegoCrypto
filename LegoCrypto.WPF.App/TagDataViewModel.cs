
using System.Windows.Media;

namespace LegoCrypto.WPF.App
{
    public class TagDataViewModel : ViewModelBase
    {
        private EditMode _editMode;
        public EditMode EditMode { get=>_editMode; set { _editMode = value; ChangeEditMode(); } }

        #region Readonly Properties
        private bool _uid_ReadOnly;
        public bool UID_ReadOnly { get => _uid_ReadOnly; set => SetProperty(ref _uid_ReadOnly, value); }

        private bool _tokenID_ReadOnly;
        public bool TokenID_ReadOnly { get => _tokenID_ReadOnly; set => SetProperty(ref _tokenID_ReadOnly, value); }

        private bool _dataPage35_ReadOnly;
        public bool DataPage35_ReadOnly { get => _dataPage35_ReadOnly; set => SetProperty(ref _dataPage35_ReadOnly, value); }

        private bool _dataPage36_ReadOnly;
        public bool DataPage36_ReadOnly { get => _dataPage36_ReadOnly; set => SetProperty(ref _dataPage36_ReadOnly, value); }

        private bool _dataPage37_ReadOnly;
        public bool DataPage37_ReadOnly { get => _dataPage37_ReadOnly; set => SetProperty(ref _dataPage37_ReadOnly, value); }

        private bool _dataPage38_ReadOnly;
        public bool DataPage38_ReadOnly { get => _dataPage38_ReadOnly; set => SetProperty(ref _dataPage38_ReadOnly, value); }

        private bool _dataPage43_ReadOnly;
        public bool DataPage43_ReadOnly { get => _dataPage43_ReadOnly; set => SetProperty(ref _dataPage43_ReadOnly, value); }
        #endregion

        #region Background Brush Properties
        private Brush _defaultbgBrush = new SolidColorBrush(Colors.White);
        public Brush DefaultBGBrush { get => _defaultbgBrush; set => SetProperty(ref _defaultbgBrush, value); }

        private Brush _readonlybgBrush = new SolidColorBrush(System.Windows.SystemColors.ControlDarkColor);
        public Brush ReadOnlyBGBrush { get => _readonlybgBrush; set => SetProperty(ref _readonlybgBrush, value); }

        private Brush _uid_bgBrush;
        public Brush UID_bgBrush { get => _uid_bgBrush; set => SetProperty(ref _uid_bgBrush, value); }

        private Brush _tokenID_bgBrush;
        public Brush TokenID_bgBrush { get => _tokenID_bgBrush; set => SetProperty(ref _tokenID_bgBrush, value); }

        private Brush _dataPage35_bgBrush;
        public Brush DataPage35_bgBrush { get => _dataPage35_bgBrush; set => SetProperty(ref _dataPage35_bgBrush, value); }

        private Brush _dataPage36_bgBrush;
        public Brush DataPage36_bgBrush { get => _dataPage36_bgBrush; set => SetProperty(ref _dataPage36_bgBrush, value); }

        private Brush _dataPage37_bgBrush;
        public Brush DataPage37_bgBrush { get => _dataPage37_bgBrush; set => SetProperty(ref _dataPage37_bgBrush, value); }

        private Brush _dataPage38_bgBrush;
        public Brush DataPage38_bgBrush { get => _dataPage38_bgBrush; set => SetProperty(ref _dataPage38_bgBrush, value); }

        private Brush _dataPage43_bgBrush;
        public Brush DataPage43_bgBrush { get => _dataPage43_bgBrush; set => SetProperty(ref _dataPage43_bgBrush, value); }
        #endregion

        public void ChangeEditMode()
        {
            switch(EditMode)
            {
                case EditMode.FullEdit:
                    SetFullEdit();
                    break;
                case EditMode.Encrypt:
                    SetEncrypt();
                    break;
                case EditMode.Decrypt:
                    SetDecrypt();
                    break;
                case EditMode.ReadOnly:
                    SetReadOnly();
                    break;
            }
        }
        
        private void SetFullEdit()
        {
            UID_ReadOnly = false;
            TokenID_ReadOnly = false;
            DataPage35_ReadOnly = false;
            DataPage36_ReadOnly = false;
            DataPage37_ReadOnly = false;
            DataPage38_ReadOnly = false;
            DataPage43_ReadOnly = false;

            UID_bgBrush = DefaultBGBrush;
            TokenID_bgBrush = DefaultBGBrush;
            DataPage35_bgBrush = DefaultBGBrush;
            DataPage36_bgBrush = DefaultBGBrush;
            DataPage37_bgBrush = DefaultBGBrush;
            DataPage38_bgBrush = DefaultBGBrush;
            DataPage43_bgBrush = DefaultBGBrush;
        }

        private void SetReadOnly()
        {
            UID_ReadOnly = true;
            TokenID_ReadOnly = true;
            DataPage35_ReadOnly = true;
            DataPage36_ReadOnly = true;
            DataPage37_ReadOnly = true;
            DataPage38_ReadOnly = true;
            DataPage43_ReadOnly = true;

            UID_bgBrush = ReadOnlyBGBrush;
            TokenID_bgBrush = ReadOnlyBGBrush;
            DataPage35_bgBrush = ReadOnlyBGBrush;
            DataPage36_bgBrush = ReadOnlyBGBrush;
            DataPage37_bgBrush = ReadOnlyBGBrush;
            DataPage38_bgBrush = ReadOnlyBGBrush;
            DataPage43_bgBrush = ReadOnlyBGBrush;
        }

        private void SetEncrypt()
        {
            UID_ReadOnly = false;
            TokenID_ReadOnly = false;
            DataPage35_ReadOnly = true;
            DataPage36_ReadOnly = true;
            DataPage37_ReadOnly = true;
            DataPage38_ReadOnly = true;
            DataPage43_ReadOnly = true;

            UID_bgBrush = DefaultBGBrush;
            TokenID_bgBrush = DefaultBGBrush;
            DataPage35_bgBrush = ReadOnlyBGBrush;
            DataPage36_bgBrush = ReadOnlyBGBrush;
            DataPage37_bgBrush = ReadOnlyBGBrush;
            DataPage38_bgBrush = ReadOnlyBGBrush;
            DataPage43_bgBrush = ReadOnlyBGBrush;
        }

        private void SetDecrypt()
        {
            UID_ReadOnly = false;
            TokenID_ReadOnly = true;
            DataPage35_ReadOnly = false;
            DataPage36_ReadOnly = false;
            DataPage37_ReadOnly = false;
            DataPage38_ReadOnly = false;
            DataPage43_ReadOnly = true;

            UID_bgBrush = DefaultBGBrush;
            TokenID_bgBrush = ReadOnlyBGBrush;
            DataPage35_bgBrush = DefaultBGBrush;
            DataPage36_bgBrush = DefaultBGBrush;
            DataPage37_bgBrush = DefaultBGBrush;
            DataPage38_bgBrush = DefaultBGBrush;
            DataPage43_bgBrush = ReadOnlyBGBrush;
        }
    }
}
