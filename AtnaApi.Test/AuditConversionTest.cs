using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtnaApi.Transport;

namespace AtnaApi.Test
{
    [TestClass]
    public class AuditConversionTest
    {
        private const string RFC_3881_AUDIT= "<AuditMessage>" +
         " <EventIdentification EventOutcomeIndicator=\"0\" EventDateTime=\"2014-11-20T08:34:06.2025649-05:00\" EventActionCode=\"E\">" + 
         "   <EventID codeSystemName=\"DCM\" displayName=\"Query\" code=\"110112\"/>" + 
         "   <EventTypeCode codeSystemName=\"IHE Transactions\" code=\"ITI-9\"/>" + 
         " </EventIdentification>" + 
         " <ActiveParticipant NetworkAccessPointTypeCode=\"2\" UserIsRequestor=\"true\" UserID=\"openhim|openhim-mediator-ohie-xds\">" + 
         "   <RoleIDCode codeSystemName=\"DCM\" displayName=\"Source\" code=\"110153\"/>" + 
         " </ActiveParticipant>" + 
         " <ActiveParticipant NetworkAccessPointTypeCode=\"1\" UserIsRequestor=\"false\" UserID=\"|\">" + 
         "   <RoleIDCode codeSystemName=\"DCM\" displayName=\"Destination\" code=\"110152\"/>" + 
         " </ActiveParticipant>" + 
         " <AuditSourceIdentification AuditSourceID=\"surface\" AuditEnterpriseSiteID=\"PAT_IDENTITY_X_REF_MGR_MISYS^^^&amp;1.3.6.1.4.1.33349.3.1.1.2&amp;ISO\">" + 
         "   <AuditSourceTypeCode codeSystemName=\"RFC-3881\" displayName=\"ApplicationServerProcess\" code=\"4\"/>" + 
         " </AuditSourceIdentification>" + 
         " <ParticipantObjectIdentification ParticipantObjectTypeCodeRole=\"24\" ParticipantObjectTypeCode=\"2\" ParticipantObjectID=\"3c3bd66c-99ef-4b50-9ce5-220a9f9dcd64\">" + 
         "   <ParticipantObjectIDTypeCode codeSystemName=\"IHE Transactions\" displayName=\"Patient Demographic Query\" code=\"ITI-9\"/>" + 
         "   <ParticipantObjectQuery>" + 
         "     TVNIfF5+XCZ8b3BlbmhpbXxvcGVuaGltLW1lZGlhdG9yLW9o" + 
         "     aWUteGRzfHx8MjAxNDExMjAxNTM0MDYrMDIwMHx8UUJQXlEyM15RQlBfUTIxfGQyMDIyNjVmLWVmOTIt" + 
         "     NDNiMy1iYjdkLTA2OWI2NjY1ZWI4ZnxQfDIuNQ1RUER8SUhFIFBJWCBRdWVyeXwzYzNiZDY2Yy05OWVm" + 
         "     LTRiNTAtOWNlNS0yMjBhOWY5ZGNkNjR8NTVmODEzMTYzMDM4NDJjXl5eJjEuMy42LjEuNC4xLjIxMzY3" + 
        "      LjIwMDkuMS4yLjMwMCZJU09eUEl8Xl5eT0hJRV9DQVRfVEVTVCYxLjIuMTAzLjQzMC4yMDMuMTAyLjQw" + 
        "      My4wJklTT15QSQ1SQ1B8SQ0=" + 
            "</ParticipantObjectQuery>" + 
        "  </ParticipantObjectIdentification>" + 
        "</AuditMessage>";
        
        [TestMethod]
        public void TestRFC3881ToDICOM()
        {
            String dicomAudit = AuditTransportUtil.ConvertAuditToDICOM(RFC_3881_AUDIT);
            Assert.IsTrue(dicomAudit.Contains("csd-code=\"110152\""), "Does not contain CSD-CODES");
            Assert.IsTrue(dicomAudit.Contains("originalText=\"Query\""), "Does not contain OriginalText");
            Assert.IsTrue(dicomAudit.Contains("originalText=\"ApplicationServerProcess\""), "Did not propogate");
            Assert.IsFalse(dicomAudit.Contains("AuditSourceTypeCode"));

        }
    }
}
