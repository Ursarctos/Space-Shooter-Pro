using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Renderer rend;

    [SerializeField]
    private Color colorToTurnTo = Color.white;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = colorToTurnTo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
