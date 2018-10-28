
namespace LegoCrypto.WPF.App
{
    public class TagDataViewModel : ViewModelBase
    {
        private EditMode _editMode;
        public EditMode EditMode { get=>_editMode; set { _editMode = value; ChangeEditMode(); } }

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
        }
    }
}
