using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class Option_Resolution : UI_Base
{


    #region Enums

    enum GameObjects
    {
        Preview,
        Dropdown,
        Toggle,
    }
    enum Buttons
    {
        NoBtn,
        OkBtn,
        PreviewBtn,
    }

    enum Texts
    {
        CountdownText
    }


    #endregion


    private TMP_Dropdown dropdown;
    private Toggle toggle;

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
        BindText(typeof(Texts));


        GetButton((int)Buttons.OkBtn).gameObject.BindEvent(Yes);
        GetButton((int)Buttons.NoBtn).gameObject.BindEvent(Nope);



        GetButton((int)Buttons.PreviewBtn).gameObject.BindEvent(Preview);

        GetObject((int)GameObjects.Preview).gameObject.SetActive(false);


        dropdown = GetObject((int)GameObjects.Dropdown).GetComponent<TMP_Dropdown>();
        toggle = GetObject((int)GameObjects.Toggle).GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(FullScreenToggle);
        dropdown.onValueChanged.AddListener(Dropbox);



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

        Refresh();

        return true;
    }

    void Refresh()
    {
        toggle.isOn = Screen.fullScreen ? true : false;
        dropdown.value = resolutions.Count - 1;
        Dropbox(resolutions.Count - 1);
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



    public void Preview(PointerEventData data)
    {
        Screen.SetResolution(resolutions[resolutionNumSet].width, resolutions[resolutionNumSet].height, screenMode);

        StartCoroutine(PreviewCountdown());
        GetObject((int)GameObjects.Preview).gameObject.SetActive(true);

    }

    IEnumerator PreviewCountdown()
    {
        float countdown = 10;

        GetText((int)Texts.CountdownText).text = countdown.ToString();

        while (true)
        {
            if (countdown <= 0) //0초되면 해상도변경취소
            {
                Nope(null); // null 되나?
                break;
            }

            yield return new WaitForSecondsRealtime(1f);

            countdown--;

            GetText((int)Texts.CountdownText).text = countdown.ToString();


        }
    }



    public void Yes(PointerEventData data) // 해상도 적용할때
    {
        StopAllCoroutines();
        AudioController.Instance.SFXPlay(SFX.Button);
        dropdown.value = resolutionNum;
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);


        GetObject((int)GameObjects.Preview).gameObject.SetActive(false);
    }

    public void Nope(PointerEventData data) // 해상도 취소할때
    {
        StopAllCoroutines();
        resolutionNum = resolutionNumSet;
        dropdown.value = resolutionNum;
        AudioController.Instance.SFXPlay(SFX.Button);


        GetObject((int)GameObjects.Preview).gameObject.SetActive(false);
    }

    public void DefaultSetting()
    {

        // 해상도 저장할땐 resolutionNum 저장
        // 로드 Screen.SetResolution(resolutions[resolutionNumSet].width, resolutions[resolutionNumSet].height, screenMode);
    }

}


