using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup
{

    enum Buttons
    {
        PointButton // 하이어라키에서 이 이름을 가진 오브젝트를 찾는거라 오브젝트명 다르면 NULL뜬다
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }
    enum GameObjects
    {
        GameObjectTest,
    }

    enum Images
    {
        ItemIcon
    }
    private void Start()
    {
        Init();
    }



    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));


        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);


        //  Get<TextMeshProUGUI>((int)Texts.ScoreText).text = "바인딩한걸 겟!";
        //  Get<TextMeshProUGUI>((int)Texts.PointText).text = "버튼 글씨도 바꿔볼까?";

        //  Get<GameObject>((int)GameObjects.GameObjectTest).SetActive(false); // 어캐쓰냐 겜오브젝트는..

    }

 
    int _score = 0;

    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
    }
}
