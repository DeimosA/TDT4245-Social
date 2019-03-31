using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINewsFeedController : MonoBehaviour
{

    public GameObject newsItemPrefab;


    // Start is called before the first frame update
    void Start()
    {
        int newsCount = 5;
        GameObject newsContainer = GameObject.Find("NewsFeedContent");
        for (int i = 0; i < newsCount; i++)
        {
            GameObject newInstance = Instantiate(newsItemPrefab, newsContainer.transform, false);
            newInstance.transform.SetParent(newsContainer.transform, false);
            newInstance.GetComponent<TextMeshProUGUI>().SetText("News item " + i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
