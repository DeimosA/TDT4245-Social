using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages the local news feed
/// Notifies other players of newsfeed updates when local player causes new item to be added
/// </summary>
public class PlayerNewsFeedController : MonoBehaviour
{
    //list of all newsfeeditems. Must be same for all players
    public List<NewsFeedItem> newsFeedItems;

    private PlayerController playerController;
    private UINewsFeedController uiNewsFeedController;
    //keep track of which newsfeeditems player has triggered, to avoid duplicates
    private HashSet<NewsFeedItem> newsItemsTriggeredByPlayer;

    // Start is called before the first frame update
    void Start()
    {
        newsItemsTriggeredByPlayer = new HashSet<NewsFeedItem>();
        uiNewsFeedController = GameObject.Find("NewsFeedContent").GetComponent<UINewsFeedController>();
        playerController = GetComponent<PlayerController>();
    }

    //trigger any valid newsitems
    public void OnTurnStart()
    {

        foreach(NewsFeedItem item in newsFeedItems)
        {
            //skip already triggered items
            if (newsItemsTriggeredByPlayer.Contains(item)) continue;
            //check if valid
            else
            {
                if (item.ValidateItem(playerController.newsCounter, playerController.choiceHistory))
                {
                    //trigger newsitem
                    newsItemsTriggeredByPlayer.Add(item);
                    //add news item to ui. Format string to include player company name
                    uiNewsFeedController.AddNewsItem(string.Format(item.content, playerController.name));
                

                    //TODO: Propagate to other players??
                }
            }
        }
    }
}
