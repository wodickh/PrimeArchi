using CsvHelper;
using Newtonsoft.Json;
using PrimeArchiCommon.PrimeModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace PrimeArchiCommon
{
    public class ImportHelper
    {
        public static void CreateArchiImportFileSimple(string element, string primeInputFile, string archiFilePrefix, string outputLocation, Boolean idIncluded = false)
        {
            #region assertions
            if (string.IsNullOrWhiteSpace(element))
            {
                throw new ArgumentException("message", nameof(element));
            }

            if (string.IsNullOrWhiteSpace(primeInputFile))
            {
                throw new ArgumentException("message", nameof(primeInputFile));
            }

            if (string.IsNullOrWhiteSpace(outputLocation))
            {
                throw new ArgumentException("message", nameof(outputLocation));
            }
            #endregion
            PayloadList payloadList = ReadAndParsePrimeSystemJSon(primeInputFile);
            StreamWriter excelFile = CreateExcelFile(payloadList, archiFilePrefix, outputLocation, idIncluded:true);
            
        }

        public static PayloadList ReadAndParsePrimeSystemJSon(string primeInputFile)
        {
            PayloadList systems;
            String json;
            try
            {
                using (StreamReader sr = new StreamReader(primeInputFile))
                {
                    json = sr.ReadToEnd();
                }
                //  = JsonConvert.DeserializeObject<List<PrimeArchiCommon.PrimeModel.System>>(json);
                systems = JsonConvert.DeserializeObject<PayloadList>(json);
                return systems;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static StreamWriter CreateExcelFile(PayloadList payload, string prefix, string outputlocation, string archiType = "ApplicationComponent", Boolean idIncluded = false)
        {
            string archiElementFile= prefix + "elements.csv";
            string archiPropertiesFile = prefix + "properties.csv";
            // not used yet
            string archiRelationsFile = prefix + "releations.csv";
            
            string elementspath = Path.Combine(outputlocation, archiElementFile);
            string propertiespath = Path.Combine(outputlocation, archiPropertiesFile);
            StreamWriter streamElements = new StreamWriter(elementspath);
            StreamWriter streamProperties = new StreamWriter(propertiespath);
            using (var csvWriter = new CsvWriter(streamElements))
            {
                using (var csvPropertiesWriter = new CsvWriter(streamProperties))
                {
                    csvWriter.Configuration.Delimiter = ",";
                    // format : ID, Type, Name, Documentation
                    csvWriter.WriteField("ID");
                    csvWriter.WriteField("Type");
                    csvWriter.WriteField("Name");
                    csvWriter.WriteField("Documentation");
                    csvWriter.NextRecord();

                    csvPropertiesWriter.Configuration.Delimiter = ",";
                    csvPropertiesWriter.WriteField("ID");
                    csvPropertiesWriter.WriteField("Key");
                    csvPropertiesWriter.WriteField("Value");
                    csvPropertiesWriter.NextRecord();

                    foreach (var customer in payload.payload) // kundeniveau
                    {
                        foreach (PrimeSystem system in customer)
                        {
                            csvWriter.WriteField(idIncluded ? system.Id : "");
                            csvWriter.WriteField(archiType);
                            csvWriter.WriteField(system.Name);
                            csvWriter.WriteField(system.Description);
                            csvWriter.NextRecord();
                            if (idIncluded)
                            {
                                csvPropertiesWriter.WriteField(system.Id);
                                csvPropertiesWriter.WriteField("Tags");
                                string tags = system.Tags is null ? "" : String.Join(";", system.Tags);
                                csvPropertiesWriter.WriteField(tags);
                                csvPropertiesWriter.NextRecord();
                            }
                        }
                       
                    }
                }
            }
                return streamElements;
            }
        }



    }
