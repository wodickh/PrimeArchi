using PrimeArchiCommon;
using System;
using System.IO;
using Xunit;

namespace PrimeArchiCommonTest
{
    public class ImportHelperTest
    {
        const string JSONINPUTFILE = @"C:\temp\PrimeArchi\system";
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void TestReadAndParsePrimeSystemJSon()
        {
            // get file 

            var result = ImportHelper.ReadAndParsePrimeSystemJSon(JSONINPUTFILE);
        }

        [Fact]
        public void TestCreateArchiImportFileSimple()
        {
            // set up file path location
            // check for location of excelfile. 
            ImportHelper.CreateArchiImportFileSimple("APPLICATIONCOMPONENT", JSONINPUTFILE, "", @"c:\temp\", true);

        }
        [Fact]
        public void TestCreateArchiImportFileSimpleWithPrefix()
        {
            // set up file path location
            // check for location of excelfile. 
            ImportHelper.CreateArchiImportFileSimple("APPLICATIONCOMPONENT", JSONINPUTFILE, "master", @"c:\temp\");

        }
    }
}
