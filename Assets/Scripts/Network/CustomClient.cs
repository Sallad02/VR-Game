using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkProtocol;
using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;

public class CustomClient : MonoBehaviour {

    private const string IP = "130.240.170.72";
    private const int PORT = 13337;

    private float tempSpawnX = 155.0F;
    private float tempSpawnY = 10.0F;
    private float tempSpawnZ = -180.0F;

    byte[] outputBytes = new byte[1024];

    private Socket socket;
    private Transform players;
    public GameObject remotePlayerPrefab;
    public GameObject localPlayer;
    public GameObject vrControllerRight;

    public GameObject laserPrefab;
    private Transform laser;

    private NetworkProtocol.Convert convert;

    public List<Message> messageQueue;


    // Use this for initialization
    void Start()
    {
        players = GameObject.Find("Players").transform;
        localPlayer = GameObject.Find("LocalPlayer");
        remotePlayerPrefab = Resources.Load("Prefabs/RemotePlayer") as GameObject;

        laser = Instantiate(laserPrefab).transform;
        laser.gameObject.SetActive(false);

        messageQueue = new List<Message>();
        convert = new NetworkProtocol.Convert();

        // Create a TCP/IP  socket.  
        socket = new Socket(AddressFamily.InterNetwork , SocketType.Stream, ProtocolType.Tcp);
        socket.NoDelay = true;

        // Connect the socket to the remote endpoint. Catch any errors.  
        try
        {
            socket.Connect(IPAddress.Parse(IP), PORT);
            //socket.Blocking = false;
            Debug.Log("Connection established");
        }
        catch (SocketException se)
        {
            Debug.Log("SocketException : " + se);
        }
        catch (Exception e)
        {
            Debug.Log("Unexpected exception : " + e);
        }

        //start thread that read input from servern
        var readInputThread = new Thread(readInput);
        readInputThread.Start();

        if (VariableStorage.PlayerName == null)
        {
            VariableStorage.PlayerName = "default";
        }

        //Create join message
        ClientJoinMsg clientJoinMsg = CreateClientJoinMsg(VariableStorage.PlayerName, tempSpawnX, tempSpawnY, tempSpawnZ);

        //Serialize join message
        byte[] serializedMessage = convert.Serialize(clientJoinMsg);

        //Send join message
        int byteSent = socket.Send(serializedMessage);

        Debug.Log("Byte successfully sent: " + byteSent);
    }

    private bool switcher = false;

    void Update()
    {   
        //Call function that match the message typ from the message queue
        while (messageQueue.Count > 0)
        {
            if (messageQueue[0] is ServerJoinMsg)
            {
                Debug.Log("Message type received: ServerJoinMsg");
                playerJoined((ServerJoinMsg)messageQueue[0]);
                messageQueue.RemoveAt(0);

            }
            else if (messageQueue[0] is ServerUpdateMsg)
            {
                Debug.Log("Message type received: ServerUpdateMsg");
                playerUpdated((ServerUpdateMsg)messageQueue[0]);
                messageQueue.RemoveAt(0);
                Debug.Log(NetworkProtocol.Convert.Version);
            }
            else if (messageQueue[0] is ServerLeaveMsg)
            {
                Debug.Log("Message type received: ServerLeaveMsg");
                playerLeft((ServerLeaveMsg)messageQueue[0]);
                messageQueue.RemoveAt(0);
            }
            else
            {
                Debug.Log("Message invalid");
            }
        }

        //Send update message to server every second game tick( 30 times/sec ) if player has received id from server.
        if (switcher && localPlayer.GetComponent<Player>().id != -1)
        {
            ClientUpdateMsg clientUpdateMsg = CreateClientUpdateMsg();

            byte[] serializedMessage = convert.Serialize(clientUpdateMsg);

            int byteSent = socket.Send(serializedMessage);

            //Debug.Log("Update messagesent, Byte successfully sent: " + byteSent);

            switcher = false;
        }
        else
        {
            switcher = true;
        }

    }

    //Function thread that read input and add it to messageQueue for the main thread to handle (custom thread cant handle unity funktions)
    private void readInput()
    {
        while (true)
        {
            Debug.Log("Waiting for input...");

            outputBytes = new byte[1024];

            int byteReceived = socket.Receive(outputBytes);

            Debug.Log("Protocol Bytes received: " + byteReceived);
            Debug.Log("Protocol Message received: " + System.Text.Encoding.UTF8.GetString(outputBytes));

            Message[] messages = convert.Deserialize(outputBytes);

            Debug.Log("Protocol deserialized");

            for (int i = 0; i < messages.Length; i++)
            {
                Debug.Log("Protocol added to queue");
                messageQueue.Add(messages[i]);
            }
        }
    }

    //----------Handles received messages---------------------------------------------------------------------------------------------

    public void  playerJoined (ServerJoinMsg sjm)
    {

        if (localPlayer.GetComponent<Player>().id == -1)
        {
            localPlayer.transform.position = new Vector3(sjm.position.x, sjm.position.y, sjm.position.z);
            localPlayer.GetComponent<Player>().id = sjm.playerId;
            localPlayer.GetComponent<Player>().PlayerName = sjm.name;

            Debug.Log("Local player joind and position, name and id updated");

            int listSize = sjm.currentPlayers.Count;

            Debug.Log("Player exists list size: " + listSize);

            for (int i = 0; i < listSize; i++)
            {
                GameObject gameObject1 = Instantiate(remotePlayerPrefab, new Vector3(sjm.currentPlayers[i].position.x, sjm.currentPlayers[i].position.y, sjm.currentPlayers[i].position.z), Quaternion.identity);
                gameObject1.transform.parent = players;
                gameObject1.GetComponent<Player>().id = sjm.currentPlayers[i].id;
                gameObject1.GetComponent<Player>().PlayerName = sjm.currentPlayers[i].name;

                Debug.Log("Local player joind and position, name and id updated");
            }
            
        }
        else
        {
            if (localPlayer.GetComponent<Player>().id == sjm.playerId)
            {
                return;
            }

            GameObject gameObject = Instantiate(remotePlayerPrefab, new Vector3(sjm.position.x, sjm.position.y, sjm.position.z), Quaternion.identity);
            gameObject.transform.parent = players;
            gameObject.GetComponent<Player>().id = sjm.playerId;
            gameObject.GetComponent<Player>().PlayerName = sjm.name;
        }
    }

    //TODO: update and diplay laser position (add model of vr controll)
    private void playerUpdated(ServerUpdateMsg sum)
    {
        Player[] playersList = players.GetComponentsInChildren<Player>();

        foreach (Player playerScript in playersList)
        {
            if (playerScript.id == sum.playerId)
            {
                playerScript.MovePlayer(new Vector3(sum.playerPosition.x, sum.playerPosition.y, sum.playerPosition.z), new Vector3(sum.playerRotation.x, sum.playerRotation.y, 0));

                if (sum.pointerPosition != null)
                {
                    playerScript.DrawLaser(new Vector3(sum.pointerPosition.x, sum.pointerPosition.y, sum.pointerPosition.z), new Vector3(sum.pointerRotation.x, sum.pointerRotation.y, sum.pointerRotation.z));
                }
                else
                {
                    playerScript.HideLaser();
                }
                return;
            }
        }
    }

    private void playerLeft(ServerLeaveMsg slm)
    {
        Player[] playersList = players.GetComponentsInChildren<Player>();
        foreach (Player playerScript in playersList) {
            if (playerScript.id == slm.playerId)
            {
                Destroy(playerScript.gameObject);
                return;
            }
        }
    }

    //-------------Create messages to send-------------------------------------------------------------------------------------------------

    public ClientJoinMsg CreateClientJoinMsg(string name, float x, float y, float z)
    {
        ClientJoinMsg cjm = new ClientJoinMsg();
        cjm.type = MsgType.ClientJoin;
        cjm.position = new Position();
        cjm.position.x = x;
        cjm.position.y = y;
        cjm.position.z = z;
        cjm.name = name;

        return cjm;
    }

    //TODO: Test for laser
    public ClientUpdateMsg CreateClientUpdateMsg()
    {
        Vector3 playerPosition = localPlayer.transform.position;
        Vector3 playerHeadRotation = localPlayer.GetComponent<Player>().head.eulerAngles;

        ClientUpdateMsg cum = new ClientUpdateMsg();
        cum.playerPosition = new Position();
        cum.playerRotation = new Rotation();
        cum.type = MsgType.ClientUpdate;
        cum.playerPosition.x = playerPosition.x;
        cum.playerPosition.y = playerPosition.y;
        cum.playerPosition.z = playerPosition.z;
        cum.playerRotation.x = playerHeadRotation.x;
        cum.playerRotation.y = playerHeadRotation.y;

        //If the laser pointer is used, send laser pointer position and rotation in message
        if (vrControllerRight != null && vrControllerRight.GetComponent<LaserPointer>().LaserActive || Input.GetKey("l"))
        {
            Debug.Log("Laser SENDING from VRController!!!!!!!!!!!!!!!!!!!!!!!");
            Vector3 vrControllerRightPosition = vrControllerRight.transform.position;
            Vector3 vrControllerRightRotation = vrControllerRight.transform.eulerAngles;

            cum.pointerPosition = new Position();
            cum.pointerRotation = new Rotation();

            cum.pointerPosition.x = vrControllerRightPosition.x;
            cum.pointerPosition.y = vrControllerRightPosition.y;
            cum.pointerPosition.z = vrControllerRightPosition.z;

            cum.pointerRotation.x = vrControllerRightRotation.x;
            cum.pointerRotation.y = vrControllerRightRotation.y;
            cum.pointerRotation.z = vrControllerRightRotation.z;
        }

        if (Input.GetMouseButton(1))
        {
            Debug.Log("Laser SENDING from head!!!!!!!!!!!!!!!!!!!!!!!!");
            Vector3 playerHeadPosition = localPlayer.GetComponent<Player>().head.position;

            cum.pointerPosition = new Position();
            cum.pointerRotation = new Rotation();

            cum.pointerPosition.x = playerHeadPosition.x;
            cum.pointerPosition.y = playerHeadPosition.y;
            cum.pointerPosition.z = playerHeadPosition.z;

            cum.pointerRotation.x = playerHeadRotation.x;
            cum.pointerRotation.y = playerHeadRotation.y;
            cum.pointerRotation.z = playerHeadRotation.z;
        }


        ////TEST

        //cum.pointerPosition = new Position();
        //cum.pointerRotation = new Rotation();

        //cum.pointerPosition.x = 0;
        //cum.pointerPosition.y = 0;
        //cum.pointerPosition.z = 0;

        //cum.pointerRotation.x = 0;
        //cum.pointerRotation.y = 0;
        //cum.pointerRotation.z = 0;

        return cum;
    }

    public ClientLeaveMsg CreateClientLeaveMsg()
    {
        ClientLeaveMsg clm = new ClientLeaveMsg();
        clm.type = MsgType.ClientLeave;

        return clm;
    }

    //-------------------------------------------------------------------------------------------------------

    public void OnApplicationQuit()
    {
        CloseConnection();
    }

    public void CloseConnection()
    {
        byte[] clm = convert.Serialize(CreateClientLeaveMsg());
        socket.Send(clm);

        Thread.Sleep(1000);

        socket.Close();
    }

}
