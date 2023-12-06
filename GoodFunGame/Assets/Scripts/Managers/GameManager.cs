using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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


    public const int StageWaveMaxCount = 7;
    // 한 웨이브당 Spawn되는 적의 숫자 
    public readonly int[] WaveVolume = { 3, 4, 3, 4, 5, 3, 1 };

    public List<string> PurchasedSkills
    {
        get => _data.purchasedSkills;
        set
        {
            _data.purchasedSkills = value;
            SaveGame();
        }
    }
    public List<string> EquippedSkills
    {
        get => _data.equippedSkills;
        set
        {
            _data.equippedSkills = value;
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
    
    public List<String> EquipSkills
    {
        get => _equipSkills;
        set
        {
            _equipSkills = value;
            OnEquipChanged?.Invoke();
        }
    }


    #endregion

    #region Fields

    // GameData.
    private GameData _data = new();
    private string _dataPath;

    private List<String> _equipSkills = new();


    // Events.
    public event Action OnResourcesChanged; // Gold 등의 재화가 변동될 때 이벤트. ex) 여기에 재화 UI 새로고침과 같은 코드를 넣어두면 좋겠습니다!

    public event Action OnEquipChanged; // 장비창 실험용!

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

    #region Skills

    public bool PurchaseSkill(string key)
    {
        // #1. 구입 여부 및 가능 여부 검사.
        SkillData data = Main.Data.Skills[key];
        if (data.skillPrice > Gold) return false;
        if (PurchasedSkills.Contains(key)) return false;

        // #2. 구입한 스킬에 추가.
        PurchasedSkills.Add(key);

        // #3. 골드 차감.
        Gold -= data.skillPrice;

        return true;
    }
    public bool EquipSkill(string key)
    {
        // #1. 장착 가능 여부 검사.
        if (!PurchasedSkills.Contains(key)) return false;
        if (EquippedSkills.Contains(key)) return false;
        if (EquippedSkills.Count >= 3) return false;
        // #2. 장착한 스킬에 추가.
        EquippedSkills.Add(key);

        // #3. 콜백.
        OnEquipChanged?.Invoke();

        SaveGame();

        return true;
    }
    public bool UnequipSkill(string key)
    {
        // #1. 장착 해제 가능 여부 검사.
        if (!EquippedSkills.Contains(key)) return false;

        // #2. 장착한 스킬에서 제거.
        EquippedSkills.Remove(key);

        // #3. 콜백.
        OnEquipChanged?.Invoke();

        SaveGame();

        return true;
    }

    #endregion

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