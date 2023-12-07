using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Popup_SelectMenu : UI_Popup
{

    #region Enums

    enum Texts
    {
        ShopBtnText,
        StatusBtnText,
        PlayBtnText,
        IntroduceText,

    }
 
    enum Buttons
    {
        ShopBtn,
        StatusBtn,
        OptionBtn,
        PlayBtn,

    }

    #endregion

    #region MonoBehaviours

    void Start()
    {
        Init();
    }

    #endregion

    #region Initialize

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.ShopBtn).gameObject.BindEvent(OnBtnShop);
        GetButton((int)Buttons.StatusBtn).gameObject.BindEvent(OnBtnStatus);
        GetButton((int)Buttons.OptionBtn).gameObject.BindEvent(OnBtnSettings);
        GetButton((int)Buttons.PlayBtn).gameObject.BindEvent(OnBtnPlayGame);

        return true;
    }

    #endregion

    #region OnButtons

    public void OnBtnShop(PointerEventData data)
    {
        AudioController.Instance.SFXPlay(SFX.Button);
        Main.UI.ShowPopupUI<UI_Popup_Shop>();
    }
    public void OnBtnStatus(PointerEventData data)
    {
        AudioController.Instance.SFXPlay(SFX.Button);
        Main.UI.ShowPopupUI<UI_Popup_Status>();
    }
    public void OnBtnSettings(PointerEventData data)
    {
        AudioController.Instance.SFXPlay(SFX.Button);
        Main.UI.ShowPopupUI<UI_Popup_Option>();
    }
    public void OnBtnPlayGame(PointerEventData data)
    {
        AudioController.Instance.SFXPlay(SFX.Button);
        Main.UI.CloseAllPopupUI();
        Main.Scene.LoadScene("GameScene");
    }
    
    #endregion
}
