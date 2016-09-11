# Windows 10 IoT PWM Controller Test Application for the Raspberry PI 2/3

This repository contains test projects I used to learn how to setup and control the PWM controller in the
Raspberry PI 2/3

## Here are the articles I used to create the PwmTest project:

* [Lightning Driver Setup Guide](https://developer.microsoft.com/en-us/windows/iot/docs/LightningSetup.htm)
* [How to Fade an LED with PWM in Windows IoT](http://www.codeproject.com/Articles/1095762/How-to-Fade-an-LED-with-PWM-in-Windows-IoT)
* [A servo library in C# for Raspberry Pi 3 - Part #1, implementing PWM](https://jeremylindsayni.wordpress.com/2016/05/08/a-servo-library-in-c-for-raspberry-pi-3-part-1-implementing-pwm/)
* Wikipedia entry for [PWM](https://en.wikipedia.org/wiki/Pulse-width_modulation)

## Prerequisites

* Raspberry PI 2/3 with the latest [Windows 10 IoT Core Insider Preview](https://developer.microsoft.com/en-US/windows/iot/GetStarted)
* Raspberry PI 2/3 setup with the [Direct Memory Mapper Dirver](https://developer.microsoft.com/en-us/windows/iot/docs/LightningSetup.htm)
* Visual Studio 2015 Comunitiy Edition or better
* Windows 10 SDK 10.0.10586.0
* Microsoft.Iot.Lighting (From Nuget)


## Notes

The PwmTest project has the code to setup two GPIOs (13,18) for PWM output. When the application runs, it displays 2 sliders to control the GPIO out put. 
