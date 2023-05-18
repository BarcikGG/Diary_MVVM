using Diary_MVVM.ViewModel;
using System.Windows;

namespace Diary_MVVM
{
    public partial class AddGroupsWindow : Window
    {
        public AddGroupsWindow()
        {
            InitializeComponent();
            DataContext = new AddGroupsViewModel();
        }
    }
}
