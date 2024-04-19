using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    private string _copyText;
    private string _originText;

    private int _textIndex = 0;

    public void SetText(string text)
    {
        _originText = text;

        Init();
    }

    public string GetCopyText()
    {
        if (_originText == null) return null;
        if (_textIndex >= _originText.Length) return null;

        _copyText += _originText[_textIndex++];
        
        return _copyText;
    }

    private void Init()
    {
        _copyText = "";
        _textIndex = 0;
    }
}
