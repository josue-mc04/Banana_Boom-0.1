using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<GameObject> armas; 
    private int indiceActual = 0;

    void Start()
    {
        ActivarArma(indiceActual);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CambiarArma();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    void CambiarArma()
    {
        armas[indiceActual].SetActive(false);
        indiceActual = (indiceActual + 1) % armas.Count;
        armas[indiceActual].SetActive(true);
        Debug.Log("arma actual: " + armas[indiceActual].name);
    }

    void Disparar()
    {
        Debug.Log("usaste el arma: " + armas[indiceActual].name);
    }

    void ActivarArma(int i)
    {
        for (int j = 0; j < armas.Count; j++)
        {
            armas[j].SetActive(j == i);
        }
    }
}
