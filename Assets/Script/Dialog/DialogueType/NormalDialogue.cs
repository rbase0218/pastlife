using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NormalDialogue : Dialogue
{
    public NormalDialogue(int key, List<string> text) : base(key, text)
    {
    }
}
