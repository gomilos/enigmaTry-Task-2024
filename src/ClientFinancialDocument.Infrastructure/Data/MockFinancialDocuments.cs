namespace ClientFinancialDocument.Infrastructure.Data
{
    public static class MockFinancialDocuments
    {
        public static string FDForProductA => @"{
              ""account_number"": ""“95867648”"",
              ""balance"": 42331.12,
              ""currency"": ""EUR"",
              ""transactions"": [
                {
                  ""transaction_id"": ""“2913”"",
                  ""amount"": 166.95,
                  ""date"": ""1/4/2015"",
                  ""description"": ""Grocery shopping"",
                  ""category"": ""Food & Dining""
                },
                {
                  ""transaction_id"": ""“3882”"",
                  ""amount"": 6.58,
                  ""date"": ""24/4/2016"",
                  ""description"": ""Grocery shopping"",
                  ""category"": ""Food & Dining""
                },
                {
                  ""transaction_id"": ""“1143”"",
                  ""amount"": -241.07,
                  ""date"": ""25/12/2019"",
                  ""description"": ""Gas station purchase"",
                  ""category"": ""Utilities""
                }
              ]
            }";

        public static string FDForProductB => @"{
              ""account_number"": ""“95867648”"",
              ""balance"": 42331.12,
              ""currency"": ""EUR"",
              ""some_new_property_product_b"": ""10"",
              ""transactions"": [
                {
                  ""transaction_id"": ""“2913”"",
                  ""amount"": 166.95,
                  ""date"": ""1/4/2015"",
                  ""description"": ""Grocery shopping"",
                  ""category"": ""Food & Dining""
                },
                {
                  ""transaction_id"": ""“3882”"",
                  ""amount"": 6.58,
                  ""date"": ""24/4/2016"",
                  ""description"": ""Grocery shopping"",
                  ""category"": ""Food & Dining""
                },
                {
                  ""transaction_id"": ""“1143”"",
                  ""amount"": -241.07,
                  ""date"": ""25/12/2019"",
                  ""description"": ""Gas station purchase"",
                  ""category"": ""Utilities""
                }
              ]
            }";
    }
}
