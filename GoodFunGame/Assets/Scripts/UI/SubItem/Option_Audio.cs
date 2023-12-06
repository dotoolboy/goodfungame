using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option_Audio : UI_Base
{

    [SerializeField] private AudioMixer _audioMixer;

    private Slider _slider;
    private Toggle _toggle;


    private Types _type;
    public Types Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }


    #region Enums
    enum Texts
    {
        NameText,
        VolumeText,
    }
    enum GameObjects
    {
        VolumeSlider,
        MuteToggle,
    }

    public enum Types
    {
        Master,
        BGM,
        SFX
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
        BindObject(typeof(GameObjects));

        switch (_type)
        {
            case Types.Master:
                GetText((int)Texts.NameText).text = "모든소리";
                break;
            case Types.BGM:
                GetText((int)Texts.NameText).text = "배경음악";
                break;
            case Types.SFX:
                GetText((int)Texts.NameText).text = "효과음";
                break;
            default:
                break;
        }


        _toggle = GetObject((int)GameObjects.MuteToggle).GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(Mute);

        _slider = GetObject((int)GameObjects.VolumeSlider).GetComponent<Slider>();
        _slider.onValueChanged.AddListener(Volume);


        Refresh();

        return true;
    }

    public void Volume(float value)
    {
        if (!_toggle.isOn) 
            _audioMixer.SetFloat(_type.ToString(), Mathf.Log10(value) * 20); // 이제 이렇게주면 -40 밑으로 내려갈때 자동으로 0 된다. 슬라이더 최소값을 0.01로 바꿔서 임시로 해결
     
        GetText((int)Texts.VolumeText).text = (_slider.value * 100).ToString("N0");
    }


    public void Mute(bool isMute)
    {
        _toggle.isOn = isMute;

        if (isMute)
            _audioMixer.SetFloat(_type.ToString(), -80f);
        else
            _audioMixer.SetFloat(_type.ToString(), Mathf.Log10(_slider.value) * 20);
    }

    void Refresh()
    {
      
        GetText((int)Texts.VolumeText).text = (_slider.value * 100).ToString("N0");

        if (_toggle.isOn)
            _audioMixer.SetFloat(_type.ToString(), -80f);
        else
            _audioMixer.SetFloat(_type.ToString(), Mathf.Log10(_slider.value) * 20);
    }


    void Save()
    {
    }
    void Load()
    {
    }
    public void DefaultSetting()
    {
    }

}
