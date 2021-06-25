using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*** Handles a player in the game ***/
public class Player : MonoBehaviour {

    public static float ATTACK_SPEED = 2f;

    public float lastAttack = 0f;

    private float animChangeDelay = 0f;

    /*** Runs in quick succession ***/
    // if the escape key is pressed then send the exit packet to the server
    // if the player is presseing the left mouse button then send the attack packet to the server
    // handles animation priroity
    public void Update() {
        if (!Main.INGAME)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) && Main.INGAME) {
            Main.que.QueEvent(() => { PacketSender.Write(20); });
        }

        Animator anim = gameObject.GetComponent<Animator>();
        if (animChangeDelay < Time.time || animationType == 2) {

            if (anim.GetInteger("State") != animationType) {
                anim.SetInteger("State", animationType);
                if (animationType == 2) {
                    animChangeDelay = Time.time + (type == 0 ? (ATTACK_SPEED / 2) : ATTACK_SPEED);
                }
            }

            if (animChangeDelay < Time.time) {
                if (lastAnimUpdate + 0.2f < Time.time) {
                    anim.SetInteger("State", 0);
                    animationType = 0;
                }
            }
        }
    }

    public byte type = 0;
    public Slider hpBar { get; set; }

    /*** Checks if the two players are colliding ***/
    public static bool AttackRange() {
        return Main.GetPlayer().GetComponent<BoxCollider2D>().IsTouching(Main.GetOther().GetComponent<BoxCollider2D>());
    }

    private float lastAnimUpdate = 0f;
    private int animationType = 0;

    /*** Sets the next animation to queue for activation ***/
    public void SetAnimation(int type) {
        this.animationType = type;
        lastAnimUpdate = Time.time;
    }

    /*** Damages this player and then calls the respawn method if the player dies ***/
    public void Damage() {
        float hp = hpBar.value;
        float damage = type == 0 ? 10 : 20;

        if ((hp - damage) <= 0) {
            hpBar.value = 0;
            ReSpawn();
        } else {
            hpBar.value = hpBar.value - damage;
        }
    }

    /*** Handles the death of a player ***/
    public void ReSpawn() {
        // end game packet
        PacketSender.Write(20);
        if (this.type == 0)
            ButtonHandler.WINNER_TEXT.text = "Assassins Win!";
        else if (this.type == 1)
            ButtonHandler.WINNER_TEXT.text = "Guards Win!";
    }
}
