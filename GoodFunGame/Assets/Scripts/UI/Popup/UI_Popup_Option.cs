using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class UI_Popup_Option : UI_Popup
{

    #region Enums

    enum GameObjects
    {
      MASTER,
      BGM,
      SFX
    }

    enum Buttons
    {
        BackspaceBtn
    }

    #endregion


    void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));

        GetObject((int)GameObjects.MASTER).GetComponent<Option_Audio>().Type = Option_Audio.Types.Master;
        GetObject((int)GameObjects.BGM).GetComponent<Option_Audio>().Type = Option_Audio.Types.BGM;
        GetObject((int)GameObjects.SFX).GetComponent<Option_Audio>().Type = Option_Audio.Types.SFX;


        GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(Close);

        return true;
    }

    void Close(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
    }
}
