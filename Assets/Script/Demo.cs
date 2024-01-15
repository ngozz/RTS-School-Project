using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] Timer timer1;

    private void Start()
    {
        timer1.SetDuration(22).Begin();
    }
}
