using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreTest2.model;
using Npgsql;

namespace CoreTest2.data
{
    public static class PostgresData
    {
        private static string connectionString;

        static PostgresData()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json").Build();
            var connectionStringConfig = builder.Build();
            connectionString = connectionStringConfig.GetConnectionString("DefaultConnection");
            Console.WriteLine("Connection string: " + connectionString);
        }

        public static List<Post> getPosts()
        {
            List<Post> results = new List<Post>();

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = "select * from posts";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int postId = reader.GetInt32(0);
                        string title = reader.GetString(1);
                        string content = reader.GetString(2);
                        NpgsqlTypes.NpgsqlDateTime createdTime = reader.GetTimeStamp(3);

                        results.Add(new Post(postId, title, content, createdTime.DateTime));
                    }
                }
            }

            return results;
        }

        public static Post getPost(int postId)
        {
            Post result = null;

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = "select * from posts where post_id = @postId";
                cmd.Parameters.Add(new NpgsqlParameter("postId", postId));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string title = reader.GetString(1);
                        string content = reader.GetString(2);
                        NpgsqlTypes.NpgsqlDateTime createdTime = reader.GetTimeStamp(3);

                        result = new Post(postId, title, content, createdTime.DateTime);
                    }
                }
            }

            return result;
        }

        public static void insertPost(Post post)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = "insert into posts (title, content) values (@title, @content)";
                cmd.Parameters.Add(new NpgsqlParameter("title", post.title));
                cmd.Parameters.Add(new NpgsqlParameter("content", post.content));
                cmd.ExecuteNonQuery();
            }
        }
    }
}
