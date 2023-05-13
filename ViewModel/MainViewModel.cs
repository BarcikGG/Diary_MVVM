using Diary_MVVM.Model;
using Diary_MVVM.View;
using Diary_MVVM.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Diary_MVVM.ViewModel
{
    internal class MainViewModel : BindingHelpers
    {

        #region свойства
        public BindableCommand OpenVisit { get; set; }
        public BindableCommand OpenPayments { get; set; }

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

        private Students _selected;
        public Students Selected
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

        public MainViewModel()
        {
            ComboItems = new ObservableCollection<string>()
            {
                "P50-4-21",
                "Четверг 20:00",
                "ПН 19:30"
            };

            Content = new ObservableCollection<Object>()
            {
                new Students("Bobrov Petr Sergeevich", "P50-4-21", 250),
                new Students("Abobov Sergay Alexandrovich", "Четверг 20:00", 450),
                new Students("Eshe Odin Chelick", "ПН 19:30", 300)
            };

            OpenPayments = new BindableCommand(_ => OpenWindow());
            OpenVisit = new BindableCommand(_ => OpenVisits());
        }

        private void OpenWindow()
        {
            var newWindow = new PaymentsWindow();
            newWindow.Show();
        }
        private void OpenVisits()
        {
            var newWindow = new VisitWindow();
            newWindow.Show();
        }
    }
}
