using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_ResolutionPreview : UI_Popup
{

    private float countdown;
    private WaitForSeconds oneSec = new WaitForSeconds(1f);

    #region Enums
    enum Texts
    {
        CountdownText,
    }

    enum Buttons
    {
        OkBtn,
        NoBtn,
    }



    #endregion
    void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.OkBtn).gameObject.BindEvent(OK);
        GetButton((int)Buttons.NoBtn).gameObject.BindEvent(NOPE);

        return true;
    }


    void OnEnable() { }

    IEnumerator PreviewCountdown()
    {

        //  GetObject((int)GameObjects.Preview).gameObject.SetActive(true);

        countdown = 10;

        GetText((int)Texts.CountdownText).text = countdown.ToString();

        while (true)
        {
            if (countdown <= 0) //0초되면 해상도변경취소
            {
                NOPE(null); // null 되나?
                break;
            }

            yield return oneSec;

            countdown--;

            GetText((int)Texts.CountdownText).text = countdown.ToString();


        }
    }
    public void OK(PointerEventData data)
    {
        StopAllCoroutines();

        Debug.Log("해상도 적용");

        Main.UI.ClosePopupUI(this);
    }

    public void NOPE(PointerEventData data)
    {
        StopAllCoroutines();

        Debug.Log("해상도 적용취소");

        Main.UI.ClosePopupUI(this);

    }


}
