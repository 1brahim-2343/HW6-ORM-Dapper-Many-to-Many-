using Dapper;
using HW6_ORM_Dapper_Many_to_Many_.Entities;
using Microsoft.Data.SqlClient;

namespace HW6_ORM_Dapper_Many_to_Many_
{
    internal class Program
    {

        static string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DapperMTMDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";


        static void Main(string[] args)
        {
            #region Books&Genres

            //using (var connection = new SqlConnection(ConnectionString))
            //{
            //    var sql = @"SELECT B.BookId, B.GenreId, B.[Name], G.GenreId, G.Genre
            //            FROM Books AS B
            //            INNER JOIN Genres AS G
            //            ON G.GenreId = B.GenreId";
            //    var books = connection.Query<Book, Gerne, Book>(sql,
            //        (book, genre) =>
            //        {
            //            book.Genre = genre;
            //            return book;
            //        }, splitOn: nameof(Gerne.GenreId));
            //    foreach (var book in books)
            //    {
            //        Console.WriteLine($"{book} -- {book.Genre.Genre}");

            //    }
            //}
            #endregion

            #region Books&Authors

            //using (var connection = new SqlConnection(ConnectionString))
            //{
            //    var sql = @"SELECT B.BookId, B.[Name], A.AuthorId, A.FullName
            //                FROM BOOKS AS B
            //                INNER JOIN BookAuthors AS BA
            //                ON B.BookId = BA.BookId
            //                 INNER JOIN Authors AS A
            //                 ON A.AuthorId = BA.AuthorId";
            //    var books = connection.Query<Book, Author, Book>(sql,
            //        (book, author) =>
            //        {
            //            book.Authors.Add(author);
            //            return book;
            //        }, splitOn: "AuthorId");
            //    var result = books.GroupBy(b => b.BookId).Select(g =>
            //    {
            //        var groupedBook = g.First();
            //        groupedBook.Authors = g.Select(a => a.Authors.Single()).ToList();
            //        return groupedBook;
            //    }); //!

            //    foreach (var book in result)
            //    {
            //        Console.WriteLine($"{book}");
            //        if (book.Authors.Any())
            //        {
            //            foreach (var author in book.Authors)
            //            {
            //                Console.WriteLine($"\t{author.FullName}");
            //            }
            //        }
            //    }

            //}
            #endregion

            #region Books&Genres&Authors
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = @"SELECT B.BookId, B.[Name], 
                    A.AuthorId, A.FullName,
                    G.GenreId, G.Genre
                    FROM Books AS B 
                    INNER JOIN BookAuthors AS BA
                    ON B.BookId = BA.BookId
                     INNER JOIN Authors AS A
                     ON A.AuthorId = BA.AuthorId
                    INNER JOIN Genres AS G
                    ON G.GenreId =B.GenreId";
                var books = connection.Query<Book, Author, Gerne, Book>(sql,
                    (book, author, genre) =>
                    {
                        book.Authors.Add(author);
                        book.Genre = genre;
                        return book;

                    }, splitOn: "BookId,AuthorId,GenreId");
                var result = books.GroupBy(b => b.BookId).Select(g =>
                {
                    var groupedBookBucket = g.First();
                    groupedBookBucket.Authors = g.Select(x => x.Authors.Single()).ToList();
                    return groupedBookBucket;
                });
                foreach (var book in result)
                {
                    Console.Write(book);
                    Console.WriteLine($" - {book.Genre.Genre}");
                    if (book.Authors.Any())
                    {
                        foreach (var author in book.Authors)
                        {
                            Console.WriteLine($"\t{author}");
                        }
                    }
                    Console.WriteLine("-----------------");
                }



            }
            #endregion
        }
    }
}
