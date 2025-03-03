namespace DisabilityCompensation.Shared.Constants
{
    public static class DateRanges
    {
        public static readonly (DateOnly,DateOnly) FourChildrenDateRange = (new DateOnly(2008, 1, 1), new DateOnly(2015, 7, 1));
        public static readonly (DateOnly, DateOnly) ThreeChildrenDateRange = (new DateOnly(2015, 7, 1), new DateOnly(2022, 1, 1));
        public static readonly (DateOnly, DateOnly) Under16DateRange = (new DateOnly(2008, 1, 1), new DateOnly(2014, 1, 1));
    }
}
