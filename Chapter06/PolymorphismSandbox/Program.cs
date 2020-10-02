using System;

namespace PolymorphismSandbox
{
    // A->B->C Simple inheritance  --------------------------------------------------------------------
    class A
    {
        public void Test() { Console.WriteLine("A::Test()"); }
    }
    class B : A { }
    class C : B { }


    // D->E->F Method Hiding (new keyword) -------------------------------------------------------------
    class D
    {
        public void Test() { Console.WriteLine("D::Test()"); }
    }

    class E : D
    {
        public new void Test() { Console.WriteLine("E::Test()"); }
    }

    class F : E
    {
        public new void Test() { Console.WriteLine("F::Test()"); }
    }

    //  X->Y->Z METHOD OVERRIDING (virtual and override keywords) ----------------------------------------
    class X
    {
        public virtual void Test() { Console.WriteLine("X::Test()"); }
    }

    class Y : X
    {
        public override void Test() { Console.WriteLine("Y::Test()"); }
    }
    
    class Z : Y
    {
        public override void Test() { Console.WriteLine("Z::Test()"); }
    }    

    // P->Q->R   Mixing Method Overriding and Method Hiding (using virtual and new together) ------------------------------------
    class P
    {
        public void Test() { Console.WriteLine("P::Test()"); }
    }

    class Q : P
    {
        public new virtual void Test() { Console.WriteLine("Q::Test()"); }
    }

    class R : Q
    {
        public override void Test() { Console.WriteLine("R::Test()"); }
    }



     class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("new keyword      - to hide a method/property/indexer/event of base class into derived class.");
            Console.WriteLine("virtual keyword  - to modify a method/property/indexer/event declared in BASE class and allow it to be OVERRIDDEN in derived class.");
            Console.WriteLine("override keyword - to extend or modify a virtual/abstract method/property/indexer/event of base class into derived class.");

            Console.WriteLine();
            Console.WriteLine("SIMPLE INHERITANCE SECTION ---------------------------------------");
            // Test() only defined by base class A
            A a = new A();
            B b = new B();
            C c = new C();
            
            a.Test(); // output --> "A::Test()"
            b.Test(); // output --> "A::Test()"
            c.Test(); // output --> "A::Test()"

            Console.WriteLine();
            Console.WriteLine("METHOD HIDING SECTION (use new keyword) NON POLYMORPHIC INHERITANCE ---------------------------------------");
            // Each class defines it's own Test(), so derived classes print out their own output...BUT COMPILER WARNINGS
            //  E.Test()' hides inherited member 'D.Test()'. Use the new keyword if hiding was intended.
            //  F.Test()' hides inherited member 'E.Test()'. Use the new keyword if hiding was intended.
            D d = new D();
            E e = new E();
            F f = new F();
            d.Test(); // output --> "D::Test()"
            e.Test(); // output --> "E::Test()"
            f.Test(); // output --> "F::Test()"

            // reassign d and e
            d = new E();    // make d reference an instantiation of and E
            e = new F();    // make e reference an instantiation of and F


            // if you are expecting the next two outputs to be "E::Foo()" and "F::Foo()" since the objects d and e are referenced by the object of E and F respectively 
            // then you have to re-write the above code for Method Overriding.... see next example
            d.Test(); // output --> "D::Test()"            
            e.Test(); // output --> "E::Test()"

            Console.WriteLine();
            Console.WriteLine("METHOD OVERRIDING SECTION (use keyword 'virtual' in base class, and 'override' in derived classes) POLYMORPHIC INHERITANCE  ---------------------------------------");
            X x = new X();
            Y y = new Y();
            Z z = new Z();
            x.Test(); // output --> "X::Test()"
            y.Test(); // output --> "Y::Test()"
            z.Test(); // output --> "Z::Test()"
            
            x = new Y();    
            y = new Z();

            x.Test(); // output --> "Y::Test()"            
            y.Test(); // output --> "Z::Test()"

            Console.WriteLine();
            Console.WriteLine("Mixing Method Overriding and Method Hiding (using virtual and new together) ------------------------------------");
            // when you want to further override the derived class method into next level i.e. overriding Class Q, Test() method in Class R
            P p = new P();
            Q q = new Q();
            R r = new R();

            p.Test(); // output --> "P::Test()"
            q.Test(); // output --> "Q::Test()"
            r.Test(); // output --> "R::Test()"

            p = new Q();
            p.Test(); // output --> "P::Test()"

            q = new R();
            q.Test(); // output --> "R::Test()"            


        }
 }





}
