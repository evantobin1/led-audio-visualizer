#include <NeoPixelBus.h>


#if defined(ESP8266)
#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
#elif defined(ESP32)
#include <WiFi.h>
#else
#error "This is not a ESP8266 or ESP32!"
#endif

// Set to the number of LEDs in your LED strip
#define NUM_LEDS 600
// Maximum number of packets to hold in the buffer
#define BUFFER_LEN 501
// Toggles FPS output (1 = print FPS over serial, 0 = disable output)
#define PRINT_FPS 1

// NeoPixelBus settings
const uint8_t PixelPin = 10;  // Pin for ESP32, ignored on ESP8266 (default to 3 for DMA)

// Wifi and socket settings
const char* ssid = "";
const char* password = "";
unsigned int localPort = 7777;  // UDP port
char packetBuffer[BUFFER_LEN];

WiFiUDP port;
// Network information (set to static IP)
IPAddress ip(192, 168, 0, 150);
IPAddress gateway(192, 168, 0, 1);
IPAddress subnet(255, 255, 255, 0);

// LED strip object
NeoPixelBus<NeoGrbFeature, Neo800KbpsMethod> ledstrip(NUM_LEDS, PixelPin);

void setup() {
    Serial.begin(115200);
    WiFi.mode(WIFI_STA);
    WiFi.config(ip, gateway, subnet);
    WiFi.begin(ssid, password);
    Serial.println("");
    while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        Serial.print(".");
    }

    Serial.println("");
    Serial.print("Connected to ");
    Serial.println(ssid);
    Serial.print("IP address: ");
    Serial.println(WiFi.localIP());

    port.begin(localPort);

    ledstrip.Begin(); // Initialize the LED strip
    ledstrip.Show();  // Clear the strip for use
}

#if PRINT_FPS
    uint16_t fpsCounter = 0;
    uint32_t secondTimer = 0;
#endif

void loop() {
    // Read data over socket
    int packetSize = port.parsePacket();
    
    // If packets have been received, interpret the command
    if (packetSize) {
        int len = port.read(packetBuffer, BUFFER_LEN);

        // First byte is the final packet flag (0x00 or 0x01)
        bool isFinalPacket = (packetBuffer[0] == 0x01);

        // Process LED data (starting from index 1)
        for (int i = 1; i < len; i += 5) {
            // Parse LED index (2 bytes: i+1 and i+2)
            uint16_t ledIndex = (packetBuffer[i] << 8) | packetBuffer[i + 1];

            // If we encounter 0xFFFF, this is padding and we stop processing
            if (ledIndex == 0xFFFF) {
                break;
            }

            // Parse RGB values for this LED
            RgbColor pixel((uint8_t)packetBuffer[i + 2], (uint8_t)packetBuffer[i + 3], (uint8_t)packetBuffer[i + 4]);

            // Set the LED color
            ledstrip.SetPixelColor(ledIndex, pixel);
        }

        // If this is the final packet, update the LED strip
        if (isFinalPacket) {
            ledstrip.Show();
        }

        #if PRINT_FPS
            fpsCounter++;
            Serial.print("/");  // Monitors connection (shows jumps/jitters in packets)
        #endif
    }

    #if PRINT_FPS
        if (millis() - secondTimer >= 1000U) {
            secondTimer = millis();
            Serial.printf("FPS: %d\n", fpsCounter);
            fpsCounter = 0;
        }
    #endif
}