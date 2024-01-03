using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public void DestroyStone()
    {
        //Destroy self
        Destroy(gameObject);
    }
}
