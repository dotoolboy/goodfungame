using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerField;
    [SerializeField] private TextMeshProUGUI contentField;

    private int _characterWrapLimit = 500;

    private RectTransform _rectTransform;
    private LayoutElement _layoutElement;
    private CanvasGroup _canvasGroup;

    private Coroutine _fadeCoroutine;

    private Vector2 _position;
    private float _pivotX;
    private float _pivotY;


    private void Awake()
    {
        _layoutElement = GetComponent<LayoutElement>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

    }
    void OnEnable()
    {
        _fadeCoroutine = StartCoroutine(Fade());

    }
    void OnDisable()
    {
        StopCoroutine(_fadeCoroutine);
    }

    void FixedUpdate()
    {
        MovePosition();
    }


    void MovePosition()
    {

        _position = Input.mousePosition;

        _pivotX = _position.x / Screen.width; //화면을 (0,0) (1,1)이라고 했을때 위치를 집어넣음 
        _pivotY = _position.y / Screen.height;

        _rectTransform.pivot = new Vector2(_pivotX, _pivotY);
        transform.position = _position;
    }

    IEnumerator Fade()
    {
        _canvasGroup.alpha = 0;

        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            _canvasGroup.alpha += Time.deltaTime * 10;

            yield return null;

            if (_canvasGroup.alpha >= 1) yield break;

        }
    }
    public void SetText(string content)
    {
        headerField.gameObject.SetActive(false);

        contentField.text = content;
        _layoutElement.enabled = (headerField.preferredWidth > _characterWrapLimit || contentField.preferredWidth > _characterWrapLimit);   // 텍스트 길면 LayoutElement 활성화해서 글에 자동으로 엔터넣기
    }

    public void SetText(string content, string header)
    {
        headerField.text = header;
        headerField.gameObject.SetActive(true);

        contentField.text = content;
        _layoutElement.enabled = (headerField.preferredWidth > _characterWrapLimit || contentField.preferredWidth > _characterWrapLimit);
    }


}


// headerLength = headerField.text.Length;
// contentLength = contentField.text.Length;
// layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
// 한글은 글자수로하면 정확하지 않은듯? 원래 lenght로 게산한거 preferredWidth로 바꿔버림
