using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*** Movement write packet ***/
public class Movement : WritePacket {

    public override int GetId() {
        return 11;
    }

    /*** Writes movement information to a byte array ***/
    public override ByteStream Gather(object[] args) {
        Writer writer = new Writer();
        writer.WriteByte(GetId());
        writer.WriteByte((int) args[0]);
        if ((((int) args[0]) & 16) == 16) {
            writer.WriteShort((short) args[1]);
        }
        return new ByteStream(writer.buffer);
    }

    /*** Gets current keypresses and generates a flag based off of this ***/
    // The flag is used to see the directions that the player should move and will be sent to the server
    public static void Update() {
        if (!Main.INGAME)
            return;

        int flag = 0;
        if (Input.GetKey(KeyCode.W)) {
            flag |= 1;
        }

        if (Input.GetKey(KeyCode.A)) {
            flag |= 2;
        }

        if (Input.GetKey(KeyCode.S)) {
            flag |= 4;
        }

        if (Input.GetKey(KeyCode.D)) {
            flag |= 8;
        }

        try {
            Player player = Main.GetPlayer().GetComponent<Player>();
            if (player != null) {
                if (Input.GetKeyDown(KeyCode.Mouse0) && player.lastAttack <= Time.time) {
                    player.lastAttack = Time.time + (player.type == 0 ? (Player.ATTACK_SPEED / 2) : Player.ATTACK_SPEED);
                    flag |= 32;

                    if (Player.AttackRange()) {
                        flag |= 64;
                    }
                }
            }
        } catch (Exception e) { }

        short rot = GetRotation();

        if (rot != PREV_ROTATION) {
            PREV_ROTATION = rot;
            flag |= 16;
        }

        if (flag != 0)
            PacketSender.Write(11, new object[] { flag, rot });
    }

    public static short PREV_ROTATION = 0;

    /*** Gets the rotation of the character ***/
    public static short GetRotation() {
        Vector3 rotation = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Main.GetPlayer().transform.position;
        rotation.Normalize();

        float zValue = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        short zRemain = (short) (zValue % 360);

        if (zRemain == 0)
            zRemain = 360;
        else if (zRemain == 360)
            zRemain = 0;

        zRemain -= 90;


        return zRemain;
    }
}
