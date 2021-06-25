using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*** This is the main class of the client, it is where everything is initialised ***/
public class Main : MonoBehaviour {

    public static GameObject ASSASSIN, GUARD, CAMERA;

    public static byte PINDEX = 0;

    /*** Gets the gameobject either the assassin or guard, based off of what this client is playing ***/
    public static GameObject GetPlayer() {
        return PINDEX == 0 ? ASSASSIN : GUARD;
    }

    /*** Gets the gameobject either the assassin or guard, based off of the opposite that client is playing ***/
    public static GameObject GetOther() {
        return PINDEX == 0 ? GUARD : ASSASSIN;
    }

    public static bool INGAME = false;

    /*** Runs at the start of the application ***/
    // asks the client to be run in the background
    // runs Initialisers
    void Start() {
        Application.runInBackground = true;

        CAMERA = GameObject.FindGameObjectWithTag("MainCamera");

        ConnectionManager.MANAGER = new ConnectionManager("127.0.0.1", 11000);

        ButtonHandler.Init();
        que = new EventQue();
        Reset();
        MapBuilder.Init();
        PacketListener.Init();
        PacketSender.Init();
    }

    public static bool RUNNING = false;

    /*** Runs when the application is closed ***/
    public void OnApplicationQuit() {
        PacketSender.Write(20);
        try {
            ConnectionManager.MANAGER.Close();
        } catch (Exception e) {
            Debug.Log(e.ToString());
        }
        RUNNING = false;
    }

    public static EventQue que { get; set; }

    /*** Executes any events that are in the queue ***/
    public void FixedUpdate() {
        try {
            que.Execute();
        } catch (Exception e) {
            //Debug.Log(e.ToString());
            Debug.Log(e.StackTrace.ToString());
        }
    }

    /*** Processes a movement tick ***/
    public void Update() {
        Movement.Update();
    }

    public static Slider gHp, aHp;

    /*** Resets the game ***/
    public static void Reset() {
        que.QueEvent(() => {
            if (gHp == null)
                gHp = ButtonHandler.CreateButton("Player2HP", -390, (-Screen.height / 2) + 15, 2).GetComponent<Slider>();
            if (aHp == null)
                aHp = ButtonHandler.CreateButton("Player1HP", 390, (-Screen.height / 2) + 15, 2).GetComponent<Slider>();

            ButtonHandler.Toggle(true);
            INGAME = false;
            try {
                if (ASSASSIN != null) {
                    aHp.value = 100;
                    GameObject.Destroy(ASSASSIN);
                }
            } catch (Exception e) { }

            try {
                if (GUARD != null) {
                    gHp.value = 100;
                    GameObject.Destroy(GUARD);
                }
            } catch (Exception e) {
            }

            GameObject player = Resources.Load<GameObject>("Prefabs/Player");

            if (player == null)
                return;

            ASSASSIN = GameObject.Instantiate(player, new Vector3(-5, 0, -5), Quaternion.identity);
            ASSASSIN.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/assassin");
			
			GameObject cape = Resources.Load<GameObject>("Prefabs/Cape");
			
			if (cape != null) {
				GameObject insCape = GameObject.Instantiate(cape, new Vector3(0, 0, 0), Quaternion.identity);
				insCape.transform.SetParent(ASSASSIN.transform);
				insCape.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/assassin cape");
				insCape.transform.position = ASSASSIN.transform.position;
			}
			
            ASSASSIN.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            ASSASSIN.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites/Animations/assassin/controller");
            ASSASSIN.GetComponent<BoxCollider2D>().size = new Vector2(1.4f, 1.4f);
            ASSASSIN.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.4f);
            ASSASSIN.GetComponent<Player>().hpBar = aHp;

            GUARD = GameObject.Instantiate(player, new Vector3(5, 0, -5), Quaternion.identity);
            GUARD.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/guard");
			
			if (cape != null) {
				GameObject insCape = GameObject.Instantiate(cape, new Vector3(0, 0, 0), Quaternion.identity);
				insCape.transform.SetParent(GUARD.transform);
				insCape.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/guard cape");
				insCape.transform.position = new Vector3(5, 0, -4);
			}
			
            GUARD.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            GUARD.GetComponent<Player>().type = 1;
            GUARD.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites/Animations/guard/controller");
            GUARD.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 1.7f);
            GUARD.GetComponent<BoxCollider2D>().offset = new Vector2(0.15f, -0.15f);
            GUARD.GetComponent<Player>().hpBar = gHp;
        });
    }
}
