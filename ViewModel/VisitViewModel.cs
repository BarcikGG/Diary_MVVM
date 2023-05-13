using Diary_MVVM.Model;
using Diary_MVVM.View;
using Diary_MVVM.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;

namespace Diary_MVVM.ViewModel
{
    internal class VisitViewModel : BindingHelpers
    {
        #region свойства
        public BindableCommand OpenStud { get; set; }
        public BindableCommand OpenPayments { get; set; }

        private Visit _selected;
        public Visit Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Object> _content;
        public ObservableCollection<Object> Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public VisitViewModel()
        {

            Content = new ObservableCollection<Object>()
            {
                new Visit("Bobrov Petr Sergeevich", DateTime.Today),
                new Visit("Abobov Sergay Alexandrovich", DateTime.Today.AddDays(-2)),
                new Visit("Eshe Odin Chelick", DateTime.Today)
            };

            OpenStud = new BindableCommand(_ => OpenWindow());
            OpenPayments = new BindableCommand(_ => OpenPay());
        }

        private void OpenWindow()
        {
            var newWindow = new MainWindow();
            newWindow.Show();
        }
        private void OpenPay()
        {
            var newWindow = new PaymentsWindow();
            newWindow.Show();
        }
    }
}
