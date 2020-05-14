using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

//Schematron xsl files from:
//http://www.schematron.com/tmp/iso-schematron-xslt2.zip

namespace Validation
{
    public class Schematron
    {
        public Validation.SchematronOutput.schematronoutput Validate(Stream xmlstream, Stream schematronstream)
        {
            ///////////////////////////////
            // Transform original Schemtron  
            ///////////////////////////////
            string path = AppDomain.CurrentDomain.BaseDirectory;

            Uri schematronxsl = new Uri(@"file:\\" + path + @"\xsl_2.0\iso_svrl_for_xslt2.xsl");

            Stream schematrontransform = new Validation.XSLTransform().Transform(schematronstream, schematronxsl);

            ///////////////////////////////
            // Apply Schemtron xslt 
            ///////////////////////////////
            Stream results = new Validation.XSLTransform().Transform(xmlstream, schematrontransform);

            results.Position = 0;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(results);

            var nodes = doc.ChildNodes[1].ChildNodes;
            System.Xml.XmlNode activepattern = null ;
            System.Xml.XmlNode firedrule = null;
            System.Xml.XmlNode failedassert = null;

            List<System.Xml.XmlNode> nodesclone = new List<System.Xml.XmlNode>();
            foreach (System.Xml.XmlNode node in nodes)
            {
                nodesclone.Add(node);
            }

            foreach (System.Xml.XmlNode node in nodesclone)
            {
                if (node.Name == "svrl:active-pattern")
                    activepattern = node;

                if (node.Name == "svrl:fired-rule")
                {
                    firedrule = node;
                    activepattern.AppendChild(firedrule);
                }
                if (node.Name == "svrl:failed-assert")
                {
                    failedassert = node;
                    firedrule.AppendChild(failedassert);
                }

            }
            //System.IO.StreamReader rd2 = new System.IO.StreamReader(results);
            //string xsltSchematronResult = rd2.ReadToEnd();
            //xsltSchematronResult = xsltSchematronResult.Replace("schematron-output", "schematronoutput");
            MemoryStream ms = new MemoryStream();
            System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(ms);

            ms.Position = 0;
            doc.WriteTo(w);
            w.Flush();

            return Serialization.DerializeXML<Validation.SchematronOutput.schematronoutput>(ms);


        }


    }


}
