using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_TimeWarp : SkillBase
{
    public bool Active
    {
        get => _active;
        set
        {
            if (_active == value) return;
            _active = value;
            if (_active == true)
            {
                Time.timeScale = 0.5f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    private bool _active;
    private float _duration = 1f;
    private float _time = 0f;

    protected override void Update()
    {
        base.Update();
        if (_time > 0)
        {
            _time -= Time.unscaledDeltaTime;
        }
        else
        {
            _time = 0;
            Active = false;
        }
    }

    public override void Initialize()
    {
        base.Initialize();

        Cooldown = 2f;
    }

    public override bool Activate()
    {
        if (!base.Activate()) return false;

        Active = true;
        _time = _duration;

        return true;
    }
}