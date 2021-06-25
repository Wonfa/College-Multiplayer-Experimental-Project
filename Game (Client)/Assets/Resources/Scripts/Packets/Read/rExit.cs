using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Exit read packet ***/
public class rExit : ReadPacket {

    public override int GetId() {
        return 21;
    }

    public override int GetSize() {
        return 1;
    }

    /*** Resets the game to main menu ***/
    public override void Gather(Reader reader) {
        Main.Reset();
    }
}
