using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public static StackManager Instance;

    public GameObject resetButton;
    public GameObject testButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        BlockSpawner[] children = GetComponentsInChildren<BlockSpawner>();

        foreach(BlockSpawner spawner in children)
        {
            spawner.SetBlocks();
        }
    }

    public void ExecuteTestStacks()
    {
        BlockSpawner[] children = GetComponentsInChildren<BlockSpawner>();

        foreach(BlockSpawner spawner in children)
        {
            spawner.TestStack();
        }

        resetButton.SetActive(true);
        testButton.SetActive(false);
    }

    public void ResetStacks()
    {
        BlockSpawner[] children = GetComponentsInChildren<BlockSpawner>();

        foreach (BlockSpawner spawner in children)
        {
            spawner.ResetStack();
        }
        resetButton.SetActive(false);
        testButton.SetActive(true);
    }
}
