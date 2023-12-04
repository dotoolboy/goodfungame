using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager
{

    #region Properties

    public GameData Data => _data;
    public string UserName
    {
        get => _data.userName;
        set => _data.userName = value;
    }
    public int Gold
    {
        get => _data.gold;
        set
        {
            _data.gold = value;
            SaveGame();
            OnResourcesChanged?.Invoke();
        }
    }
    public List<string> PurchasedSkills
    {
        get => _data.purchasedSkills;
        set
        {
            _data.purchasedSkills = value;
            SaveGame();
        }
    }
    public bool MuteBGM
    {
        get => _data.muteBGM;
        set
        {
            if (_data.muteBGM == value) return;
            _data.muteBGM = value;
            if (_data.muteBGM == false)
            {
                // TODO:: BGM 끄기. ex) Main.Sound.MuteBGM();
            }
            else
            {
                // TODO:: BGM 재생. ex) Main.Sound.PlayBGM();
            }
            SaveGame();
        }
    }
    public bool MuteSFX
    {
        get => _data.muteSFX;
        set
        {
            if (_data.muteSFX == value) return;
            _data.muteSFX = value;
            if (_data.muteSFX == false)
            {
                // TODO:: SFX 끄기. ex) Main.Sound.MuteSFX(true);
            }
            else
            {
                // TODO:: SFX 켜기. ex) Main.Sound.MuteSFX(false);
            }
        }
    }

    #endregion

    #region Fields

    // GameData.
    private GameData _data = new();
    private string _dataPath;

    // Events.
    public event Action OnResourcesChanged; // Gold 등의 재화가 변동될 때 이벤트. ex) 여기에 재화 UI 새로고침과 같은 코드를 넣어두면 좋겠습니다!

    #endregion

    public void Initialize()
    {
        _dataPath = Application.persistentDataPath + "/SaveData.json";
        if (LoadGame()) return;

        // ================== 로드 실패, 초기화. ==================

        // TODO:: 게임을 처음 실행 시 처리할 일들을 이 곳에 넣으면 좋겠습니다!

        // ========================================================
        SaveGame();
    }

    #region Save / Load

    public void SaveGame()
    {
        string jsonStr = JsonConvert.SerializeObject(_data);
        File.WriteAllText(_dataPath, jsonStr);
    }

    public bool LoadGame()
    {
        if (!File.Exists(_dataPath)) return false;

        string file = File.ReadAllText(_dataPath);
        GameData data = JsonConvert.DeserializeObject<GameData>(file);
        if (data != null) this._data = data;

        return true;
    }

    #endregion
}