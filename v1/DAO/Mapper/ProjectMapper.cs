using System.Data;
using v1.Entities;

public class ProjectMapper
{
    public static Project MapProject(IDataReader reader)
    {
        return new Project
        {
            Id = reader["id"] != DBNull.Value ? Convert.ToInt32(reader["id"]) : 0,
            Name = reader["name"] != DBNull.Value ? reader["name"].ToString() : "",
            Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : "",
            StartDate = Convert.ToDateTime(reader["start_date"]),
            EndDate = reader["end_date"] != DBNull.Value ? Convert.ToDateTime(reader["end_date"]) : null,
            Status = reader["status"] != DBNull.Value ? reader["status"].ToString() : "",
            Risk = reader["risk"] != DBNull.Value ? reader["risk"].ToString() : "",
            ResponmsableId = reader["responsible_id"] != DBNull.Value ? Convert.ToInt32(reader["responsible_id"]) : 0,
            CreatedAt = Convert.ToDateTime(reader["created_at"]),
            UpdatedAt = reader["updated_at"] != DBNull.Value ? Convert.ToDateTime(reader["updated_at"]) : null,
            IsArchived = reader["is_archived"] != DBNull.Value && Convert.ToBoolean(reader["is_archived"])

        };
    }
}
