using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLSample.Models
{
    public class CreateAuthorInputType:InputObjectGraphType
    {
        public CreateAuthorInputType()
        {
            Name = "createAuthorInput1";
            //Field<NonNullGraphType<IntGraphType>>("id");
            Field<NonNullGraphType<StringGraphType>>("firstName");
            Field<NonNullGraphType<StringGraphType>>("lastName");
        }
    }
}
