using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using System;
using System.Threading.Tasks;

namespace SampleStoreCQRS.Tests.Contexts.Common.ValueObjects
{
    [TestClass]
    public class ValueObjectsTest
    {

        [TestMethod]
        public void ShouldCreateVaidEmail()
        {
            var email = new Email("teste@teste.com.br");
            Assert.AreEqual(true, email.IsValid());
        }

        [TestMethod]
        public void ShouldCreateInvaidEmail()
        {
            var email = new Email("teste.com.br");
            Assert.AreEqual(false, email.IsValid());
        }

        [TestMethod]
        public void ShouldCreateVaidDocument()
        {
            var document = new Document("82854519094");
            Assert.AreEqual(true, document.IsValid());
        }

        [TestMethod]
        public void ShouldCreateInvaidDocument()
        {
            var document = new Document("82854519094123");
            Assert.AreEqual(false, document.IsValid());
        }

        [TestMethod]
        public void ShouldCreateVaidName()
        {
            var name = new Name("nicolas", "silva dos santos");
            Assert.AreEqual(true, name.IsValid());
        }

        [TestMethod]
        public void ShouldCreateInvalidName()
        {
            var name = new Name("nicolas", "s");
            Assert.AreEqual(false, name.IsValid());
        }

        [TestMethod]
        public void ShouldCreateValidCreditCard()
        {
            var creditCard = new CreditCard("5542507272601145", 408, "18/02/2020", "Teste S. Santos");
            Assert.AreEqual(true, creditCard.IsValid());
        }

        [TestMethod]
        public void ShouldCreateInvalidCreditCard()
        {
            var creditCard = new CreditCard("", 234, "", "");
            Assert.AreEqual(false, creditCard.IsValid());
        }

        [TestMethod]
        public void ShouldCreateValidDiscountCupon()
        {
            var discountCupon = new DiscountCupon("XPTO", 10, Period.Fortnightly);
            Assert.AreEqual(true, discountCupon.IsValid());
        }

        [TestMethod]
        public void ShouldCreateInvalidDiscountCupon()
        {
            var discountCupon = new DiscountCupon("XPTO", 10, Period.Create(new DateTime(2008, 12, 25), new DateTime(2008, 12, 26)));
            Assert.AreEqual(false, discountCupon.IsValid());
        }

        [TestMethod]
        public void ShouldCreateValidFortnightlyPeriod()
        {
            
            var periodFortnightly = Period.Fortnightly;
            Assert.AreEqual(periodFortnightly.End.Day, DateTime.Now.AddDays(15).Day);
        }

        [TestMethod]
        public void ShouldCreateInvalidFortnightlyPeriod()
        {
            var periodFortnightly = Period.Fortnightly;
            Assert.AreNotEqual(periodFortnightly.End.Day, DateTime.Now.AddDays(60).Day);
        }

        [TestMethod]
        public void ShouldCreateValidMonthlyPeriod()
        {

            var periodFortnightly = Period.Monthly;
            Assert.AreEqual(periodFortnightly.End.Day, DateTime.Now.AddDays(30).Day);
        }

        [TestMethod]
        public void ShouldCreateInvalidMonthlyPeriod()
        {
            var periodFortnightly = Period.Monthly;
            Assert.AreNotEqual(periodFortnightly.End.Day, DateTime.Now.AddDays(65).Day);
        }
    }
}
