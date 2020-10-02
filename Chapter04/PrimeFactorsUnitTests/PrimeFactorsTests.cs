using System;
using Xunit;

namespace PrimeFactors
{
    public class PrimeFactorsUnitTests
    {

        [Fact]
        public void TestPrimeFactors2()
        {
            // arrange
            int number = 2;
            string expected = "2 ";

            // act
            string actual = Primes.PrimeFactors(number);
            
            // assert
            Assert.Equal(expected, actual);
        }           

        [Fact]
        public void TestPrimeFactors3()
        {
            // arrange
            int number = 3;
            string expected = "3 ";

            // act
            string actual = Primes.PrimeFactors(number);
            
            // assert
            Assert.Equal(expected, actual);
        } 

        [Fact]
        public void TestPrimeFactors4()
        {
            // arrange
            int number = 4;
            string expected = "2 2 ";

            // act
            string actual = Primes.PrimeFactors(number);
            
            // assert
            Assert.Equal(expected, actual);
        }                  

        [Fact]
        public void TestPrimeFactors186()
        {
            // arrange
            int number = 186;
            string expected = "2 3 31 ";

            // act
            string actual = Primes.PrimeFactors(number);
            
            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestPrimeFactors588()
        {
            // arrange
            int number = 588;
            string expected = "2 2 3 7 7 ";

            // act
            string actual = Primes.PrimeFactors(number);
            
            // assert
            Assert.Equal(expected, actual);
        }

    }    
}
