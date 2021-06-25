using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

/*** Handles the connections between the client and server ***/
public class ConnectionManager {

    public static ConnectionManager MANAGER;

    public TcpClient client { get; set; }

    /*** Attempts to connect to the server ***/
    public ConnectionManager(string hostAddress, int port) {

        Task task = null;

        task = new Task(() => {
            try {
                Debug.Log("Attempting Connection...");
                client = new TcpClient(hostAddress, port);
                Debug.Log("Connection Established.");
                Main.RUNNING = true;
            } catch (Exception e) {
                Debug.Log("Failed Connection...");
            }
        });

        task.Start();
    }

    /*** Closes connections and resources ***/
    public void Close() {
        Debug.Log("Closing Connections");
        try {
            client.Dispose();
        } catch (Exception e) {

        }
        if (client != null)
            client.Close();
    }
}
