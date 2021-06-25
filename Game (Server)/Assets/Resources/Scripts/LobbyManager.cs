using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Handles the management of lobbies, such as their creation ***/
public class LobbyManager : MonoBehaviour {

    public static List<Lobby> lobbies = new List<Lobby>();

    /*** Handles the creation of a lobby ***/
    public static void CreateLobby(Session session, string name) {
        Lobby lobby = new Lobby(session);
        if (session.SetLobby(lobby)) {
            session.name = name;
            lobbies.Add(lobby);
            MainThread.games.Add(new Game(lobby));
        }
    }

    /*** Gets a lobby by its name ***/
    public static Lobby GetLobby(string name) {
        foreach (Lobby lobby in lobbies) {
            if (lobby.owner.name.Equals(name)) {
                return lobby;
            }
        }
        return null;
    }

    /*** Joins a session into the lobby with the given name ***/
    public static void JoinLobby(Session session, string name) {
        Lobby lobby = GetLobby(name);
        if (lobby != null && session.SetLobby(lobby)) {
            lobby.AddSession(session);
        }
    }

    /*** The given session leaves its lobby ***/
    public static void LeaveLobby(Session session) {
        Lobby lobby = session.GetLobby();

        if (lobby != null) {
            lobby.RemoveSession(session);
            lobbies.Remove(lobby);
        }
    }

    /*** Refreshes the given sessions lobby ***/
    // sends a packet to the given session which signals a lobby refresh
    public static void RefreshLobby(Session session) {
        session.Write(4);
    }
}
