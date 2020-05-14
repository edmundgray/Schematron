using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Validation.Test
{
    [TestClass]
    public class SchematronBasic
    {
        //[TestMethod]
        //public void XSLT_SAXON_PO_GOOD_Schematron2()
        //{
        //    ///////////////////////////////
        //    // Transform original Schemtron  
        //    ///////////////////////////////
        //    string path = AppDomain.CurrentDomain.BaseDirectory;

        //    Uri schematron = new Uri(@"file:\\" + path + @"\po\po-schema.sch");
        //    Uri schematronxsl = new Uri(@"file:\\" + path + @"\xsl_2.0\iso_svrl_for_xslt2.xsl");

        //    Stream schematrontransform = new Validation.XSLTransform().Transform(schematron, schematronxsl);

        //    ///////////////////////////////
        //    // Apply Schemtron xslt 
        //    ///////////////////////////////
        //    FileStream xmlstream = new FileStream(path + @"\po\po-good.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
        //    Stream results = new Validation.XSLTransform().Transform(xmlstream, schematrontransform);

        //    System.Diagnostics.Debug.WriteLine("RESULTS");
        //    results.Position = 0;
        //    System.IO.StreamReader rd2 = new System.IO.StreamReader(results);
        //    string xsltSchematronResult = rd2.ReadToEnd();
        //    System.Diagnostics.Debug.WriteLine(xsltSchematronResult);

        //}

        //[TestMethod]
        //public void XSLT_SAXON_PO_BAD_Schematron2()
        //{
        //    ///////////////////////////////
        //    // Transform original Schemtron  
        //    ///////////////////////////////
        //    string path = AppDomain.CurrentDomain.BaseDirectory;

        //    Uri schematron = new Uri(@"file:\\" + path + @"\po\po-schema.sch");
        //    Uri schematronxsl = new Uri(@"file:\\" + path + @"\xsl_2.0\iso_svrl_for_xslt2.xsl");

        //    Stream schematrontransform = new Validation.XSLTransform().Transform(schematron, schematronxsl);

        //    ///////////////////////////////
        //    // Apply Schemtron xslt 
        //    ///////////////////////////////
        //    FileStream xmlstream = new FileStream(path + @"\po\po-bad.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
        //    Stream results = new Validation.XSLTransform().Transform(xmlstream, schematrontransform);

        //    System.Diagnostics.Debug.WriteLine("RESULTS");
        //    results.Position = 0;
        //    System.IO.StreamReader rd2 = new System.IO.StreamReader(results);
        //    string xsltSchematronResult = rd2.ReadToEnd();
        //    System.Diagnostics.Debug.WriteLine(xsltSchematronResult);

        //}

        //[TestMethod]
        //public void XSLT_SAXON_PO_BAD_Schematron3()
        //{
        //    string path = AppDomain.CurrentDomain.BaseDirectory;
        //    FileStream xmlstream = new FileStream(path + @"\po\po-bad.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
        //    FileStream schematronstream = new FileStream(path + @"\po\po-schema.sch", FileMode.Open, FileAccess.Read, FileShare.Read);

        //    var results = new Validation.Schematron().Validate(xmlstream, schematronstream);
        //    Assert.AreEqual(results.activepattern.Length, 3);

        //    var items = from activepattern in results.activepattern
        //            from firedrule in activepattern.firedrule
        //            where firedrule.failedassert != null
        //            from failedassert in firedrule.failedassert
        //            select failedassert;
        //    Assert.IsTrue(items.Count() == 5);


        //}


        [TestMethod]
        public void XSLT_SAXON_PO_GOOD_Schematron()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            FileStream schema = new FileStream( path + @"\po\po-schema.sch", FileMode.Open,FileAccess.Read );
            FileStream xml = new FileStream(  path + @"\po\po-good.xml", FileMode.Open, FileAccess.Read);

            
            var results = new Validation.Schematron().Validate(xml, schema);
            var items = from activepattern in results.activepattern
                        from firedrule in activepattern.firedrule
                        where firedrule.failedassert != null
                        from failedassert in firedrule.failedassert
                        select failedassert;
            Assert.IsTrue(items.Count() == 0);

            xml.Close();
            schema.Close(); 
        }

        [TestMethod]
        public void XSLT_SAXON_PO_BAD_Schematron()
        {

            string path = AppDomain.CurrentDomain.BaseDirectory;
            FileStream schema = new FileStream(@"C:\Users\edmun\OneDrive\Documents\GitHub\peppol-bis-invoice-3\rules\sch\CEN-EN16931-UBL.sch", FileMode.Open, FileAccess.Read); //"C:\Users\edmun\Documents\ITSligo(2)\eInvoice for Communities\Validator-V2.1\Validator\sch\DH_InvoiceDataDictionaryCheck v2.sch", FileMode.Open, FileAccess.Read);// path + @"\po\po-schema.sch", FileMode.Open, FileAccess.Read);
            FileStream xml = new FileStream(@"C:\Users\edmun\Documents\ITSligo(2)\eInvoice for Communities\XML\Vat-category-S with Error.xml", FileMode.Open, FileAccess.Read); // path + @"\po\po-bad.xml", FileMode.Open, FileAccess.Read);

            var results = new Validation.Schematron().Validate(xml, schema);
            var items = from activepattern in results.activepattern
                        from firedrule in activepattern.firedrule
                        where firedrule.failedassert != null
                        from failedassert in firedrule.failedassert
                        select failedassert;
            Assert.IsTrue(items.Count()!= 0);

            xml.Close();
            schema.Close();

        }

    }
}
