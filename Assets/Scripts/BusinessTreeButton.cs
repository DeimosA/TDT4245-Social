using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusinessTreeButton : MonoBehaviour
{
    public GameObject businessTreePrefab;
    public Transform prefabSpawnPosition;

    public void OnClick()
    {
        Instantiate(businessTreePrefab, prefabSpawnPosition, false);
    }
}
