using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    public Dialogue DialogueSet;
    public TextMeshProUGUI TMP;
    public SpriteRenderer SpeakerSR;
    public UnityEvent OnComplete;
    private int lineIndex;
    private int lineCharacterCount;
    private bool finishedTypewriterEffect;
    private const float SPEED = 0.05f;

    public void Reset()
    {
        StopAllCoroutines();
        lineIndex = 0;
        lineCharacterCount = 0;
        finishedTypewriterEffect = true;
    }

    public void AdvanceLine()
    {
        if(!finishedTypewriterEffect)
        {
            StopAllCoroutines();
            TMP.maxVisibleCharacters = lineCharacterCount;
            finishedTypewriterEffect = true;
            return;
        }


        if(lineIndex >= DialogueSet.Lines.Count)
        {
            Complete();
            return;
        }

        DialogueElement Line = DialogueSet.Lines[lineIndex];
        if(Line.Speaker != null) SpeakerSR.sprite = Line.Speaker;
        TMP.text = Line.Text;
        lineCharacterCount = Line.Text.Length;
        TMP.maxVisibleCharacters = 0;
        lineIndex++;
        StartCoroutine(TypewriterEffect());
    }

    public void Complete()
    {
        OnComplete.Invoke();
    }

    private IEnumerator TypewriterEffect()
    {
        finishedTypewriterEffect = false;
        int visibleCharacters = 0;
        while(visibleCharacters <= lineCharacterCount)
        {
            TMP.maxVisibleCharacters = visibleCharacters;
            visibleCharacters++;
            yield return new WaitForSeconds(SPEED);
        }
        finishedTypewriterEffect = true;
    }
}
