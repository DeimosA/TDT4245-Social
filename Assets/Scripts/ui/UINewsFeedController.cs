using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINewsFeedController : MonoBehaviour
{

    public GameObject newsItemPrefab;

    private GameObject newsContainer;


    // Start is called before the first frame update
    void Start()
    {

        //// Test data /////
        int newsCount = 50;
        newsContainer = GameObject.Find("NewsFeedContent");
        for (int i = 0; i < newsCount; i++)
        {
            AddNewsItem("News item " + i);
        }
        /////////////////////
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewsItem(string newsItemText)
    {
        GameObject newInstance = Instantiate(newsItemPrefab, newsContainer.transform, false);
        newInstance.transform.SetParent(newsContainer.transform, false);
        newInstance.GetComponent<TextMeshProUGUI>().SetText(newsItemText);
    }
}
