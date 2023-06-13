using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleUniverse : MonoBehaviour
{
    public Grafo mapa;
    public MovimientoEnGrafo personaje;
    public int Posicion;

    //private bool buscar = true;
    // Start is called before the first frame update
    private void Awake()
    {
        mapa = GameObject.Find("Mapa").GetComponent<Grafo>();
        personaje = GameObject.Find("dotperson").GetComponent<MovimientoEnGrafo>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnMouseDown()
    {
        mapa.final = Posicion;
        Debug.Log("debug " + Posicion);
        mapa.CalculaRuta();
        mapa.inicio = Posicion;
        Debug.Log("El inicio ahora es: " + mapa.inicio);
        personaje.enObjetivo = false;
        //mapa.moverPersonaje = true;
        Debug.Log("NANI"+Posicion);
    }
}
