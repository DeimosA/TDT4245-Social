using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINewsFeedController : MonoBehaviour
{

    public GameObject newsItemPrefab;
    public List<NewsFeedItem> newsFeedItems;

    private GameObject newsContainer;

    // Start is called before the first frame update
    void Start()
    {
        newsContainer = GameObject.Find("NewsFeedContent");


        ////// Test data /////
        //int newsCount = 50;
        //for (int i = 0; i < newsCount; i++)
        //{
        //    AddNewsItem("News item " + i);
        //}
        /////////////////////
        
    }

    public void AddNewsItem(string content)
    {

        GameObject newInstance = Instantiate(newsItemPrefab, newsContainer.transform, false);
        newInstance.transform.SetParent(newsContainer.transform, false);
        newInstance.transform.Find("NewsItemContent").GetComponent<TextMeshProUGUI>().SetText(content);
    }
}
