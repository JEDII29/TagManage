namespace TagManage.API.Requests
{
    public class CreateTagRequest
    {
        public string Name { get; set; }
        public int Count { get; set; } = 0;
        public bool IsRequired { get; set; } = false;
        public bool IsModeratorOnly { get; set; } = false;
        public bool HasSynonyms { get; set; } = false;
    }
}
