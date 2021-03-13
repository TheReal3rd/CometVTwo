namespace CometVTwo.Utils
{
    public class Pair<T, U>
    {
        private T first;
        private U second;

        public Pair(T first, U second)
        {
            this.first = first;
            this.second = second;
        }

        public T First
        {
            get => first;
            set => first = value;
        }

        public U Second
        {
            get => second;
            set => second = value;
        }
    }
}