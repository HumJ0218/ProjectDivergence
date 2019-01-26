基于 System.Device.Gpio 库编写的各种外围设备驱动程序

关于 System.Device.Gpio , 请前往 https://dotnet.myget.org/feed/dotnet-corefxlab/package/nuget/System.Devices.Gpio 查看

================

核心代码目录
========

`
HumphreyJ.NetCore.Devices  
 |-	GPIO  
 |-	I2C  
 |	 |-	NXP  
 |		 |-	PCA9685 ...... NXP PCA9685 - 16 通道 12 比特 I2C 接口 PWM 发生器  
 |-	SPI  
 |	 |-	Waveshare  
 |		 |-	EPD2in3Hat ...... 微雪 2.13 吋单色电子墨水屏模块   
 |  
 |-	Util // 各种硬件接口的工厂类
 `

================

有了 System.Device.Gpio 来操作各种外围设备 , 加上 .Net Core 的跨平台能力 , 终于可以开心地用 C# 写树莓派了!!!
