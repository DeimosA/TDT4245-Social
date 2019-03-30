using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UICompanyController : MonoBehaviour
{

    private TextMeshProUGUI companyName;
    private CompanyModel company;


    // Start is called before the first frame update
    void Start()
    {
        companyName = transform.Find("CompanyNameText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetCompanyName(string newCompanyName)
    {
        //transform.Find("CompanyNameText").GetComponent<TextMeshProUGUI>().SetText(companyName);
        if (companyName == null)
        {
            companyName = transform.Find("CompanyNameText").GetComponent<TextMeshProUGUI>();
        }
        this.companyName.SetText(newCompanyName);
    }

    private void EstablishCommunication()
    {
        // TODO do something. Show establish dialog?
    }

    public void SetCompanyModel(CompanyModel companyModel)
    {
        this.company = companyModel;
        SetCompanyName(company.companyName);
    }

    public void HandleCommButtonClick()
    {
        if (company.commEstablished)
        {
            EstablishCommunication();
        }
        else
        {

        }
    }
}
