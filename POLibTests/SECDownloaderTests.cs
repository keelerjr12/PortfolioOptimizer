using System;
using POLib.SECScraper;
using System.Collections.Generic;
using Xunit;

namespace POLibTests
{
    public class SECDownloaderTests
    {
        [Fact]
        public void RetrieveAllReportLinks_Returns_SingleValidLink()
        {
            var html = @"<html> 
                            <head>
                            </head>
                            <body>
                                <div id='contentDiv'>
                                    <div id='seriesDiv' style='margin-top: 0px;'>
                                        <table class='tableFile2' summary='Results'>
                                            <tr>
                                                <th width = '7%' scope='col'>Filings</th>
                                                <th width = '10%' scope='col'>Format</th>
                                                <th scope = 'col'>Description</th>
                                                <th width='10%' scope='col'>Filing Date</th>
                                                <th width = '15%' scope= 'col' > File / Film Number</th>
                                            </tr>
                                            <tr>
                                                <td nowrap ='nowrap'> 10 - Q </td>
                                                <td nowrap ='nowrap'>
                                                    <a href= '/Archives/edgar/data/1598014/000159801419000043/0001598014-19-000043-index.htm' id= 'documentsbutton'> &nbsp; Documents</a>&nbsp; <a href = '/cgi-bin/viewer?action=view&amp;cik=1598014&amp;accession_number=0001598014-19-000043&amp;xbrl_type=v' id='interactiveDataBtn'>&nbsp;Interactive Data</a></td>
                                                <td class='small'>Quarterly report[Sections 13 or 15(d)]<br />Acc-no: 0001598014-19-000043&nbsp;(34 Act)&nbsp; Size: 6 MB</td>
                                                <td>2019-03-26</td>
                                                <td nowrap = 'nowrap'><a href='/cgi-bin/browse-edgar?action=getcompany&amp;filenum=001-36495&amp;owner=include&amp;count=100'>001-36495</a><br>19705709</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </body>
                        </html>";

            var srPage = new SearchResultsPage(html);
            var links = srPage.GetAllReportLinks();

            Assert.Equal(new List<string>() { "/Archives/edgar/data/1598014/000159801419000043/0001598014-19-000043-index.htm" }, links);
        }

        [Fact]
        public void RetrieveAllReportLinks_Returns_MultipleValidLinks()
        {
            var html = @"<html> 
                            <head>
                            </head>
                            <body>
                                <div id='contentDiv'>
                                    <div id='seriesDiv' style='margin-top: 0px;'>
                                        <table class='tableFile2' summary='Results'>
                                            <tr>
                                                <th width = '7%' scope='col'>Filings</th>
                                                <th width = '10%' scope='col'>Format</th>
                                                <th scope = 'col'>Description</th>
                                                <th width='10%' scope='col'>Filing Date</th>
                                                <th width = '15%' scope= 'col' > File / Film Number</th>
                                            </tr>
                                            <tr>
                                                <td nowrap ='nowrap'> 10 - Q </td>
                                                <td nowrap ='nowrap'>
                                                    <a href= '/Archives/edgar/data/1598014/000159801419000043/0001598014-19-000043-index.htm' id= 'documentsbutton'> &nbsp; Documents</a>&nbsp; <a href = '/cgi-bin/viewer?action=view&amp;cik=1598014&amp;accession_number=0001598014-19-000043&amp;xbrl_type=v' id='interactiveDataBtn'>&nbsp;Interactive Data</a></td>
                                                <td class='small'>Quarterly report[Sections 13 or 15(d)]<br />Acc-no: 0001598014-19-000043&nbsp;(34 Act)&nbsp; Size: 6 MB</td>
                                                <td>2019-03-26</td>
                                                <td nowrap = 'nowrap'><a href='/cgi-bin/browse-edgar?action=getcompany&amp;filenum=001-36495&amp;owner=include&amp;count=100'>001-36495</a><br>19705709</td>
                                            </tr>
                                            <tr>
                                                <td nowrap ='nowrap'> 10 - Q </td>
                                                <td nowrap ='nowrap'>
                                                    <a href= '/Archives/edgar/data/1598014/000159801419000069/0001598014-19-000069-index.htm' id= 'documentsbutton'> &nbsp; Documents</a>&nbsp; <a href = '/cgi-bin/viewer?action=view&amp;cik=1598014&amp;accession_number=0001598014-19-000069&amp;xbrl_type=v' id='interactiveDataBtn'>&nbsp;Interactive Data</a></td>
                                                <td class='small'>Quarterly report[Sections 13 or 15(d)]<br />Acc-no: 0001598014-19-000069&nbsp;(34 Act)&nbsp; Size: 6 MB</td>
                                                <td>2019-03-26</td>
                                                <td nowrap = 'nowrap'><a href='/cgi-bin/browse-edgar?action=getcompany&amp;filenum=001-36495&amp;owner=include&amp;count=100'>001-36495</a><br>19705709</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </body>
                        </html>";

            var srPage = new SearchResultsPage(html);
            var links = srPage.GetAllReportLinks();

            Assert.Equal(new List<string>()
            {
                "/Archives/edgar/data/1598014/000159801419000043/0001598014-19-000043-index.htm",
                "/Archives/edgar/data/1598014/000159801419000069/0001598014-19-000069-index.htm"
            }, links);
        }

        [Fact]
        public void RetrieveAllReportLinks_Returns_MultipleValidLinks_IgnoresLinksWithoutInteractiveDataButtons()
        {
            var html = @"<html> 
                            <head>
                            </head>
                            <body>
                                <div id='contentDiv'>
                                    <div id='seriesDiv' style='margin-top: 0px;'>
                                        <table class='tableFile2' summary='Results'>
                                            <tr>
                                                <th width = '7%' scope='col'>Filings</th>
                                                <th width = '10%' scope='col'>Format</th>
                                                <th scope = 'col'>Description</th>
                                                <th width='10%' scope='col'>Filing Date</th>
                                                <th width = '15%' scope= 'col' > File / Film Number</th>
                                            </tr>
                                            <tr>
                                                <td nowrap ='nowrap'> 10 - Q </td>
                                                <td nowrap ='nowrap'>
                                                    <a href= '/Archives/edgar/data/1598014/000159801419000043/0001598014-19-000043-index.htm' id= 'documentsbutton'> &nbsp; Documents</a>&nbsp; <a href = '/cgi-bin/viewer?action=view&amp;cik=1598014&amp;accession_number=0001598014-19-000043&amp;xbrl_type=v' id='interactiveDataBtn'>&nbsp;Interactive Data</a></td>
                                                <td class='small'>Quarterly report[Sections 13 or 15(d)]<br />Acc-no: 0001598014-19-000043&nbsp;(34 Act)&nbsp; Size: 6 MB</td>
                                                <td>2019-03-26</td>
                                                <td nowrap = 'nowrap'><a href='/cgi-bin/browse-edgar?action=getcompany&amp;filenum=001-36495&amp;owner=include&amp;count=100'>001-36495</a><br>19705709</td>
                                            </tr>
                                            <tr>
                                                <td nowrap ='nowrap'> 10 - Q </td>
                                                <td nowrap ='nowrap'>
                                                    <a href= '/Archives/edgar/data/1598014/000159801419000069/0001598014-19-000069-index.htm' id= 'documentsbutton'> &nbsp; Documents</a>&nbsp; <a href = '/cgi-bin/viewer?action=view&amp;cik=1598014&amp;accession_number=0001598014-19-000069&amp;xbrl_type=v' id='interactiveDataBtn'>&nbsp;Interactive Data</a></td>
                                                <td class='small'>Quarterly report[Sections 13 or 15(d)]<br />Acc-no: 0001598014-19-000069&nbsp;(34 Act)&nbsp; Size: 6 MB</td>
                                                <td>2019-03-26</td>
                                                <td nowrap = 'nowrap'><a href='/cgi-bin/browse-edgar?action=getcompany&amp;filenum=001-36495&amp;owner=include&amp;count=100'>001-36495</a><br>19705709</td>
                                            </tr>
                                            <tr>
                                                <td nowrap='nowrap'>10-Q</td>
                                                <td nowrap='nowrap'><a href='/Archives/edgar/data/1800/000110465909029592/0001104659-09-029592-index.htm' id='documentsbutton'>&nbsp;Documents</a></td>
                                                <td class='small'>Quarterly report [Sections 13 or 15(d)]<br>Acc-no: 0001104659-09-029592&nbsp;(34 Act)&nbsp; Size: 1 MB</td>
                                                <td>2009-05-05</td>
                                                <td nowrap='nowrap'><a href='/cgi-bin/browse-edgar?action=getcompany&amp;filenum=001-02189&amp;owner=include&amp;count=100'>001-02189</a><br>09798333</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </body>
                        </html>";

            var srPage = new SearchResultsPage(html);
            var links = srPage.GetAllReportLinks();

            Assert.Equal(new List<string>()
            {
                "/Archives/edgar/data/1598014/000159801419000043/0001598014-19-000043-index.htm",
                "/Archives/edgar/data/1598014/000159801419000069/0001598014-19-000069-index.htm"
            }, links);
        }

        [Fact]
        public void XBRLDocument_GetEPS_ReturnsValidEPS()
        {
            var xml =
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

            var xbrlDoc = new XBRLDocument(xml);
            var epsData = xbrlDoc.GetAllEPSData();

            Assert.Equal(new DateTime(2009, 06, 30), epsData[0].EndDate) ;
        }
    }
}
