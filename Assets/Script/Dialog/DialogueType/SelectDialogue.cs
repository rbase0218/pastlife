using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectDialogue : Dialogue
{
    
    public readonly string answer1;
    public readonly string answer2;

    public readonly string answer1SprPath;
    public readonly string answer2SprPath;

    public readonly int answer1NextDialogKey;
    public readonly int answer2NextDialogKey;
    
    public SelectDialogue(int key, List<string> text, string answer1, string answer2, string answer1SprPath,
        string answer2SprPath, int answer1NextDialogKey, int answer2NextDialogKey) : base(key, text)
    {
        this.answer1 = answer1;
        this.answer2 = answer2;
        this.answer1SprPath = answer1SprPath;
        this.answer2SprPath = answer2SprPath;
        this.answer1NextDialogKey = answer1NextDialogKey;
        this.answer2NextDialogKey = answer2NextDialogKey;
    }

    public void AddClickEvent()
    {
        
    }

    public void RemoveClickEvent()
    {
        
    }
}
