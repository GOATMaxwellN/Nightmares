using System;
using System.IO.Ports;
using System.Threading;

class BPM_ANALYSIS
{
    static void Main(string[] args)
    {
        string[] ports = SerialPort.GetPortNames();

        foreach (string port in ports)
        {
            Console.WriteLine($"Using Port: {port}");
            SerialPort arduino = new SerialPort(port, 115200);

            try
            {
                arduino.Open();
                Console.WriteLine("Connected to Arduino.");

                // Wait for 25 seconds to initialize and stabilize heartbeats
                for (int i = 0; i < 26; i++)
                {
                    Console.WriteLine($"remaining: T - {25 - i} seconds.");
                    Thread.Sleep(1000); // Sleep for 1 second
                }

                while (true)
                {
                    try
                    {
                        string BPM = arduino.ReadLine().Trim();
                        if (!string.IsNullOrEmpty(BPM) && float.TryParse(BPM, out float bpmValue) && bpmValue > 60)
                        {
                            Console.WriteLine($"BPM: {bpmValue} And Fear level: {Math.Round((0.0909 * bpmValue - 5.4545))}");
                        }
                    }
                    catch (TimeoutException) { }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error in serial communication: " + e.Message);
            }
            finally
            {
                arduino.Close();
                Console.WriteLine("Serial connection closed.");
            }
        }
    }
}
