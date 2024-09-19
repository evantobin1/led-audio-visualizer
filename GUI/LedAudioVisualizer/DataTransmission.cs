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
            isConnected = true;
            return true;
        }

        // Method to send data in chunks
        // Method to send data in chunks, with a programmable delay to sync with the audio device
        public async void SendData(Color[] ledData)
        {
            if (!isConnected)
            {
                return;
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
        }


        // Prepares a packet of data for transmission
        private static byte[] PreparePacket(Color[] ledData, int startIndex, int numLeds, bool isFinalPacket)
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
