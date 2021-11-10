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

        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _xtraMessageBox;

        private DateTime _statementDate = new DateTime(2021, 11, 04);
        private Housekeeper _housekeeper;
        private string _statementFilename;
        private string _subject;


        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper
            {
                Email = "mail1",
                FullName = "fullName",
                Oid = 1,
                StatementEmailBody = "statement body"
            };

            _statementFilename = "filename";
            _subject = $"Sandpiper Statement {_statementDate:yyyy-MM} {_housekeeper.FullName}";

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable);

            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();

            _service =
                 new HousekeeperService(
                     unitOfWork.Object,
                     _statementGenerator.Object,
                     _emailSender.Object,
                     _xtraMessageBox.Object);
        }

        // Check if method was called -> "Interaction Tests"
        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }


        [Test]
        public void SendStatementEmails_HousekeeperHasNoEmail_DontGenerateStatement()
        {
            _housekeeper.Email = null;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }
        [Test]
        public void SendStatementEmails_HousekeeperEmailIsWhiteSpace_DontGenerateStatement()
        {
            _housekeeper.Email = " ";

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }
        [Test]
        public void SendStatementEmails_HousekeeperEmailIsEmpty_DontGenerateStatement()
        {
            _housekeeper.Email = "";

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _statementGenerator
                .Setup(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(_statementFilename);


            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                _housekeeper.Email, 
                _housekeeper.StatementEmailBody, 
                _statementFilename, 
                It.IsAny<string>()));
        }
        [Test]
        public void SendStatementEmails_StatementFilenameIsNull_DontEmailTheStatement()
        {
            _statementGenerator
                .Setup(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => null);


            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                _housekeeper.Email,
                _housekeeper.StatementEmailBody,
                _statementFilename,
                It.IsAny<string>()), Times.Never);
        }
        [Test]
        public void SendStatementEmails_StatementFilenameIsWhiteSpace_DontEmailTheStatement()
        {
            _statementGenerator
                .Setup(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(" ");


            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()), Times.Never);
        }
        [Test]
        public void SendStatementEmails_StatementFilenameIsEmptyString_DontEmailTheStatement()
        {
            _statementGenerator
                .Setup(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns("");


            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()), Times.Never);
        }






    }
}
