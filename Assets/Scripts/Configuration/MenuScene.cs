using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour {


    private float fadeInspeed = 0.44f;
    public Transform weaponsPanel;
    public Transform weaponsOwnedPanel;
    public Text weaponsBuySetText;



    private int[] cansCost = new int[] { 20, 100 };
    private int[] bagsCost = new int[] { 20, 100 };
    private int[] bottlesCost = new int[] { 20, 100 };

    private int selectedWeaponIndex;

    public Text cristalSwordText;
    public Text hSlayerText;
    public Text materials;

    public Text stateText;
    public Text changeButtonText;



    private void Start()
    {
        cristalSwordText.text = cansCost[0].ToString() + "       " + bottlesCost[0].ToString() + "       " + bagsCost[0].ToString();
        hSlayerText.text = cansCost[1].ToString() + "       " + bottlesCost[1].ToString() + "       " + bagsCost[1].ToString();
        Debug.Log("el modo estatico esta "+SaveMananger.Instance.state.StaticMode);
        InitShop();
    }
    private void Update()
    {
        CheckState();
        InitProgress();
        materials.text = SaveMananger.Instance.cansOwned() + "    " + SaveMananger.Instance.bottleOwned() + "    " + SaveMananger.Instance.bagsOwned();
    }
    private void InitShop()
    {
        //we have asigned the referenci
        if (weaponsPanel == null)
        {
            Debug.Log("you no asigned the panel in the inspector");
        }
        // for every children put an add an event
        int i = 0;
        foreach (Transform t in weaponsPanel)
        {
            int currentIndex = i;

            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnWeaponSelect(currentIndex));
            i++;
        }
    }
    private void InitProgress()
    {
        //we have asigned the referenci
        if (weaponsOwnedPanel == null)
        {
            Debug.Log("you no asigned the panel in the inspector");
        }
        // for every children put an add an event
        int i = 0;
        foreach (Transform t in weaponsOwnedPanel)
        {
            int currentIndex = i;

            Image b = t.GetComponent<Image>();
            if (SaveMananger.Instance.isWeaponOwned(currentIndex))
            {
                b.gameObject.SetActive(true);
            }
            else
            {
                b.gameObject.SetActive(false);
            }
            i++;
        }
    }
    private void OnWeaponSelect(int currentIndex)
    {



        selectedWeaponIndex = currentIndex;

        weaponsBuySetText.text = "COMPRAR";




        //change the content of the buy set button dpending on the state of the weapon
        if (SaveMananger.Instance.isWeaponOwned(selectedWeaponIndex))
        {

            Debug.Log("entro if");
            //weapon owned
            weaponsBuySetText.text = "EQUIPAR";
        }
        else
        {
            //weapon no owned
            Debug.Log(" no entro if");
        }

    }
    private void SetWeapon(int index)
    {
        //change the weapon on the player


        //change buy/ set button text
        weaponsBuySetText.text = "EQUIPADO";
    }
    public void OnWeaponBuySet()
    {
        Debug.Log("Buy/set weapon");
        //is the selected weapon owned

        if (SaveMananger.Instance.isWeaponOwned(selectedWeaponIndex))
        {
            SetWeapon(selectedWeaponIndex);
        }
        else
        {
            //attempo to buy
            if (SaveMananger.Instance.BuyWeapon(selectedWeaponIndex, cansCost[selectedWeaponIndex], bottlesCost[selectedWeaponIndex], bagsCost[selectedWeaponIndex]))
            {
                //succes
                SetWeapon(selectedWeaponIndex);

            }
            else
            {
                //do no have enough gold
                //play sound feedback
                Debug.Log("you dont have enought materials");
            }
        }



    }
    public void ChangeState()
    {
        SaveMananger.Instance.OnOffStaticMode(); 
    }
    public void CheckState()
    {
        if (SaveMananger.Instance.modeConsult())
        {
            stateText.text = "MODO ESTÁTICO ACTIVADO";
            changeButtonText.text = "DESACTIVAR";
        }
        else
        {
            stateText.text = "MODO ESTÁTICO DESACTIVADO";
            changeButtonText.text = "ACTIVAR";
        }
    }
    



    




}
