using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSrCnn.Models;
using WpfSrCnn.Services;

namespace WpfSrCnn
{
    public class MainViewModel : BindableBase
    {
        private readonly AnomalyDetectionService _service;

        private ObservableCollection<CpuRecord> _cpuRecords = [];
        public ObservableCollection<CpuRecord> CpuRecords
        {
            get => _cpuRecords;
            set => SetProperty(ref _cpuRecords, value);
        }

        private string _statusMessage = "데이터를 생성하고 탐지를 실행하세요.";
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        private int _totalCount;
        public int TotalCount
        {
            get => _totalCount;
            set => SetProperty(ref _totalCount, value);
        }

        private int _anomalyCount;
        public int AnomalyCount
        {
            get => _anomalyCount;
            set => SetProperty(ref _anomalyCount, value);
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                SetProperty(ref _isRunning, value);
                RunDetectionCommand.RaiseCanExecuteChanged();
                ClearCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand RunDetectionCommand { get; }
        public DelegateCommand ClearCommand { get; }

        public MainViewModel()
        {
            _service = new AnomalyDetectionService();

            RunDetectionCommand = new DelegateCommand(
                executeMethod: async () => await RunDetectionAsync(),
                canExecuteMethod: () => !IsRunning
            );

            ClearCommand = new DelegateCommand(
                executeMethod: Clear,
                canExecuteMethod: () => !IsRunning
            );
        }

        private async Task RunDetectionAsync()
        {
            IsRunning = true;
            StatusMessage = "이상 탐지 실행 중...";

            try
            {
                var results = await Task.Run(() =>
                {
                    var sampleData = AnomalyDetectionService.GenerateSampleData();
                    return _service.Detect(sampleData);
                });

                CpuRecords = new ObservableCollection<CpuRecord>(results);
                TotalCount = results.Count;
                AnomalyCount = results.Count(r => r.IsAnomaly);
                StatusMessage = $"완료! 총 {TotalCount}개 중 {AnomalyCount}개 이상 탐지됨";
            }
            catch (Exception ex)
            {
                StatusMessage = $"오류 발생: {ex.Message}";
            }
            finally
            {
                IsRunning = false;
            }
        }

        private void Clear()
        {
            CpuRecords.Clear();
            TotalCount = 0;
            AnomalyCount = 0;
            StatusMessage = "초기화되었습니다.";
        }
    }
}

