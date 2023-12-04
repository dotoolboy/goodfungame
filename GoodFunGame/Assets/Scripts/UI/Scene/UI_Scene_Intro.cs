using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Intro : UI_Scene
{
    void Start()
    {
        // 배경만, 메뉴는 팝업로고

        Main.UI.ShowPopupUI<UI_Popup_IntroMenu>();
    }

  
}
