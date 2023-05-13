using Diary_MVVM.Model;
using Diary_MVVM.View;
using Diary_MVVM.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary_MVVM.ViewModel
{
    internal class PaymentsViewModel : BindingHelpers
    {
        private string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\mvvm.json";

        #region свойства
        public BindableCommand OpenStud { get; set; }
        public BindableCommand OpenVisit { get; set; }
        public BindableCommand Add { get; set; }

        private string _fioText;
        public string FIOText
        {
            get { return _fioText; }
            set
            {
                _fioText = value;
                OnPropertyChanged();
            }
        }

        private string _statusText;
        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged();
            }
        }

        private string _lessonsText;
        public string LessonsText
        {
            get { return _lessonsText; }
            set
            {
                _lessonsText = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _comboItems;
        public ObservableCollection<string> ComboItems
        {
            get { return _comboItems; }
            set
            {
                _comboItems = value;
                OnPropertyChanged();
            }
        }

        private Payments _selected;
        public Payments Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();

                if (Selected != null)
                {
                    FIOText = Selected.FIO;
                    StatusText = Selected.Status;
                    LessonsText = Selected.Lessons.ToString();
                }
                else
                {
                    FIOText = string.Empty;
                    StatusText = string.Empty;
                    LessonsText = string.Empty;
                }
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

        public PaymentsViewModel()
        {
            ComboItems = new ObservableCollection<string>()
            {
                "Оплачено",
                "Ожидает оплаты",
                "Отменено"
            };

            Content = new ObservableCollection<Object>()
            {
                new Payments("Bobrov Petr Sergeevich", "Оплачено", 6),
                new Payments("Abobov Sergay Alexandrovich", "Ожидает оплаты", 4),
                new Payments("Eshe Odin Chelick", "Ожидает оплаты", 4)
            };

            OpenStud = new BindableCommand(_ => OpenWindow());

            OpenVisit = new BindableCommand(_ => OpenVisits());

            Add = new BindableCommand(_ => AddPay());
        }

        private void AddPay()
        {
            Content.Add(new Payments(FIOText, StatusText, Convert.ToInt32(LessonsText)));
        }

        private void OpenWindow()
        {
            var newWindow = new MainWindow();
            newWindow.Show();
        }
        private void OpenVisits()
        {
            var newWindow = new VisitWindow();
            newWindow.Show();
        }
    }
}
