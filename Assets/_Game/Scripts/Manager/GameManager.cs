using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isVictory;

    private static GameManager instance;

    public static GameManager Instance { get => instance; }
    public bool IsVictory { get => isVictory; }

    private void Awake()
    {
        instance = this;
        isVictory = false;
    }

    public void ChangeVictory()
    {
        isVictory = !isVictory;
    }
}
