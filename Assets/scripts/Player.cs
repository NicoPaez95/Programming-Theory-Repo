using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator myPlayerAnim;

    private void Awake()
    {
        myPlayerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
      
    }
}
