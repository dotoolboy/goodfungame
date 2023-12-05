using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Select : UI_Scene
{
    void Start()
    {
        Main.UI.ShowPopupUI<UI_Popup_SelectMenu>();
    }
  
}
