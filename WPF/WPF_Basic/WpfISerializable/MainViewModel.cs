using Microsoft.Win32;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfISerializable
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private ObservableCollection<UserInfo> uers = new ObservableCollection<UserInfo>();
        public ObservableCollection<UserInfo> Users { get => uers; set => SetProperty(ref uers, value); }
        #endregion

        #region commands
        public DelegateCommand XmlSaveCommand { get; private set; }
        public DelegateCommand XmlLoadCommand { get; private set; }
        public DelegateCommand JsonSaveCommand { get; private set; }
        public DelegateCommand JsonLoadCommand { get; private set; }
        #endregion


        public MainViewModel()
        {
            XmlSaveCommand = new DelegateCommand(OnXmlSave, CanXmlSave);
            XmlLoadCommand = new DelegateCommand(OnXmlLoad, CanXmlLoad);

            JsonSaveCommand = new DelegateCommand(OnJsonSave, CanJsonSave);
            JsonLoadCommand = new DelegateCommand(OnJsonLoad, CanJsonLoad);
        }

        private void OnXmlSave()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.FileName = "users.xml";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = saveFileDialog.FileName;
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<UserInfo>));
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        serializer.Serialize(writer, Users);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private bool CanXmlSave()
        {
            return true;
        }

        private void OnXmlLoad()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<UserInfo>));
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        var data = (ObservableCollection<UserInfo>)serializer.Deserialize(reader);
                        Users = data;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private bool CanXmlLoad()
        {
            return true;
        }

        private void OnJsonSave()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.FileName = "users.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = saveFileDialog.FileName;
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    };

                    string jsonString = JsonSerializer.Serialize(Users, options);
                    File.WriteAllText(filePath, jsonString);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private bool CanJsonSave()
        {
            return true;
        }

        private void OnJsonLoad()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    string jsonString = File.ReadAllText(filePath);
                    var data = JsonSerializer.Deserialize<ObservableCollection<UserInfo>>(jsonString);
                    if (data != null)
                    {
                        Users = data;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private bool CanJsonLoad()
        {
            return true;
        }
    }
}
