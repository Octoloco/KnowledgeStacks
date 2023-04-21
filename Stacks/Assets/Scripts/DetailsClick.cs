using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailsClick : MonoBehaviour
{

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text clusterText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Animator detailsBar;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Block"))
                {
                    levelText.text = hit.transform.GetComponent<BlockData>().Data.domain;
                    clusterText.text = hit.transform.GetComponent<BlockData>().Data.cluster;
                    descriptionText.text = hit.transform.GetComponent<BlockData>().Data.standarddescription;
                    ShowBar();
                }
                else
                {
                    HideBar();
                }
            }
            else
            {
                HideBar();
            }
        }
    }

    void ShowBar()
    {
        detailsBar.SetBool("show", true);
    }

    void HideBar()
    {
        detailsBar.SetBool("show", false);
    }

}

