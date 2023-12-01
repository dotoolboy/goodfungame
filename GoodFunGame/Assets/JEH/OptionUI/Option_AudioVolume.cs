using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option_AudioVolume : MonoBehaviour
{

    private Animator animator;

    public AudioMixer audioMixer;

    public Slider slider_BGM;
    public Slider slider_SFX;

    public Toggle toggle_BGM;
    public Toggle toggle_SFX;

    public TMP_Text bgmVolumeText; //볼륨 크기 숫자. 안쓸거면 이변수 쓰는거 다 지우면됨
    public TMP_Text sfxVolumeText;

    string bgm = "BGM";
    string sfx = "SFX";


    public void Awake()
    {
        animator = GetComponent<Animator>();

    }

    public void Volume_BGM(float value)
    {

        if (!toggle_BGM.isOn) 
            audioMixer.SetFloat(bgm, Mathf.Log10(value) * 20); // audioMixer.SetFloat("SFX", i); 기본 믹서에 설정넣는법

        bgmVolumeText.text = (slider_BGM.value * 100).ToString("N0");
    }

    public void Volume_SFX(float value)
    {
        if (!toggle_SFX.isOn) 
            audioMixer.SetFloat(sfx, Mathf.Log10(value) * 20);

        sfxVolumeText.text = (slider_SFX.value * 100).ToString("N0");

    }

    //-------------------------------------

    public void Mute_BGM(bool isMute)
    {
        toggle_BGM.isOn = isMute; //온클릭말고 함수로 실행시키는경우도있으니까 토글 온오프 한번더 바꿔줌

        if (isMute)
            audioMixer.SetFloat(bgm, -80f);
        else
            audioMixer.SetFloat(bgm, Mathf.Log10(slider_BGM.value) * 20);

    }

    public void Mute_SFX(bool isMute)
    {
        toggle_SFX.isOn = isMute;

        if (isMute)
            audioMixer.SetFloat(sfx, -80f);
        else
            audioMixer.SetFloat(sfx, Mathf.Log10(slider_SFX.value) * 20);
    }

    void Save()
    {

        // 브금크기 = slider_BGM.value;
        // 브금음소거 =  muteToggle_BGM.isOn;

        //  sfxVolume = slider_SFX.value;
        //   sfxMute = muteToggle_SFX.isOn;

        // 데이터저장

    }
    void Load()
    {

     //   slider_BGM.value = SaveManager.Instance.userData.bgmVolume; // 다이나믹으로 연결해도 겜 처음엔 수동으로 슬라이더 옮겨줘야된다 ㅡㅡ
     //   slider_SFX.value = SaveManager.Instance.userData.sfxVolume;

      //  Volume_BGM(SaveManager.Instance.userData.bgmVolume);
      //  Mute_BGM(SaveManager.Instance.userData.bgmMute);

    //    Volume_SFX(SaveManager.Instance.userData.sfxVolume);
     //   Mute_SFX(SaveManager.Instance.userData.sfxMute);


    }



    public void Load_DefaultSetting()
    {
     //  Volume_BGM(dataCenter.bgmVolume);
     //   Mute_BGM(dataCenter.bgmMute);

      //  Volume_SFX(dataCenter.sfxVolume);
      //  Mute_SFX(dataCenter.sfxMute);


    }
}
