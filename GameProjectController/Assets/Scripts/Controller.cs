using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class Controller : MonoBehaviour
{

    public static Controller Instance { get; private set; }

    public int READ_TIMEOUT { get { return 2; } }
    public int WRITE_TIMEOUT { get { return 2; } }

    public Dropdown ComPortsDropDownMenu;
    public Dropdown BaudRatesDropDownMenu;

    private string[] comPorts;
    private string comPort;
    public string ComPort
    {
        get { return this.comPort; }
        private set { this.comPort = value; }
    }

    private int[] baudRates = { 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 28800, 38400, 57600, 115200 };
    private int baudRate;
    public int BaudRate
    {
        get { return this.baudRate; }
        private set { this.baudRate = value; }
    }


    public void RefreshOptionsComPorts()
    {
        ComPortsDropDownMenu.ClearOptions();
        if (GetAvailableComPorts() != null)
        {
            List<string> options = new List<string>();
            options.AddRange(GetAvailableComPorts());
            ComPortsDropDownMenu.AddOptions(options);
        }
    }
    public void ApplyButton()
    {
        AcceptComPort();
        AcceptBaudRate();
        try
        {
            SceneManager.LoadScene("ExampleScene");
        }
        catch (System.Exception e)
        {
            Debug.Log("Error:" + e.Message);
        }
    }

    private void AcceptComPort()
    {
        if (ComPortsDropDownMenu.options.Count > 0 && GetAvailableComPorts() != null)
        {
            ComPort = ComPortsDropDownMenu.options[ComPortsDropDownMenu.value].text;
            Debug.Log(ComPort);
        }
    }
    private void AcceptBaudRate()
    {
        BaudRate = int.Parse(BaudRatesDropDownMenu.options[BaudRatesDropDownMenu.value].text);
        Debug.Log(BaudRate);
    }

    private string[] GetAvailableComPorts()
    {
        comPorts = SerialPort.GetPortNames();
        if (comPorts.Length > 0)
            return comPorts;
        else
            Debug.Log("no ports available");
        return null;
    }
    private void SetBoudRatesDropDownMenu()
    {
        List<string> brs = new List<string>();
        foreach (int br in baudRates)
        {
            brs.Add(br.ToString());
        }
        BaudRatesDropDownMenu.AddOptions(brs);
        BaudRatesDropDownMenu.value = 5;
    }
    
    void Start()
    {
        RefreshOptionsComPorts();
        SetBoudRatesDropDownMenu();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}