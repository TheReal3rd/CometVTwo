namespace CometVTwo.Utils.Objects
{
    public class Quad<T, U, Z, B>
    {
        private T first;
        private U second;
        private Z third;
        private B fourth;

        public Quad(T first, U second, Z third, B fourth)
        {
            this.first = first;
            this.second = second;
            this.third = third;
            this.fourth = fourth;
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

        public Z Third
        {
            get => third;
            set => third = value;
        }

        public B Fourth
        {
            get => fourth;
            set => fourth = value;
        }
    }
}