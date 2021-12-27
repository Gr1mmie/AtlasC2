using System.Runtime.Serialization;

namespace Implant.Models
{
    [DataContract]
    public class ImplantTask
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "args")]
        public string Args { get; set; }

        [DataMember(Name = "file")]
        public string File { get; set; }
    }
}
