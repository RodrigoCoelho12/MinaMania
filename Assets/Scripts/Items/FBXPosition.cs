using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class FBXPosition : MonoBehaviour
{
    public List<GameObject> positions = new List<GameObject>();
    public List<GameObject> prefab = new List<GameObject>();

    int Randomizar(int limit)
    {
        System.Random rand = new System.Random();

        return rand.Next(0, limit);
    }
    public void Position()
    {
        Debug.Log("Entrei");
        int contador = 0;
        int indexPosicao, indexPrefab;
        do
        {
            indexPosicao = Randomizar(positions.Count);
            indexPrefab = Randomizar(prefab.Count);
            try
            {
                Instantiate(prefab[indexPrefab], positions[indexPosicao].transform.position,Quaternion.identity);
            } 
            catch (System.Exception ex) 
            {
                Debug.Log($"Objeto de index {indexPrefab} nao instanciado na posicao de index {indexPosicao}. Erro {ex}");
            }
            contador++;
        }
        while (contador < 10);
    }
}
