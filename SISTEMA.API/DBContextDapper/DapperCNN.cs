namespace SISTEMA.API.DBContextDapper
{
    public class DapperCNN(IConfiguration _Config)
    {
        public string Connection() => _Config.GetConnectionString("PROD");

    }
}
