using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersistentPlayerData : MonoBehaviour
{

    private string companyName;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SetCompanyName(string companyName)
    {
        this.companyName = companyName;
    }
    public string GetCompanyName()
    {
        return companyName;
    }

}
