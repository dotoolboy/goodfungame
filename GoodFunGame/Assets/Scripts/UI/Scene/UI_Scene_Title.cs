using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Scene_Title : UI_Scene
{
    #region Enums

    enum Buttons
    {
        StartButton,
    }

    #endregion

    void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnButtonStart);

        return true;
    }

    private void OnButtonStart(PointerEventData data)
    {
        Main.UI.CloseAllPopupUI();
        Main.Scene.LoadScene("MainScene");
    }
}