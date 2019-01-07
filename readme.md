From python code: https://github.com/adafruit/Adafruit_Python_PCA9685

参考 Adafruit 提供的 python 版驱动程序编写，调用方式与其大体相同。

在 Raspberry Pi 3B+ 上的 Raspbian 测试通过。


实例化对象
--------

`var pca9685 = new PCA9685(); // 使用默认参数 address = 0x40, busId = 0 初始化设备`

`var pca9685 = new PCA9685(int address = PCA9685_ADDRESS); // 使用指定的地址初始化设备`

`var pca9685 = new PCA9685(int address = PCA9685_ADDRESS, int busId = 0); // 使用指定的地址和总线号初始化设备`

配置 PWM 频率
--------

`pca9685.SetPwmFrequency(50); // 设置设备的 PWM 频率为 50Hz`

* PWM 频率范围为 24Hz 至 1526Hz ，默认值为 PRE_SCALE 寄存器的 0x1E 也就是 200Hz

* 所有通道均使用相同的 PWM 频率

设置占空比
--------

`SetPwm(0, 0, 512); // 设置 0 号通道的波形为  ￣＿＿＿＿＿＿＿`

`SetAllPwm(3072, 2048); // 设置所有通道的波形为  ￣￣￣￣＿＿￣￣`

* 通道号为 0 起始的 16 个通道，即 0 到 15

* 后两个参数 on, off 为把每个 PWM tick 分成 2 ^ 12 = 4096 子刻后的“设置为高电平”、“设置为低电平”的子刻序号，也是从 0 开始，即 0 到 4095
