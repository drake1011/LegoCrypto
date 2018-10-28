using LegoCrypto.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LegoCrypto.WPF.App
{
    /// <summary>
    /// Interaction logic for TagDataView.xaml
    /// </summary>
    public partial class TagDataView : UserControl
    {
        private TagDataViewModel _vm;
        public TagDataView()
        {
            InitializeComponent();
            _vm = root.DataContext as TagDataViewModel;
        }

        public string UID { get => (string)GetValue(UIDProperty); set { SetValue(UIDProperty, value); } }

        public static readonly DependencyProperty UIDProperty =
            DependencyProperty.Register("UID",
                typeof(string), typeof(TagDataView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public uint? TokenID { get => (uint?)GetValue(TokenIDProperty); set => SetValue(TokenIDProperty, value); }

        public static readonly DependencyProperty TokenIDProperty =
            DependencyProperty.Register("TokenID",
                typeof(uint?), typeof(TagDataView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string DataPage35 { get => (string)GetValue(DataPage35Property); set => SetValue(DataPage35Property, value); }

        public static readonly DependencyProperty DataPage35Property =
            DependencyProperty.Register("DataPage35",
                typeof(string), typeof(TagDataView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string DataPage36 { get => (string)GetValue(DataPage36Property); set => SetValue(DataPage36Property, value); }

        public static readonly DependencyProperty DataPage36Property =
            DependencyProperty.Register("DataPage36",
                typeof(string), typeof(TagDataView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string DataPage37 { get => (string)GetValue(DataPage37Property); set => SetValue(DataPage37Property, value); }

        public static readonly DependencyProperty DataPage37Property =
            DependencyProperty.Register("DataPage37",
                typeof(string), typeof(TagDataView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string DataPage38 { get => (string)GetValue(DataPage38Property); set => SetValue(DataPage38Property, value); }

        public static readonly DependencyProperty DataPage38Property =
            DependencyProperty.Register("DataPage38",
                typeof(string), typeof(TagDataView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string DataPage43 { get => (string)GetValue(DataPage43Property); set => SetValue(DataPage43Property, value); }

        public static readonly DependencyProperty DataPage43Property =
            DependencyProperty.Register("DataPage43",
                typeof(string), typeof(TagDataView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ITagData TagData
        {
            get => (ITagData)GetValue(TagDataProperty);
            set { SetValue(TagDataProperty, value);
                UID = TagData.UID;
                TokenID = TagData.ID;
                DataPage35 = TagData.Pages[DataRegister.Page35];
                DataPage36 = TagData.Pages[DataRegister.Page36];
                DataPage37 = TagData.Pages[DataRegister.Page37];
                DataPage38 = TagData.Pages[DataRegister.Page38];
                DataPage43 = TagData.Pages[DataRegister.Page43];
            }
        }

        public static readonly DependencyProperty TagDataProperty =
            DependencyProperty.Register("TagData",
                typeof(ITagData), typeof(TagDataView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public EditMode EditMode { get => (EditMode)GetValue(EditModeProperty); set => SetValue(EditModeProperty, value); }

        public static readonly DependencyProperty EditModeProperty = 
            DependencyProperty.Register("EditMode",
                typeof(EditMode), typeof(TagDataView),
                new PropertyMetadata(EditMode.FullEdit, (d, e) => ((TagDataView)d)._vm.EditMode = (EditMode)e.NewValue));
    }

    public enum EditMode
    {
        FullEdit,
        Encrypt,
        Decrypt,
        ReadOnly
    }
}
