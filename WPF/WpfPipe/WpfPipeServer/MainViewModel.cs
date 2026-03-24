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
        private CancellationTokenSource? cs = null;
        public CancellationTokenSource? Cs { get => cs; set => SetProperty(ref cs, value); }

        private string pipeName = "testpipe";
        public string PipeName { get => pipeName; set => SetProperty(ref pipeName, value); }

        private ObservableCollection<PipeMessage> messages = new ObservableCollection<PipeMessage>();
        public ObservableCollection<PipeMessage> Messages { get => messages; set => SetProperty(ref messages, value); }

        private object lockMessages = new object();

        #endregion

        #region command methods

        public DelegateCommand PipeServerStartCommand { get; private set; }
        public DelegateCommand PipeServerCloseCommand { get; private set; }


        public MainViewModel()
        {
            PipeServerStartCommand = new DelegateCommand(OnPipeServerStart, CanPipeServerStart).ObservesProperty(() => Cs);
            PipeServerCloseCommand = new DelegateCommand(OnPipeServerClose, CanPipeServerClose).ObservesProperty(() => Cs);

            BindingOperations.EnableCollectionSynchronization(Messages, lockMessages);
        }

        private void OnPipeServerStart()
        {
            Cs = new CancellationTokenSource();
            Task.Run(() => PipeServerStartTask(Cs.Token));
        }

        private bool CanPipeServerStart()
        {
            if (Cs != null)
                return false;

            return true;
        }

        private void OnPipeServerClose()
        {
            Cs?.Cancel();
            Cs = null;
        }

        private bool CanPipeServerClose()
        {
            if (Cs == null)
                return false;

            return true;
        }

        #endregion


        private void AddMessage(string message)
        {
            Messages.Add(new PipeMessage()
            {
                Message = message,
            });
        }


        #region Task

        private void PipeServerStartTask(CancellationToken token)
        {
            AddMessage("Start Task");

            while (!token.IsCancellationRequested)
            {
                NamedPipeServerStream serverStream = new NamedPipeServerStream("testpipe",
                    PipeDirection.InOut,
                    NamedPipeServerStream.MaxAllowedServerInstances,
                    PipeTransmissionMode.Byte,
                    PipeOptions.Asynchronous);

                try
                {
                    serverStream.WaitForConnectionAsync(token).Wait();
                    AddMessage("Client Connect");

                    Task.Run(() => PipeServerRecvTask(serverStream, token));

                    token.WaitHandle.WaitOne(100);
                }
                catch (Exception e)
                {
                    AddMessage($"Exception: {e.Message}");
                }
                finally
                {
                    if (token.IsCancellationRequested)
                        serverStream?.Close();

                }
            }

            AddMessage("End Task");
        }

        private void PipeServerRecvTask(NamedPipeServerStream serverStream, CancellationToken token)
        {
            StreamReader reader = new StreamReader(serverStream, Encoding.UTF8);
            try
            {
                using (token.Register(() => serverStream.Close()))
                {
                    while (!token.IsCancellationRequested)
                    {
                        string? msg = reader.ReadLine();
                        if (msg != null)
                        {
                            AddMessage($"read message: {msg}");
                            StreamWriter writer = new StreamWriter(serverStream, Encoding.UTF8) { AutoFlush = true };

                            AddMessage($"write message: {msg}");
                            writer.WriteLine(msg);
                            token.WaitHandle.WaitOne(100);
                        }
                        else
                        {
                            AddMessage("Client Close");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddMessage($"Exception: {ex.Message}");
            }
            finally
            {
                serverStream.Close();
            }
        }

        #endregion
    }
}
