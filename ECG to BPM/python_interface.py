#This python programs serves the purpose of feeding on to the digital serial output
import serial,serial.tools.list_ports, time
from Arduino import Arduino

ports = list(serial.tools.list_ports.comports())

for p in ports:
    p = str(p).split()
    print(f"Using Port: {p[0]}")
    try:
        arduino = serial.Serial(p[0], 115200, timeout=1)
        
        print("Connected to Arduino.")
        
        for i in range(26): #we wait 25 seconds to initialize and stabalize heartbeats
            print(f"remaining: T - {25-i} seconds.")
            time.sleep(1)
        
        while True:
            BMP = arduino.readline().decode("utf-8").rstrip()
            if BMP and eval(BMP) > 60: print(BMP, f"Fear level: {round((0.11*float(BMP)-6.6))}") #we take 200 as the max since the sensor has error of +-20 BPM
        
    except serial.SerialException as e:
        print("Error in serial communication:", e)

    finally:
        arduino.close()
        print("Serial connection closed.")

        
    