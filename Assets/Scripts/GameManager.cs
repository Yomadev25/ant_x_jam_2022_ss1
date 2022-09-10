using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Action onStageStart, onStageEnd;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        StageStart();
    }

    void Update()
    {
        
    }

    public void StageStart()
    {
        onStageStart?.Invoke();
    }

    public void StageEnd()
    {
        onStageEnd?.Invoke();
    }
}
