using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterText : MonoBehaviour
{

    [SerializeField] private float textSpeed;
    [SerializeField] private TextMeshProUGUI targetText;

    private string _fullText;
    private bool _isTyping;
    public bool isTyping => _isTyping;

    private Coroutine _typingCoroutine;

    private IEnumerator TypeCoroutine()
    {
        _isTyping = true;

        for (int i = 0; i < _fullText.Length; i++)
        {
            targetText.text+= _fullText[i];
            yield return new WaitForSeconds(textSpeed);
        }
        _isTyping = false;
    }
    
    public void Play(string text)
    {
        Stop();
        _fullText = text;
        targetText.text = "";
        _typingCoroutine = StartCoroutine(TypeCoroutine());
    }
    public void Stop()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }
        _isTyping = false;
        targetText.text = "";
    }
    public void Skip()
    {
        if (!_isTyping) return;
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }
      
        targetText.text = _fullText;
        _isTyping = false;
    }

}
