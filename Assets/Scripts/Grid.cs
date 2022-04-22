namespace Assets.Scripts
{
    class Grid<T>
    {
        private T[] mValues;
        private int mWidth;
        private int mHeight;
        public int Width { get { return mWidth; } }
        public int Height { get { return mHeight; } }
        public int Size { get { return mHeight * mWidth; } }
        public Grid(int width, int height)
        {
            mValues = new T[width * height];
            mHeight = height;
            mWidth = width;
        }
        public T GetValue(int x, int y)
        {
            return mValues[GetIndex(x, y)];
        }
        public bool GetValue(int x, int y, out T value)
        {
            if (x >= mWidth || y >= mHeight || x < 0 || y < 0)
            {
                value = default;
                return false;
            }
            value = GetValue(x, y);
            return true;
        }
        public T GetValue(int index)
        {
            return mValues[index];
        }

        public void SetValue(T value, int x, int y)
        {
            mValues[GetIndex(x, y)] = value;
        }
        public void SetValue(T value, int index)
        {
            mValues[index] = value;
        }
        private int GetIndex(int x, int y)
        {
            return x + y * mHeight;
        }
    }
}