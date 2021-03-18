namespace CometVTwo.Utils.Objects
{
    public class Triplet<T, U, Z>
    {
        private T first;
        private U second;
        private Z third;

        public Triplet(T first, U second, Z third)
        {
            this.first = first;
            this.second = second;
            this.third = third;
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
    }
}