using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerExample : MonoBehaviour {

    public ControllerCommunication controller;

    public Light spotlight;

    public void Awake()
    {
        controller.OnMsgReceived += OnMsgReceived;
    }

    private bool LedOn;
    public void ToggleLed()
    {
        if(LedOn)
        {
            controller.SendMsg("ledRed 10");
            LedOn = false;
        }
        else
        {
            controller.SendMsg("ledRed 11");
            LedOn = true;
        }
    }

    private void OnMsgReceived(string msg)
    {
        try
        {
            spotlight.intensity = float.Parse(msg) / 600 * 8;
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
