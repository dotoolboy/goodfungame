using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Option_Resolution : MonoBehaviour
{

    private Animator animator;

    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;

    private List<Resolution> resolutions; //해상도 목록

    private int resolutionNumSet;
    private int resolutionNum; //해상도 고르고 취소할때 계산하는용

    private FullScreenMode screenMode; //Screen.fullScreenMode는 아예 걍 이 프로그램의 게임창을 말하는거같아서 오류 많이남. 변수처럼쓰려면 저장해놓고 써야할듯.


    // ---------아래는 해상도 적용 확인창 용

    public GameObject ResolutionQestion; // 해상도 적용할건지 확인창
    public TMP_Text countdownTxt;
    private float countdown;
    private WaitForSeconds oneSec;



    public void Awake()
    {
        animator = GetComponent<Animator>();
        resolutions = new List<Resolution>();
        oneSec = new WaitForSeconds(1f);
    }
    void Start()
    {
        Init();
        ResolutionQestion.SetActive(false);

    }


    int Gcd(int n, int m) // a와 b의 최대공약수 계산. 해상도 비율계산
    {
        //두 수 n, m 이 있을 때 어느 한 수가 0이 될 때 까지
        //gcd(m, n%m) 의 재귀함수 반복
        if (m == 0) return n;
        else return Gcd(m, n % m);
    }

    //=======================================================================


    void Init() // 해상도목록 만들어서 드롭박스에 추가
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutions.Add(Screen.resolutions[i]);
        }

        resolutionDropdown.options.Clear();


        for (int i = 0; i < resolutions.Count; i++)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();

            //해상도 비율계산
            option.text = resolutions[i].width + "x" + resolutions[i].height + " " +
                resolutions[i].width / Gcd(resolutions[i].width, resolutions[i].height) + ":" + resolutions[i].height / Gcd(resolutions[i].width, resolutions[i].height) + " " +
                resolutions[i].refreshRateRatio.value.ToString("F0") + "hz";

            //refreshRate말고 refreshRateRatio 쓰라고 비쥬얼스튜디오가 그러는데 저거쓰면 59.213123123이런 주파수도 나와서 난감함. 그래서 소수점짜름

            resolutionDropdown.options.Add(option);
            resolutionDropdown.value = i;

        }

        resolutionDropdown.RefreshShownValue();

    }




    public void Resolution_Dropbox(int num) // 해상도 드롭박스에 다이나믹 연결3+1  / 해상도 목록에서 선택만 하는거고 아래 Resolution_Preview() 에서 적용 누른 후에 제대로 해상도 변경됨. 드롭박스 온클릭에 이거랑 프리뷰 둘다 달려있음
    {
        resolutionNumSet = num;
        resolutionDropdown.value = num; // 해상도목록에 체크마크도 갱신

    }



    public void Save()
    {
        resolutionNum = resolutionNumSet;
        resolutionDropdown.value = resolutionNum;

      //  SaveManager.Instance.userData.gameScreenSizeNumber = resolutionNum;
      //  SaveManager.Instance.SaveUserDataToJson();
    }

    public void Load()
    {
        /*

        if (SaveManager.Instance.userData.gameScreenSizeNumber >= resolutions.Count + 10) // 해상도숫자가 해상도목록 갯수보다 크면 기본해상도
        {
            resolutionNum = resolutions.Count - 1;
            Resolution_Dropbox(resolutions.Count - 1); //세이브에 해상도 기본값이면 젤 큰 화면으로
        }
        else
        {
            resolutionNum = SaveManager.Instance.userData.gameScreenSizeNumber; //계산용 변수도 갱신해주기
            Resolution_Dropbox(SaveManager.Instance.userData.gameScreenSizeNumber);
        }

        */
    }




    //---------------------해상도 적용 확인창 -----------------------

    public void Resolution_Preview() // 해상도 목록 옆에 적용 버튼에 달것
    {
        Screen.SetResolution(resolutions[resolutionNumSet].width, resolutions[resolutionNumSet].height, screenMode);

        StartCoroutine("PreviewCountdown");

    }
    IEnumerator PreviewCountdown()
    {
        ResolutionQestion.SetActive(true);

        countdown = 10;
        countdownTxt.text = countdown.ToString();

        while (true)
        {
            if (countdown <= 0) //0초되면 해상도변경취소
            {
                Resolution_NOPE();
                break;
            }

            yield return oneSec;

            countdown--;
            countdownTxt.text = countdown.ToString();

        }
    }
    public void Resolution_OK() // 해상도 맘에드냐 창에서 "네" 해상도 적용하고 저장
    {
        StopAllCoroutines();
        Save();

        ResolutionQestion.SetActive(false);
    }

    public void Resolution_NOPE() // 해상도 맘에드냐 창에서 "아니요"
    {
        StopAllCoroutines();

        resolutionDropdown.value = resolutionNum;
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);

        ResolutionQestion.SetActive(false);
    }

    public void FullScreenToggle(bool On) // 전체화면 토글에 다이나믹 연결
    {
        fullScreenToggle.isOn = On;
        screenMode = On ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.fullScreen = On;

     //   SaveManager.Instance.userData.isFullscreen = On;
     //   SaveManager.Instance.SaveUserDataToJson();
    }



    public void DefaultSetting()
    {

     //   FullScreenToggle(dataCenter.isFullscreen);

        resolutionDropdown.value = resolutions.Count - 1;

        Resolution_Dropbox(resolutions.Count - 1); //세이브에 해상도 기본값이면 젤 큰 화면으로

        Save();

        Screen.SetResolution(resolutions[resolutionNumSet].width, resolutions[resolutionNumSet].height, screenMode);


    }

}


