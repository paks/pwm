using Microsoft.IoT.Lightning.Providers;
using System;
using System.ComponentModel;
using Windows.Devices.Pwm;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PwmTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private BackgroundWorker _worker;
        private CoreDispatcher _dispatcher;
        private PwmPin _pwm0Pin;
        private PwmPin _pwm1Pin;

        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;

            Unloaded += MainPage_Unloaded;
        }

        private void slider_ValueChanged0(object sender, RangeBaseValueChangedEventArgs e)
        {
            var percent = percentSlider0.Value;
            percentageTextBlock0.Text = percent.ToString();
            if (LightningProvider.IsLightningEnabled)
            {
                _pwm0Pin.SetActiveDutyCyclePercentage(percent / 100.0);
            }
        }

        private void slider_ValueChanged1(object sender, RangeBaseValueChangedEventArgs e)
        {
            var percent = percentSlider1.Value;
            percentageTextBlock1.Text = percent.ToString();
            if (LightningProvider.IsLightningEnabled)
            {
                _pwm1Pin.SetActiveDutyCyclePercentage(percent / 100.0);
            }
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

            _worker = new BackgroundWorker();
            _worker.DoWork += SetupPWM;
            _worker.RunWorkerAsync();
        }

        private void MainPage_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _pwm0Pin.Stop();
            _pwm1Pin.Stop();
        }

        private async void SetupPWM(object sender, DoWorkEventArgs e)
        {
            if (LightningProvider.IsLightningEnabled)
            {
                // PWM Pins http://raspberrypi.stackexchange.com/questions/40812/raspberry-pi-2-b-gpio-pwm-and-interrupt-pins
                var pwmControllers = await PwmController.GetControllersAsync(LightningPwmProvider.GetPwmProvider());
                var pwmController = pwmControllers[1]; // use the on-device controller
                pwmController.SetDesiredFrequency(50); // try to match 50Hz
                _pwm0Pin = pwmController.OpenPin(18);
                _pwm0Pin.SetActiveDutyCyclePercentage(.0);
                _pwm0Pin.Start();

                _pwm1Pin = pwmController.OpenPin(13);
                _pwm1Pin.SetActiveDutyCyclePercentage(.0);
                _pwm1Pin.Start();
            }

            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                percentSlider0.IsEnabled = true;
                percentSlider1.IsEnabled = true;
            });

        }
    }
}
