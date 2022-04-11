using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;
using WeatherArchive.Models.DTOs;

namespace WeatherArchive.Services.FileConverter
{
    public class ExcelFileConverter : IFileConverter<IEnumerable<WeatherConditionsDTO>>
    {
        public ExcelFileConverter()
        {

        }
        public IEnumerable<WeatherConditionsDTO> ConvertFile(FileStream inputFile)
        {

            try
            {
                IWorkbook workbook = null;
                
                if (string.Equals(Path.GetExtension(inputFile.Name),".xlsx"))
                {
                    workbook=new XSSFWorkbook(inputFile);
                }
                else if(string.Equals(Path.GetExtension(inputFile.Name), ".xls"))
                {
                    workbook = new HSSFWorkbook(inputFile);
                }
                else
                {
                    throw new ArgumentException("File is invalid");
                }

                List<WeatherConditionsDTO> weatherConditionsList = new List<WeatherConditionsDTO>();
                var sheetsNumber = workbook.NumberOfSheets;
                if(sheetsNumber==0)
                {
                    throw new ArgumentException("There are no sheets in file");
                }
                for (int i = 0; i < sheetsNumber; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);

                    if (sheet == null)
                    {
                        throw new ArgumentException("One of sheets is invalid");
                    }

                    var rowCount = sheet.LastRowNum;
                    if(rowCount == 0)
                    {
                        throw new ArgumentException("There are no rows in sheet");
                    }
                    for(int j = 4; j < rowCount+1; j++)
                    {
                        var Row=sheet.GetRow(j);
                        if (Row == null)
                        {
                            throw new ArgumentException("One of rows is invalid");
                        }
                        DataFormatter formatter = new DataFormatter();

                        WeatherConditionsDTO weatherConditionsFromFile = new WeatherConditionsDTO()
                        {
                            Date = DateTime.Parse(formatter.FormatCellValue(Row.GetCell(0)).Trim()),
                            Time = TimeSpan.Parse(formatter.FormatCellValue(Row.GetCell(1)).Trim()),
                            AirTemerature = GetValueOrNull<double>(formatter.FormatCellValue(Row.GetCell(2)).Trim()),
                            RelativeHumidity = GetValueOrNull<double>(formatter.FormatCellValue(Row.GetCell(3)).Trim()),
                            Td = GetValueOrNull<double>(formatter.FormatCellValue(Row.GetCell(4)).Trim()),
                            AtmosphericPressure = GetValueOrNull<int>(formatter.FormatCellValue(Row.GetCell(5)).Trim()),
                            WindDirection = CheckString(formatter.FormatCellValue(Row.GetCell(6))),
                            WindSpeed = GetValueOrNull<int>(formatter.FormatCellValue(Row.GetCell(7)).Trim()),
                            CloudCover = GetValueOrNull<int>(formatter.FormatCellValue(Row.GetCell(8)).Trim()),
                            H = GetValueOrNull<int>(formatter.FormatCellValue(Row.GetCell(9)).Trim()),
                            VV = CheckString(formatter.FormatCellValue(Row.GetCell(10))),
                            WeatherPhenomena =  CheckString(formatter.FormatCellValue(Row.GetCell(11)))
                        };
                        
                        weatherConditionsList.Add(weatherConditionsFromFile);
                    }

                }
                if(weatherConditionsList.Count > 0)
                {
                    return weatherConditionsList;
                }
                else
                {
                    throw new Exception();
                }

                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private T? GetValueOrNull<T>(string valueAsString) where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString)||string.Equals(valueAsString," "))
                return null;
            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }

        private string? CheckString(string str )
        {
            if (string.IsNullOrEmpty(str)|| string.Equals(str, " "))
            {
                return null;
            }
            else return str.Trim();
        }


    }
}
