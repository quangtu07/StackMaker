using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startBtnUI;
    [SerializeField] private GameObject levelSelectionUI;

    private void Start()
    {
        Init(StartBtnHandler);
    }

    private void Init(Action<GameObject, Button> startBtnHandler) 
    {
        startBtnUI.onClick.AddListener(() => {
            startBtnHandler.Invoke(levelSelectionUI, startBtnUI);
        });
    }

    private void StartBtnHandler(GameObject levelSelection, Button startBtn)
    {
        levelSelection.SetActive(true);
        startBtn.gameObject.SetActive(false);
    }

}
