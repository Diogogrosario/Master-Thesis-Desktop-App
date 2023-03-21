using System.Globalization;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class UDPReceiver : MonoBehaviour
{
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
        displayTouch = GameObject.Find("TouchScreen").GetComponent<DisplayTouch>();

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
        string[] splitMessage = message.Split(';');
        if (splitMessage.Length == 1) // If message has  size 1 then its just starting screen size
        {
            //Get only coords after : and then split by ,
            string[] coords = splitMessage[0].Split(':')[1].Split(',');
            //Remove parenthesis 
            float width = float.Parse(coords[0].Substring(1),CultureInfo.InvariantCulture);
            float height = float.Parse(coords[1].Substring(0, coords[1].Length - 1),CultureInfo.InvariantCulture);
            displayTouch.setHeight(height);
            displayTouch.setWidth(width);
        }
        else //Its a touch message -- ID:id;Begin:(12,12)
        {
            for (int i = 0; i < splitMessage.Length; i++)
            {
                string[] idSplit = splitMessage[0].Split(':');
                int id = int.Parse(idSplit[1]);
                string[] touchData = splitMessage[1].Split('-');
                if (touchData[0] == "Begin" || touchData[0] == "Moved")
                {
                    string[] coords = touchData[1].Split(',');
                    //Remove parenthesis
                    float x = float.Parse(coords[0].Substring(1),CultureInfo.InvariantCulture);
                    float y = float.Parse(coords[1].Substring(0, coords[1].Length - 1),CultureInfo.InvariantCulture);
                    displayTouch.showTouch(id, x, y);
                }
                else if (touchData[0] == "End")
                {
                    displayTouch.removeTouch(id);
                }
            }
        }
    }
}