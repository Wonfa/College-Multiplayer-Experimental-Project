using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Handles a running game ***/
public class Game {

    private Lobby lobby;

    public Game(Lobby lobby) {
        this.lobby = lobby;
    }

    private float updateSpeed = 0f;
    private float lastUpdate = 0f;

    /*** Calls the UpdateSession method in quick succession ***/
    public void FixedUpdate() {
        //if (Time.time >= lastUpdate) {
            //lastUpdate = Time.time + updateSpeed;
        UpdateSession(lobby.owner, lobby.member);
        //}
    }

    private int CharacterUpdate = 0;

    /*** Each session connected will be updated with player propeties ***/
    // These properties will be sent to the client and will contain information
    // Information such as whether the player has moved or attacked
    public void UpdateSession(Session oSession, Session mSession) {
        if (oSession == null || mSession == null)
            return;

        if (++CharacterUpdate >= int.MaxValue) {
            CharacterUpdate = 0;
        }

        if ((CharacterUpdate & 1) == 1) {
            object rot = oSession.player.GetProperty("rotation");
            int oFlag = GetFlag(oSession.player);

            if (oFlag == 0 || oFlag == 1)
                return;

            oSession.Write(10, new object[] { oFlag, rot });
            mSession.Write(10, new object[] { oFlag, rot });
        } else {
            object rot2 = mSession.player.GetProperty("rotation");
            int mFlag = GetFlag(mSession.player);
            mFlag |= 1;

            if (mFlag == 0 || mFlag == 1)
                return;

            oSession.Write(10, new object[] { mFlag, rot2 });
            mSession.Write(10, new object[] { mFlag, rot2 });
        }
    }

    /*** Generates a flag based on player properties ***/
    // This flag will be sent to the clients
    public int GetFlag(Player player) {
        if (player == null)
            return 0;

        int flag = 0;
        if (player.HasProperty("player")) {
            flag |= 1;
        }

        if (player.HasProperty("forward")) {
            flag |= 2;
        }

        if (player.HasProperty("left")) {
            flag |= 4;
        }

        if (player.HasProperty("backward")) {
            flag |= 8;
        }

        if (player.HasProperty("right")) {
            flag |= 16;
        }

        if (player.HasProperty("rotation")) {
            flag |= 32;
        }

        if (player.HasProperty("animation")) {
            flag |= 64;
        }

        if (player.HasProperty("damaged")) {
            flag |= 128;
        }

        player.properties.Clear();

        return flag;
    }

    public void LeaveGame(Session session) {

    }
}
