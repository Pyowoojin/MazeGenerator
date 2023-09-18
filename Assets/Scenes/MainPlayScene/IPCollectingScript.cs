using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class IPCollectingScript : MonoBehaviour
{
    
    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(IPAddress iPHostEntry in host.AddressList)
        {
            if(iPHostEntry.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                Debug.Log(iPHostEntry.ToString());
        }
    }

}

