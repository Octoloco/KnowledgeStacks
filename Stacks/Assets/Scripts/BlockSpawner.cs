using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] Transform positionLeft;
    [SerializeField] Transform positionCenter;
    [SerializeField] Transform positionRight;
    [SerializeField] Transform positionLeftAlt;
    [SerializeField] Transform positionRightAlt;
    [SerializeField] GameObject block;
    [SerializeField] string grade;

    [SerializeField] Material woodMat;
    [SerializeField] Material stoneMat;
    [SerializeField] Material glassMat;

    [SerializeField] TMP_Text label;

    private List<Transform> blockList = new List<Transform>();

    public void SetBlocks()
    {
        for (int i = 0; i < JsonReader.Instance.blocks.Count; i++)
        {
            if (JsonReader.Instance.blocks[i].grade == grade)
            {
                GameObject newBlock = Instantiate(block, pivot.transform.position, Quaternion.identity);
                newBlock.transform.SetParent(pivot);
                newBlock.GetComponent<BlockData>().Data = JsonReader.Instance.blocks[i];

                if (JsonReader.Instance.blocks[i].mastery == 0)
                {
                    newBlock.GetComponent<Renderer>().material = glassMat;
                }
                else if (JsonReader.Instance.blocks[i].mastery == 1)
                {
                    newBlock.GetComponent<Renderer>().material = woodMat;
                }
                else
                {
                    newBlock.GetComponent<Renderer>().material = stoneMat;
                }

                blockList.Add(newBlock.transform);
            }
        }

        label.text = grade;

        PositionBLocks();
    }

    private void PositionBLocks()
    {
        int sets = Mathf.CeilToInt(blockList.Count / 3f);
        int childIndex = 0;
        bool rotate = false;

        for (int i = 0; i < sets; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (childIndex < blockList.Count)
                {
                    if (rotate)
                    {
                        pivot.transform.GetChild(childIndex).transform.Rotate(new Vector3(0, 90, 0));
                        if (j == 0)
                        {
                            pivot.transform.GetChild(childIndex).transform.localPosition = new Vector3(0, i * .7f, positionLeftAlt.position.z);
                        }
                        else if (j == 1)
                        {
                            pivot.transform.GetChild(childIndex).transform.position = new Vector3(positionCenter.position.x, i * .7f, 0);
                        }
                        else
                        {
                            pivot.transform.GetChild(childIndex).transform.localPosition = new Vector3(0, i * .7f, positionRightAlt.position.z);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            pivot.transform.GetChild(childIndex).transform.position = new Vector3(positionLeft.position.x, i * .7f, 0);
                        }
                        else if (j == 1)
                        {
                            pivot.transform.GetChild(childIndex).transform.position = new Vector3(positionCenter.position.x, i * .7f, 0);
                        }
                        else
                        {
                            pivot.transform.GetChild(childIndex).transform.position = new Vector3(positionRight.position.x, i * .7f, 0);
                        }
                    }
                }
                else
                {
                    break;
                }

                childIndex++;
            }
            rotate = !rotate;
        }
    }

    public void TestStack()
    {
        foreach(Transform block in blockList)
        {
            if (block.GetComponent<BlockData>().Data.mastery == 0)
            {
                block.gameObject.SetActive(false);
            }
            else
            {
                block.GetComponent<Rigidbody>().isKinematic = false;
                block.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    public void ResetStack()
    {
        foreach (Transform block in blockList)
        {
            if (block.GetComponent<BlockData>().Data.mastery == 0)
            {
                block.gameObject.SetActive(true);
            }
            block.transform.rotation = Quaternion.identity;
            block.GetComponent<Rigidbody>().isKinematic = true;
            block.GetComponent<Rigidbody>().useGravity = false;
            block.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        PositionBLocks();

    }
}
