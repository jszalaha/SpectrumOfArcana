using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static InputManager inputManager;

    public GameObject player;
    public GameObject dialogue;
    public GameObject start;

    public GameObject itemListObjectPrefab;

    private MovementController playerScript;
    private ItemListController itemList;

    public enum UIState
    {
        PLAY,
        PAUSED,
        START,
        GAMEOVER
    };
    public UIState state = UIState.PLAY;
    public UIState prevState;

    public static InputManager Instance()
    {
        if (!inputManager)
        {
            inputManager = FindObjectOfType(typeof(InputManager)) as InputManager;
            if (!inputManager)
                Debug.LogError("There needs to be one active InputManager script on a GameObject in your scene.");
        }

        return inputManager;
    }

    void Awake()
    {
        playerScript = player.GetComponent<MovementController>();
        itemList = start.GetComponent<ItemListController>();
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case UIState.PAUSED:
                if (dialogue.activeSelf)
                {
                    if (Input.GetButtonUp("Fire1"))
                    {
                        dialogue.SetActive(false);
                        state = UIState.PLAY;
                    }
                }
                startMenuListener();
                break;
            case UIState.START:
                startMenuListener();
                break;
            case UIState.GAMEOVER:
                break;
            case UIState.PLAY:
                playerScript.Movement();
                playerScript.Raycasting();
                playerScript.Interact();
                startMenuListener();
                break;
        }
    }

    void startMenuListener()
    {
        if (state == UIState.PLAY || state == UIState.PAUSED) {
            if (Input.GetButtonUp("Cancel"))
            {
                prevState = state;
                state = UIState.START;
                start.SetActive(true);
            }
        }
        else if (state == UIState.START)
        {
            if (Input.GetButtonUp("Cancel"))
            {
                state = prevState;
                prevState = UIState.PLAY;
                start.SetActive(false);
            }
        }
    }

    public void itemListTest()
    {
        GameObject newItem = Instantiate(itemListObjectPrefab) as GameObject;
        newItem.transform.parent = itemList.content.transform;
    }
    public void itemListTest2()
    {
        GameObject newItem = Instantiate(itemListObjectPrefab) as GameObject;
        Transform newItemName = newItem.transform.GetChild(1);
        Transform newItemType = newItem.transform.GetChild(2);
        Transform newItemCount = newItem.transform.GetChild(3);
        newItemName.GetComponent<Text>().text = "Herbal Remedy";
        newItemType.GetComponent<Text>().text = "Consumable";
        newItemCount.GetComponent<Text>().text = "x 1";

        newItem.transform.parent = itemList.content.transform;
        
    }
}
