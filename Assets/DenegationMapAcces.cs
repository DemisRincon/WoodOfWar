
using UnityEngine;

public class DenegationMapAcces : MonoBehaviour {


    public GameObject mapAcces;

	// Update is called once per frame
	void Update () {

        if (SaveMananger.Instance.modeConsult())
        {
            mapAcces.SetActive(false);
        }
        
	}
}
