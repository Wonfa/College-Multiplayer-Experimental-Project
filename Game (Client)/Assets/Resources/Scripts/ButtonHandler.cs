using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*** Handles all button presses and UI related tasks ***/
public class ButtonHandler : MonoBehaviour {

    public static GameObject CANVAS, BUTTON, INPUT, SLIDER, TEXT;

    public static InputField INPUT_TEXT;

    public static Text WINNER_TEXT;

    /*** Generates UI elements ***/
    public static void Init() {
        CANVAS = Resources.Load<GameObject>("Prefabs/Canvas");

        if (CANVAS != null) {
            CANVAS = GameObject.Instantiate(CANVAS, new Vector3(0, 0, 0), Quaternion.identity);
            CANVAS.transform.position = new Vector3(0, 0, 0);
        } else {
            Debug.Log("canvus NULL returning");
            return;
        }

        BUTTON = Resources.Load<GameObject>("Prefabs/Button");
        INPUT = Resources.Load<GameObject>("Prefabs/InputField");
        SLIDER = Resources.Load<GameObject>("Prefabs/Slider");
        TEXT = Resources.Load<GameObject>("Prefabs/Text");

        if (BUTTON != null) {
            WINNER_TEXT = CreateButton("Winner Text", -21, 150, 3).GetComponent<Text>();
            WINNER_TEXT.text = "";
            INPUT_TEXT = CreateButton("name", -21, 96, 0).GetComponent<InputField>();
            CreateButton("Create Lobby", -21, 63);
            CreateButton("Refresh", -21, 33);
            CreateButton("Exit Game", -21, -92);
        } else {
            Debug.Log("button NULL returning");
            return;
        }
    }

    /*** Based on the type given it will spawn a UI element at the given x and y coordinates ***/
    // a name is also given to this element
    public static GameObject CreateButton(string name, float x = 0, float y = 0, int type = 1) {
        GameObject[] types = { INPUT, BUTTON, SLIDER, TEXT };
        GameObject button = GameObject.Instantiate(types[type], new Vector3(0, 0, 0), Quaternion.identity);

        button.GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        button.transform.position = new Vector3(x, y, 0);

        if (button != null) {
            button.transform.SetParent(CANVAS.transform);
            button.gameObject.name = name;
            if (type != 2)
                button.GetComponentInChildren<Text>().text = name;
        } else {
            Debug.Log("new button NULL returning");
        }

        return button;
    }

    void Start() {
        GetComponent<Button>().onClick.AddListener(delegate {
            OnClick(gameObject.name.ToLower());
        });
    }

    /*** When a button is pressed; that button has an action based on its name ***/
    public void OnClick(string name) {
        switch (name) {

            case "leave lobby":
                Main.INGAME = false;
                PacketSender.Write(6);
                break;

            case "create lobby":
                Main.INGAME = true;
                WINNER_TEXT.text = "";
                Main.PINDEX = 0;
                Toggle(false);
                PacketSender.Write(1);
                break;

            case "refresh":
                PacketSender.Write(3);
                break;

            default:
                Debug.Log("Unhandled button: " + name);
                Main.INGAME = true;
                WINNER_TEXT.text = "";
                Main.PINDEX = 1;
                Toggle(false);
                PacketSender.Write(5, new object[] { name });
                break;
        }
    }

    private static GameObject[] previousElements;

    /*** Hides or un-Hides UI elements ***/
    public static void Toggle(bool toggle) {
        if (previousElements == null) {
            previousElements = GameObject.FindGameObjectsWithTag("Button");
        }
        foreach (GameObject element in previousElements) {
            if (element == null)
                continue;
            element.SetActive(toggle);
        }
    }
}
