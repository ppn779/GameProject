//18년 6월 30일 황재석

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingTextMng : MonoBehaviour
{
    public Text endingTextBox = null;

    private TextTrigger textTrigger = null;
    private Queue<string> sentences = null;

    private void Start()
    {
        textTrigger = gameObject.GetComponentInChildren<TextTrigger>();
        if (textTrigger == null)
        {
            Debug.LogError("엔딩텍스트매니져의 텍스트 트리거에 값이 없음");
            Debug.Break();
        }
        sentences = new Queue<string>();
        if (sentences == null)
        {
            Debug.LogError("엔딩텍스트매니져의 센텐스 is missing.");
            Debug.Break();
        }
        TypingEndingText(textTrigger.endingText);
    }

    public void TypingEndingText(EndingText _endingTexts)
    {
        sentences.Clear();

        foreach (string _sentence in _endingTexts.sentences)
        {
            sentences.Enqueue(_sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string _sentence)
    {
        endingTextBox.text = "";
        foreach (char letter in _sentence.ToCharArray())
        {
            endingTextBox.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
