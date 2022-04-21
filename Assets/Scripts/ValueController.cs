using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Assets.Scripts
{
    struct Action
    {
        public int moveToX;
        public int moveToY;
        public ActionType type;
    }
    enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    enum ActionType
    {
        Nothing,
        Merge,
        Move
    }
    public class ValueController : MonoBehaviour
    {
        Grid<int> mCurrentGrid;
        Grid<GameObject> mVisualGrid;
        [SerializeField] private List<GameObject> pGridObjects;
        private bool[] mCanMoveorMerge = new bool[4];
        public int Left { get { return -1; } }
        public int Right { get { return 1; } }
        public int Up { get { return -1; } }//Need to check
        public int Down { get { return 1; } }//Need to check
        private void Awake()
        {
            mCurrentGrid = new Grid<int>(4, 4);
            mVisualGrid = new Grid<GameObject>(4, 4);
            for (int i = 0; i < mCurrentGrid.Size; i++)
            {
                mCurrentGrid.SetValue(0, i);
                mVisualGrid.SetValue(pGridObjects[i], i);
            }

            mCurrentGrid.SetValue(2, 5);
            UpdateVisuals();
        }
        private void UpdateVisuals()
        {
            for (int i = 0; i < mVisualGrid.Size; i++)
            {
                mVisualGrid.GetValue(i).GetComponent<Value>().SetValue(mCurrentGrid.GetValue(i));
            }
        }
        public void MoveLeft()
        {
            for (int y = 0; y < mCurrentGrid.Height; y++)
            {
                for (int x = 0; x < mCurrentGrid.Width; x++)
                {
                    Action action = CheckLeft(x, y);
                    int value = mCurrentGrid.GetValue(x, y);
                    int moveToValue = mCurrentGrid.GetValue(action.moveToX, action.moveToY);
                    switch (action.type)
                    {
                        case ActionType.Nothing:
                            break;
                        case ActionType.Merge:
                            if (!mCanMoveorMerge[(int)Direction.Left])
                            {
                                mCanMoveorMerge[(int)Direction.Left] = true;
                            }
                            mCurrentGrid.SetValue(moveToValue*2, action.moveToX, action.moveToY);
                            mCurrentGrid.SetValue(0, x, y);
                            break;
                        case ActionType.Move:
                            if(!mCanMoveorMerge[(int)Direction.Left])
                            {
                                mCanMoveorMerge[(int)Direction.Left] = true;
                            }
                            mCurrentGrid.SetValue(value,action.moveToX, action.moveToY);
                            mCurrentGrid.SetValue(moveToValue, x, y);
                            break;
                        default:
                            break;
                    }
                }
            }
            AddTwo();
            UpdateVisuals();
        }

        private void AddTwo()
        {
            List<int> freeSpace = new List<int>();
            for (int i = 0; i < mCurrentGrid.Size; i++)
            {
                if(mCurrentGrid.GetValue(i) == 0)
                {
                    freeSpace.Add(i);
                }
            }
            if (freeSpace.Count == 0)
                return;
            int randomIndex = UnityEngine.Random.Range(0, freeSpace.Count);
            mCurrentGrid.SetValue(2, freeSpace[randomIndex]);
        }

        private Action CheckLeft(int indexX, int indexY)
        {
            Action action = new Action();
            if(mCurrentGrid.GetValue(indexX,indexY) == 0)//Nothing to move stop check
            {
                action.type = ActionType.Nothing;
                return action;
            }
            bool done = false;
            int checkIndexX = indexX + Left;// 
            while (!done)
            {
                if(checkIndexX == -1 ||
                    (mCurrentGrid.GetValue(checkIndexX,indexY) != 0 &&
                    mCurrentGrid.GetValue(checkIndexX, indexY) != mCurrentGrid.GetValue(indexX, indexY)))
                {
                    //Wall or different number stop check and move
                    action.type = ActionType.Move;
                    action.moveToX = checkIndexX - Left;
                    action.moveToY = indexY;
                    done = true;
                    
                }
                else if(mCurrentGrid.GetValue(checkIndexX, indexY) == 0)
                {
                    //Continue
                    action.moveToX = checkIndexX;
                    action.moveToY = indexY;
                }
                else if (mCurrentGrid.GetValue(checkIndexX, indexY) == mCurrentGrid.GetValue(indexX, indexY))
                {
                    //Merge
                    action.moveToX = checkIndexX;
                    action.moveToY = indexY;
                    action.type = ActionType.Merge;
                    done = true;
                }
                checkIndexX += Left;
            }
            return action;
        }
    }
}