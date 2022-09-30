using System;
using Xunit;
using Xunit.Abstractions;

namespace CleanUpIndia.xTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void Test1()
        {
            int a = 7, b = 5;
            int expectedResult = 35;
            int actualResult;

            actualResult = a*b;

            _testOutputHelper.WriteLine($"Input: a = {a} and b = {b}");
            _testOutputHelper.WriteLine($"Result: expected = {expectedResult}, actual = {actualResult}");


            Assert.Equal<int>(expectedResult, actualResult);

        }
    }
}
