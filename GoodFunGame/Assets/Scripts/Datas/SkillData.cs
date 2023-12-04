
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SkillData
{

    public enum Skills
    { 
        REFLECTSHIELD,  //Active Skill - 적을 향해 반사하는 실드를 생성
        TIMEWARP,   // Active Skill - 일시적으로 시간을 느리게 만들어 탄을 피하기 쉽게한다.
        ENERGYBURST,    // Active Skill - 전체 공격으로 화면상 적의 탄막을 제거한다.
        GRAVITYFIELD,    // Active Skill - 플레이어의 전방에 적의 탄막을 모으는 고정된 필드를 생성한다.
        PIERCINGSHOT,   // Passive Skill - 적을 1회 관통하는 공격을 한다.
        AUTOTARGET, // Passive Skill - 유도공격을 하는 미사일을 주기적으로 발사한다.
        HOMINGMISSILE,  // Passive Skill - 화면상 좌우 상하 벽을 튕겨 다니는 미사일을 생성한다.

    }

    public Skills skill;

    public enum SkillTypes
    {
        ACTIVE,
        PASSIVE
    }

    public SkillTypes skillType;

    public string skillStringKey;

    public int skillLevel;  // 스킬 레벨이 상승하면, 스킬의 형태가 변합니다.

    public float skillCool; // 스킬의 쿨타임

    public float skillDuration;

    public int skillDamage;  // 스킬의 데미지

    public int skillPrice;   // 스킬의 상점 가격

    public string skillDesc; // 스킬 설명
}

[Serializable]
public class SkillDataLoader : ILoadData<string, SkillData>
{
    public List<SkillData> skills = new List<SkillData>();

    public Dictionary<string, SkillData> MakeData()
    {
        return skills.ToDictionary(skill => skill.skillStringKey);
    }
}