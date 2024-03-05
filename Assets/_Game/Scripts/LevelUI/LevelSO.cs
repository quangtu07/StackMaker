using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level")]
public class LevelSO : ScriptableObject
{
    public List<LevelItemData> listItems;
}
