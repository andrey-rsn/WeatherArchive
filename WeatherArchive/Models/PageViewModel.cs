namespace WeatherArchive.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        
        public int Year { get; private set; }
        public int Month { get; private set; }

        public int PageSize { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize,int year,int month)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Year=year;
            Month=month;
            PageSize = pageSize;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
