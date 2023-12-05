using UnityEngine;
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


    private Vector2 _position;
    private float _pivotX;
    private float _pivotY;


    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        GetObject((int)GameObjects.Tooltip).gameObject.SetActive(true);
    }


    void FixedUpdate()
    {
        MovePosition();
    }

    public override void Init()
    {

        base.Init();
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        GetObject((int)GameObjects.Tooltip).gameObject.SetActive(true);

        _rectTransform = GetObject((int)GameObjects.Tooltip).gameObject.GetComponent<RectTransform>();
        _layoutElement = GetObject((int)GameObjects.Tooltip).gameObject.GetComponent<LayoutElement>();

     //   this.gameObject.BindEvent(null, type: UIEvent.PointerEnter);

        _position = Input.mousePosition;
        GetObject((int)GameObjects.Tooltip).gameObject.SetActive(false);




    }


    void MovePosition()
    {
        _position = Input.mousePosition;

        _pivotX = _position.x / Screen.width; //화면을 (0,0) (1,1)이라고 했을때 위치를 집어넣음 
        _pivotY = _position.y / Screen.height;

        _rectTransform.pivot = new Vector2(_pivotX, _pivotY);
        GetObject((int)GameObjects.Tooltip).transform.position = _position;
    }

    public void Show(string content)
    {
        GetText((int)Texts.Content).text = content;
        GetObject((int)GameObjects.Header).gameObject.SetActive(false);

        _layoutElement.enabled = GetText((int)Texts.Content).preferredWidth > _characterWrapLimit;   // 텍스트 길면 LayoutElement 활성화해서 글에 자동으로 엔터넣기

        GetObject((int)GameObjects.Tooltip).gameObject.SetActive(true);

    }


    public void Show(string content, string header)
    {

        GetText((int)Texts.Header).text = header;
        GetText((int)Texts.Content).text = content;
        GetObject((int)Texts.Content).gameObject.SetActive(true);

        _layoutElement.enabled = GetText((int)Texts.Header).preferredWidth > _characterWrapLimit || (GetText((int)Texts.Content)).preferredWidth > _characterWrapLimit;

        GetObject((int)GameObjects.Tooltip).gameObject.SetActive(true);

    }

    public void Hide()
    {
        GetObject((int)GameObjects.Tooltip).gameObject.SetActive(false);
    }



}

