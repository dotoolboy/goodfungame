using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option_Audio : UI_Base
{

    private AudioMixer _audioMixer;

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
            Refresh();
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

        GetText((int)Texts.NameText).text = _type.ToString();

        _toggle = GetObject((int)GameObjects.MuteToggle).GetComponent<Toggle>();
        _slider = GetObject((int)GameObjects.VolumeSlider).GetComponent<Slider>();

        return true;
    }

    public void Volume(float value)
    {

        if (!_toggle.isOn)
            _audioMixer.SetFloat(_type.ToString(), Mathf.Log10(value) * 20);


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
        // 자기 타입에 맞게 변화

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
