using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;
using UnityEngine.UI;


public class TCP : MonoBehaviour
{
    // Start is called before the first frame update

    public string staInfo = "NULL";//状态信息
    public string Ip = "127.0.0.1";
    public string Port = "8888";
    public int recTimes = 0;
    public string RevMes = "NULL";
    private Socket socketSend;
    public bool IsConnect = false;
    static public bool clickSend = false;
    private string statecode;
    public static StringBuilder toSendMsg;
    
    //Settings
    public InputField InputIP;
    public InputField InputPort;
    public Text State;
    void Start()
    {
        IsConnect = false;
        clickSend = false;
        toSendMsg = new StringBuilder();
        InputIP.text = PlayerPrefs.GetString("Ip");
        InputPort.text = PlayerPrefs.GetString("Port");
        statecode = "未连接";
    }

    public void ClickConnect()
    {
        Port = InputPort.text;
        Ip = InputIP.text;
        try
        {
            int _port = Convert.ToInt32(Port);
            string _ip = Ip;
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint point = new IPEndPoint(ip, _port);
            
            socketSend.Connect(point);
            Debug.Log("Connect Success!At "+ip+":"+_port);
            IsConnect = true;
            clickSend = false;
            
            PlayerPrefs.SetString("IP",Ip);
            PlayerPrefs.SetString("Port",Port);
            
            Thread r_thread = new Thread(Received);
            r_thread.IsBackground = true;
            r_thread.Start();

            Thread s_thread = new Thread(SendMessage);
            s_thread.IsBackground = true;
            s_thread.Start();
        }
        catch (Exception)
        {
            IsConnect = false;
            Debug.Log("IP/Port Err");
            statecode = "连接失败，请检查IP/端口设置";
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (IsConnect)
        {
            State.text = "已连接";
            State.color = Color.green;
        }
        else
        {
            State.text = statecode;
            State.color = Color.red;
        }
    }

    void Received()
    {
        while (IsConnect)
        {
            try
            {
                byte[] buffer = new byte[1024 * 6];
                //实际接收到的有效字节数
                //Debug.Log("Reving");
                int len = socketSend.Receive(buffer);
                Debug.Log(len);
                if (len == 0)
                {
                    break;
                }
                RevMes = Encoding.UTF8.GetString(buffer, 0, len);
                
                Debug.Log("客户端接收到的数据 ： " + RevMes);

                recTimes++;
                staInfo = "接收到一次数据，接收次数为 ：" + recTimes;
                Debug.Log("接收次数为：" + recTimes);
            }
            catch { }
        }
    }

    public void SendMessage()
    {
        try
        {
            while (IsConnect)
            {
                if (clickSend) //如果点击了发送按钮
                {
                    clickSend = false;
                    byte[] buffer = new byte[1024 * 6];
                    buffer = Encoding.UTF8.GetBytes(toSendMsg.ToString());
                    socketSend.Send(buffer);
                    Debug.Log("发送的数据为：" + toSendMsg);
                }
            }
        }
        catch
        {
            Debug.Log("发送失败");
        }
    }

    public void SendButton()
    {
        clickSend = true;
    }
}
