using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    private enum DialogueState
    {
        Init,
        Wait,
        Playing,
        PlayEnd,
        End
    }
    
    [SerializeField] private List<Dialogue> dialogues;
    [SerializeField] private TextWriter textWriter;

    private Dialogue _currentDialogue;
    private Dialogue _nextDialogue;
    private DialogueState _dialogueState;

    [SerializeField] private float textPrintSpeed = .0f;
    private int _currentIndex = 0;
    private string _exampleString = "나는 어렸을 적에 무언가를 아니 아니 아니 이게 맞는지 모르겠지만, 어쨌든 내가 코딩을 해~";
    
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text dialogueName;

    private void Awake()
    {
        if (dialogueText == null)
        {
            Debug.Log($"Dialogue Text가 존재하지 않음.");
            Debug.Break();
        }
        
        // if (dialogueName == null)
        // {
        //     Debug.Log($"Dialogue Name이 존재하지 않음.");
        //     Debug.Break();
        // }
        
        dialogues = new List<Dialogue>();
        
        dialogues.Add(new Dialogue("김철수", _exampleString));
        SetDialogueState(DialogueState.Init);
    }

    private void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        StartDialogue(0);
    }

    public void StartDialogue(int index)
    {
        if (_dialogueState == DialogueState.Playing) return;
        SetDialogueState(DialogueState.Playing);
        _currentIndex = index;
        
        SetCurrentDialogue();
        WriteText();
    }

    private void SetCurrentDialogue()
    {
        _currentDialogue = dialogues[_currentIndex];
        NextDialogue();
    }

    private void NextDialogue()
    {
        if (_nextDialogue == null)
        {
            return;
        }

        _nextDialogue = (_currentIndex < dialogues.Count) ? dialogues[_currentIndex + 1] : null;
    }

    private void WriteText()
    {
        textWriter.SetText(_currentDialogue.text);
        StartCoroutine("StartText");
    }

    private IEnumerator StartText()
    {
        string copyText = "";

        while (copyText != null)
        {
            dialogueText.text = copyText;
            yield return new WaitForSeconds(textPrintSpeed);
            copyText = textWriter.GetCopyText();
        }
        
        StopCoroutine("StartText");
        SetDialogueState(DialogueState.PlayEnd);

        NextDialogue();

        if (_nextDialogue == null)
        {
            SetDialogueState(DialogueState.End);
            StopCoroutine("StartText");
            yield break;
        }

        StartCoroutine("StartText");
    }
    
    private void SetDialogueState(DialogueState state)
    {
        _dialogueState = state;
    }
}
