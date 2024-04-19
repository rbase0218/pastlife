using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int key;
    public List<string> question;
    protected int _currentIndex;

    public Dialogue(int key, List<string> text)
    {
        this.key = key;
        this.question = text;
    }

    public int GetKey()
    {
        return key;
    }

    public string GetQuestionToIndex(int index)
    {
        return (index >= 0 && index < question.Count) ? question[index] : null;
    }

    public string GetFirstQuestion()
    {
        return question[0];
    }

    public int GetQuestionCount()
    {
        return question.Count;
    }

    public List<string> GetQuestion()
    {
        return question;
    }

    public string GetCurrentIndexText()
    {
        return GetQuestionToIndex(_currentIndex++);
    }

    public int GetCurrentIndex()
    {
        return _currentIndex;
    }
}