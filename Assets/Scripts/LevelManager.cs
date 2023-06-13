using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //  1.(LevelManager)  Esta clase jugara con los bloques de niveles y generara los niveles procedurales. 
    //Se tratara de un singleton como casi todos los managers 
    public static LevelManager sharedInstanceLevelManager;

    //  1.1(LevelManager) Se guardaran los bloques de dos formas, uno con los bloques actuales, el otro con la totalidad de bloques.
    public int numberOnGrafo = 0;

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    public Transform levelStartPosition;
    //********************************
    //  Unity methods
    private void Awake()
    {
        if (sharedInstanceLevelManager == null)
        {
            sharedInstanceLevelManager= GetComponent<LevelManager>();
        }
    }
    void Start()
    {
        GenerateInitialBlocks();
    }
 
    void Update()
    {
        
    }
    //********************************
    /*public void AddLevelBlock()
    {
        // 1.2(Level Manager)   Ahora debemos hacer que aparezca alguno de los bloques de manera aleatoria, donde el primero sera el primero de la lista que spawneara en la posición 0,0,0.
        int randomIdx = Random.Range(0,allTheLevelBlocks.Count);
        LevelBlock block;

        Vector3 spawnPosition = Vector3.zero;

        if(currentLevelBlocks.Count == 0)
        {// 1.3(Level Manager) si no hay nada instanciara el bloque de la posición 0 del array allTheLevelblocks en la posición inicial
            block = Instantiate(allTheLevelBlocks[0]);
        spawnPosition = levelStartPosition.position;
        }
        else
        {// 1.4(Level Manager) si hay instanciara una posición random con todas igual posibilidad de aparecer con posición inicial en el exit point de la instancia inmediatamente anterior
            block = Instantiate(allTheLevelBlocks[randomIdx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
        }
        block.transform.SetParent(this.transform, false);
        Vector3 correction = new Vector3(spawnPosition.x - block.startPoint.position.x,
            spawnPosition.y - block.startPoint.position.y,
            0);
        block.transform.position = correction; 
        currentLevelBlocks.Add(block);
    }*/

    public void AddLevelBlock()
    {
        // 1.2(Level Manager)   Ahora debemos hacer que aparezca alguno de los bloques de manera aleatoria, donde el primero sera el primero de la lista que spawneara en la posición 0,0,0.
        //int numberOnGrafo = Random.Range(0, allTheLevelBlocks.Count); 
        LevelBlock block;
        
        block = Instantiate(allTheLevelBlocks[numberOnGrafo]);
        currentLevelBlocks.Add(block);
    }

    public void RemoveLevelBlock()
    {
        // 1.5(Level Manager)indico que levelblock desaparecerá y lo destruyo, siempre sera el primero por lo que asi esta bien.
        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllLevelBlocks()
    {
        while(currentLevelBlocks.Count > 0) 
        {
            RemoveLevelBlock(); 
        }
    }

    public void GenerateInitialBlocks()
    {
        for (int i = 0; i < 1; i++)
        {
            AddLevelBlock();
        }
    }
}
