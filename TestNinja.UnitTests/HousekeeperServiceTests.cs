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

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                new Housekeeper
                {
                    Email = "mail1",
                    FullName = "fullName",
                    Oid = 1,
                    StatementEmailBody = "statement body"
                }
            }.AsQueryable);

            var statementGenerator = new Mock<IStatementGenerator>();
            //statementGenerator.Setup(x => x.SaveStatement()).Returns("filename");
            var emailSender = new Mock<IEmailSender>();
            var xtraMessageBox = new Mock<IXtraMessageBox>();

            var housekeeperService =
                new HousekeeperService(
                    unitOfWork.Object,
                    statementGenerator.Object,
                    emailSender.Object,
                    xtraMessageBox.Object);

            housekeeperService.SendStatementEmails(new DateTime(2021, 11, 04));

            statementGenerator.Verify(x => x.SaveStatement(1, "fullName", new DateTime(2021, 11, 04)));
        }
    }
}
