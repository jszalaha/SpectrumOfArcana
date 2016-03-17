using UnityEngine;
using System.Collections;

public class StartMenuController : MonoBehaviour {

    Canvas startMenu;

	// Use this for initialization
	void Start () {
        startMenu = GetComponent<Canvas>();
        startMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Cancel"))
        {
            if(startMenu.enabled == false)
            {
                startMenu.enabled = true;
            } else
            {
                startMenu.enabled = false;
            }
        }
	}
}
