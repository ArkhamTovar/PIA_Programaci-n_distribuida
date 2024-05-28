namespace ENTITY
{
    public record Libro
    {
        public int? IDLibro { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string ISBN { get; set; }
        public string Editorial { get; set; }
        public int Precio { get; set; }

    }
}
