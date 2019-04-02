using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnScript : NetworkBehaviour
{

	public GameObject playerObject;
	GameObject m_Instantiated;
    // Start is called before the first frame update
    void Start()
    {
		if(!isServer){
		m_Instantiated = Instantiate(playerObject);
		NetworkServer.Spawn(m_Instantiated);
		}
    }

}
