using Diary_MVVM.ViewModel.Helpers;
using Diary_MVVM.Model;
using Diary_MVVM.View;
using System;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Diary_MVVM.ViewModel
{
    internal class AddGroupsViewModel : BindingHelpers
    {
        private string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\groups.json";

        #region свойства

        public BindableCommand Add { get; set; }
        public BindableCommand Delete { get; set; }
        public BindableCommand Apply { get; set; }

        private Groups _selected;
        public Groups Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Groups> _content;
        public ObservableCollection<Groups> Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public AddGroupsViewModel()
        {
            if (!File.Exists(fileName))
            {
                File.AppendAllText(fileName, "[]");
            }

            string json = File.ReadAllText(fileName);
            if (json != "[]")
            {
                var groups = JsonConvert.DeserializeObject<List<Groups>>(json);
                Content = new ObservableCollection<Groups>(groups);
            }
            else
            {
                Content = new ObservableCollection<Groups>();
            }
            Selected = new Groups();

            Add = new BindableCommand(_ => AddPay());
            Delete = new BindableCommand(_ => DeletePay());
            Apply = new BindableCommand(_ => ApplySave());
        }

        private void AddPay()
        {
            try
            {
                if (Selected.GroupName != null)
                {
                    Content.Add(new Groups(Selected.GroupName));
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
    }
}
