using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textIndex;
    [SerializeField] private Button levelButton;
    public void Init(string textIndex, Action<string> levelButtonClick)
    {
        this.textIndex.text = textIndex;
        levelButton.onClick.AddListener(() => {
            levelButtonClick.Invoke(textIndex);
        });
    }
}
