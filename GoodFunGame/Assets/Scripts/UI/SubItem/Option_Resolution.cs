using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class Option_Resolution : UI_Base
{


    #region Enums

    enum GameObjects
    {
        Dropdown,
        Toggle,
    }
    enum Buttons
    {

        PreviewBtn,
    }



    #endregion


    private TMP_Dropdown dropdown;
    private UnityEngine.UI.Toggle toggle;

    private List<Resolution> resolutions; //해상도 목록

    private int resolutionNumSet;
    private int resolutionNum; //해상도 고르고 취소할때 계산하는용

    private FullScreenMode screenMode;


    void Start()
    {
        Init();
    }

    int Gcd(int n, int m) // a와 b의 최대공약수 계산. 해상도 비율계산
    {
        //두 수 n, m 이 있을 때 어느 한 수가 0이 될 때 까지
        //gcd(m, n%m) 의 재귀함수 반복
        if (m == 0) return n;
        else return Gcd(m, n % m);
    }


    public override bool Init() 
    {
        if (!base.Init()) return false;
        resolutions = new List<Resolution>();

        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));



        GetButton((int)Buttons.PreviewBtn).gameObject.BindEvent(Preview);

        dropdown = GetObject((int)GameObjects.Dropdown).GetComponent<TMP_Dropdown>();
        toggle = GetObject((int)GameObjects.Toggle).GetComponent<UnityEngine.UI.Toggle>();



        // 해상도목록 만들어서 드롭박스에 추가

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutions.Add(Screen.resolutions[i]);
        }

        dropdown.options.Clear();

        for (int i = 0; i < resolutions.Count; i++)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();

            //해상도 비율계산
            option.text = resolutions[i].width + "x" + resolutions[i].height + " " +
                resolutions[i].width / Gcd(resolutions[i].width, resolutions[i].height) + ":" + resolutions[i].height / Gcd(resolutions[i].width, resolutions[i].height) + " " +
                resolutions[i].refreshRateRatio.value.ToString("F0") + "hz";

            dropdown.options.Add(option);
            dropdown.value = i;

        }

        dropdown.RefreshShownValue();

        return true;
    }

    public void Preview(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_ResolutionPreview>();
    }

    public void Dropbox(int num) 
    {
        resolutionNumSet = num;
        dropdown.value = num; // 해상도목록에 체크마크도 갱신

    }


    public void FullScreenToggle(bool On)
    {
        toggle.isOn = On;
        screenMode = On ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.fullScreen = On;
    }

    public void Save()
    {
    }

    public void Load()
    {

    }



    public void DefaultSetting()
    {

    }

}


