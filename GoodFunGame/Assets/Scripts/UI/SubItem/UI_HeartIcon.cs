using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HeartIcon : MonoBehaviour
{
    [SerializeField] int _iconNum = 3;

    Player player;

    void Start()
    {
        player = Main.Object.Player;
        player.OnPlayerHealthChanged += UpdateUI;
    }

    void UpdateUI()
    {
        if(player.Hp < _iconNum)
            gameObject.SetActive(false);
    }
}
