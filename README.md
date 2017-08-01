# MEDIC Atna API

<div class="wikidoc">
<p><b>Project Description</b><br>
The MEDIC ATNA API project provides a simple to use series of utility classes centered around creating IHE Audit Trail and Node Authentication (ATNA) audit messages (ITI-20). This API provides the following functionality:</p>
<ul>
<li>Serialize/parse ITI-20 (ATNA Audit) messages
<ul>
<li>To/From IETF RFC3881 format </li><li>To/From DICOM audit format</li></ul>
</li><li>Transport audit messages via:
<ul>
<li>UDP Syslog (RFC5426 &#43; RFC5424) </li><li>Secure (TLS) TCP Syslog (RFC5425 &#43; RFC5424)</li></ul>
</li><li>Automatically create audit messages from annotated data classes (via attribution)</li></ul>
<p>The AtnaApi is used by the MEDIC Client Registry and Visualizer and tested with a variety of partners, including at the IHE North American Connect-a-thon 2015 within the MEDIC Client Registry.</p>
</div><div class="ClearBoth"></div>
