using System;
using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine;

public class testconnection : MonoBehaviour
{

    public GameObject Player;

    SerialPort data_stream;

    public string receivedstring;
    public int bpm;
    public int fearLevel;
    bool newBool = true;

    // Start is called before the first frame update
    void Start()
    {
        string[] ports = SerialPort.GetPortNames();
        foreach (string port in ports)
        {
            Debug.Log($"Using Port: {port}");
            data_stream = new SerialPort(port, 115200);
            data_stream.Open();
            string ok = data_stream.ReadLine();
            if (ok == "OK")
            {
                Debug.Log(port + " is the correct port");
                break;
            }
        }
        // data_stream.ReadTimeout = 3000;
        // serialPort.ReadTimeout = 1;
        // data_stream.Open(); // Initiate the Serial stream
    }

    // Update is called once per frame
    void Update()
    {
        if (newBool) {
            StartCoroutine(New());
        }
        
    }

    public IEnumerator New() {
        Debug.Log("enter");
        newBool = false;
        
        // try
        // {
        //     // receivedstring = data_stream.ReadLine();
        try {
            var receivedstring = data_stream.ReadLine();
            Debug.Log(receivedstring);
            // data_stream.ReadTimeout = 25;
            bpm = (int) float.Parse(receivedstring);
            fearLevel = (int) (0.0909 * Mathf.Abs(bpm) - 5.4545);

        } catch (TimeoutException e) {
            Debug.Log("timeout");
        }

        if (Player.TryGetComponent(out Health player))
        {
            player.setbpm(Mathf.Abs(bpm));
            player.setfear(fearLevel);
        }
        
        yield return new WaitForSeconds(5f);
        newBool = true;
            // do other stuff with the data
        // }
        // catch (TimeoutException e)
        // {
        //     Debug.Log("timeout");
        //     // no-op, just to silence the timeouts. 
        //     // (my arduino sends 12-16 byte packets every 0.1 secs)
        // }
        
        // data_stream.ReadTimeout = 25;
        // bpm = (int) float.Parse(receivedstring);
        // fearLevel = (int) (0.0909 * bpm - 5.4545);
        
        
    }
}