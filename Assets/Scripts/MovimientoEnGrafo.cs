using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnGrafo : MonoBehaviour
{
    public Grafo mapa;
    private int nodoActual = 0;
    public bool enObjetivo = false;

    public bool empezar = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (empezar)
        //{
            movepiece();
            //empezar=false;
        //}
    }

    public void movepiece()
    {
        if (enObjetivo == false)
        {
            Vector3 nodoObjetivo =
                new Vector3(
                    mapa.NodosCoords[mapa.ruta[nodoActual]].x + 0.5f,
                    mapa.NodosCoords[mapa.ruta[nodoActual]].y + 0.5f,
                    0.5f);
            if (Vector3.Magnitude(transform.position - nodoObjetivo) < 0.1)
            {
                nodoActual++;
                Debug.Log(nodoActual);
                if (nodoActual == mapa.ruta.Count)
                {
                    enObjetivo = true;
                    nodoActual = 0;
                    
                }
            }

            transform.LookAt(nodoObjetivo);
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
        }
    }
}
