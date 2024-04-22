using System.Data;
using v1.Entities;

public class UserRecordMapper
{
    public static UserRecord MapUserRecord(IDataReader reader)
    {
        return new UserRecord
        {
            Id = Convert.ToInt32(reader["id"]),
            FirstName = reader["first_name"] != DBNull.Value ? reader["first_name"].ToString() : "",
            LastName = reader["last_name"] != DBNull.Value ? reader["last_name"].ToString() : "",
            Email = reader["email"] != DBNull.Value ? reader["email"].ToString() : "",
            CreatedAt = Convert.ToDateTime(reader["created_at"]),
            UpdatedAt = reader["updated_at"] != DBNull.Value ? Convert.ToDateTime(reader["updated_at"]) : null,
        };
    }
}
