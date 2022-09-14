using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    SphereCollider collider;
    IEnumerator Start()
    {
        collider = GetComponent<SphereCollider>();
        yield return new WaitForSeconds(0.1f);
        collider.enabled = false;
    }

}
