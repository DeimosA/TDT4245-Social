using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusinessTreeButton : MonoBehaviour
{
    public GameObject businessTreePrefab;
    private Transform spawnPosition;

    private void Start()
    {
        spawnPosition = GameObject.Find("ActivityCardPanel").transform;
    }

    public void OnClick()
    {
        GameObject g = Instantiate(businessTreePrefab, spawnPosition, false);
        g.transform.SetAsLastSibling();
    }
}
