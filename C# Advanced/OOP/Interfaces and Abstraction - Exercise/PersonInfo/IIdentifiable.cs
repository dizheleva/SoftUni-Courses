namespace PersonInfo
{
    public interface IIdentifiable : IBirthable
    {
        public string Id { get; set; }
    }
}
