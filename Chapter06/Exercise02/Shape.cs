namespace Exercise02
{
    public abstract class Shape
    {
        // fields
        protected double height;
        protected double width;

        // properties
        public virtual double Height 
        { 
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public virtual double Width 
        { 
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }        


        public abstract double Area { get; }
    }
}