using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp2.Annotations;

namespace WpfApp2
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ViewModel> _itemsList;

        public MainViewModel()
        {
            BeginCycleCommand = new RelayCommand(() =>
            {
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                Task.Run(() => DoCycle(_cancellationTokenSource.Token));
            });

            StopCycleCommand = new RelayCommand(() => { _cancellationTokenSource?.Cancel(); });
        }

        private async Task DoCycle(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var list = new List<ViewModel>();
                    for (int i = 0; i < 10; i++)
                    {
                        list.Add(new ViewModel());
                    }

                    ItemsList = new ObservableCollection<ViewModel>(list);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
                await Task.Delay(500, token);    
            }
        }

        public ObservableCollection<ViewModel> ItemsList
        {
            get => _itemsList;
            set
            {
                _itemsList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public ICommand BeginCycleCommand { get; set; }
        public ICommand StopCycleCommand { get; set; }
        private CancellationTokenSource _cancellationTokenSource;
        private Random _rnd = new Random();

    }
}