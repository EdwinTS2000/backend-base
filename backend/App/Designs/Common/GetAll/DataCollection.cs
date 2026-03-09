namespace App.Designs.Common.GetAll
{
    public class DataCollection<T>
    {
        public IReadOnlyCollection<T> Items { get; set; } = [];
        public int Total { get; set; } = 0;         // Total de registros
        public int PageSize { get; set; } = 0;      // Número de elementos obtenidos en una pagina
        public int Page { get; set; } = 1;          // Página actual (1 - based)
        public int Pages => PageSize <= 0 ? 0       // Total páginas
            : (int)Math.Ceiling((double)Total / PageSize);
    }
}