using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ControllerCommunication : MonoBehaviour {

    public delegate void MsgReceived(string msg);
    public event MsgReceived OnMsgReceived;

    private SerialPort sp;
      
    
    private void Start()
    {
        sp = new SerialPort(Controller.Instance.ComPort, Controller.Instance.BaudRate);

        sp.ReadTimeout = Controller.Instance.READ_TIMEOUT;
        sp.WriteTimeout = Controller.Instance.WRITE_TIMEOUT;
        sp.Open();
        InvokeRepeating("ReadSerial", .001f, .008f);

        Debug.Log("Serial connected");
    }
    
    public void SendMsg(string msg)
    {
        if (!sp.IsOpen)
            return;

        sp.Write(msg);
        //Debug.Log("Sending message: " + msg);
    }

    private string bufferStr = "";
    public void ReadSerial()
    {
        if (!sp.IsOpen)
            return;
        while (true)
        {
            try
            {
                char c = (char)sp.ReadChar();
                bufferStr += c;
                if (c == '\n')
                    DispatchMsg();
            }
            catch (System.Exception)
            {
                break;
            }
        }
    }

    private void DispatchMsg()
    {
        if (OnMsgReceived != null && bufferStr.Length != 0)
        {
            OnMsgReceived(bufferStr);
        }
        bufferStr = "";
    }

}
