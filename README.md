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

The PwmTest project has the code to setup two GPIOs (13,18) for PWM output. When the application runs, it displays 2 sliders to control the GPIO output. 

Here is a table with the GPIO pins that can be used for PWM:
| GPIO | PWM channel | Harware supported                      |
|:----:|:-----------:|---------------------------------------:|
|   12 |      0      | A+/B+/Pi2/Zero and compute module only |
|   13 |      1      | A+/B+/Pi2/Zero and compute module only |
|   18 |      0      | All models                             |
|   19 |      1      | A+/B+/Pi2/Zero and compute module only |
|      |             |                                        |
|   40 |      0      | Compute module only                    |
|   41 |      1      | Compute module only                    |
|   45 |      1      | Compute module only                    |
|   52 |      0      | Compute module only                    |
|   53 |      1      | Compute module only                    |


The TestL298NMotorController has a screen with sliders and buttons to test each of the motors attached to a [L298N](https://www.amazon.com/DROK-Controller-H-Bridge-Mega2560-Duemilanove/dp/B00CAG6GX2) Motor Drive Controller. This test includes using PWM outputs to control the motor speed.
