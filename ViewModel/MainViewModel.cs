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
    internal class MainViewModel : BindingHelpers
    {
        private string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\students.json";
        private string groupsFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\groups.json";

        #region свойства

        public BindableCommand Add { get; set; }
        public BindableCommand AddGroup { get; set; }
        public BindableCommand Delete { get; set; }
        public BindableCommand Apply { get; set; }

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

        private ObservableCollection<Students> _content;
        public ObservableCollection<Students> Content
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
            if (!File.Exists(fileName))
            {
                File.AppendAllText(fileName, "[]");
            }

            string json = File.ReadAllText(fileName);
            if (json != "[]")
            {
                var student = JsonConvert.DeserializeObject<List<Students>>(json);
                Content = new ObservableCollection<Students>(student);
            }
            else
            {
                Content = new ObservableCollection<Students>();
            }
            Selected = new Students();

            json = File.ReadAllText(groupsFile);
            if (json != "[]")
            {
                var groups = JsonConvert.DeserializeObject<List<Groups>>(json);
                ComboItems = new ObservableCollection<string>();
                foreach (var item in groups)
                {
                    ComboItems.Add(item.GroupName);
                }
            }
            else ComboItems = new ObservableCollection<string>();

            OpenPayments = new BindableCommand(_ => OpenWindow());
            OpenVisit = new BindableCommand(_ => OpenVisits());

            Add = new BindableCommand(_ => AddPay());
            Delete = new BindableCommand(_ => DeletePay());
            Apply = new BindableCommand(_ => ApplySave());

            AddGroup = new BindableCommand(_ => OpenAddGroup());
        }

        private void AddPay()
        {
            try
            {
                if (Selected.FIO != null & Selected.Group != null & Selected.Price > 0)
                {
                    Content.Add(new Students(Selected.FIO, Selected.Group, Convert.ToInt32(Selected.Price)));
                }
                else MessageBox.Show("Поля пустые!");
            }
            catch
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
            var newWindow = new PaymentsWindow();
            newWindow.Show();
        }
        private void OpenVisits()
        {
            var newWindow = new VisitWindow();
            newWindow.Show();
        }
        private void OpenAddGroup()
        {
            var newWindow = new AddGroupsWindow();
            newWindow.Show();
        }
    }
}
