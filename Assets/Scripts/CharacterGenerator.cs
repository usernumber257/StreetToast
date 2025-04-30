using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// generates character randomly by gameobject pieces
/// </summary>
public class CharacterGenerator : MonoBehaviour
{
    [SerializeField] Transform hairParent;
    [SerializeField] Transform faceParent;
    [SerializeField] Transform topParent;
    [SerializeField] Transform accParent;

    GameObject[] hairs;
    GameObject[] faces;
    GameObject[] tops;
    GameObject[] accs;

    private void Start()
    {
        //make array
        MakeArray(ref hairs, hairParent);
        MakeArray(ref faces, faceParent);
        MakeArray(ref tops, topParent);
        MakeArray(ref accs, accParent);


        //disactive all gameobjects
        DisActive(hairs);
        DisActive(faces);
        DisActive(tops);
        DisActive(accs);

        //then generate by random
        Generate(hairs);
        Generate(faces);
        Generate(tops);
        Generate(accs, true); //can have empty accs
    }

    /// <summary>
    /// make array with parnet's child
    /// </summary>
    /// <param name="array"></param>
    /// <param name="parent"></param>
    void MakeArray(ref GameObject[] array, Transform parent)
    {
        array = new GameObject[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
            array[i] = parent.GetChild(i).gameObject;
    }

    /// <summary>
    /// generate random character pieces. if has none true, they have empty piece
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="hasNone"></param>
    void Generate(GameObject[] arr, bool hasNone = false)
    {
        if (arr == null || arr.Length == 0)
            return;

        int randNum = 0; //init

        //-1 for empty piece
        randNum = hasNone ? Random.Range(-1, arr.Length) : Random.Range(0, arr.Length);

        if (randNum != -1)
            arr[randNum].SetActive(true);
    }

    /// <summary>
    /// disactive array's gameobjects
    /// </summary>
    /// <param name="arr"></param>
    void DisActive(GameObject[] arr)
    {
        if (arr == null || arr.Length == 0)
            return;

        for (int i = 0; i < arr.Length; i++)
            arr[i].SetActive(false);
    }
}
