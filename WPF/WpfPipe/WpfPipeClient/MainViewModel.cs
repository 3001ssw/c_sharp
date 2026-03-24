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
        private CancellationTokenSource? cs = null;
        //public CancellationTokenSource? cs { get => cs; set => SetProperty(ref cs, value); }

        private NamedPipeClientStream? clientStream = null;
        public NamedPipeClientStream? ClientStream { get => clientStream; set => SetProperty(ref clientStream, value); }


        private string pipeName = "testpipe";
        public string PipeName { get => pipeName; set => SetProperty(ref pipeName, value); }


        private string sendMessage = "";
        public string SendMessage { get => sendMessage; set => SetProperty(ref sendMessage, value); }


        private ObservableCollection<PipeMessage> messages = new ObservableCollection<PipeMessage>();
        public ObservableCollection<PipeMessage> Messages { get => messages; set => SetProperty(ref messages, value); }

        private object lockMessages = new object();

        #endregion

        #region command methods

        public DelegateCommand PipeClientConnectCommand { get; private set; }
        public DelegateCommand PipeClientCloseCommand { get; private set; }
        public DelegateCommand SendMessageCommand { get; private set; }

        public MainViewModel()
        {
            PipeClientConnectCommand = new DelegateCommand(OnPipeClientConnect, CanPipeClientConnect).ObservesProperty(() => ClientStream);
            PipeClientCloseCommand = new DelegateCommand(OnPipeClientClose, CanPipeClientClose).ObservesProperty(() => ClientStream);
            SendMessageCommand = new DelegateCommand(OnSendMessage, CanSendMessage).ObservesProperty(() => ClientStream).ObservesProperty(() => SendMessage);

            BindingOperations.EnableCollectionSynchronization(Messages, lockMessages);
        }

        private void OnPipeClientConnect()
        {
            cs = new CancellationTokenSource();
            Task.Run(() => PipeClientStartTask(cs.Token));
        }

        private bool CanPipeClientConnect()
        {
            if (ClientStream != null)
                return false;

            return true;
        }

        private void OnPipeClientClose()
        {
            cs?.Cancel();
            cs = null;
        }

        private bool CanPipeClientClose()
        {
            if (ClientStream == null)
                return false;

            return true;
        }

        private void OnSendMessage()
        {
            StreamWriter writer = new StreamWriter(ClientStream, Encoding.UTF8)
            {
                AutoFlush = true,
            };
            writer.WriteLine(SendMessage);
            AddMessage($"write message: {SendMessage}");
            SendMessage = "";
        }

        private bool CanSendMessage()
        {
            if (cs == null || string.IsNullOrEmpty(SendMessage))
                return false;

            return true;
        }

        #endregion

        private void PipeClientStartTask(CancellationToken token)
        {
            AddMessage("Task Start");

            try
            {
                ClientStream = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

                ClientStream.ConnectAsync(token).Wait();

                AddMessage("Connect complete");

                StreamReader reader = new StreamReader(ClientStream, Encoding.UTF8);

                using (token.Register(() => ClientStream.Close()))
                {
                    while (!token.IsCancellationRequested)
                    {
                        string? msg = reader.ReadLine();
                        if (msg != null)
                        {
                            AddMessage($"read message: {msg}");

                            token.WaitHandle.WaitOne(100);
                        }
                        else
                        {
                            AddMessage("Server Close");
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                AddMessage($"Exception: {e.Message}");
            }
            finally
            {
                ClientStream?.Close();
                ClientStream = null;
            }

            AddMessage("Task Close");
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
