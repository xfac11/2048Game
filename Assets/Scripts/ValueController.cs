using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Assets.Scripts
{
    public struct Action
    {
        public int moveToX;
        public int moveToY;
        public ActionType type;
    }
    [Serializable]public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    public enum ActionType
    {
        Nothing,
        Merge,
        Move
    }
    public class ValueController : MonoBehaviour
    {
        Grid<int> mCurrentGrid;
        Grid<GameObject> mVisualGrid;
        [SerializeField] private GameObject pGridvaluePrefab;
        [SerializeField] private RectTransform pGrid;
        [SerializeField] private List<GameObject> pGridObjects;
        [SerializeField] private int mWidth;
        [SerializeField] private int mHeight;
        [SerializeField] private bool Regenerate;
        private List<bool> mCanMoveorMerge;
        public int Left { get { return -1; } }
        public int Right { get { return 1; } }
        public int Up { get { return -1; } }//Need to check
        public int Down { get { return 1; } }//Need to check
        private void Awake()
        {
            if (mWidth == 0 || mHeight == 0)
            {
                Debug.LogError("Trying to generate with a 0");
                return;
            }
            for (int i = 0; i < pGrid.transform.childCount; i++)
            {
                Destroy(pGrid.GetChild(i).gameObject);
            }
            pGridObjects.Clear();

            for (int i = 0; i < mWidth * mHeight; i++)
            {
                GameObject obj = Instantiate(pGridvaluePrefab);
                pGridObjects.Add(obj);
                obj.transform.SetParent(pGrid);
            }
            mCanMoveorMerge = new List<bool>();
            for (int i = 0; i < 4; i++)
            {
                mCanMoveorMerge.Add(false);
            }
            mCurrentGrid = new Grid<int>(mWidth, mHeight);
            mVisualGrid = new Grid<GameObject>(mWidth, mHeight);
            for (int i = 0; i < mCurrentGrid.Size; i++)
            {
                mCurrentGrid.SetValue(0, i);
                mVisualGrid.SetValue(pGridObjects[i], i);
            }

            mCurrentGrid.SetValue(2, UnityEngine.Random.Range(0, mWidth), UnityEngine.Random.Range(0, mHeight));
            UpdateVisualsCount();
        }
        private void OnValidate()
        {
            if (Regenerate)
            {
                Regenerate = false;
                
            }
        }
        private void UpdateVisualsCount()
        {
            for (int i = 0; i < mVisualGrid.Size; i++)
            {
                mVisualGrid.GetValue(i).GetComponent<Value>().SetValue(mCurrentGrid.GetValue(i));
            }
        }
        public void Move(DirectionSO direction)
        {
            List<Action> actionList = null;
            switch(direction.Direction)
            {
                case Direction.Down:
                    actionList = MoveDown();
                    break;
                case Direction.Up:
                    actionList = MoveUp();
                    break;
                case Direction.Right:
                    actionList = MoveRight();
                    break;
                case Direction.Left:
                    actionList = MoveLeft();
                    break;
            }
            bool shouldAdd = false;

            for (int i = 0; i < actionList.Count; i++)
            {
                Action action = actionList[i];
                if(action.type != ActionType.Nothing)
                {
                    shouldAdd = true;
                }
            }
            if(shouldAdd)
            {
                AddTwo();
            }
            UpdateVisualsCount();
        }

        private void AnimateGrid(List<Action> actions)
        {
            
        }

        public List<Action> MoveLeft()
        {
            var actionList = new List<Action>();
            for (int y = 0; y < mCurrentGrid.Height; y++)
            {
                for (int x = 0; x < mCurrentGrid.Width; x++)
                {
                    Action action = EvaluateAction(x, y, new Vector2Int(Left, 0));
                    actionList.Add(action);
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
            return actionList;
        }
        public List<Action> MoveRight()
        {
            var actionList = new List<Action>();
            for (int y = 0; y < mCurrentGrid.Height; y++)
            {
                for (int x = mCurrentGrid.Width-2; x >= 0; x--)
                {
                    Action action = EvaluateAction(x, y, new Vector2Int(Right, 0));
                    actionList.Add(action);
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
            return actionList;
        }
        public List<Action> MoveUp()
        {
            var actionList = new List<Action>();

            for (int x = 0; x < mCurrentGrid.Width; x++)
            {
                for (int y = 0; y < mCurrentGrid.Height; y++)
                {
                    Action action = EvaluateAction(x, y, new Vector2Int(0, Up));
                    actionList.Add(action);
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
            return actionList;
        }
        public List<Action> MoveDown()
        {
            var actionList = new List<Action>();

            for (int x = 0; x < mCurrentGrid.Width; x++)
            {
                for (int y = mCurrentGrid.Height - 2; y >= 0; y--)
                {
                    Action action = EvaluateAction(x, y, new Vector2Int(0, Down));
                    actionList.Add(action);
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
            return actionList;
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
            bool isInsideGrid = mCurrentGrid.GetValue(checkIndexX, checkIndexY, out int _);
            if(!isInsideGrid)
            {
                //Outside directly so must be beside a wall so no move or merge.
                action.type = ActionType.Nothing;
                return action;
            }
            while (!done)
            {
                isInsideGrid = mCurrentGrid.GetValue(checkIndexX, checkIndexY, out int checkValue);
                if (!isInsideGrid ||
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