
namespace IntegrationService.ResourceParameters
{
    public static class DefaultSettings
    {
        private const int _maxPageSize = 100;
        private const int _defaultPageSize = 10;

        public static int GetMaxPageSize()
        {
            return _maxPageSize;
        }

        public static int GetDefaultPageSize()
        {
            return _defaultPageSize;
        }
    }
}
