namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class GroupSumResult<T, TSum>
    {
        public T Key { get; set; }

        public TSum Sum { get; set; }
    }
}