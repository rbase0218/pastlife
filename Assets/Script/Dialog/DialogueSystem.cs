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
    [SerializeField] private float dialogueChangeSpeed = .0f;
    
    [SerializeField]
    private int _currentIndex = 0;
    
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

        dialogues = new List<Dialogue>()
        {
            new Dialogue("김철수", "안녕하세요?"),
            new Dialogue("김철수", "이것이 얼마나 잘못된 생각인지를.."),
            new Dialogue("김철수", "이렇게 하면 다형성까지 붙잡을 수 있어요!")
        };
        
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

    public void StartDialogue(int index, bool isCoroutine = true) {
        if (_dialogueState == DialogueState.Playing) return;
        if (index >= dialogues.Count) return;
        
        SetDialogueState(DialogueState.Playing);
        _currentIndex = index;
        
        SetCurrentDialogue();
        
        if(isCoroutine)
            WriteText();
    }

    private void SetCurrentDialogue()
    {
        _currentDialogue = dialogues[_currentIndex];
        NextDialogue();
        
        SetText();
    }

    private void NextDialogue()
    {
        if (_nextDialogue == null)
        {
            return;
        }

        _nextDialogue = (_currentIndex < dialogues.Count) ? dialogues[_currentIndex] : null;
    }

    private void SkipText()
    {
        if(_dialogueState == DialogueState.Playing) StopCoroutine("StartText");
        dialogueText.text = textWriter.GetText();
    }

    private void SetText()
    {
        textWriter.SetText(_currentDialogue.text);
    }

    private void WriteText()
    {
        StartCoroutine("StartText");
    }
    
    private IEnumerator StartText()
    {
        string copyText;

        for (int i = 0; i < dialogues.Count; ++i)
        {
            copyText = "";
            
            while (copyText != null)
            {
                dialogueText.text = copyText;
                yield return new WaitForSeconds(textPrintSpeed);
                copyText = textWriter.GetCopyText();
            }

            SetDialogueState(DialogueState.PlayEnd);
            StartDialogue(_currentIndex + 1, false);

            yield return new WaitForSeconds(dialogueChangeSpeed);
        }
        
        Debug.Log("Dialogue ENDED");
        SetDialogueState(DialogueState.End);
        StopCoroutine("StartText");
    }
    
    private void SetDialogueState(DialogueState state)
    {
        _dialogueState = state;
    }
}
