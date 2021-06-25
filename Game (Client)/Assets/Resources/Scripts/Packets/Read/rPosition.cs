using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Position read packet ***/
public class rPosition : ReadPacket {

    public static readonly float SPEED = 0.1f;

    public override int GetId() {
        return 10;
    }

    public override int GetSize() {
        return 1;
    }

    /*** Handles movement, animation setting, rotating and damage/attacking ***/
    public override void Gather(Reader reader) {
        Main.que.QueEvent(() => {
            int flag = reader.ReadByte();

            GameObject player = ((flag & 1) == 1) ? Main.GUARD : Main.ASSASSIN;

            player.GetComponent<Player>().SetAnimation(0);

            if ((flag & 2) == 2) {
                player.transform.position = InBox(player, Direction.FORWARD);
                player.GetComponent<Player>().SetAnimation(1);
            }

            if ((flag & 4) == 4) {
                player.transform.position = InBox(player, Direction.LEFT);
                player.GetComponent<Player>().SetAnimation(1);
            }

            if ((flag & 8) == 8) {
                player.transform.position = InBox(player, Direction.RIGHT);
                player.GetComponent<Player>().SetAnimation(1);
            }

            if ((flag & 16) == 16) {
                player.transform.position = InBox(player, Direction.BACKWOOD);
                player.GetComponent<Player>().SetAnimation(1);
            }

            if ((flag & 32) == 32) {
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (short) reader.ReadShort()));
            }

            if ((flag & 64) == 64) {
                Debug.Log("ANIMATION");
                player.GetComponent<Player>().SetAnimation(2);
            }

            if ((flag & 128) == 128) {
                Debug.Log("DAMAGE");
                player.GetComponent<Player>().Damage();
            }
        });
    }

    /*** Checks if the given gameobject is going to be in the map if it moves in the given direction ***/
    public Vector3 InBox(GameObject obj, Direction direction) {
        Vector3 newPos = obj.transform.position;
        if (direction == Direction.FORWARD) {
            newPos = new Vector3(obj.transform.position.x, obj.transform.position.y + SPEED, -5);
        } else if (direction == Direction.LEFT) {
            newPos = new Vector3(obj.transform.position.x - SPEED, obj.transform.position.y, -5);
        } else if (direction == Direction.RIGHT) {
            newPos = new Vector3(obj.transform.position.x + SPEED, obj.transform.position.y, -5);
        } else if (direction == Direction.BACKWOOD) {
            newPos = new Vector3(obj.transform.position.x, obj.transform.position.y - SPEED, -5);
        }

        return newPos.x > -8 && newPos.x < 8 && newPos.y > -3.5 && newPos.y < 3.5 ? newPos : obj.transform.position;
    }

    /*** An enum of possible directions ***/
    public enum Direction {
        FORWARD,
        LEFT,
        RIGHT,
        BACKWOOD
    }
}
