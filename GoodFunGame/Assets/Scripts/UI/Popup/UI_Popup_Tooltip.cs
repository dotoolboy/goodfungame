using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class UI_Popup_Tooltip : UI_Popup
{
    #region Enums
    enum GameObjects
    {
        Tooltip,
        Header,
        Content
    }
    enum Texts
    {
        Header,
        Content
    }

    #endregion


    private int _characterWrapLimit = 500;

    private RectTransform _rectTransform;
    private LayoutElement _layoutElement;


    private CanvasGroup _canvasGroup;

    private Vector2 _position;
    private float _pivotX;
    private float _pivotY;


    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
       
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 0.2f);
    }


    void FixedUpdate()
    {
        MovePosition();
    }

    public override bool Init()
    {

        if (!base.Init()) return false;
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        _rectTransform = GetObject((int)GameObjects.Tooltip).gameObject.GetComponent<RectTransform>();
        _layoutElement = GetObject((int)GameObjects.Tooltip).gameObject.GetComponent<LayoutElement>();
        _canvasGroup = GetObject((int)GameObjects.Tooltip).gameObject.GetComponent<CanvasGroup>();

        _position = Input.mousePosition;



      //  this.gameObject.GetComponent<UI_EventHandler>().OnPointerEnterHandler -= null;
      //  this.gameObject.GetComponent<UI_EventHandler>().OnPointerEnterHandler += null;

        //    this.gameObject.GetComponent<UI_EventHandler>().OnPointerExitHandler -= Hide();
        //   this.gameObject.GetComponent<UI_EventHandler>().OnPointerExitHandler += Hide();

       

        return true;
    }





    void MovePosition()
    {
        _position = Input.mousePosition;

        _pivotX = _position.x / Screen.width;
        _pivotY = _position.y / Screen.height;

        _rectTransform.pivot = new Vector2(_pivotX, _pivotY);
        GetObject((int)GameObjects.Tooltip).transform.position = _position;
    }

    public void Show(string content)
    {
        GetText((int)Texts.Content).text = content;
        GetObject((int)GameObjects.Header).gameObject.SetActive(false);

        _layoutElement.enabled = GetText((int)Texts.Content).preferredWidth > _characterWrapLimit;   // 텍스트길면 LayoutElement 활성화해서 글에 자동으로 엔터넣기

        GetObject((int)GameObjects.Tooltip).gameObject.SetActive(true);

    }


    public void Show(string content, string header)
    {

        GetText((int)Texts.Header).text = header;
        GetText((int)Texts.Content).text = content;
        GetObject((int)Texts.Content).gameObject.SetActive(true);

        _layoutElement.enabled = GetText((int)Texts.Header).preferredWidth > _characterWrapLimit || (GetText((int)Texts.Content)).preferredWidth > _characterWrapLimit;

    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, 0.2f);
        Main.UI.ClosePopupUI(this);
    }



}

