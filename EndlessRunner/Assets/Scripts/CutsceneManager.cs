using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    PlayerActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerActions();

        inputActions.Dialogue.Advance.performed += ctx => Advance();
    }

    [SerializeField] PlayableAsset[] cutsceneParts;
    [Space]
    [SerializeField] TextMeshProUGUI topText;
    [SerializeField] TextMeshProUGUI midText;
    [SerializeField] TextMeshProUGUI bottomText;

    Queue<PlayableAsset> cutsceneQueue = new Queue<PlayableAsset>();
    PlayableDirector timeline;

    private void Start()
    {
        inputActions.Dialogue.Disable();
        timeline = GetComponent<PlayableDirector>();
        foreach (PlayableAsset item in cutsceneParts)
        {
            cutsceneQueue.Enqueue(item);
        }
    }

    public void WaitForInput()
    {
        inputActions.Dialogue.Enable();
    }

    public void ChangeTopText(string newText)
    {
        topText.text = newText;
    }

    public void ChangeMidText(string newText)
    {
        midText.text = newText;
    }

    public void ChangeBottomText(string newText)
    {
        bottomText.text = newText;
    }

    void Advance()
    {
        inputActions.Dialogue.Disable();
        if (cutsceneQueue.Count > 0)
        {
            PlayableAsset nextPlayable = cutsceneQueue.Dequeue();

            timeline.playableAsset = nextPlayable;
            timeline.Play();
        }
        else
        {
            SceneLoader.instance.StartSwitchingScene(1);
        }
    }
}
