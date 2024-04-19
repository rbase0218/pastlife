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

    [SerializeField] private int _currentIndex = 0;

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

        SetDialogueState(DialogueState.Init);
    }

    public void StartDialogue()
    {
        StartDialogue(0);
    }

    public void StartDialogue(int index, bool isCoroutine = true)
    {
        if (_dialogueState == DialogueState.Playing) return;
        if (index >= dialogues.Count) return;

        SetDialogueState(DialogueState.Playing);
        _currentIndex = index;

        SetCurrentDialogue();

        if (isCoroutine)
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
        if (_dialogueState == DialogueState.Playing) StopCoroutine("StartText");
        dialogueText.text = textWriter.GetText();
    }

    private void SetText()
    {
        var isNull = textWriter.SetText(_currentDialogue.GetCurrentIndexText());
        
        if (isNull == 0)
            return;
    }

    private void WriteText()
    {
        StartCoroutine("StartText");
    }

    private IEnumerator StartText()
    {
        string copyText;

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

        SetDialogueState(DialogueState.End);
        StopCoroutine("StartText");
    }

    private void SetDialogueState(DialogueState state)
    {
        _dialogueState = state;
    }
}