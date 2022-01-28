using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    PlayerActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerActions();

        inputActions.Dialogue.Advance.performed += ctx => AdvanceDialogue();
    }

    [System.Serializable]
    public class DialoguePart
    {
        public string talker;
        public string text;
    }

    [SerializeField] DialoguePart[] dialogueSequences;

    Queue<DialoguePart> dialogueParts = new Queue<DialoguePart>();

    private void Start()
    {
        foreach (DialoguePart part in dialogueSequences)
        {
            dialogueParts.Enqueue(part);
        }
    }

    private void AdvanceDialogue()
    {
        if (dialogueParts.Count > 0)
        {
            DialoguePart next = dialogueParts.Dequeue();

        }
        else
        {
            //end dialogue
            SceneLoader.instance.StartSwitchingScene(1);
        }
    }
}
