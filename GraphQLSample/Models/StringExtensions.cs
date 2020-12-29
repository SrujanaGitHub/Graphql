using GraphQL;
using GraphQL.NewtonsoftJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLSample.Models
{
    public static class StringExtensions
    {
        public static Inputs ToInputs(this JObject obj)
        {
            var variables = obj?.GetValue() as Dictionary<string, object>
                            ?? new Dictionary<string, object>();
            return new Inputs(variables);
        }
    }
}
