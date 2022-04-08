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
                if(inputFile.Name.IndexOf(".xlsx")>0)
                {
                    workbook=new XSSFWorkbook(inputFile);
                }
                else if(inputFile.Name.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(inputFile);
                }
                else
                {
                    throw new Exception();
                }

                List<WeatherConditionsDTO> weatherConditionsList = new List<WeatherConditionsDTO>();
                var sheetsNumber = workbook.NumberOfSheets;
                if(sheetsNumber==0)
                {
                    throw new Exception();
                }
                for (int i = 0; i < sheetsNumber; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);

                    if (sheet == null)
                    {
                        throw new Exception();
                    }

                    var rowCount = sheet.LastRowNum;
                    if(rowCount == 0)
                    {
                        throw new Exception();
                    }
                    for(int j = 4; j < rowCount+1; j++)
                    {
                        var Row=sheet.GetRow(j);
                        if (Row == null)
                        {
                            throw new Exception();
                        }
                        DataFormatter formatter = new DataFormatter();

                        //Row.GetCell(10).CellType
                        //int VVParse;
                        //int? vv;
                        //var result=Int32.TryParse(Row.GetCell(10).StringCellValue.Trim(),out VVParse);
                        //if(!result)
                        //{
                        //    vv=null;
                        //}
                        //else
                        //{
                        //    vv = VVParse;
                        //}
                        //WeatherConditionsDTO weatherConditionsFromFile = new WeatherConditionsDTO()
                        //{
                        //    Date = Row.GetCell(0).DateCellValue.Date,
                        //    Time = Row.GetCell(1).DateCellValue.TimeOfDay,
                        //    AirTemerature = (double)Row.GetCell(2).NumericCellValue,
                        //    RelativeHumidity = (int)Row.GetCell(3).NumericCellValue,
                        //    Td = (double)Row.GetCell(4).NumericCellValue,
                        //    AtmosphericPressure = (int)Row.GetCell(5).NumericCellValue,
                        //    WindDirection = Row.GetCell(6).StringCellValue.Trim(),
                        //    WindSpeed = (int)Row.GetCell(7).NumericCellValue,
                        //    CloudCover = (int)Row.GetCell(8).NumericCellValue,
                        //    H = (int)Row.GetCell(9).NumericCellValue,
                        //    VV = vv,
                        //    WeatherPhenomena = Row.GetCell(11).StringCellValue.Trim()
                        //};
                        //var Date = DateTime.Parse(formatter.FormatCellValue(Row.GetCell(0)).Trim());
                        //var Time = TimeSpan.Parse(formatter.FormatCellValue(Row.GetCell(1)).Trim());
                        //var AirTemerature = double.Parse(formatter.FormatCellValue(Row.GetCell(2)).Trim());
                        //var RelativeHumidity = Int32.Parse(formatter.FormatCellValue(Row.GetCell(3)).Trim());
                        //var Td = double.Parse(formatter.FormatCellValue(Row.GetCell(4)).Trim());
                        //var AtmosphericPressure = Int32.Parse(formatter.FormatCellValue(Row.GetCell(5)).Trim());
                        //var WindDirection = formatter.FormatCellValue(Row.GetCell(6)).Trim();
                        //var WindSpeed = Int32.Parse(formatter.FormatCellValue(Row.GetCell(7)).Trim());
                        //var CloudCover = Int32.Parse(formatter.FormatCellValue(Row.GetCell(8)).Trim());
                        //var H = Int32.Parse(formatter.FormatCellValue(Row.GetCell(9)).Trim());
                        //var VV = GetValueOrNull<int>(formatter.FormatCellValue(Row.GetCell(10)).Trim());
                        //var WeatherPhenomena = formatter.FormatCellValue(Row.GetCell(11)).Trim();
                        WeatherConditionsDTO weatherConditionsFromFile = new WeatherConditionsDTO()
                        {
                            Date = DateTime.Parse(formatter.FormatCellValue(Row.GetCell(0)).Trim()),
                            Time = TimeSpan.Parse(formatter.FormatCellValue(Row.GetCell(1)).Trim()),
                            AirTemerature = double.Parse(formatter.FormatCellValue(Row.GetCell(2)).Trim()),
                            RelativeHumidity = Int32.Parse(formatter.FormatCellValue(Row.GetCell(3)).Trim()),
                            Td = double.Parse(formatter.FormatCellValue(Row.GetCell(4)).Trim()),
                            AtmosphericPressure = Int32.Parse(formatter.FormatCellValue(Row.GetCell(5)).Trim()),
                            WindDirection = formatter.FormatCellValue(Row.GetCell(6)).Trim(),
                            WindSpeed = Int32.Parse(formatter.FormatCellValue(Row.GetCell(7)).Trim()),
                            CloudCover = Int32.Parse(formatter.FormatCellValue(Row.GetCell(8)).Trim()),
                            H = Int32.Parse(formatter.FormatCellValue(Row.GetCell(9)).Trim()),
                            VV = GetValueOrNull<int>(formatter.FormatCellValue(Row.GetCell(10)).Trim()),
                            WeatherPhenomena = formatter.FormatCellValue(Row.GetCell(11)).Trim()
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private T? GetValueOrNull<T>(string valueAsString) where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;
            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }


    }
}
