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
    internal class PaymentsViewModel : BindingHelpers
    {
        private string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\payments.json";

        #region свойства
        public BindableCommand OpenStud { get; set; }
        public BindableCommand OpenVisit { get; set; }
        public BindableCommand Add { get; set; }
        public BindableCommand Delete { get; set; }
        public BindableCommand Apply { get; set; }

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

        private Payments _selected = new Payments();
        public Payments Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Payments> _content;
        public ObservableCollection<Payments> Content
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
            if (!File.Exists(fileName))
            {
                File.AppendAllText(fileName, "[]");
            }

            ComboItems = new ObservableCollection<string>()
            {
                "Оплачено",
                "Ожидает оплаты",
                "Отменено"
            };

            string json = File.ReadAllText(fileName);
            if (json != "[]")
            {
                var pay = JsonConvert.DeserializeObject<List<Payments>>(json);
                Content = new ObservableCollection<Payments>(pay);
            }
            else 
            {
                Content = new ObservableCollection<Payments>();
            }

            OpenStud = new BindableCommand(_ => OpenWindow());
            OpenVisit = new BindableCommand(_ => OpenVisits());
            Add = new BindableCommand(_ => AddPay());
            Delete = new BindableCommand(_ => DeletePay());
            Apply = new BindableCommand(_ => ApplySave());
        }

        private void AddPay()
        {
            try
            {
                if (Selected.FIO != null & Selected.Status != null & Selected.Lessons > 0)
                {
                    Content.Add(new Payments(Selected.FIO, Selected.Status, Convert.ToInt32(Selected.Lessons)));
                }
                else MessageBox.Show("Поля пустые!");
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Ошибка добавления" + ex.ToString()); 
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
        private void OpenVisits()
        {
            var newWindow = new VisitWindow();
            newWindow.Show();
        }
    }
}
