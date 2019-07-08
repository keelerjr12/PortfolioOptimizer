using POLib.SECScraper;
using System.Collections.Generic;
using Xunit;

namespace POLibTests
{
    public class SearchResultsPageTests
    {
        [Fact]
        public void RetrieveAllReportLinks_Returns_SingleValidLink()
        {
            const string html = @"<html> 
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
            const string html = @"<html> 
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
            const string html = @"<html> 
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
    }
}
