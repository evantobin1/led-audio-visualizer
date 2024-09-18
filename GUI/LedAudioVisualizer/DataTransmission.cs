﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;

namespace LedAudioVisualizer
{
    public class DataTransmission
    {
        // Transmission rate in Hz (led strip updates per second)
        public byte TransmissionRate = 48;  // Default transmission rate of 12 led strip updates per second
        public int DelayMs = 10;
        private DateTime _lastTransmissionTime;

        // Maximum LEDs per packet
        private const int MaxLedsPerPacket = 100;  // Each packet can contain 100 LEDs
        private const int PacketSize = 501;        // Each packet is 501 bytes

        // UDP client and target endpoint for sending data
        private UdpClient udpClient;
        private IPEndPoint targetEndPoint;
        private bool isConnected = false; // Track if connected

        public DataTransmission(string ipAddress, int port)
        {
            // Initialize the UDP client and target endpoint
            udpClient = new UdpClient();
            targetEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);

            _lastTransmissionTime = DateTime.Now;
        }

        // Method to connect to the ESP8266/ESP32, validates the connection
        public bool Connect()
        {
            try
            {
                // Send a test message or perform a basic "ping" or handshake validation (optional)
                byte[] testMessage = new byte[] { 0x00 }; // Example ping message
                udpClient.Send(testMessage, testMessage.Length, targetEndPoint);


                // Optionally wait for a response from the ESP to confirm it's ready (e.g., if you implement a handshake on the ESP)
                isConnected = true;  // Mark as connected for further operations
                return true;
            }
            catch
            {
                isConnected = false;
                return false;
            }
        }

        // Method to send data in chunks
        // Method to send data in chunks, with a programmable delay to sync with the audio device
        public async void SendData(Color[] ledData)
        {
            if (!isConnected)
            {
                return;
            }

            // Calculate the interval between packets based on the transmission rate (in milliseconds)
            double interval = 1000.0 / TransmissionRate;

            // Check the elapsed time since the last packet was sent
            double timeElapsedSinceLastPacket = (DateTime.Now - _lastTransmissionTime).TotalMilliseconds;

            // If it's the first transmission or if the time since the last transmission exceeds the interval
            if (timeElapsedSinceLastPacket >= interval)
            {
                // Wait for the delay if DelayMs is greater than 0
                if (DelayMs > 0)
                {
                    await Task.Delay(DelayMs);  // Apply the delay to sync with Bluetooth audio
                }

                int totalLeds = ledData.Length;
                int ledsSent = 0;
                byte sequenceNumber = 0;

                while (ledsSent < totalLeds)
                {
                    // Calculate how many LEDs to send in this packet
                    int ledsInThisPacket = Math.Min(MaxLedsPerPacket, totalLeds - ledsSent);

                    // Check if this is the final packet
                    bool isFinalPacket = (ledsSent + ledsInThisPacket) >= totalLeds;

                    // Prepare the packet
                    byte[] packetData = PreparePacket(ledData, ledsSent, ledsInThisPacket, isFinalPacket);

                    // Send the packet over UDP
                    SendPacket(packetData);

                    ledsSent += ledsInThisPacket;
                    sequenceNumber++;
                }

                // Record the time of this transmission
                _lastTransmissionTime = DateTime.Now;
            }
        }


        // Prepares a packet of data for transmission
        private byte[] PreparePacket(Color[] ledData, int startIndex, int numLeds, bool isFinalPacket)
        {
            // Create the packet with a fixed size of 501 bytes
            byte[] packet = new byte[PacketSize];

            // First byte is the "final packet" flag (0x01 if final, 0x00 otherwise)
            packet[0] = isFinalPacket ? (byte)0x01 : (byte)0x00;

            // Add LED data for this packet
            int packetIndex = 1; // Start after the final packet flag

            for (int i = 0; i < numLeds; i++)
            {
                // LED index (2 bytes, big-endian)
                ushort ledIndex = (ushort)(startIndex + i);
                packet[packetIndex++] = (byte)(ledIndex >> 8);   // High byte of LED index
                packet[packetIndex++] = (byte)(ledIndex & 0xFF); // Low byte of LED index

                // RGB values (3 bytes)
                packet[packetIndex++] = ledData[startIndex + i].R;
                packet[packetIndex++] = ledData[startIndex + i].G;
                packet[packetIndex++] = ledData[startIndex + i].B;
            }

            // Fill the rest of the packet with 0xFFFF and 0x00 if there are fewer than 100 LEDs
            while (packetIndex < PacketSize)
            {
                // LED index is 0xFFFF for padding
                packet[packetIndex++] = 0xFF;
                packet[packetIndex++] = 0xFF;

                // RGB values are 0x00
                packet[packetIndex++] = 0x00;
                packet[packetIndex++] = 0x00;
                packet[packetIndex++] = 0x00;
            }

            return packet;
        }

        // Sends the prepared packet over UDP
        private void SendPacket(byte[] data)
        {
            try
            {
                // Send the data to the target IP and port
                udpClient.Send(data, data.Length, targetEndPoint);
            }
            catch
            {

            }
        }
    }
}
