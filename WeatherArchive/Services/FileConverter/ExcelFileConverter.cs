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

                for (int i = 0; i < 12; i++)
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
                    for(int j = 4; j < rowCount; j++)
                    {
                        var Row=sheet.GetRow(j);
                        if (Row == null)
                        {
                            throw new Exception();
                        }
                        
                        WeatherConditionsDTO weatherConditionsFromFile = new WeatherConditionsDTO()
                        {
                            Date = Row.GetCell(0).DateCellValue.Date,
                            Time = Row.GetCell(1).DateCellValue.TimeOfDay,
                            AirTemerature = Double.Parse(Row.GetCell(2).StringCellValue.Trim()),
                            RelativeHumidity = Int32.Parse(Row.GetCell(3).StringCellValue.Trim()),
                            Td = Double.Parse(Row.GetCell(4).StringCellValue.Trim()),
                            AtmosphericPressure = Int32.Parse(Row.GetCell(5).StringCellValue.Trim()),
                            WindDirection = Row.GetCell(6).StringCellValue.Trim(),
                            WindSpeed = Int32.Parse(Row.GetCell(7).StringCellValue.Trim()),
                            CloudCover = Int32.Parse(Row.GetCell(8).StringCellValue.Trim()),
                            H = Int32.Parse(Row.GetCell(9).StringCellValue.Trim()),
                            VV = Int32.Parse(Row.GetCell(10).StringCellValue.Trim()),
                            WeatherPhenomena = Row.GetCell(11).StringCellValue.Trim()
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
    }
}
