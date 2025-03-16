namespace KnKModTools.DatClass
{
    public static class StackExtensions
    {
        public static T GetElementAtIndex<T>(this Stack<T> stack, int index)
        {
            if (index < 0 || index > stack.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            var temp = new Stack<T>();
            for (var i = 0; i < index; i++)
            {
                temp.Push(stack.Pop());
            }
            T result = temp.Pop();
            stack.Push(result);

            while (temp.Count != 0)
            {
                stack.Push(temp.Pop());
            }

            return result;
        }

        public static bool SetElementAtIndex<T>(this Stack<T> stack, int index, T value, Func<T, bool> condition)
        {
            if (index < 0 || index > stack.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            var temp = new Stack<T>();
            for (var i = 0; i < index; i++)
            {
                temp.Push(stack.Pop());
            }
            var isOk = false;
            T? item = temp.Pop();
            if (condition(item))
            {
                stack.Push(value);
                isOk = true;
            }
            else
            {
                stack.Push(item);
            }

            while (temp.Count != 0)
            {
                stack.Push(temp.Pop());
            }
            return isOk;
        }
    }
}