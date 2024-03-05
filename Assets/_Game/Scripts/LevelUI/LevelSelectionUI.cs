using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private LevelItemUI buttonPrefab;
    [SerializeField] private Transform parentPosition;
    [SerializeField] private LevelSO levelDataSO;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] private TextMeshProUGUI textBrickNumber;
    private GameObject currentMap;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SpawnLevelList();
    }

    private void SpawnLevelItem(string textIndex)
    {
        LevelItemUI levelItemUI = Instantiate(buttonPrefab, parentPosition);
        levelItemUI.Init( textIndex, OnLevelItemUIClickHandle);

    }

    private void OnLevelItemUIClickHandle(string index)
    {
        //spawn prefab map cua level tuong ung
        GameObject mapPrefab = Resources.Load<GameObject>($"{PathConstants.MAP_PATH}{index}");
        Debug.Log($"{PathConstants.MAP_PATH}{index}");
        currentMap = Instantiate(mapPrefab);
        currentMap.SetActive(true);
        cameraUI.SetActive(false);
        textBrickNumber.gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
    }

    private void SpawnLevelList()
    {
        List<LevelItemData> list = levelDataSO.listItems;
        for (int i = 0; i < list.Count; i++)
        {
            SpawnLevelItem(list[i].levelIndex.ToString());
        }
    }
}
