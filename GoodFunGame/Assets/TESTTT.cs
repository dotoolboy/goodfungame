using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTTT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Main.UI.ShowPopupUI<UI_Popup_Option>();

        Main.UI.ShowPopupUI<UI_Popup_Pause>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
