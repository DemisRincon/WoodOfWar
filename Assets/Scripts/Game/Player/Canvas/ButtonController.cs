using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public Button attack1;
    public Button attack2;
    public Button cristalSword;
    public Button hSlayer;

    private void Start()
    {
        
        if (SaveMananger.Instance.isWeaponOwned(1))
        {
            attack1.gameObject.SetActive(false);
            hSlayer.gameObject.SetActive(true);
        }
        else
        {
            attack1.gameObject.SetActive(true);
            hSlayer.gameObject.SetActive(false);
        }
        if (SaveMananger.Instance.isWeaponOwned(0))
        {
            attack2.gameObject.SetActive(false);
            cristalSword.gameObject.SetActive(true);
        }
        else
        {
            attack2.gameObject.SetActive(true);
            cristalSword.gameObject.SetActive(false);
        }
    }

}
