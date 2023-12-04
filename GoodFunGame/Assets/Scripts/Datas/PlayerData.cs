using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class PlayerData
{
    public enum PlayerKeys
    {
        FIRSTPLAYER,    // 1P Player 
        SECONDPLAYER    // 2P Player
    }
    public PlayerKeys playerEnumKey;    // Player EnumType Key
    public string playerStringKey;  // Player StringType Key
    public int playerLife;  // Player Life 하트 수만큼 
    public int gold;    // gold 재화 
    public Dictionary<SkillData.Skills, SkillData> playerSkills;    // SkillData 의 EnumType의 Skill Dictionary
    public List<Dictionary<SkillData.Skills, SkillData>> playerSkillList; // Player가 보유한 Skill 리스트
    public int damage;  // Player Damage
    public float moveSpeed; // Player Move Speed
}

