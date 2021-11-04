using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class HousekeeperServiceTests
    {

        private HousekeeperService service;
        private Mock<IStatementGenerator> statementGenerator;
        private Mock<IEmailSender> emailSender;
        private Mock<IXtraMessageBox> xtraMessageBox;

        private DateTime statementDate = new DateTime(2021, 11, 04);
        private Housekeeper housekeeper;


        [SetUp]
        public void SetUp()
        {
            housekeeper = new Housekeeper
            {
                Email = "mail1",
                FullName = "fullName",
                Oid = 1,
                StatementEmailBody = "statement body"
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                housekeeper
            }.AsQueryable);

            statementGenerator = new Mock<IStatementGenerator>();
            emailSender = new Mock<IEmailSender>();
            xtraMessageBox = new Mock<IXtraMessageBox>();

            service =
                 new HousekeeperService(
                     unitOfWork.Object,
                     statementGenerator.Object,
                     emailSender.Object,
                     xtraMessageBox.Object);
        }


        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            service.SendStatementEmails(statementDate);

            statementGenerator.Verify(x => x.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate));
        }
    }
}
