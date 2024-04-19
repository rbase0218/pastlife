using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public readonly string npcName;
    public readonly string text;

    public Dialogue(string name, string desc)
    {
        npcName = name;
        text = desc;
    }
}
