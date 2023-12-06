
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnemySpawn;
using Random = UnityEngine.Random;

public class Enemy : Creature 
{
    #region Properties
    public EnemyData.EnemyKey enemyType;
    public int hp;
    public float speed;
    public int currentHp;
    public int damage;
    public EnemyData.FireType fireType;
    public Pattern movePattern;
    private SpriteRenderer _enemySpriteRenderer;
    #endregion

    #region Fields

    private Coroutine _moveCoroutine;
    private Coroutine _coAttack;
    private string _key;
    private string _projectileKey;
    #endregion

    #region MonoBehaviours

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
                OnHit(projectile.Owner);
            else
                OnHit(2);
            Main.Stage.StageCurrentScore++;
        }
    }

    protected override void Update()
    {
        _coAttack ??= StartCoroutine(CoAttack());
    }

    #endregion

    #region Initialize / Set
    public override bool Initialize() 
    {
        if (base.Initialize() == false) return false;
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        foreach (KeyValuePair<string, EnemyData> enemy in Main.Data.Enemies)
        {
            SetInfo(enemy.Key);
        }
        return true;
    }

    public override void SetInfo(string key) {
        base.SetInfo(key);
        _key = key;
        EnemyData enemy = Main.Data.Enemies.FirstOrDefault(e => e.Key == key).Value;
        enemyType = (EnemyData.EnemyKey)Enum.Parse(typeof(EnemyData.EnemyKey), enemy.keyName);
        _enemySpriteRenderer.sprite = Main.Resource.Load<Sprite>($"{key}.sprite");
        hp = enemy.hp;
        currentHp = hp;
        speed = enemy.speed;
        damage = enemy.damage;
        movePattern = AssignmentPattern(key);
        State = CreatureState.IDLE;
        _coAttack = null;
    }

    /// <summary>
    ///  랜덤한 패턴 타입 할당 "Key == BOSS를 포함하면 BOSS Type을 반환"
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private Pattern AssignmentPattern(string key)
    {
        Pattern patternType;
        if (key.Contains("BOSS"))
        {
            patternType = Pattern.BOSS;
        }
        else
        {
            Pattern[] patternTypes = Enum.GetValues(typeof(Pattern)).Cast<Pattern>().ToArray();
            patternType = patternTypes[Random.Range(0,patternTypes.Length)];
        }
        return patternType;
    }
    #endregion

    #region State
    protected override void OnStateEntered_Dead() 
    {
        base.OnStateEntered_Dead();

        // 적을 죽일때마다 스코어 점수 추가
        Main.Stage.StageCurrentScore += 50;
        Main.Game.Gold += 10;
        // 터지는 효과
        Main.Resource.InstantiatePrefab("Explosion.prefab", transform);

        // TODO:: 오브젝트 디스폰
        var explosionVFX = Main.Object.Spawn<Explosion>("Explosion", this.transform.position);
        explosionVFX.gameObject.GetComponent<Explosion>().DespawnExplosion();

        EndToEnemyCoroutine(this);
    }
    #endregion

    #region MoveMentPattern
    /// <summary>
    ///  지그재그패턴  총 3번의 움직임 변화가 있음  (파라매터로 전달)
    /// </summary>
    public void Zigzag()
    {
        CoroutineInit();
        Vector2[] wayPoints = CalculateWaypoints(this, 3);
        _moveCoroutine = StartCoroutine(MoveZigzag(this, wayPoints));
    }
    /// <summary>
    ///  일직선 움직임
    /// </summary>
    public void Vertical()
    {
        CoroutineInit();
        _moveCoroutine = StartCoroutine(MoveToVertical(this));
    }

    /// <summary>
    ///  보스일때 움직임
    /// </summary>
    public void Boss()
    {
        CoroutineInit();
        _moveCoroutine = StartCoroutine(Main.Spawn.BossAppear(this));
    }

    public void BossHorizontal()
    {
        CoroutineInit();
        _moveCoroutine = StartCoroutine(BossHorizontalPattern(this));
    }

    public void BossInfinity()
    {
        CoroutineInit();
        _moveCoroutine = StartCoroutine(BossInfinityPattern(this));
    }

    public void BossInAndOut()
    {
        CoroutineInit();
        _moveCoroutine = StartCoroutine(BossInAndOutPattern(this));
    }

    public void EndToEnemyCoroutine<T>(T coroutineObject) where T : Thing
    {
        CoroutineInit();
        Main.Object.Despawn(coroutineObject);
    }

    private void CoroutineInit()
    {
        if (_moveCoroutine == null) return;
        StopCoroutine(_moveCoroutine);
        _moveCoroutine = null;
    }
    #endregion

    #region AttackPattern
    private string[] MappingProjectileKey()
    {
        string[] key = { };
        if (enemyType.ToString() != _key)
        {
            return new string[] { };
        }

        if (Main.Object.ProjectileMappings.TryGetValue(enemyType, out string[] projectileKeys))
        {
            key = projectileKeys;
        }

        return key;
    }
    private IEnumerator CoAttack()
    {
        ProjectileGenerator pg = null;
        int count = Random.Range(2, 8);
        float time = Random.Range(0, 4);
        int a = Random.Range(0, 4);
        string key = _key.Contains("BOSS") 
            ? MappingProjectileKey()[Random.Range(0, MappingProjectileKey().Length)] 
            : MappingProjectileKey()[0];
        switch (a)
        {
            case 1:
                PG_Fan fanShot = Main.Object.SpawnProjectileGenerator<PG_Fan>();
                fanShot.transform.position = transform.position;
                fanShot.transform.SetParent(transform);
                fanShot.Initialize(this, key, count, time, 3, 250, 40);
                fanShot.Shot();
                pg = fanShot;
                break;
            case 2:
                PG_Circle circleShot = Main.Object.SpawnProjectileGenerator<PG_Circle>();
                circleShot.transform.position = transform.position;
                circleShot.transform.SetParent(transform);
                circleShot.Initialize(this, key, count, time, 3);
                circleShot.Shot();
                pg = circleShot;
                break;
            case 3:
                PG_Ring ringShot = Main.Object.SpawnProjectileGenerator<PG_Ring>();
                ringShot.transform.position = transform.position;
                ringShot.transform.SetParent(transform);
                ringShot.Initialize(this, key, 20, time, 3);
                ringShot.Shot();
                pg = ringShot;
                break;
            case 4:
                PG_Vertical verticalShot = Main.Object.SpawnProjectileGenerator<PG_Vertical>();
                verticalShot.transform.position = transform.position;
                verticalShot.transform.SetParent(transform);
                verticalShot.Initialize(this, key, count, time, 3);
                verticalShot.Shot();
                pg = verticalShot;
                break;
        }
        yield return new WaitUntil(() => pg == null);
        yield return new WaitForSeconds(1);
        _coAttack = null;
        yield break;
    }

    #endregion
}