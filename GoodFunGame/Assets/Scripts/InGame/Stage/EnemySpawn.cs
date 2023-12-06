using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawn
{
    // 적이 움직일 수 있는 범위
    // 가로 (-4,4)
    // 세로 (5,-5)
    private int _enemyVolume;
    private List<Enemy> _enemySpawnList = new();
    private const float SpeedOffset = 1.5f;
    private const float WayPointWidthValue = 2;
    private DOTweenPath[] _bossPattern;

    #region SpawnGroup Controller
    private readonly string[] _bossKey = {
        "BOSS_MWJ",
        "BOSS_CHH",
        "BOSS_LJH",
        "BOSS_JEH",
        "BOSS_KSJ"
    };
    private int _bossIndex;

    private Coroutine _bossMoveCoroutine;

    private readonly Dictionary<string, List<string>> _enemyGroups = new()
    {
        {"BOSS_MWJ", new List<string> {"SOLDIER1_MWJ", "SOLDIER2_MWJ", "SOLDIER3_MWJ"}},
        {"BOSS_CHH", new List<string> {"SOLDIER1_CHH", "SOLDIER2_CHH", "SOLDIER3_CHH"}},
        {"BOSS_LJS", new List<string> {"SOLDIER1_LJH", "SOLDIER2_LJH", "SOLDIER3_LJH"}},
        {"BOSS_JEH", new List<string> {"SOLDIER1_JEH", "SOLDIER2_JEH", "SOLDIER3_JEH"}},
        {"BOSS_KSJ", new List<string> {"SOLDIER1_KSJ", "SOLDIER2_KSJ", "SOLDIER3_KSJ"}}
    };
    #endregion


    public enum Pattern { VERTICAL, ZIGZAG, BOSS}

    public enum BossPattern { Horizontal, Infinity, InAndOut}

    #region Get And Set Enemy Pattern
    public void StageVolume(int stageValue)
    {
        _enemySpawnList.Clear();
        _enemyVolume = stageValue switch
        {
            1 => 3,
            2 => 4,
            3 => 5,
            4=> 1,
            _ => _enemyVolume
        };
        EnemyPickList();
    }

    private void EnemyPickList()
    {
        if (_enemyVolume == 1)
        {
            Enemy bossObject = Main.Object.Spawn<Enemy>(_bossKey[_bossIndex], new Vector2(0, 5f));
            bossObject.gameObject.SetActive(false);
            _enemySpawnList.Add(bossObject);
            _bossIndex++;
        }
        else
        {
            string bossKey = _bossKey[_bossIndex];
            List<string> soldierKeys = _enemyGroups[bossKey];

            for (int i = 0; i < _enemyVolume; i++)
            {
                string key = soldierKeys[Random.Range(0, soldierKeys.Count)];
                Vector2 position = RandomKeyPick();
                Enemy enemyObj = Main.Object.Spawn<Enemy>(key, position);
                enemyObj.gameObject.SetActive(false);
                _enemySpawnList.Add(enemyObj);
            }
        }
        EnemyPattern(_enemySpawnList);
    }

    private Vector2 RandomKeyPick()
    {
        Vector2 position = new(Random.Range(-3,3),6);
        return position;
    }
    #endregion

    #region Assignment Enemy Pattern
    private void EnemyPattern(List<Enemy> enemiesList)
    {
        foreach (Enemy enemy in enemiesList)
        {
            enemy.gameObject.SetActive(true);
            
            switch (enemy.movePattern)
            {
                case Pattern.VERTICAL:
                    enemy.Vertical();
                    break;
                case Pattern.ZIGZAG:
                    enemy.Zigzag();
                    break;
                case Pattern.BOSS:
                    enemy.Boss();
                    break;
            }
        }
    }
    #endregion

    #region ZigZag
    public Vector2[] CalculateWaypoints(Enemy enemy, int wayPointCount)
    {
        int direction = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector2[] wayPoints = new Vector2[wayPointCount];
        Vector2 startPosition = enemy.transform.position;

        for (int i = 0; i < wayPoints.Length; i++)
        {
            float nextX = Mathf.Clamp(startPosition.x + direction * WayPointWidthValue, -3.5f, 3.5f);
            float nextY = Mathf.Clamp(startPosition.y - 2 * (i + 1), -5f, 5f);
            wayPoints[i] = new Vector2(nextX, nextY);
            direction *= -1;
            startPosition = wayPoints[i];
        }
        return wayPoints;
    }

    public IEnumerator MoveZigzag(Enemy enemy, Vector2[] wayPoints)
    {
        int currentWaypointIndex = 0;
        while (currentWaypointIndex < wayPoints.Length)
        {
            Vector2 currentWaypoint = wayPoints[currentWaypointIndex];
            while (Vector2.Distance(enemy.transform.position, currentWaypoint) > 0.01f)
            {
                enemy.transform.position = Vector2.MoveTowards(
                    enemy.transform.position, 
                    currentWaypoint, 
                    enemy.speed * Time.deltaTime * SpeedOffset);
                yield return null;
            }
            currentWaypointIndex++;
        }
        enemy.EndToEnemyCoroutine(enemy);
    }
    #endregion

    #region Vertical
    public IEnumerator MoveToVertical(Enemy enemy)
    {
        Vector2 endPos = new(enemy.transform.position.x, -5f);
        while (enemy.transform.position.y > -5f)
        {
            enemy.transform.position = Vector2.MoveTowards(
                enemy.transform.position, 
                endPos, 
                enemy.speed * Time.deltaTime * SpeedOffset);
            yield return null; // 다음 프레임까지 대기
        }
        enemy.EndToEnemyCoroutine(enemy);
    }
    #endregion

    #region BOSS

    /// <summary>
    ///  보스 출현
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public IEnumerator BossAppear(Enemy enemy)
    {
        _bossPattern = enemy.GetComponents<DOTweenPath>();
        if (_bossPattern == null || _bossPattern.Length == 0)
        {
            Debug.LogError("DOTweenPath components not found on the enemy.");
            yield break;
        }
        Vector2 endPos = new(enemy.transform.position.x, 3.5f);
        while (enemy.transform.position.y > 3.5f)
        {
            enemy.transform.position = Vector2.MoveTowards(
                enemy.transform.position, 
                endPos, 
                enemy.speed * Time.deltaTime * SpeedOffset);
            yield return null; // 다음 프레임까지 대기
        }
        yield return new WaitForSeconds(1f);
        BossNextPattern(enemy);
    }

    private void BossNextPattern(Enemy enemy)
    {
        enemy.StopCoroutine(enemy.MoveCoroutine);
        enemy.MoveCoroutine = null;
        BossPattern[] bossPatterns = Enum.GetValues(typeof(BossPattern)).Cast<BossPattern>().ToArray();
        BossPattern bossPattern = bossPatterns[Random.Range(0, bossPatterns.Length)];
        BossMovePattern(enemy, bossPattern);
    }

    private void BossMovePattern(Enemy enemy, BossPattern patternName)
    {
        DOTweenPath patternPlay = _bossPattern.FirstOrDefault(pattern => patternName.ToString() == pattern.id);
        if (patternPlay == null)
        {
            Debug.LogError("Pattern play is null.");
            return;
        }

        // patternPlay.onComplete.RemoveAllListeners();
        // patternPlay.onComplete.AddListener(() => BossNextPattern(enemy));
        patternPlay.DOPlayById(patternName.ToString());
    }
    #endregion
}