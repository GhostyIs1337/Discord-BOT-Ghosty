using System;
using System.Collections.Generic;
using System.Text;

namespace PUBG.Services
{
    public class Attributes
    {
        public string Name { get; set; }
        public string shardId { get; set; }
        public string createdAt { get; set; }
        public string patchVersion { get; set; }
        public string titleId { get; set; }
    }


    public class Datum2
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class Matches
    {
        public List<Datum2> data { get; set; }
    }

    public class Relationships
    {
        public Matches matches { get; set; }
    }

    public class Links
    {
        public string schema { get; set; }
        public string self { get; set; }
    }

    public class Datum
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes attributes { get; set; }
        public Relationships relationships { get; set; }
        public Links links { get; set; }
    }

    public class Links2
    {
        public string self { get; set; }
    }

    public class Meta
    {
    }

    public class PUBGJson
    {
        public List<Datum> data { get; set; }
        public Links2 links { get; set; }
        public Meta meta { get; set; }
    }
}
