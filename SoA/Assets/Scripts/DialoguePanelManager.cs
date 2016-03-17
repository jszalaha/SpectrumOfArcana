using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class DialoguePanelManager : MonoBehaviour {

    public GameObject dialoguePanelObject;
    public Text outputText;
    public Image iconImage;

    public static DialoguePanelManager dialoguePanel;

    public static DialoguePanelManager Instance ()
    {
        if (!dialoguePanel)
        {
            dialoguePanel = FindObjectOfType(typeof(DialoguePanelManager)) as DialoguePanelManager;
            if (!dialoguePanel)
                Debug.LogError("There needs to be one active DialoguePanelManager script on a GameObject in your scene.");
        }

        return dialoguePanel;
    }

    void FixedUpdate ()
    {
    }

    public void Display (string text)
    {
        this.outputText.text = "";
        dialoguePanelObject.SetActive(true);
        this.outputText.text = text;
    } 

    void ClosePanel ()
    {
        dialoguePanelObject.SetActive(false);
    }
}
