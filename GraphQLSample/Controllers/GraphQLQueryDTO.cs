using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using GraphQL.Utilities;
using GraphQLSample.Models;
using GraphQLSample.Types;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace GraphQLSample.Controllers
{
    public class GraphQLQueryDTO
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        //public JObject Variables { get; set; }
        //public Dictionary<string, object> Variables { get; set; }
        public JObject Variables { get; set; }

        public Inputs GetInputs(IDictionary<string, object> Variables)
        {
            //var dict = new Inputs(Variables);
            var dict= new Dictionary<string, object>();

            foreach (var key in Variables.Keys)
            {
                if (dict[key] is JObject jObject)
                {
                    dict[key] = jObject.ToObject<Dictionary<string, object>>();
                    GetInputs((IDictionary<string, object>)dict[key]);
                }
                if (dict[key] is JArray jArray)
                {
                    dict[key] = GetInputs(jArray);
                }
            }
            return dict.ToInputs();
        }

        public Array GetInputs(JArray array)
        {
            var arr = new List<object>();
            for (var i = 0; i < array.Count; ++i)
            {

                var obj = array[i];
                arr.Add(obj);
                if (obj is JObject o)
                {
                    var dict = o.ToObject<Dictionary<string, object>>();
                    arr[i] = GetInputs(dict);

                }
                if (obj is JArray jArray)
                {
                    arr[i] = GetInputs(jArray);
                }
            }
            return arr.ToArray();
        }

    }

    public class GraphQLDemoSchema : Schema
    {
        public GraphQLDemoSchema(IServiceProvider provider)
        : base(provider)
        {
            Query = provider.GetRequiredService<AuthorQuery>();
            Mutation = provider.GetRequiredService<AuthorMutations>();
        }
    }
}
