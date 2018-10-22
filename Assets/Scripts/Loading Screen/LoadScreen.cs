using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LoadScreen : MonoBehaviour {

    [SerializeField]
    private float units;
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject infofield;
    [SerializeField]
    private Image fill;
    [SerializeField]
    private Text messageText;
    
    private float fillAmount;

    private string[] Messages = new string[] {
        "Enjuaga la lata de aluminio y procede a aplastarla y véndela a los chatarreros",
        "Clasifica por color el vidrio: verde, ámbar y transparente, lávalos y llévalo a una planta recicladora de vidrio",
        "Evita que el vidrio(botellas, frascos, etc) se rompa para que pueda ser reciclado",
        "Enjuaga la botella de plástico, aplástala y llévalo a un contenedor de plástico",
        "Lleva tus papeles que ya no utilices limpios y secos a un deposito o a fabricas recicladoras",
        "Lleva el cartón limpio y seco a los contenedores donde se recolectan para ser llevado a la planta recicladora",
        "Sabías que al reciclar una tonelada de cartón se ahorran 50000 litros de agua",
        "Sabías que al reciclar una tonelada de cartón se disminuyen 900 kilos de dióxido de carbono",
        "Sabías que el papel reciclado es mucho más barato que el papel nuevo",
        "Puedes reciclar el vidrio si este está completamente limpio de etiquetas y otros utensilios",
        "Sabías que se ahorra un 30% de energía al reciclar el vidrio",
        "Sabías que el plástico reduce la cantidad de residuos provocados por botellas, bolsas plásticas",
        "Al reciclar el aluminio se ahorra un 94% del coste energético original",
        "Puedes reciclar latas, embalajes, virutas, etc",
        "Sabías que el plástico tarda 180 años en descomponerse",
        "El papel tarda 1 año en degradarse",
        "Sabías que el cartón reciclado se vuelve a utilizar para hacer nuevas cajas o tubos de cartón"
    };
    

    private void Start()
    {
        StartCoroutine(BuildUnits());
        AssignMessage();
    }
    private void Update()
    {
        UpdateBar();
    }

    public IEnumerator BuildUnits()
    {
        for (int i = 0; i <= units; i++)
        {
            fillAmount = i / units;
            yield return null;
        }

        //DONE LOADING;
        button.SetActive(true);
        infofield.SetActive(false);

    }
    
    private void AssignMessage()
    {   
        int posicionMensaje = Random.Range(0,Messages.Length);
        messageText.text = Messages[posicionMensaje];
    }


    private void UpdateBar()
    {
        fill.fillAmount = fillAmount;
    }

  
}
