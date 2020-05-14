<?xml version="1.0" encoding="UTF-8"?>
<sch:schema xmlns:sch="http://purl.oclc.org/dsdl/schematron" xmlns:xs="http://www.w3.org/2001/XMLSchema" queryBinding="xslt2" >
<sch:ns uri=" http://www.nmstech.uk/ecommerce/schemas/v01 " prefix="nms" />
<sch:pattern name="Check Existence">
    <sch:rule context="nms:PurchaseOrder">
      <sch:assert test="nms:Id">PurchaseOrder Id is missing</sch:assert>
    </sch:rule>
  </sch:pattern>
</sch:schema>
