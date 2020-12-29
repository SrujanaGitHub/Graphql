using GraphQLSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLSample
{
    public class AuthorService
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorService(AuthorRepository
                authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public List<Author> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }
        public Author GetAuthorById(int id)
        {
            return _authorRepository.GetAuthorById(id);
        }
        public List<BlogPost> GetPostsByAuthor(int id)
        {
            return _authorRepository.GetPostsByAuthor(id);
        }
        public BlogPost GetPostsById(int id)
        {
            return _authorRepository.GetPostsById(id);
        }
        public Author CreateAuthor(Author author)
        {
            return _authorRepository.CreateAuthor(author);
        }
    }
}
