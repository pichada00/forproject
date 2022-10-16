using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzlemanager : MonoBehaviour
{
    public int indexForDestroyTotem;
    public GameObject wall;
    public MeshRenderer renderer;
    public Material material;
    private float speed = 0.1f;
    private float dissolve;
    private void Update()
    {
        if(indexForDestroyTotem == 4)
        {
            dissolve += speed * Time.deltaTime;
            //animation
            //Material material = renderer.material;
            renderer.material.SetFloat("Dissolve", dissolve);

            //wall.SetActive(false);
        }
    }

}
