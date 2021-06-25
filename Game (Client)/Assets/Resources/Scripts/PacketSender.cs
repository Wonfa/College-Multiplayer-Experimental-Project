using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*** Handles all packets being sent to the connected server ***/
public class PacketSender : MonoBehaviour {

    public static Dictionary<int, WritePacket> writers;

    /*** This will be run on the launch of this application. ***/
    public static void Init() {
        Debug.Log("Setting up packet sender.");
        writers = new Dictionary<int, WritePacket>();

        try {
            PluginLoader.Load<int, WritePacket>(ref writers);
        } catch (Exception e) {
            Debug.Log(e);
        }
    }

    private static object Lock = new object();

    /*** Writes a packet to the stream connected to the server ***/
    public static bool Write(int packetId, object[] args = default(object[])) {
        lock (Lock) {
            try {
                if (!writers.ContainsKey(packetId)) {
                    Debug.Log("Unable to send packet: " + packetId);
                    return false;
                }
                ByteStream stream = writers[packetId].Gather(args);
                if (stream != null) {
                    ConnectionManager.MANAGER.client.GetStream().Write(stream.buffer, 0, stream.buffer.Length);
                }
                return true;
            } catch (Exception e) {
                Debug.Log(e.ToString());
                return false;
            }
        }
    }
}
