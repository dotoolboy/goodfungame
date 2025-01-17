using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Scene_Select : UI_Scene
{

    #region Enums
    enum Texts
    {
        SoloText,
        MultipleText,
    }

    enum Buttons
    {
        Select_Btn_Solo,
        Select_Btn_Multiple
    }
 
    #endregion
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.Select_Btn_Solo).gameObject.BindEvent(Solo);
        GetButton((int)Buttons.Select_Btn_Multiple).gameObject.BindEvent(Multi);


    }

    public void Solo(PointerEventData data)
    {
        Debug.Log("솔플");
    }
    public void Multi(PointerEventData data)
    {
        Debug.Log("멀티플");
    }
}
