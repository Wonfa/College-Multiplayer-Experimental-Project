using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** The main utilities ***/
public class MainThread : MonoBehaviour {

    public static List<Game> games = new List<Game>();

    /*** Runs on application Start ***/
    public void Start() {
        Application.runInBackground = true;
        ConnectionManager.MANAGER = new ConnectionManager();
        Session.Init();
    }

    public static bool RUNNING;

    /*** Method that updates with quick succession ***/
    // closes application if it should not be running
    // closes session if not connected
    // disbands invalid/empty lobbies
    public void Update() {
        if (!RUNNING) {
            Application.Quit();
            return;
        }

        syncs: lock (ConnectionManager.MANAGER.mainLobby) {
            foreach (Session session in ConnectionManager.MANAGER.mainLobby) {
                if (!session.IsConnected()) {
                    session.Close();
                    goto syncs;
                }
            }
        }

        syncl: lock (LobbyManager.lobbies) {
            foreach (Lobby lobby in LobbyManager.lobbies) {
                if (lobby.Null()) {
                    lobby.Disband(1 | 2 | 4);
                    goto syncl;
                }
            }
        }
    }

    public void FixedUpdate() {
        foreach (Game game in games) {
            game.FixedUpdate();
        }
    }

    /*** Runs on application quit ***/
    public void OnApplicationQuit() {
        RUNNING = false;
        if (ConnectionManager.MANAGER != null)
            ConnectionManager.MANAGER.Close();
    }

}
