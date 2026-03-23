using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WpfPipeClient;

namespace WpfPipeServer
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private Thread? threadPipeServer = null;
        private CancellationTokenSource? csThreadPipeServer = null;
        private NamedPipeServerStream? serverStream = null;

        private string pipeName = "testpipe";
        public string PipeName { get => pipeName; set => SetProperty(ref pipeName, value); }

        private ObservableCollection<PipeMessage> messages = new ObservableCollection<PipeMessage>();
        public ObservableCollection<PipeMessage> Messages { get => messages; set => SetProperty(ref messages, value); }

        private object lockMessages = new object();

        #endregion

        #region command methods

        public DelegateCommand NamedPipeServerCreateCommand { get; private set; }
        public DelegateCommand NamedPipeServerCloseCommand { get; private set; }

        private void OnNamedPipeServerCreate()
        {
            csThreadPipeServer = new CancellationTokenSource();
            threadPipeServer = new Thread(() => ThreadServerCreate(csThreadPipeServer.Token))
            {
                IsBackground = true,
            };
            threadPipeServer?.Start();
        }

        private bool CanNamedPipeServerCreate()
        {
            return true;
        }

        private void OnNamedPipeServerClose()
        {
            csThreadPipeServer?.Cancel();
            serverStream?.Close();
            threadPipeServer?.Join();
            threadPipeServer = null;
            csThreadPipeServer = null;
        }

        private bool CanNamedPipeServerClose()
        {
            return true;
        }

        #endregion

        public MainViewModel()
        {
            NamedPipeServerCreateCommand = new DelegateCommand(OnNamedPipeServerCreate, CanNamedPipeServerCreate);
            NamedPipeServerCloseCommand = new DelegateCommand(OnNamedPipeServerClose, CanNamedPipeServerClose);

            BindingOperations.EnableCollectionSynchronization(Messages, lockMessages);
        }

        #region Thread
        private void ThreadServerCreate(CancellationToken token)
        {
            AddMessage("Thread Start");
            serverStream = new NamedPipeServerStream("testpipe",
                PipeDirection.InOut,
                NamedPipeServerStream.MaxAllowedServerInstances,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous);

            serverStream.WaitForConnection();
            AddMessage("Client Connect");

            Thread threadRecv = new Thread(() => ThreadPipeServerRecv(serverStream, token))
            {
                IsBackground = true,
            };
            threadRecv.Start();
            threadRecv.Join();

            serverStream?.Close();
            serverStream = null;
            AddMessage("Close Thread");
        }

        private void ThreadPipeServerRecv(NamedPipeServerStream serverStream, CancellationToken token)
        {
            StreamReader reader = new StreamReader(serverStream, Encoding.UTF8, true, 1024, leaveOpen: true);
            try
            {
                while (!token.IsCancellationRequested)
                {
                    string? msg = reader.ReadLine();
                    if (msg != null)
                    {
                        AddMessage(msg);
                        StreamWriter writer = new StreamWriter(serverStream) { AutoFlush = true };
                        writer.WriteLine(msg);
                        token.WaitHandle.WaitOne(100);
                    }
                    else
                    {
                        AddMessage("msg is null");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                AddMessage($"Exception: {ex.Message}");
            }
        }
        #endregion

        private void AddMessage(string message)
        {
            Messages.Add(new PipeMessage()
            {
                Message = message,
            });
        }

    }
}
