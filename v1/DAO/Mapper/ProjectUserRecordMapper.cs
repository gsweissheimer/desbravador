using System.Data;
using v1.Entities;

public class ProjectUserRecordMapper
{
    public static ProjectUserRecord MapProject(IDataReader reader)
    {
        return new ProjectUserRecord
        {
            ProjectId = reader["project_id"] != DBNull.Value ? Convert.ToInt32(reader["project_id"]) : 0,
            UserRecordId = reader["user_record_id"] != DBNull.Value ? Convert.ToInt32(reader["user_record_id"]) : 0,
            CreatedAt = Convert.ToDateTime(reader["created_at"])

        };
    }
}
