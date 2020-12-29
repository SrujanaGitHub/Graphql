using GraphQL;
using GraphQL.Types;
using GraphQLSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLSample.Types
{
    public class AuthorMutations : ObjectGraphType<object>
    {
        public AuthorMutations(AuthorService authorService)
        {
            Field<AuthorType>(
             name: "createAuthor",
             arguments: new QueryArguments(new
             QueryArgument<NonNullGraphType<CreateAuthorInputType>>
             { Name = "authorInput" }),
             resolve: context =>
             {
                 var authorInput = context.GetArgument<CreateAuthorInput>("authorInput");
                 Random random = new Random();
                 var id = random.Next(2, 100);
                 var author = new Author();
                 author.Id = id;
                 author.FirstName = authorInput.FirstName;
                 author.LastName = authorInput.LastName;
                 return authorService.CreateAuthor(author);
             }
         );
        }
    }
}
