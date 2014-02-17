using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtnaApi.Attributes;
using AtnaApi.Model;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using AtnaApi.Transport;

namespace AtnaApi.Test
{
    /// <summary>
    /// Tests that the AuditUtil CreateAudit method works as expected
    /// </summary>
    [TestClass]
    public class AutoAuditTest
    {
        /// <summary>
        /// A simple audit class
        /// </summary>
        public class SimpleAuditClass
        {
            [AuditableObject(AtnaApi.Model.AuditableObjectType.SystemObject, AtnaApi.Model.AuditableObjectRole.Report, AtnaApi.Model.AuditableObjectIdType.ReportNumber, LifeCycleTarget = true, DataFormat = "{0}^^^TEST^RI")]
            public int? Id { get; set; }
            [AuditableObject(AtnaApi.Model.AuditableObjectType.Person, AtnaApi.Model.AuditableObjectRole.Patient, AtnaApi.Model.AuditableObjectIdType.PatientNumber, DataFormat = "{0}^^^TEST^PI")]
            public int? CaseId { get; set; }
            public string Description { get; set; }
            public bool Primary { get; set; }
            public DateTime Diagnosed { get; set; }
        }

        /// <summary>
        /// Represents a nested audit class 
        /// </summary>
        public class NestedAuditClass
        {
            [AuditableObject(AtnaApi.Model.AuditableObjectType.Person, AtnaApi.Model.AuditableObjectRole.Patient, AtnaApi.Model.AuditableObjectIdType.PatientNumber, LifeCycleTarget = true, DataFormat = "{0}^^^TEST^PI", ParticipantObjectPropertyName = "PatientEmail")]
            public int CaseId { get; set; }
            public string PatientFirstName { get; set; }
            public string PatientLastName { get; set; }
            public string PatientEmail { get; set; }
            public string PatientPhoto { get; set; }
            public List<SimpleAuditClass> Diagnoses { get; set; }
        }

        
        public AutoAuditTest()
        {
        
        }

        private TestContext m_testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return this.m_testContextInstance;
            }
            set
            {
                this.m_testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// Tests the creation of an audit from an object which contains a simple set of data
        /// </summary>
        [TestMethod]
        public void AuditUtilCreateAuditSimpleTest()
        {
            SimpleAuditClass testClass = new SimpleAuditClass()
            {
                CaseId = 102,
                Description = "This is a description",
                Diagnosed = DateTime.Parse("2014-02-03"),
                Id = 20495,
                Primary = true
            };

            // Use the create tool
            var audit = AuditUtil.GenerateAuditObjects(new List<object>() { testClass }, Model.ActionType.Create, Model.OutcomeIndicator.Success, Model.EventIdentifierType.Query, new Model.CodeValue<string>("TEST"));

            // Test : Output the XML
            Trace.WriteLine(AuditTransportUtil.CreateMessageBody(audit));

            // There should be an audit with the following attribute
            Assert.AreEqual(ActionType.Create, audit.EventIdentification.ActionCode);
            Assert.AreEqual(OutcomeIndicator.Success, audit.EventIdentification.EventOutcome);
            Assert.AreEqual(EventIdentifierType.Query, audit.EventIdentification.EventId.StrongCode);


            // There should be two participant objects
            // 1. The IDentifier of the case
            // 2. The IDentifier for the object
            Assert.AreEqual(2, audit.AuditableObjects.Count, "Insufficient ParticipantObjects in the audit message");
            Assert.AreEqual("20495^^^TEST^RI", audit.AuditableObjects[0].ObjectId);
            Assert.AreEqual("102^^^TEST^PI", audit.AuditableObjects[1].ObjectId);

         

        }

        /// <summary>
        /// Tests the creation of an audit from an object which contains a simple set of data having one field marked as interesting null
        /// </summary>
        [TestMethod]
        public void AuditUtilCreateAuditSimpleNullTest()
        {
            SimpleAuditClass testClass = new SimpleAuditClass()
            {
                Description = "This is a description",
                Diagnosed = DateTime.Parse("2014-02-03"),
                Id = 20495,
                Primary = true
            };

            // Use the create tool
            var audit = AuditUtil.GenerateAuditObjects(new List<object>() { testClass }, Model.ActionType.Create, Model.OutcomeIndicator.Success, Model.EventIdentifierType.Query, new Model.CodeValue<string>("TEST"));

            // Test : Output the XML
            Trace.WriteLine(AuditTransportUtil.CreateMessageBody(audit));


            // There should be an audit with the following attribute
            Assert.AreEqual(ActionType.Create, audit.EventIdentification.ActionCode);
            Assert.AreEqual(OutcomeIndicator.Success, audit.EventIdentification.EventOutcome);
            Assert.AreEqual(EventIdentifierType.Query, audit.EventIdentification.EventId.StrongCode);

            // There should be two participant objects
            // 1. The IDentifier of the case
            // 2. The IDentifier for the object
            Assert.AreEqual(1, audit.AuditableObjects.Count, "Insufficient ParticipantObjects in the audit message");
            Assert.AreEqual("20495^^^TEST^RI", audit.AuditableObjects[0].ObjectId);

        }

        /// <summary>
        /// Tests the creation of an audit from an object which contains a simple set of data having one field marked as interesting null
        /// </summary>
        [TestMethod]
        public void AuditUtilCreateAuditSimpleListTest()
        {
            SimpleAuditClass testClass = new SimpleAuditClass()
            {
                Description = "This is a description",
                Diagnosed = DateTime.Parse("2014-02-03"),
                Id = 20495,
                Primary = true
            },
            testClass1 = new SimpleAuditClass()
            {
                Description = "This is a description",
                Diagnosed = DateTime.Parse("2014-02-03"),
                Id = 3094,
                Primary = true
            };

            // Use the create tool
            var audit = AuditUtil.GenerateAuditObjects(new List<object>() { testClass, testClass1 }, Model.ActionType.Create, Model.OutcomeIndicator.Success, Model.EventIdentifierType.Query, new Model.CodeValue<string>("TEST"));

            // Test : Output the XML
            Trace.WriteLine(AuditTransportUtil.CreateMessageBody(audit));


            // There should be an audit with the following attribute
            Assert.AreEqual(ActionType.Create, audit.EventIdentification.ActionCode);
            Assert.AreEqual(OutcomeIndicator.Success, audit.EventIdentification.EventOutcome);
            Assert.AreEqual(EventIdentifierType.Query, audit.EventIdentification.EventId.StrongCode);

            // There should be two participant objects
            // 1. The IDentifier of the case
            // 2. The IDentifier for the object
            Assert.AreEqual(2, audit.AuditableObjects.Count, "Insufficient ParticipantObjects in the audit message");
            Assert.AreEqual("20495^^^TEST^RI", audit.AuditableObjects[0].ObjectId);
            Assert.AreEqual("3094^^^TEST^RI", audit.AuditableObjects[1].ObjectId);

        }

        /// <summary>
        /// Tests the creation of an audit from an object which contains a simple set of data having one field marked as interesting null
        /// </summary>
        [TestMethod]
        public void AuditUtilCreateAuditComplexTest()
        {
            SimpleAuditClass nestedClass = new SimpleAuditClass()
            {
                Description = "This is a description",
                Diagnosed = DateTime.Parse("2014-02-03"),
                Id = 20495,
                Primary = true
            };
            NestedAuditClass testClass = new NestedAuditClass()
            {
                CaseId = 102,
                PatientEmail = "john@test.com",
                PatientFirstName = "John",
                PatientLastName = "Smith",
                Diagnoses = new List<SimpleAuditClass>() {
                    nestedClass
                }
            };

            // Use the create tool
            var audit = AuditUtil.GenerateAuditObjects(new List<object>() { testClass }, Model.ActionType.Create, Model.OutcomeIndicator.Success, Model.EventIdentifierType.Query, new Model.CodeValue<string>("TEST"));

            // Test : Output the XML
            Trace.WriteLine(AuditTransportUtil.CreateMessageBody(audit));

            // There should be an audit with the following attribute
            Assert.AreEqual(ActionType.Create, audit.EventIdentification.ActionCode);
            Assert.AreEqual(OutcomeIndicator.Success, audit.EventIdentification.EventOutcome);
            Assert.AreEqual(EventIdentifierType.Query, audit.EventIdentification.EventId.StrongCode);

            // There should be two participant objects
            // 1. The IDentifier of the case
            // 2. The IDentifier for the object
            Assert.AreEqual(2, audit.AuditableObjects.Count, "Insufficient ParticipantObjects in the audit message");
            Assert.AreEqual("102^^^TEST^PI", audit.AuditableObjects[0].ObjectId);
          
            Assert.AreEqual("20495^^^TEST^RI", audit.AuditableObjects[1].ObjectId);

        }
    }
}
