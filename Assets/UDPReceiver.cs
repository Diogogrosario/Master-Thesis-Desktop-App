using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class UDPReceiver : MonoBehaviour {
    // Server code (running on desktop)
    private UdpClient udpClient;
    private IPEndPoint localEndPoint;
    [SerializeField] private string ip;
    [SerializeField] private int port;

    private DisplayTouch displayTouch;
    
    void Start()
    {
        // Initialize the UDP client and set the local endpoint to the desktop app's address and port
        localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        udpClient = new UdpClient(localEndPoint);
        displayTouch = GameObject.Find("MobileDevice").GetComponent<DisplayTouch>();

        Debug.Log("Server started");
    }

    void Update()
    {
        // Receive messages from the mobile device and log them to the console
        if (udpClient.Available > 0)
        {
            byte[] data = udpClient.Receive(ref localEndPoint);
            string message = System.Text.Encoding.UTF8.GetString(data);
            Debug.Log("Received message: " + message);
            ParseMessage(message);
        }
    }

    void ParseMessage(string message)
    {
        string[] splitMessage = message.Split(':');
        if (splitMessage[0] == "Touch")
        {
            string[] coords = splitMessage[1].Split(',');
            //Remove parenthesis and replace . with ,
            float x = float.Parse(coords[0].Substring(1).Replace(".",","));
            float y = float.Parse(coords[1].Substring(0, coords[1].Length - 1).Replace(".",","));
            displayTouch.showTouch(x,y);
            
        }
        else if (splitMessage[0] == "Size")
        {
            string[] coords = splitMessage[1].Split(',');
            //Remove parenthesis 
            float width = float.Parse(coords[0].Substring(1));
            float height = float.Parse(coords[1].Substring(0, coords[1].Length - 1));
            displayTouch.setHeight(height);
            displayTouch.setWidth(width);
            displayTouch.setAspectRatio();
        }
        
    }
}
