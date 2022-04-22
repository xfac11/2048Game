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
                    Action action = EvaluateAction(x, y, new Vector2Int(Left, 0));
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
                            mCurrentGrid.SetValue(moveToValue * 2, action.moveToX, action.moveToY);
                            mCurrentGrid.SetValue(0, x, y);

                            break;
                        case ActionType.Move:

                            if (!mCanMoveorMerge[(int)Direction.Left])
                            {
                                mCanMoveorMerge[(int)Direction.Left] = true;
                            }
                            mCurrentGrid.SetValue(value, action.moveToX, action.moveToY);
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
        public void MoveRight()
        {
            for (int y = 0; y < mCurrentGrid.Height; y++)
            {
                for (int x = mCurrentGrid.Width-2; x >= 0; x--)
                {
                    Action action = EvaluateAction(x, y, new Vector2Int(Right, 0));
                    int value = mCurrentGrid.GetValue(x, y);
                    int moveToValue = mCurrentGrid.GetValue(action.moveToX, action.moveToY);
                    switch (action.type)
                    {
                        case ActionType.Nothing:

                            break;
                        case ActionType.Merge:

                            if (!mCanMoveorMerge[(int)Direction.Right])
                            {
                                mCanMoveorMerge[(int)Direction.Right] = true;
                            }
                            mCurrentGrid.SetValue(moveToValue * 2, action.moveToX, action.moveToY);
                            mCurrentGrid.SetValue(0, x, y);

                            break;
                        case ActionType.Move:

                            if (!mCanMoveorMerge[(int)Direction.Right])
                            {
                                mCanMoveorMerge[(int)Direction.Right] = true;
                            }
                            mCurrentGrid.SetValue(value, action.moveToX, action.moveToY);
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
        public void MoveUp()
        {
            for (int x = 0; x < mCurrentGrid.Width; x++)
            {
                for (int y = 0; y < mCurrentGrid.Height; y++)
                {
                    Action action = EvaluateAction(x, y, new Vector2Int(0, Up));
                    int value = mCurrentGrid.GetValue(x, y);
                    int moveToValue = mCurrentGrid.GetValue(action.moveToX, action.moveToY);
                    switch (action.type)
                    {
                        case ActionType.Nothing:

                            break;
                        case ActionType.Merge:

                            if (!mCanMoveorMerge[(int)Direction.Up])
                            {
                                mCanMoveorMerge[(int)Direction.Up] = true;
                            }
                            mCurrentGrid.SetValue(moveToValue * 2, action.moveToX, action.moveToY);
                            mCurrentGrid.SetValue(0, x, y);

                            break;
                        case ActionType.Move:

                            if (!mCanMoveorMerge[(int)Direction.Up])
                            {
                                mCanMoveorMerge[(int)Direction.Up] = true;
                            }
                            mCurrentGrid.SetValue(value, action.moveToX, action.moveToY);
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
        public void MoveDown()
        {
            for (int x = 0; x < mCurrentGrid.Width; x++)
            {
                for (int y = mCurrentGrid.Height - 2; y >= 0; y--)
                {
                    Action action = EvaluateAction(x, y, new Vector2Int(0, Down));
                    int value = mCurrentGrid.GetValue(x, y);
                    int moveToValue = mCurrentGrid.GetValue(action.moveToX, action.moveToY);
                    switch (action.type)
                    {
                        case ActionType.Nothing:

                            break;
                        case ActionType.Merge:

                            if (!mCanMoveorMerge[(int)Direction.Down])
                            {
                                mCanMoveorMerge[(int)Direction.Down] = true;
                            }
                            mCurrentGrid.SetValue(moveToValue * 2, action.moveToX, action.moveToY);
                            mCurrentGrid.SetValue(0, x, y);

                            break;
                        case ActionType.Move:

                            if (!mCanMoveorMerge[(int)Direction.Down])
                            {
                                mCanMoveorMerge[(int)Direction.Down] = true;
                            }
                            mCurrentGrid.SetValue(value, action.moveToX, action.moveToY);
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
        private int InitXLoop(Direction direction)
        {
            int xValue = 0;
            switch (direction)
            {
                case Direction.Left:
                    xValue = 0;
                    break;
                case Direction.Right:
                    xValue = mCurrentGrid.Width;
                    break;
                case Direction.Down:
                    xValue = mCurrentGrid.Height;
                    break;
                case Direction.Up:
                    xValue = 0;
                    break;
            }
            return xValue;
        }
        private int InitYLoop(Direction direction)
        {
            int yValue = 0;
            switch (direction)
            {
                case Direction.Left:
                    yValue = 0;
                    break;
                case Direction.Right:
                    break;
                case Direction.Down:
                    break;
                case Direction.Up:
                    break;
            }
            return yValue;
        }
        private void Increase(Direction direction,ref int val)
        {
            switch (direction)
            {
                case Direction.Left:
                    val++;
                    break;
                case Direction.Right:
                    val--;
                    break;
                case Direction.Down:
                    val++;
                    break;
                case Direction.Up:
                    val--;
                    break;
            }
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

        private Action EvaluateAction(int indexX, int indexY, Vector2Int direction)
        {
            var action = new Action();
            int currentValue = mCurrentGrid.GetValue(indexX, indexY);
            if (currentValue == 0)//Nothing to move stop check
            {
                action.type = ActionType.Nothing;
                return action;
            }
            bool done = false;
            int checkIndexX = indexX + direction.x;//
            int checkIndexY = indexY + direction.y;
            while (!done)
            {
                bool isInsideGrid = mCurrentGrid.GetValue(checkIndexX, checkIndexY, out int checkValue);

                if(!isInsideGrid ||
                    (checkValue != 0 && checkValue != currentValue))
                {
                    //Wall or different number stop check and move
                    action.type = ActionType.Move;
                    action.moveToX = checkIndexX - direction.x;//checkindex is currently on a wall or a number so need to go back to the previous
                    action.moveToY = checkIndexY - direction.y;
                    done = true;
                    
                }
                else if(checkValue == 0)
                {
                    //Continue
                    action.moveToX = checkIndexX;
                    action.moveToY = checkIndexY;
                }
                else if (checkValue == currentValue)
                {
                    //Merge
                    action.moveToX = checkIndexX;
                    action.moveToY = checkIndexY;
                    action.type = ActionType.Merge;
                    done = true;
                }

                checkIndexX += direction.x;
                checkIndexY += direction.y;
            }
            return action;
        }
    }
}