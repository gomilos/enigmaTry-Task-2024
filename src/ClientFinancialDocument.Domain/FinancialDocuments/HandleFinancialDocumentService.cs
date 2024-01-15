using ClientFinancialDocument.Application.Tenants.Query;
using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Common;
using ClientFinancialDocument.Domain.Shared;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;

namespace ClientFinancialDocument.Domain.FinancialDocuments
{
    public class HandleFinancialDocumentService : IHandleFinancialDocumentService
    {
        private readonly IConfiguration _configuration;
        private readonly HashSet<string> _leaveFealds;
        private readonly HashSet<string> _hashFealds;

        public HandleFinancialDocumentService(IConfiguration configuration)
        {
            _configuration = configuration;
            _leaveFealds = !string.IsNullOrEmpty(_configuration["Anonymize:Default:leave"]) ?
                _configuration["Anonymize:Default:leave"].Split(",").ToHashSet() : new HashSet<string>();
            _hashFealds = !string.IsNullOrEmpty(_configuration["Anonymize:Default:hash"]) ?
                _configuration["Anonymize:Default:hash"].Split(",").ToHashSet() : new HashSet<string>();
        }

        public Result<dynamic> ModifyFinancialDocument(string jsonString, int registrationMumber, CompanyType companyType, ProductCode productCode)
        {
            if (!IsValid(jsonString))
            {
                return Result.Failure<dynamic>(FinancialDocumentErrors.NotValidFormat);
            }

            ExtendManipulationFieldsConfiguration(productCode);

            var anonymizedJson = AnonymizeFinancialDocument(jsonString);
            return ExtendFinancialDocument(anonymizedJson, registrationMumber, companyType);
        }

        private void ExtendManipulationFieldsConfiguration(ProductCode productCode)
        {
            switch (productCode)
            {
                case ProductCode.ProductA:
                    SetManipulationFields(ProductCode.ProductA);
                    break;
                case ProductCode.ProductB:
                    SetManipulationFields(ProductCode.ProductB);
                    break;
                default: break;
            }
        }

        private void SetManipulationFields(ProductCode product)
        {
            if (!string.IsNullOrEmpty(_configuration[$"Anonymize:{product.ToString()}:leave"]))
            {
                _leaveFealds.UnionWith(_configuration[$"Anonymize:{product.ToString()}:leave"].Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(_configuration[$"Anonymize:{product.ToString()}:hash"]))
            {
                _hashFealds.UnionWith(_configuration[$"Anonymize:{product.ToString()}:hash"].Split(',').ToList());
            }
        }

        private dynamic ExtendFinancialDocument(string jsonString, int registrationNumber, CompanyType companyType)
        {
            var jsonObject = JObject.Parse(jsonString);

            dynamic resultObj = new JObject();
            resultObj.Add("data", jsonObject);

            dynamic companyObj = new JObject();
            companyObj.Add(nameof(registrationNumber), registrationNumber);
            companyObj.Add(nameof(companyType), companyType.ToString().ToLowerInvariant());

            resultObj.Add("company", companyObj);

            dynamic dynamicObject = JsonConvert.DeserializeObject<ExpandoObject>(resultObj.ToString());
            return dynamicObject;
        }

        private string AnonymizeFinancialDocument(string jsonString)
        {
            var jsonObject = JObject.Parse(jsonString);

            foreach (JProperty jProperty in (JToken)jsonObject)
            {
                AnonymizeProperty(jProperty);
            }

            return jsonObject.ToString();
        }

        private void AnonymizeProperty(JProperty jProperty)
        {
            string name = jProperty.Name;
            JToken value = jProperty.Value;
            JTokenType valueType = value.Type;

            if (valueType == JTokenType.String || valueType == JTokenType.Integer
                || valueType == JTokenType.Date || valueType == JTokenType.Boolean
                || valueType == JTokenType.Float)
            {
                if (_hashFealds.Contains(name))
                {
                    jProperty.Value = HasheProperty(value.ToString());
                };

                if (!_hashFealds.Contains(name) && !_leaveFealds.Contains(name))
                {
                    jProperty.Value = MaskeProperty(value.ToString());
                };
            }

            if (valueType == JTokenType.Array)
            {
                var a = (JArray)value;
                IterateUsingJArray(a);
            }
        }

        private static string MaskeProperty(string value)
        {
            return "####";
        }

        private static string HasheProperty(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return String.Empty;
            }

            using (SHA256 sha = SHA256.Create())
            {
                byte[] textBytes = Encoding.UTF8.GetBytes(value);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }

        private void IterateUsingJArray(JArray jsonArray)
        {
            foreach (JToken jToken in jsonArray)
            {
                var itemProperties = jToken.Children();
                var list = itemProperties.ToList();
                foreach (JProperty property in itemProperties)
                {
                    AnonymizeProperty(property);
                }
            }
        }

        public static bool IsValid(string jsonString)
        {
            try
            {
                if (jsonString.StartsWith('{') && jsonString.EndsWith('}'))
                {
                    JObject.Parse(jsonString);
                }
                else if (jsonString.StartsWith('[') && jsonString.EndsWith(']'))
                {
                    JArray.Parse(jsonString);
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }
    }
}
