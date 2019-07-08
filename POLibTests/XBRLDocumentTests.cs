using POLib.SECScraper;
using System;
using NodaTime;
using Xunit;

namespace POLibTests
{
    public class XBRLDocumentTests
    {
        private const string xml =
                @"<xbrli:xbrl xmlns:xbrli='http://www.xbrl.org/2003/instance' xmlns:abt='http://abbott.com/20090630' xmlns:dei='http://xbrl.us/dei/2009-01-31' xmlns:dei-std='http://xbrl.us/dei-std/2009-01-31' xmlns:iso4217='http://www.xbrl.org/2003/iso4217' xmlns:link='http://www.xbrl.org/2003/linkbase' xmlns:negated='http://xbrl.us/us-gaap/negated/2008-03-31' xmlns:ref='http://www.xbrl.org/2006/ref' xmlns:us-gaap='http://xbrl.us/us-gaap/2009-01-31' xmlns:us-gaap-all='http://xbrl.us/us-gaap-all/2009-01-31' xmlns:us-gaap-std='http://xbrl.us/us-gaap-std/2009-01-31' xmlns:us-roles='http://xbrl.us/us-roles/2009-01-31' xmlns:us-types='http://xbrl.us/us-types/2009-01-31' xmlns:xbrldt='http://xbrl.org/2005/xbrldt' xmlns:xlink='http://www.w3.org/1999/xlink' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                        <xbrli:context id='D2009Q2YTD'>
                            <xbrli:entity>
                                <xbrli:identifier scheme='http://www.sec.gov/CIK'>0000001800</xbrli:identifier>
                            </xbrli:entity>
                            <xbrli:period>
                                <xbrli:startDate>2009-01-01</xbrli:startDate>
                                <xbrli:endDate>2009-06-30</xbrli:endDate>
                            </xbrli:period>
                        </xbrli:context>
                        <xbrli:context id='D2008Q2YTD'>
                            <xbrli:entity>
                                <xbrli:identifier scheme='http://www.sec.gov/CIK'>0000001800</xbrli:identifier>
                            </xbrli:entity>
                            <xbrli:period>
                                <xbrli:startDate>2008-01-01</xbrli:startDate>
                                <xbrli:endDate>2008-06-30</xbrli:endDate>
                            </xbrli:period>
                        </xbrli:context>
                        <xbrli:context id='D2009Q2'>
                            <xbrli:entity>
                                <xbrli:identifier scheme='http://www.sec.gov/CIK'>0000001800</xbrli:identifier>
                            </xbrli:entity>
                            <xbrli:period>
                                <xbrli:startDate>2009-04-01</xbrli:startDate>
                                <xbrli:endDate>2009-06-30</xbrli:endDate>
                            </xbrli:period>
                        </xbrli:context>
                        <xbrli:context id='D2008Q2'>
                            <xbrli:entity>
                                <xbrli:identifier scheme='http://www.sec.gov/CIK'>0000001800</xbrli:identifier>
                            </xbrli:entity>
                            <xbrli:period>
                                <xbrli:startDate>2008-04-01</xbrli:startDate>
                                <xbrli:endDate>2008-06-30</xbrli:endDate>
                            </xbrli:period>
                        </xbrli:context>
                    <us-gaap:EarningsPerShareDiluted contextRef='D2008Q2YTD' decimals='2' unitRef='USDPerShare'>1.45</us-gaap:EarningsPerShareDiluted>
                </xbrli:xbrl>";

        private XBRLDocument xbrlDoc = new XBRLDocument(xml);

        [Fact]
        public void XBRLDocument_EPS_HasValidStartDate()
        {
            var epsData = xbrlDoc.GetAllQuarterlyEPSData();
            Assert.Equal(new LocalDate(2008, 01, 1), epsData[0].DateInterval.Start);
        }

        [Fact]
        public void XBRLDocument_EPS_HasValidEndDate()
        {
            var epsData = xbrlDoc.GetAllQuarterlyEPSData();
            Assert.Equal(new LocalDate(2008, 06, 30), epsData[0].DateInterval.End);
        }
    }
}
