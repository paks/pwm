using Microsoft.IoT.Lightning.Providers;
using Windows.Devices.Gpio;
using Windows.Devices.Pwm;
using Windows.Devices;

namespace TestL298NMotorController
{
    public class Motor
    {
        private readonly GpioPin _motorGpioPinA;
        private readonly GpioPin _motorGpioPinB;
        private readonly PwmPin _motorPwmPin;

        public Motor(PwmController pwmController, int pwmPin, int gpioPinIn1, int gpioPinIn2) : this(gpioPinIn1, gpioPinIn2)
        {
            if (LightningProvider.IsLightningEnabled)
            {
                _motorPwmPin = pwmController.OpenPin(pwmPin);
                _motorPwmPin.SetActiveDutyCyclePercentage(.0);
                _motorPwmPin.Start();
            }
        }

        public Motor(int gpioPinIn1, int gpioPinIn2)
        {
            var gpio = GpioController.GetDefault();

            _motorGpioPinA = gpio.OpenPin(gpioPinIn1);
            _motorGpioPinB = gpio.OpenPin(gpioPinIn2);
            _motorGpioPinA.Write(GpioPinValue.Low);
            _motorGpioPinB.Write(GpioPinValue.Low);
            _motorGpioPinA.SetDriveMode(GpioPinDriveMode.Output);
            _motorGpioPinB.SetDriveMode(GpioPinDriveMode.Output);
        }

        public void MoveForward()
        {
            _motorGpioPinA.Write(GpioPinValue.Low);
            _motorGpioPinB.Write(GpioPinValue.High);
        }

        public void MoveBackward()
        {
            _motorGpioPinA.Write(GpioPinValue.High);
            _motorGpioPinB.Write(GpioPinValue.Low);
        }

        public void Stop()
        {
            _motorGpioPinA.Write(GpioPinValue.Low);
            _motorGpioPinB.Write(GpioPinValue.Low);
            this.SpeedPercentage(0.0);
        }

        public void SpeedPercentage(double percent)
        {
            if(_motorPwmPin != null)
            {
                _motorPwmPin.SetActiveDutyCyclePercentage(percent);
            }
        }
    }
}
