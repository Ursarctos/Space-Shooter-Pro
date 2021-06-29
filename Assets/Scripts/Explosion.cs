using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3);
    }

}
