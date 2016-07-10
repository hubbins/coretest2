using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest2.model
{
    public class Post
    {
        public int postId { get; }
        public string title { get; }
        public string content { get; }
        public DateTime createdTime { get; }

        public Post(int postId, string title, string content, DateTime createdTime)
        {
            this.postId = postId;
            this.title = title;
            this.content = content;
            this.createdTime = createdTime;
        }
    }
}
