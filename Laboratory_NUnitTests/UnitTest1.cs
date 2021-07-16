using Laboratory;
using Laboratory.Data;
using Laboratory.Validation;
using NUnit.Framework;


namespace Laboratory_NUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

       
        [Test]
        public void Test_Email()
        {
            Test test = new Test(1, "Ivan", "Ivanov", "Ivanov", "nasko@abv.bg");
            Test test2 = new Test(1, "Ivan", "Ivanov", "Ivanov", "ivan.bg");
            Assert.True(test.CheckEmail());
            Assert.False(test2.CheckEmail());
        }

        [Test]
        public void check_password_hashing()
        {
            string salt = "lR7y7Lj7kjomcA==";
            string hashedpass = "12o9YSIaE22UU/e0+IJVB9gkuZMk5bO91v4KGXoi2BE=";
            Password_salt pass = new Password_salt();
            Assert.AreEqual(hashedpass, pass.GenerateSHA256Hash("az", salt));
            Assert.AreNotEqual(hashedpass, pass.GenerateSHA256Hash("AZ", salt));
        }

    }
}