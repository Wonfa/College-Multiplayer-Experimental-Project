using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*** Handles a lobby ***/
public class Lobby {

    /*** Adds a memeber session to this lobby ***/
    public void AddSession(Session session) {
        if (member != null)
            return;
        member = session;
    }

    /*** Removes a session from this lobby ***/
    public void RemoveSession(Session session) {
        if (session == owner) {
            Disband(1 | 2 | 4);
        } else if (session == member) {
            Disband(2);
        }
    }

    /*** Checks if the owner is still connected ***/
    public bool Null() {
        return !owner.IsConnected();
    }

    /*** Disbands a member or the whole lobby depending of the given flag ***/
    public void Disband(byte flag) {
        if ((flag & 1) == 1 && owner != null) {
            owner.Write(21);
            owner.SetLobby(null);
            owner.player = null;
            owner = null;
        }
        if ((flag & 2) == 2 && member != null) {
            member.Write(21);
            member.SetLobby(null);
            member.player = null;
            member = null;
        }
        if ((flag & 4) == 4) {
            lock (LobbyManager.lobbies) {
                LobbyManager.lobbies.Remove(this);
            }
        }
    }

    public Session owner { get; set; }

    public Session member { get; set; }

    public Lobby(Session owner) {
        this.owner = owner;
    }

}
