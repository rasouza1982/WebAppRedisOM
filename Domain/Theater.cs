using Redis.OM.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppRedisOM.Domain
{

    [Document(StorageType = StorageType.Json)]
    public class Theater
    {
        [RedisIdField]
        public string Id { get; set; }
        [Indexed]
        public string BoxOfficeId { get; set; }
        [Indexed]
        public string Name { get; set; }
        public DateTime Datetime { get; set; }
        public string ErrorDescription { get; set; }

    }
}
