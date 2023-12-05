
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : Creature 
{
    #region Properties

    public EnemyData.EnemyKey enemyType;
    public int hp;
    public float speed;
    public int currentHp;
    public int damage;
    public EnemyData.FireType fireType;


    #endregion

    #region Fields

    private EnemyData _enemyData;
    private DataManager _dataManager;

    [SerializeField] float moveMagnitude = 4f; // 움직임의 크기
    [SerializeField] float moveFrequency = 3f; // 움직임의 빈도
    [SerializeField] float moveTime = 2f;  // 움직이는 시간

    #endregion

    #region MonoBehaviours

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
            OnHit(collision.gameObject);
    }

    #endregion

    #region Initialize / Set
    public override bool Initialize() 
    {
        if (base.Initialize() == false) return false;
        foreach (var enemy in Main.Data.Enemies)
        {
            SetInfo(enemy.Key);
        }

        return true;
    }

    public override void SetInfo(string key) {
        base.SetInfo(key);

        var enemy = Main.Data.Enemies.FirstOrDefault(e => e.Key == key).Value;
        enemyType = (EnemyData.EnemyKey)Enum.Parse(typeof(EnemyData.EnemyKey), enemy.keyName);
        hp = enemy.hp;
        currentHp = hp;
        speed = enemy.speed;
        damage = enemy.damage;
    }
    #endregion

    #region State
    protected override void OnStateEntered_Dead() 
    {
        base.OnStateEntered_Dead();

        // TODO:: Player의 KillCount 증가.

        // TODO:: 오브젝트 디스폰

    }
    #endregion

    #region Move

    IEnumerator Move()
    {
        while(State != CreatureState.DEAD)
        {
            System.Random random = new System.Random();
            int randomNumber = random.Next(0, 3);

            Vector3 originalPosition = transform.position;

            switch (randomNumber)
            {
                case 0:
                    yield return StartCoroutine(UpDown());
                    break;

                case 1:
                    yield return StartCoroutine(LeftRight());
                    break;

                case 2:
                    yield return StartCoroutine(Diagonal());
                    break;
                default:
                    break;
            }
            yield return StartCoroutine(MoveToOriginalPosition(originalPosition, 0.15f));

        }
        yield break;
    }

    IEnumerator UpDown()
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < moveTime)
        {
            float newY = originalPosition.y + Mathf.Sin(elapsedTime * moveFrequency) * moveMagnitude;
            transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator LeftRight()
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < moveTime)
        {
            float newX = originalPosition.x + Mathf.Sin(elapsedTime * moveFrequency) * moveMagnitude;
            transform.position = new Vector3(newX, originalPosition.y, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator Diagonal()
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < moveTime)
        {
            float newX = originalPosition.x + Mathf.Sin(elapsedTime * moveFrequency) * moveMagnitude;
            float newY = originalPosition.y + Mathf.Sin(elapsedTime * moveFrequency) * moveMagnitude;
            transform.position = new Vector3(newX, newY, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return StartCoroutine(MoveToOriginalPosition(originalPosition, 0.2f));
    }

    IEnumerator MoveToOriginalPosition(Vector3 originalPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }

    #endregion
}