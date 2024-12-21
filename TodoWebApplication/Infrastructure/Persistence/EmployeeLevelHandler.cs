using System.Data;
using Dapper;
using TodoWebApplication.Domain.Enums;

namespace TodoWebApplication.Infrastructure.Persistence
{
    public class EmployeeLevelHandler : SqlMapper.TypeHandler<EmployeeLevel>
    {
        public override EmployeeLevel Parse(object value)
        {
            // chuyen int -> enum
            return (EmployeeLevel)Enum.ToObject(typeof(EmployeeLevel), value);
        }

        public override void SetValue(IDbDataParameter parameter, EmployeeLevel value)
        {
            // chuyen enum -> int
            parameter.Value = (int)value;
        }
    }
}
