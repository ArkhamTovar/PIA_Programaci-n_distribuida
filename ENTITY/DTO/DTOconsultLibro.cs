namespace ENTITY.DTO
{
    public record DTOconsultLibro
    {
        public int? IDLibro { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; }= string.Empty;
        public string ISBN { get; set; }
        public string Editorial { get; set; }= string.Empty;
        public int Precio { get; set; }
    }
}
