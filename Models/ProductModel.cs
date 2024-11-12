using Azure;
using Azure.Data.Tables;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;




namespace CloudP3.Models
{
    public class ProductModel : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }


        public ProductModel()
        {
            PartitionKey = "ProductModel";
            RowKey = Guid.NewGuid().ToString();

            Name = string.Empty;
            Price = 0;
            Category = string.Empty;
            
        }
    }
}
