From python code: https://github.com/adafruit/Adafruit_Python_PCA9685

PCA9685 data sheet: https://www.nxp.com/docs/en/data-sheet/PCA9685.pdf

--------

参考 Adafruit 提供的 python 版驱动程序编写，调用方式与其大体相同。

在 Raspberry Pi 3B+ 上的 Raspbian 测试通过。


实例化对象
--------

`var pca9685 = new PCA9685(); // 使用默认参数 address = 0x40, busId = 0 初始化设备`

`var pca9685 = new PCA9685(int address); // 使用指定的地址初始化设备`

`var pca9685 = new PCA9685(int address, int busId); // 使用指定的地址和总线号初始化设备`

配置 PWM 频率
--------

`pca9685.SetPwmFrequency(50.0); // 设置设备的 PWM 频率为 50Hz （使用 double 型参数）`
`pca9685.SetPwmFrequency(121); // 设置设备的 PWM 频率为 50Hz （使用 byte 型参数直接指定 prescale 的值，具体计算公式见下方说明）`

* prescale = Round( osc_clock / 4096 / update_rate  - 1 )

其中，osc_clock 为时钟频率，默认是 25MHz；update_rate 是 PWM 频率

* PWM 频率最大约为 1526Hz @ prescale = 3 (0x03)
* PWM 频率最小约为 24Hz @ prescale = 255 (0xFF)

* 所有通道均使用相同的 PWM 频率

设置占空比
--------

`SetPwm(0, 0, 512); // 设置 0 号通道的波形为  ￣＿＿＿＿＿＿＿`

`SetAllPwm(3072, 2048); // 设置所有通道的波形为  ￣￣￣￣＿＿￣￣`

* 通道号为 0 起始的 16 个通道，即 0 到 15

* 后两个参数 on, off 是把每个 PWM tick 分成 2 ^ 12 = 4096 子刻后的“设置为高电平”、“设置为低电平”的子刻序号，也是从 0 开始，即 0 到 4095
