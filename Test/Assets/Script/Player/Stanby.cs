using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stanby : MonoBehaviour
{
    public Animator animator;
    public GameObject gun;

    void Start()
    {
        animator.SetBool("Stanby",true);
        this.GetComponent<Player>().enabled = false;
        gun.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.P)){
            animator.SetBool("Stanby",false);
            this.GetComponent<Player>().enabled = true;
            StartCoroutine(GunPlay());
        }
    }

    IEnumerator GunPlay()
    {
        yield return new WaitForSeconds(3f);
        gun.SetActive(true);

    }
}
