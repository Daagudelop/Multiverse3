using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public struct NodoGrafo
{
    public float x;
    public float y;

    public NodoGrafo(float px, float py)
    {
        x = px;
        y = py;
    }
}
public class Grafo : MonoBehaviour
{
    public int inicio = 0;
    public int final = 6;
    public int cantNodos = 7;
    public List<int> ruta;
    public GameObject personaje;
    public MovimientoEnGrafo pers2;
    [SerializeField] CircleUniverse circle;
    public bool moverPersonaje = false;

    private int[,] mAdyacencia;
    private int[] indegree;
    public NodoGrafo[] NodosCoords;
    private bool inicializado = false;

    private void Start()
    {
        mAdyacencia = new int[cantNodos, cantNodos];

        indegree = new int[cantNodos];

        NodosCoords = new NodoGrafo[cantNodos];

        ruta = new List<int>();

        AdicionarArista(0, 1);
        AdicionarArista(0, 2);
        AdicionarArista(0, 3);
        AdicionarArista(0, 4);
        AdicionarArista(0, 5);
        AdicionarArista(0, 35);

        AdicionarArista(1, 6);

        AdicionarArista(2, 3);

        AdicionarArista(3, 4);
        AdicionarArista(3, 6);

        AdicionarArista(4, 5);

        AdicionarArista(5, 6);
        AdicionarArista(5, 9);
        AdicionarArista(5, 21);
        AdicionarArista(5, 22);

        AdicionarArista(6, 7);

        AdicionarArista(7, 8);

        AdicionarArista(8, 9);

        AdicionarArista(9, 10);

        AdicionarArista(10, 11);
        AdicionarArista(10, 21);

        AdicionarArista(11, 21);
        AdicionarArista(11, 12);

        AdicionarArista(12, 13);

        AdicionarArista(13, 14);
        AdicionarArista(13, 15);

        AdicionarArista(14, 15);
        AdicionarArista(14, 16);
        AdicionarArista(14, 17);
        AdicionarArista(14, 18);
        AdicionarArista(14, 20);

        AdicionarArista(15, 16);
        AdicionarArista(15, 19);

        AdicionarArista(16, 17);

        AdicionarArista(17, 18);

        AdicionarArista(18, 19);

        AdicionarArista(19, 20);

        AdicionarArista(20, 21);

        AdicionarArista(21, 22);

        AdicionarArista(22, 23);

        AdicionarArista(23, 24);

        AdicionarArista(24, 25);

        AdicionarArista(25, 26);

        AdicionarArista(26, 27);

        AdicionarArista(27, 28);

        AdicionarArista(28, 29);
        AdicionarArista(28, 30);

        AdicionarArista(29, 30);
        AdicionarArista(29, 31);
        AdicionarArista(29, 32);

        AdicionarArista(30, 31);
        AdicionarArista(30, 32);

        AdicionarArista(31, 32);

        AdicionarArista(32, 33);

        AdicionarArista(33, 34);

        AdicionarArista(34, 35);

        AdicionaCoords(0, 0, -20);
        AdicionaCoords(1, 4, -20);
        AdicionaCoords(2, 4, -24);
        AdicionaCoords(3, 8, -24);
        AdicionaCoords(4, 12, -24);
        AdicionaCoords(5, 16, -24);
        AdicionaCoords(6, 8, -20);
        AdicionaCoords(7, 12, -20);
        AdicionaCoords(8, 16, -20);
        AdicionaCoords(9, 20, -20);
        AdicionaCoords(10, 20, -24);
        AdicionaCoords(11, 20, -28);
        AdicionaCoords(12, 20, -32);
        AdicionaCoords(13, 20, -36);
        AdicionaCoords(14, 20, -40);
        AdicionaCoords(15, 16, -36);
        AdicionaCoords(16, 12, -36);
        AdicionaCoords(17, 8, -36);
        AdicionaCoords(18, 16, -40);
        AdicionaCoords(19, 12, -40);
        AdicionaCoords(20, 16, -32);
        AdicionaCoords(21, 16, -28);
        AdicionaCoords(22, 12, -28);
        AdicionaCoords(23, 12, -32);
        AdicionaCoords(24, 8, -32);
        AdicionaCoords(25, 8, -28);
        AdicionaCoords(26, 4, -28);
        AdicionaCoords(27, 4, -32);
        AdicionaCoords(28, 8, -40);
        AdicionaCoords(29, 4, -36);
        AdicionaCoords(30, 4, -40);
        AdicionaCoords(31, 0, -40);
        AdicionaCoords(32, 0, -36);
        AdicionaCoords(33, 0, -32);
        AdicionaCoords(34, 0, -28);
        AdicionaCoords(35, 0, -24);

        inicializado = true;

        personaje.transform.position = new Vector3(NodosCoords[inicio].x, NodosCoords[inicio].y, 0.5f);

        CreatePoints();

        CalculaRuta();

    }
    private void Update()
    {
        /*if (moverPersonaje== true)
        {
            Debug.Log("amover " + final );
            CalculaRuta();
            Debug.Log("amover1");
            pers2.empezar = true;
            Debug.Log("amover2");
            moverPersonaje = false;
            Debug.Log("amover3");
        }*/
    }
    private void OnDisable()
    {
        inicializado = false;
    }

    public void AdicionarArista(int pNodoInicio, int pNodoFinal)
    {
        mAdyacencia[pNodoInicio, pNodoFinal] = 1;
        mAdyacencia[pNodoFinal, pNodoInicio] = 1;
    }

    public void AdicionaCoords(int pNodo,float px, float py)
    {
        NodosCoords[pNodo] = new NodoGrafo(px, py);
    }

    private void CreatePoints() 
    {
        circle.Posicion = 0;
        foreach (NodoGrafo miNodo in NodosCoords)
        {
            
            Instantiate(circle, new Vector3(
                        miNodo.x,
                        miNodo.y,
                        0),Quaternion.identity);
            circle.Posicion++;
        }
        circle.Posicion = 0;
    }

    private void OnDrawGizmos()
    {
        if (inicializado)
        {
            foreach (NodoGrafo miNodo in NodosCoords)
            {
                /*Gizmos.color = Color.green;
                Gizmos.DrawSphere(
                    new Vector3(
                        miNodo.x, 
                        miNodo.y, 
                        0),0.5f);*/
                //Instantiate(circle);
            }

            int n = 0;
            int m = 0;

            for (n = 0; n < cantNodos; n++)
            {
                for (m = 0;m< cantNodos; m++)
                {
                    if (mAdyacencia[n,m]!=0)
                    {
                        Gizmos.color = Color.black;
                        Gizmos.DrawLine(
                            new Vector3(
                            NodosCoords[n].x,
                            NodosCoords[n].y,
                            0),
                            new Vector3(
                            NodosCoords[m].x,
                            NodosCoords[m].y,
                            0));
                    }
                }
            }
        }
    }

    

    public int ObtenAdyacencia (int pFila, int pColumna)
    {
        return mAdyacencia[pFila, pColumna];
    }

    public void CalcularIndegree()
    {
        int n = 0;
        int m = 0;

        for(n = 0;n < cantNodos; n++)
        {
            for(m = 0; m< cantNodos; m++)
            {
                if (mAdyacencia[m,n] == 1)
                {
                    indegree[n]++;
                }
            }
        }
    }
    public int EncuentraIO()
    {
        bool encontrado = false;
        int n = 0;

        for (n = 0; n < cantNodos; n++)
        {
            if (indegree[n] == 0)
            {
                encontrado = true;
                break;
            }
        }
        if (encontrado)
        {
            return n;
        }
        else
        {
            return -1;
        }
    }

    public void DecrementaIndegree(int pNodo)
    {
        indegree[pNodo] = -1;

        int n = 0;

        for (n = 0 ; n < cantNodos; n++)
        {
            if (mAdyacencia[pNodo, n] == 1)
            {
                indegree[n]--;
            }
        }
    }

    public void CalculaRuta()
    {
        ruta.Clear();
        int[,] tabla = new int[cantNodos, 3];

        int n = 0;
        int distancia= 0;
        int m = 0;

        for (n = 0; n < cantNodos; n++)
        {
            tabla[n, 0] = 0;
            tabla[n, 1] = int.MaxValue;
            tabla[n, 2] = 0;
        }
        tabla[inicio,1] = 0;

        for (distancia = 0;
            distancia < cantNodos;
            distancia++) 
        { 
            for (n = 0; n < cantNodos; n++) 
            {
                if ((tabla[n,0]==0)
                    && 
                    (tabla[n,1]==distancia) )
                {
                    tabla[n, 0] = 1;
                    for (m = 0;
                        m < cantNodos;
                        m++)
                    {
                        if (ObtenAdyacencia(n,m) == 1)
                        {
                            if (tabla[m,1] == int.MaxValue)
                            {
                                tabla[m,1] = distancia+1;
                                tabla[m,2] = n;
                            }
                        }
                    }
                }
            }
        }
        ruta.Clear();
        int nodo = final;

        while(nodo != inicio)
        {
            ruta.Add(nodo);
            nodo = tabla[nodo, 2];
        }
        ruta.Add(inicio); 
        ruta.Reverse();

        foreach (var item in ruta)
        {
            Debug.Log("L "+item);
        }
    }
}
