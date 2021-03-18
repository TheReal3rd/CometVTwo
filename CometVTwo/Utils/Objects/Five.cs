namespace CometVTwo.Utils.Objects
{
    public class Five<T, U, Z, B, H>
    {
        private T first;
        private U second;
        private Z third;
        private B fourth;
        private H fifth;

        public Five(T first, U second, Z third, B fourth, H fifth)
        {
            this.first = first;
            this.second = second;
            this.third = third;
            this.fourth = fourth;
            this.fifth = fifth;
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

        public H Fifth
        {
            get => fifth;
            set => fifth = value;
        }
    }
}