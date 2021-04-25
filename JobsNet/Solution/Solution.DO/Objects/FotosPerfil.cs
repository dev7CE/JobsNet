
namespace Solution.DO.Objects
{
    public partial class FotosPerfil
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Guid { get; set; }
        public byte[] FileContent { get; set; }
        public string Type { get; set; }

        public virtual Usuarios Usuario { get; set; }
    }
}
