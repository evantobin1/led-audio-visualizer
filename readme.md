
# 🚥led-audio-visualizer

**LedAudioVisualizer** is a Windows Forms application that visualizes real-time audio from a selected application or system audio output using **ScottPlot** for plotting. This application can capture audio data and visualize the frequency spectrum, while sending the data to an ESP-8266 or ESP-32 to control up an LED strip.

## Features

- **Real-Time Audio Capture**: Capture audio from any selected running application or system output via loopback.
- **Frequency Visualization**: Displays the frequency spectrum of the audio using **ScottPlot**.
- **LED Color Visualization**: Maps frequency bands to different LED colors (Red, Green, Blue) with customizable settings for min/max frequency ranges.
- **Customizable Transmission Rate**: Allows control over the rate of sending LED color data to external devices (e.g., ESP8266, ESP32).
- **User Interface**: Simple UI that displays running applications and allows the user to select a target application for audio capture.
- **Wi-Fi Control for LEDs**: Sends real-time RGB color data to an ESP8266 or ESP32 via UDP over Wi-Fi to control LED strips.
- **Adjustable Audio Thresholds**: Users can adjust the thresholds and power scaling for each RGB channel to fine-tune the LED visualization.

## Dependencies

The project relies on the following libraries:

- **CSCore**: For capturing system audio (loopback capture).
- **ScottPlot.WinForms**: For plotting real-time audio data and frequency visualization.
- **.NET 6.0+**: Required to run the application.
- **UDP Communication**: Allows for sending color data to a remote ESP8266/ESP32 device to control LED strips.

## Getting Started

### Prerequisites

- **Visual Studio 2019/2022** or newer.
- **.NET 6.0** installed.
- NuGet packages:
  - `CSCore`
  - `ScottPlot.WinForms`

### Installation Steps

1. **Clone this repository**:

   ```bash
   git clone https://github.com/evantobin1/led-audio-visualizer.git
   ```

2. **Open the project** in Visual Studio.

3. **Restore NuGet packages**:
   - Go to **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution**.
   - Restore the packages for `CSCore`, `ScottPlot.WinForms`, and any other dependencies.

4. **Build the project** using Visual Studio.

5. **Run the application** by pressing `F5` or clicking on **Start**.

## Usage

### User Interface

1. **List Running Applications**: The application automatically lists all running applications that produce audio.
   
   - Use the **Refresh Applications** button to refresh the list of running applications.
   - Select an application from the list and click **Select** to start capturing its audio.

2. **Audio Visualization**:
   - **Real-Time Audio Spectrum**: Displays the frequency spectrum of the captured audio.
   - **Color Visualization**: Shows the breakdown of the audio frequency into LED color channels (Red, Green, Blue).

3. **Wi-Fi LED Strip Control**:
   - The application can send real-time color data (RGB values) to a Wi-Fi-connected LED strip controller (e.g., ESP8266/ESP32).
   - The **DataTransmission** class handles the communication with the LED strip controller over UDP.

4. **Customization**:
   - **LED Count**: Adjust the number of LEDs in the strip by changing the value in the LED count input.
   - **Frequency Bands**: Customize the min/max frequency values for the Red, Green, and Blue channels.
   - **Transmission Rate**: Adjust the rate at which the data is sent to the LED strip.
   - **Power Adjustment**: Fine-tune the brightness and responsiveness of each color channel by adjusting the power for Red, Green, and Blue.

5. **UDP Communication Settings**:
   - **Delay Compensation**: Set the `DelayMs` parameter in the UI to compensate for any audio delay (e.g., if using Bluetooth audio).
   - **Network Configuration**: The application sends color data to an IP address and port that must be configured in the `DataTransmission` class or UI settings.

### Wi-Fi LED Control (ESP8266/ESP32)

The application is capable of controlling a remote LED strip via an ESP8266 or ESP32 using UDP. The following steps describe how to configure the microcontroller:

1. **Connect the LED strip** to the ESP8266/ESP32.
2. **Set up Wi-Fi** on the ESP8266/ESP32 and ensure it's reachable from your PC running the LedAudioVisualizer.
3. **UDP Communication**: The application sends LED color data over UDP to the ESP8266/ESP32. Ensure the IP address and port of the microcontroller match the settings in the application.

Example Arduino code for the ESP8266/ESP32 is available in the repository's `Firmware` folder.

### Customizing the Frequency Bands

- **Red Channel**: Typically maps to the lower frequencies (bass).
- **Green Channel**: Maps to mid frequencies.
- **Blue Channel**: Maps to high frequencies (treble).

You can adjust the frequency range for each color by changing the min/max values in the UI for the Red, Green, and Blue channels.

### Customizing Power Settings

Each color channel has a corresponding "power" setting. This allows you to scale the brightness or intensity of the LED color channel in proportion to the audio signal.

### Adjusting Transmission Rate

The default transmission rate is set to **48 updates per second**, but you can increase or decrease this rate using the UI. Adjust this setting based on your LED strip's responsiveness and the Wi-Fi network performance.

### Real-Time Color Smoothing

The application includes options to smooth the color transitions between LEDs by averaging the values of neighboring LEDs. This helps prevent sharp transitions and produces smoother visualizations.

## Known Issues

- **Bluetooth Audio Delay**: If you're using a Bluetooth audio device, there may be a noticeable delay between the audio and the visualization. Use the **DelayMs** option to manually compensate for the Bluetooth delay.

## Roadmap

- Add more advanced smoothing algorithms for the LED strip visualization.
- Allow more customizable visualization effects (e.g., fade effects, wave patterns).
- Allow for searching of ESP devices to connect to from the UI.
- Create installer for UI, make release.

## Contributions

Contributions are welcome! Feel free to submit issues or pull requests. For significant changes, please discuss them via issue before submitting a PR.

### How to Contribute

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Commit your changes.
4. Push to the branch.
5. Create a pull request.
