using System;
using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine;

public class testconnection : MonoBehaviour
{

    SerialPort data_stream = new SerialPort("COM3", 115200);
    public string receivedstring;
    public int bpm;
    public int fearLevel;

    // Start is called before the first frame update
    void Start()
    {
        data_stream.Open(); // Initiate the Serial stream
    }

    // Update is called once per frame
    void Update()
    {
        receivedstring = data_stream.ReadLine();
        bpm = (int) float.Parse(receivedstring);
        fearLevel = (int) (0.0909 * bpm - 5.4545);
    }
}