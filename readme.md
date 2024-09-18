# LedAudioVisualizer

**LedAudioVisualizer** is a Windows Forms application built with C# and the .NET Framework that visualizes real-time audio from a selected application or system audio output using **ScottPlot** for plotting.

## Features

- Lists all currently running applications.
- Allows the user to select an application to capture audio from.
- Real-time audio visualization using **ScottPlot**.
- Simple and intuitive user interface.
- Can start and stop audio streaming from the selected application.

## Dependencies

The project relies on the following libraries:

- **CSCore**: For capturing system audio (loopback capture).
- **ScottPlot.WinForms**: For plotting real-time audio data.
- **.NET 6.0+**: Required to run the application.

## Getting Started

### Prerequisites

- Visual Studio 2019/2022 or newer.
- .NET 6.0 installed
- NuGet packages:
  - `CSCore`
  - `ScottPlot.WinForms`

### How to Run

1. Clone this repository:

   ```bash
   git clone https://github.com/evantobin1/LedAudioVisualizer.git
