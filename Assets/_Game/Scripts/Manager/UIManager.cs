using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bricksNumberTxt;

    private static UIManager instance;

    public static UIManager Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }

    public void ChangeBrickNumber(int count)
    {
        bricksNumberTxt.text = TextUIConstants.BRICK_TEXT + count;
    }
}
