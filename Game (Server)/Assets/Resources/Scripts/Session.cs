using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Threading.Tasks;
using System;

/*** Handles communcation between a client and this server ***/
public class Session {

    public Player player { get; set; }

    private Lobby lobby;

    public Lobby GetLobby() {
        return lobby;
    }

    public string name { get; set; }

    /*** Assigns a lobby to this session ***/
    public bool SetLobby(Lobby lobby) {
        if (lobby != null && this.lobby != null) {
            return false;
        }
        this.lobby = lobby;
        if (lobby != null) {
            player = new Player();
        } else {
            player = null;
        }
        return true;
    }

    /*** Initalisation method ***/
    // This will be run once at the application launch
    public static void Init() {
        Debug.Log("Setting up session");
        writers = new Dictionary<int, WritePacket>();
        readers = new Dictionary<int, ReadPacket>();

        try {
            PluginLoader.Load<int, WritePacket>(ref writers);
        } catch (Exception e) {
            Debug.Log(e);
        }
        try {
            PluginLoader.Load<int, ReadPacket>(ref readers);
        } catch (Exception e) {
            Debug.Log(e);
        }
    }

    public static Dictionary<int, WritePacket> writers;
    public static Dictionary<int, ReadPacket> readers;

    private TcpClient client;
    public Session(TcpClient client) {
        this.client = client;
        Listen();
        Write(4);
    }

    /*** Sets up a task to listen for incoming packets***/
    public void Listen() {
        Task task = null;
        task = new Task(async () => {
            while (MainThread.RUNNING) {
                if (!client.Connected) {
                    break;
                }
                byte[] buffer = new byte[1024];
                int length = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                if (length > 0) {
                    Decode(buffer);
                }
            }
            task.Dispose();
        });
        task.Start();
    }

    /*** Checks whether this sessions assigned client is still connected ***/
    public bool IsConnected() {
        try {
            client.GetStream();
        } catch (Exception e) {
            return false;
        }
        return client.Connected;
    }

    /*** Closes resources related to this session ***/
    public void Close() {
        if (lobby != null)
            LobbyManager.LeaveLobby(this);
        if (lobby != null)
            lobby.RemoveSession(this);
        ConnectionManager.MANAGER.mainLobby.Remove(this);
        try {
            client.GetStream().Close();
        } catch (Exception e) {

        }
        client.Close();
    }

    /*** Decodes an incomming packet from the client ***/
    public void Decode(byte[] buffer) {
        try {
            Reader reader = new Reader(buffer);
            int packetId = reader.ReadByte();
            if (!readers.ContainsKey(packetId)) {
                Debug.Log("Invalid packetId: " + packetId);
                return;
            }
            ReadPacket readerPacket = readers[packetId];
            /*if (buffer.Length != readerPacket.GetSize()) {
				Debug.Log("Invalid PacketSize: expected: "+readerPacket.GetSize()+" actual: "+buffer.Length);
				return;
			}*/
            readerPacket.Gather(this, reader);
        } catch (Exception e) {
            Debug.Log(e.ToString());
        }
    }

    private static object Lock = new object();

    /*** Writes a packet to the connected stream ***/
    // Sends a packet to this sessions assigned client stream
    public bool Write(int packetId, object[] args = default(object[])) {
        lock (Lock) {
            try {
                if (!writers.ContainsKey(packetId)) {
                    Debug.Log("Unable to send packet: " + packetId);
                    return false;
                }
                ByteStream stream = writers[packetId].Gather(args);
                if (stream != null) {
                    client.GetStream().Write(stream.buffer, 0, stream.buffer.Length);
                }
                return true;
            } catch (Exception e) {
                Debug.Log(e.ToString());
                return false;
            }
        }
    }

}
