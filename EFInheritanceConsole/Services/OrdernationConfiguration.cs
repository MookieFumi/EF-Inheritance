using System;

namespace EFInheritanceConsole.Services
{
    public class OrdernationConfiguration
    {
        public OrdernationConfiguration()
        {
            SortExpression = String.Empty;
        }

        public SortDirection SortDirection { get; set; }
        public string SortExpression { get; set; }
    }
}