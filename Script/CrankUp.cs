using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankUp : MonoBehaviour
{
    public Renderer cusRender;
    public CrankDown crankDown;
    void Start()
    {
        cusRender = GetComponent<Renderer>();
        crankDown = GameObject.FindGameObjectWithTag("CrankDown").GetComponent<CrankDown>();

        cusRender.enabled = true;
        crankDown.cusRender.enabled = false;

    }


    void Update()
    {
        
    }
    public void CrankDown()
    {
        cusRender.enabled = false;    
        crankDown.cusRender.enabled = true;

    }
}
