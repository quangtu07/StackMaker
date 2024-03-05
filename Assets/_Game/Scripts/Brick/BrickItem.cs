using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickItem : MonoBehaviour
{
    [SerializeField] public GameObject childObject;

    public void ShowChildObject()
    {
        childObject.SetActive(true);
    }
}
