using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using WpfApp2.Annotations;

namespace WpfApp2
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            var series = new ObservableCollection<ISeries>
            {
                new CandlesticksSeries<FinancialPoint>
                {
                    Values = new []
                    {
                        new FinancialPoint(DateTime.Now.AddHours(-10),(double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(-9), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(-8), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(-7), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(-6), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(-5), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(-4), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(-3), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(-2), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(-1), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now,              (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(1), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(2), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(3), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(4), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(5), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(6), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(7), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(8), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(9), (double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(10),(double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                        new FinancialPoint(DateTime.Now.AddHours(11),(double)_rnd.Next(100,120), (double)_rnd.Next(85,95), (double)_rnd.Next(95,100), (double)_rnd.Next(75,85)),                           
                    },
                    Name = "LTC",
                    AnimationsSpeed = TimeSpan.Zero,
                },
            };

            Series = series;
        }

        
        protected void DoWithDispatcher(Action action)
        {
            Application.Current?.Dispatcher.Invoke(action);
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public ObservableCollection<ISeries> Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged();
            }
        }
 
        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                IsVisible = false,
                //Labeler = value => new DateTime((long)value).ToString("yyyy-MM-dd HH:mm"),
                UnitWidth = TimeSpan.FromHours(1).Ticks,
                ShowSeparatorLines = false
            }
        };

        public Axis[] YAxes { get; set; } =
        {
            new Axis
            {
                //Labeler = value => new DateTime((long)value).ToString("yyyy-MM-dd HH:mm"),
                //UnitWidth = TimeSpan.FromHours(1).Ticks
                ShowSeparatorLines = false
            }
        };
        

        private ObservableCollection<ISeries> _series;
        private Random _rnd = new Random();
    }
}