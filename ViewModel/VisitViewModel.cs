using Diary_MVVM.Model;
using Diary_MVVM.View;
using Diary_MVVM.ViewModel.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Diary_MVVM.ViewModel
{
    internal class VisitViewModel : BindingHelpers
    {
        private string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\visits.json";

        #region свойства

        public BindableCommand Add { get; set; }
        public BindableCommand Delete { get; set; }
        public BindableCommand Apply { get; set; }

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

        private ObservableCollection<Visit> _content;
        public ObservableCollection<Visit> Content
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
            if (!File.Exists(fileName))
            {
                File.AppendAllText(fileName, "[]");
            }

            string json = File.ReadAllText(fileName);
            if (json != "[]")
            {
                var visit = JsonConvert.DeserializeObject<List<Visit>>(json);
                Content = new ObservableCollection<Visit>(visit);
            }
            else
            {
                Content = new ObservableCollection<Visit>();
            }

            OpenStud = new BindableCommand(_ => OpenWindow());
            OpenPayments = new BindableCommand(_ => OpenPay());

            Add = new BindableCommand(_ => AddPay());
            Delete = new BindableCommand(_ => DeletePay());
            Apply = new BindableCommand(_ => ApplySave());
        }

        private void AddPay()
        {
            try
            {
                if (Selected.FIO != null & Selected.Date != null)
                {
                    Content.Add(new Visit(Selected.FIO, Selected.Date));
                }
                else MessageBox.Show("Поля пустые!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления");
            }
        }

        private void DeletePay()
        {
            if (Selected != null)
            {
                Content.Remove(Selected);
                OnPropertyChanged();
            }
            else MessageBox.Show("Запись не выбрана");
        }

        private void ApplySave()
        {
            Json.Serialize(Content, fileName);
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
