using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

/*** Handles incomming connections ***/
public class ConnectionManager {

    public static readonly int PORT = 11000;
    public static readonly string IP = "127.0.0.1";

    public static ConnectionManager MANAGER;

    public List<Session> mainLobby { get; }
    private TcpListener listener;

    /*** Binds the server to the given port ***/
    public ConnectionManager() {
        this.mainLobby = new List<Session>();

        try {
            listener = new TcpListener(IPAddress.Any, PORT);
            ConnectionListener();
        } catch (Exception e) {
            Debug.Log("Failed to bind to port " + PORT);
            return;
        }

        Debug.Log("Server listening on port " + PORT);
    }

    public static Task connectionTask = null;

    /*** Creates a task that listens for incomming connections ***/
    // Also handles the creation of sessions
    public void ConnectionListener() {
        if (MainThread.RUNNING) {
            Debug.Log("Already running...");
            return;
        }

        MainThread.RUNNING = true;
        listener.Start();

        connectionTask = new Task(async () => {
            while (MainThread.RUNNING) {
                TcpClient socket = await listener.AcceptTcpClientAsync();
                lock (mainLobby) {
                    Debug.Log("Accepted connection from " + socket.ToString());
                    mainLobby.Add(new Session(socket));
                }
                connectionTask.Wait(1000);
            }
            Debug.Log("Stopped accepting connections...");
        });
        connectionTask.Start();
    }

    public void ReadListener() {

    }

    /*** Closes listenting resources ***/
    public void Close() {
        Debug.Log("Closing Connections");
        connectionTask.Dispose();
        listener.Stop();
    }
}
