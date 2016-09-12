using Microsoft.IoT.Lightning.Providers;
using System;
using System.ComponentModel;
using Windows.Devices;
using Windows.Devices.Pwm;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestL298NMotorController
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private BackgroundWorker _worker;
        private CoreDispatcher _dispatcher;
        private Motor _motor0;
        private Motor _motor1;

        public MainPage()
        {
            this.InitializeComponent();
            if (LightningProvider.IsLightningEnabled) // no need to change the GPIO code
            {
                LowLevelDevicesController.DefaultProvider = LightningProvider.GetAggregateProvider();
            }

            Loaded += MainPage_Loaded;

            Unloaded += MainPage_Unloaded;
        }

        private void slider_ValueChanged0(object sender, RangeBaseValueChangedEventArgs e)
        {
            var percent = percentSlider0.Value;
            percentageTextBlock0.Text = percent.ToString();
            if (LightningProvider.IsLightningEnabled)
            {
                _motor0.SpeedPercentage(percent / 100.0);
            }
        }

        private void slider_ValueChanged1(object sender, RangeBaseValueChangedEventArgs e)
        {
            var percent = percentSlider1.Value;
            percentageTextBlock1.Text = percent.ToString();
            if (LightningProvider.IsLightningEnabled)
            {
                _motor1.SpeedPercentage(percent / 100.0);
            }
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

            _worker = new BackgroundWorker();
            _worker.DoWork += SetupMotors;
            _worker.RunWorkerAsync();
        }

        private void MainPage_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _motor0.Stop();
            _motor1.Stop();
        }

        private async void SetupMotors(object sender, DoWorkEventArgs e)
        {
            if (LightningProvider.IsLightningEnabled)
            {
                // PWM Pins http://raspberrypi.stackexchange.com/questions/40812/raspberry-pi-2-b-gpio-pwm-and-interrupt-pins
                var pwmControllers = await PwmController.GetControllersAsync(LightningPwmProvider.GetPwmProvider());
                var pwmController = pwmControllers[1]; // use the on-device controller
                pwmController.SetDesiredFrequency(50); // try to match 50Hz
                _motor0 = new Motor(pwmController, 18, 27, 22);
                _motor1 = new Motor(pwmController, 13, 5, 6);
            }
            else
            {
                _motor0 = new Motor(27, 22);
                _motor1 = new Motor( 5,  6);
            }

            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (LightningProvider.IsLightningEnabled) // we can only control speed if the Lightning driver is enabled
                {
                    percentSlider0.IsEnabled = true;
                    percentSlider1.IsEnabled = true;
                }
                var startSpeed = 50.0;
                percentSlider0.Value = startSpeed;
                percentSlider1.Value = startSpeed;
                _motor0.MoveForward();
                _motor1.MoveForward();
            });

        }

        private void forwardButton0_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _motor0.MoveForward();
            _motor0.SpeedPercentage(percentSlider0.Value / 100.0);
        }
        private void forwardButton1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _motor1.MoveForward();
            _motor1.SpeedPercentage(percentSlider1.Value / 100.0);
        }

        private void reverseButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(sender == reverseButton0)
            {
                _motor0.MoveBackward();
                _motor0.SpeedPercentage(percentSlider0.Value / 100.0);
            }
            else if(sender == reverseButton1)
            {
                _motor1.MoveBackward();
                _motor1.SpeedPercentage(percentSlider1.Value / 100.0);
            }
        }

        private void stopButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(sender == stopButton0)
            {
                _motor0.Stop();
            }
            else if(sender == stopButton1)
            {
                _motor1.Stop();
            }
        }
    }
}
