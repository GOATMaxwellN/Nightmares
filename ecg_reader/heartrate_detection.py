#!/usr/bin/env python
"""
 Blinks an LED on digital pin 13
 in 1 second intervals
"""

import serial
from Arduino import Arduino
import time

counter = 0
start = time.time()
heartbeats = []

def calculate_rate_of_change(heart_rates):
    rate_of_change = []
    for i in range(1, len(heart_rates)):
        change = (heart_rates[i] - heart_rates[i - 1]) / heart_rates[i - 1]
        rate_of_change.append(change)
    
    significant_change = [change for change in rate_of_change if abs(change) > 20]
    if significant_change:
        return False
    return True

try:
    arduino = serial.Serial('COM3', 115200, timeout=1)
    time.sleep(2)

    print("Connected to Arduino.")

    while True:
        if arduino.in_waiting > 0:
            line = arduino.readline().decode("utf-8").rstrip()
            if int(line) == 1:
                end = time.time()
                interval = end - start
                if interval != 0:
                    bpm = 60 / interval
                    if bpm < 220:
                        heartbeats.append(bpm)
                        if len(heartbeats) >= 3:
                            valid = calculate_rate_of_change(heartbeats)
                            if valid:
                                average = sum(heartbeats) / len(heartbeats)
                                print(f"{counter}: bpm {average}: list: {heartbeats}")
                                counter += 1
                            heartbeats.clear()
                start = end
    
except serial.SerialException as e:
    print("Error in serial communication:", e)

finally:
    arduino.close()
    print("Serial connection closed.")

        
    
