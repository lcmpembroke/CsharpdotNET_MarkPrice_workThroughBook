namespace Packt.Shared
{
    public partial class Person
    {
        public string Origin
        { 
            // readonly properties only have a get implementation
            get
            { 
                return $"{Name} was born on {HomePlanet}";
            }
        }
        
        public string Greeting => $"{Name} says hello";

        public int Age => System.DateTime.Today.Year - DateOfBirth.Year;

        // auto syntax: compiler creates a private, anonymous backing field that can only be accessed through the property's get and set accessors
        public string FavouriteIceCream {get; set;}

        // have more control over what value a property can be set to
        private string favouritePrimaryColour;
        
        public string FavouritePrimaryColour
        {
            get
            {
                return favouritePrimaryColour;
            }
            set
            {        
                switch (value.ToLower())
                {
                    case "red":
                    case "green":
                    case "blue":
                        favouritePrimaryColour = value;
                        break;
                    default:
                        throw new System.ArgumentException($"{value} not primary. Choose from red, green, blue");
                }

            }
        }

        // indexers: allow calling code to use array syntax to access a property
        public Person this[int index]
        {
            get
            {
                return Children[index];
            }
            set
            {
                Children[index] = value;
            }
        }        


    }
}