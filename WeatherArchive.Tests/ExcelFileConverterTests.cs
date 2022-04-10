using Newtonsoft.Json;
using NUnit.Framework;
using WeatherArchive.Models.DTOs;
using WeatherArchive.Services.FileConverter;

namespace WeatherArchive.Tests
{
    [TestFixture]
    public class ExcelFileConverterTests
    {
        private ExcelFileConverter _fileConverter;
        private readonly string _projectDir;
        public ExcelFileConverterTests()
        {
            _projectDir = Path.GetDirectoryName(Path.GetDirectoryName(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        AppDomain.CurrentDomain.SetupInformation.ApplicationBase))));
        }

        private string GetTestPath(string fileName)
        {
            return Path.Combine(_projectDir, "TestFIles", fileName);
        }

        [SetUp]
        public void Setup()
        {
            _fileConverter = new ExcelFileConverter();
        }

        [Test]
        public void ConvertFile_CorrectFile_CorrectData()
        {
            List<WeatherConditionsDTO> resultList = new List<WeatherConditionsDTO>();
            List<WeatherConditionsDTO> expectedResult = TestCase1;
            using (FileStream file = new FileStream(GetTestPath("Test1.xlsx"), FileMode.Open, FileAccess.Read))
            {
                resultList.AddRange(_fileConverter.ConvertFile(file));
            }
            var result= JsonConvert.SerializeObject(resultList);
            var expected = JsonConvert.SerializeObject(expectedResult);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("Uncorrect1.xl")]
        [TestCase("Uncorrect2.txt")]
        [TestCase("Uncorrect3.xlsxx")]
        public void ConvertFile_FileWithUncorrectExtension_Exception(string fileName)
        {
            using (FileStream file = new FileStream(GetTestPath(fileName), FileMode.Open, FileAccess.Read))
            {
                Assert.Throws<Exception>(() => _fileConverter.ConvertFile(file));
            }
            
        }

        [Test]
        [TestCase("Invalid1.xlsx")]
        [TestCase("Invalid2.xlsx")]
        [TestCase("Invalid3.xlsx")]
        public void ConvertFile_InvalidFile_Exception(string fileName)
        {
            using (FileStream file = new FileStream(GetTestPath(fileName), FileMode.Open, FileAccess.Read))
            {
                Assert.Throws<Exception>(() => _fileConverter.ConvertFile(file));
            }

        }

        private static List<WeatherConditionsDTO> TestCase1 = new List<WeatherConditionsDTO>()
        {



                    new WeatherConditionsDTO()
                    {

                        Date = DateTime.Parse("01.01.2010"),
                        Time = TimeSpan.Parse("00:00"),
                        AirTemerature = -5.5,
                        RelativeHumidity = 89,
                        Td = -6.9,
                        AtmosphericPressure = 737,
                        WindDirection = "З,ЮЗ",
                        WindSpeed = 1,
                        CloudCover = 100,
                        H = 800,
                        VV = null,
                        WeatherPhenomena = "Дымка"
                    },
                    new WeatherConditionsDTO()
                    {

                        Date = DateTime.Parse("01.01.2010"),
                        Time = TimeSpan.Parse("03:00"),
                        AirTemerature = -6,
                        RelativeHumidity = 91,
                        Td = -7.1,
                        AtmosphericPressure = 738,
                        WindDirection = "штиль",
                        WindSpeed = 0,
                        CloudCover = 100,
                        H = 800,
                        VV = null,
                        WeatherPhenomena = "Дымка"
                    },
                    new WeatherConditionsDTO()
                    {

                        Date = DateTime.Parse("01.01.2010"),
                        Time = TimeSpan.Parse("09:00"),
                        AirTemerature = -11.5,
                        RelativeHumidity = 89,
                        Td = -12.8,
                        AtmosphericPressure = 739,
                        WindDirection = "штиль",
                        WindSpeed = 0,
                        CloudCover = 90,
                        H = 800,
                        VV = "4",
                        WeatherPhenomena = "Дымка"
                    }


        };


    } 
}