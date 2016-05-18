namespace Ex03_Ultimate
{

    public partial class ShoppingData
    {
        public partial class ItemRow
        {
            public override string ToString()
            {
                return base.ToString() + " " + Name;
            }
        }
    }
}
