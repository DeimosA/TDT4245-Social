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

    //Instantiates all valid newsfeed items
    public void UpdateNewsFeed()
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        for(int i = newsFeedItems.Count-1; i >= 0; i--)
        {
            if(newsFeedItems[i].ValidateItem(playerController.newsCounter, playerController.choiceHistory))
            {
                AddNewsItem(newsFeedItems[i]);
                newsFeedItems.RemoveAt(i);
            }
        }
    }

    public void AddNewsItem(NewsFeedItem item)
    {
        GameObject newInstance = Instantiate(newsItemPrefab, newsContainer.transform, false);
        newInstance.transform.SetParent(newsContainer.transform, false);
        newInstance.transform.Find("NewsItemHeader").GetComponent<TextMeshProUGUI>().SetText(item.header);
        newInstance.transform.Find("NewsItemContent").GetComponent<TextMeshProUGUI>().SetText(item.content);
    }
}
