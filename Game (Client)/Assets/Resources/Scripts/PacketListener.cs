using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

/*** Handles the listening for incomming packets ***/
public class PacketListener {

    public static Dictionary<int, ReadPacket> readers;

    /*** Sets up the stream asyncronous listeners ***/
    public static void Init() {
        Debug.Log("Setting up packet listener.");
        readers = new Dictionary<int, ReadPacket>();

        try {
            PluginLoader.Load<int, ReadPacket>(ref readers);
        } catch (Exception e) {
            Debug.Log(e);
        }

        Task task = null;
        task = new Task(async () => {
            while (Main.RUNNING) {
                if (!Main.RUNNING) {
                    break;
                }
                byte[] buffer = new byte[1024];
                try {
                    int length = await ConnectionManager.MANAGER.client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                    if (length > 0) {
                        decode(buffer);
                    }
                } catch (Exception e) {
                    Debug.Log(e.ToString());
                }
            }
            Debug.Log("Closed Read");
            task.Dispose();
        });
        task.Start();
    }

    private static object Lock = new object();

    /*** Decodes an incomming packet and handles it ***/
    public static void decode(byte[] buffer) {
        lock (Lock) {
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
                readerPacket.Gather(reader);
            } catch (Exception e) {
                Debug.Log(e.ToString());
            }
        }
    }
}
