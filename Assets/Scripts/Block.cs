using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float minHeight;
    public float maxHeight;

    public GameObject root;
    public float bottomBlock;

    void Start()
    {
        ChangeHeight();
    }
        
        void ChangeHeight(){
            //setting for random height
            float height = Random.Range(minHeight, maxHeight);
            root.transform.localPosition = new Vector3(0.0f, height, 0.0f);
        }
       void ChangeGap()
    {
        float offset = Random.Range(bottomBlock, minHeight);
        root.transform.localPosition = new Vector3(offset, 0.0f, 0.0f);
    }

        //receiving message change height
        void OnScrollEnd()
        {
            ChangeHeight();
        ChangeGap();
        }
        
    }

