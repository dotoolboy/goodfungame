using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Popup_Button : UI_Popup
{

    // 마우스올렸을때 글씨 띄울놈들한테 달아야할 스크립트.


     string header = "테스트";
     string content = "설명입니다";


    enum Buttons
    {
        Button // 하이어라키에서 이 이름을 가진 오브젝트를 찾는거라 오브젝트명 다르면 NULL뜬다
    }

    enum Texts
    {
        Text,
    }
    enum GameObjects
    {
        UI_Popup_Button,
    }

    private void Start()
    {
        // init 실행
        Init();
    }



    public override void Init()
    {
        base.Init();


        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));



        GetButton((int)Buttons.Button).gameObject.BindEvent(OnButtonClicked);

    }


    public void OnButtonClicked(PointerEventData data)
    {

       Debug.Log("클릭했다");

       Main.UI.ShowPopupUI<UI_Popup_Tooltip>();
        

    }
}
