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
    private const float SpeedOffset = 1.5f;
    private const float WayPointWidthValue = 2;
    public enum Pattern { VERTICAL, ZIGZAG, BOSS}

    private enum BossPattern { Horizontal, Infinity, InAndOut}

    #region Get And Set Enemy Pattern

    public List<Enemy> EnemyPickList(int wave, int bossIndex)
    {
        List<Enemy> spawnList = new();
        if (wave == 1)
        {
            Enemy bossObject = Main.Object.Spawn<Enemy>(Main.Stage.BossKey[bossIndex], new Vector2(0, 5f));
            bossObject.gameObject.SetActive(false);
            spawnList.Add(bossObject);
        }
        else
        {
            string bossKey = Main.Stage.BossKey[bossIndex];
            List<string> soldierKeys = Main.Stage.EnemyGroups[bossKey];

            for (int i = 0; i < wave; i++)
            {
                string key = soldierKeys[Random.Range(0, soldierKeys.Count)];
                Vector2 position = RandomKeyPick();
                Enemy enemyObj = Main.Object.Spawn<Enemy>(key, position);
                enemyObj.gameObject.SetActive(false);
                spawnList.Add(enemyObj);
            }
        }
        return spawnList;
    }

    public Vector2 RandomKeyPick()
    {
        Vector2 position = new(Random.Range(-3,3),6);
        return position;
    }
    #endregion

    #region Assignment Enemy Pattern
    public void AssignmentEnemyPattern(List<Enemy> enemiesList)
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
    public static Vector2[] CalculateWaypoints(Enemy enemy, int wayPointCount)
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

    public static IEnumerator MoveZigzag(Enemy enemy, Vector2[] wayPoints)
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
    public static IEnumerator MoveToVertical(Enemy enemy)
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
    /// <param name="paths"></param>
    /// <returns></returns>
    public IEnumerator BossAppear(Enemy enemy)
    {
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

    private static void BossNextPattern(Enemy enemy)
    {
        BossPattern[] bossPatterns = Enum.GetValues(typeof(BossPattern)).Cast<BossPattern>().ToArray();
        BossPattern bossPattern = bossPatterns[Random.Range(0, bossPatterns.Length)];

        switch (bossPattern)
        {
            case BossPattern.Horizontal:
                enemy.BossHorizontal();
                break;
            case BossPattern.Infinity:
                enemy.BossInfinity();
                break;
            case BossPattern.InAndOut:
                enemy.BossInAndOut();
                break;
        }
    }

    public static IEnumerator BossHorizontalPattern(Enemy enemy)
    {
        Vector3[] waypoints =
        {
            new(2.5f,3.5f,0f), 
            new(-2.5f,3.5f,0f), 
            new(0f,3.5f,0f)
        };

        int repeatCount = 2;

        while (repeatCount > 0)
        {
            foreach (Vector3 wayPoint in waypoints)
            {
                while (Vector3.Distance(enemy.transform.position, wayPoint) > 0.01f)
                {
                    enemy.transform.position = Vector3.MoveTowards(
                        enemy.transform.position, 
                        wayPoint,
                        enemy.speed * Time.deltaTime * SpeedOffset);
                    yield return null;
                }
            }
            repeatCount--;
        }
        BossNextPattern(enemy);
    }

    public static IEnumerator BossInfinityPattern(Enemy enemy)
    {
        Vector3[] waypoints =
        {
            new(0f,3.5f,0f), 
            new(-1.5f,2.5f,0f), 
            new(-3f,3.5f,0f), 
            new(-1.5f,4.5f,0f), 
            new(1.5f,2.5f,0f), 
            new(3f,3.5f,0f), 
            new(1.5f,4.5f,0f), 
            new Vector3(0f,3.5f,0f)
        };
        foreach (Vector3 wayPoint in waypoints)
        {
            while (Vector3.Distance(enemy.transform.position, wayPoint) > 0.01f)
            {
                enemy.transform.position = Vector3.MoveTowards(
                    enemy.transform.position, 
                    wayPoint,
                    enemy.speed * Time.deltaTime * SpeedOffset);
                yield return null;
            }
        }
        BossNextPattern(enemy);
    }

    public static IEnumerator BossInAndOutPattern(Enemy enemy)
    {
        Vector3[] waypoints =
        {
            new(0f,3.5f,0f), 
            new(0f,1f,0f), 
            new(0f,3.5f,0f), 
            new(-1.7f,2.5f,0f), 
            new(0f,3.5f,0f), 
            new(1.7f,2.5f,0f), 
            new(0f,3.5f,0f)
        };
        foreach (Vector3 wayPoint in waypoints)
        {
            while (Vector3.Distance(enemy.transform.position, wayPoint) > 0.01f)
            {
                enemy.transform.position = Vector3.MoveTowards(
                    enemy.transform.position, 
                    wayPoint,
                    enemy.speed * Time.deltaTime * SpeedOffset);
                yield return null;
            }
        }
        BossNextPattern(enemy);
    }
    #endregion
}