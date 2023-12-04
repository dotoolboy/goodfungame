using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Popup_IntroMenu : UI_Popup
{
    #region Enums

    enum Texts
    {
        StartBtnText,
        GameNameText,
    }
    enum Images
    {
        LogoImage,
    }
    enum Buttons
    {
        StartButton,
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
        BindImage(typeof(Images));

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnButtonClicked);

        GetText((int)Texts.GameNameText).text = "I'm thinking about how to break down the Spartan Challenge gateway guarded by Hyun-ho, Eun-ha, Expedition, Se-jin, and Jung-hoon, and how to become a challenge. What is a game? What is development? I'm thinking about solving the mystery secret of the world of game development\r\nEating game!";
  
        GetText((int)Texts.StartBtnText).text = "game start";

    }

    public void OnButtonClicked(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Shop>();


    }
}
