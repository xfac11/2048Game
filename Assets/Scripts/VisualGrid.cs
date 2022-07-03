using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGrid : MonoBehaviour
{
    [SerializeField] private int mWidth;
    [SerializeField] private int mHeight;
    [SerializeField] private Vector2 mCellSize;
    [SerializeField] private Vector2 mSpacing;

    public void SetTransform(Transform objTransform, int x, int y)
    {
        if (x >= mWidth || y >= mHeight || x < 0 || y < 0)
        {
            return;
        }
        objTransform.SetParent(transform);
        objTransform.localPosition = new Vector3((x * mCellSize.x) + mSpacing.x, (y * mCellSize.y) + mSpacing.y);
    }

    private void Awake()
    {
        int x = 0;
        int y = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if(mWidth == i)
            {
                y++;
                x = 0;
            }
            SetTransform(transform.GetChild(i), x, y);
            x++;
        }
    }
}
