using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfPipeClient
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private Thread? threadPipeClient = null;
        private CancellationTokenSource? csThreadPipeClient = null;
        private NamedPipeClientStream? clientStream = null;


        private string pipeName = "testpipe";
        public string PipeName { get => pipeName; set => SetProperty(ref pipeName, value); }

        private string sendMessage = "";
        public string SendMessage { get => sendMessage; set => SetProperty(ref sendMessage, value); }

        private ObservableCollection<PipeMessage> messages = new ObservableCollection<PipeMessage>();
        public ObservableCollection<PipeMessage> Messages { get => messages; set => SetProperty(ref messages, value); }

        private object lockMessages = new object();

        #endregion

        #region command methods

        public DelegateCommand NamedPipeClientConnectCommand { get; private set; }
        public DelegateCommand NamedPipeClientCloseCommand { get; private set; }
        public DelegateCommand SendMessageCommand { get; private set; }

        private void OnNamedPipeClientConnect()
        {
            csThreadPipeClient = new CancellationTokenSource();
            threadPipeClient = new Thread(() => ThreadNamedPipeClient(csThreadPipeClient.Token));
            threadPipeClient.IsBackground = true;
            threadPipeClient.Start();
        }

        private bool CanNamedPipeClientConnect()
        {
            return true;
        }

        private void OnNamedPipeClientClose()
        {
            csThreadPipeClient?.Cancel();
            clientStream?.Close();
            threadPipeClient?.Join();
            threadPipeClient = null;
            csThreadPipeClient = null;
        }

        private bool CanNamedPipeClientClose()
        {
            return true;
        }

        private void OnSendMessage()
        {
            StreamWriter writer = new StreamWriter(clientStream, Encoding.UTF8)
            {
                AutoFlush = true,
            };
            writer.WriteLine(SendMessage);
            AddMessage($"Send: {SendMessage}");
            SendMessage = "";
        }

        private bool CanSendMessage()
        {
            return true;
        }

        #endregion

        public MainViewModel()
        {
            NamedPipeClientConnectCommand = new DelegateCommand(OnNamedPipeClientConnect, CanNamedPipeClientConnect);
            NamedPipeClientCloseCommand = new DelegateCommand(OnNamedPipeClientClose, CanNamedPipeClientClose);
            SendMessageCommand = new DelegateCommand(OnSendMessage, CanSendMessage);

            BindingOperations.EnableCollectionSynchronization(Messages, lockMessages);
        }

        private void ThreadNamedPipeClient(CancellationToken token)
        {
            AddMessage("Thread Start");

            clientStream = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

            clientStream.Connect();

            AddMessage("Connect complete");

            Thread threadRecv = new Thread(() => ThreadNamedPipeClientRecv(token))
            {
                IsBackground = true,
            };
            threadRecv.Start();
            threadRecv.Join();

            AddMessage("");

            clientStream.Close();
            clientStream = null;

            AddMessage("Thread Close");
        }

        private void ThreadNamedPipeClientRecv(CancellationToken token)
        {
            StreamReader reader = new StreamReader(clientStream, Encoding.UTF8, true, 1024, leaveOpen: true);

            while (!token.IsCancellationRequested)
            {
                string? msg = reader.ReadLine();
                if (msg != null)
                {
                    AddMessage(msg);
                    token.WaitHandle.WaitOne(100);
                }
                else
                {
                    AddMessage("msg is null");
                    break;
                }
            }
        }

        private void AddMessage(string message)
        {
            Messages.Add(new PipeMessage()
            {
                Message = message,
            });
        }

    }
}
