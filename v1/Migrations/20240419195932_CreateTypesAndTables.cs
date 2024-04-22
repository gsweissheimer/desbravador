using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace v1.Migrations
{
    /// <inheritdoc />
    public partial class CreateTypesAndTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
            (@$"
                CREATE TYPE project_status AS ENUM (
	                'under_analysis',
	                'analysis_performed',
	                'analysis_approved',
	                'started',
	                'planned',
	                'in_progress',
	                'completed',
	                'cancelled'
                );

                CREATE TYPE project_risk AS ENUM (
	                'low_risk',
	                'medium_risk',
	                'high_risk'
                );

                CREATE TABLE user_record (
                    id SERIAL PRIMARY KEY,
                    first_name VARCHAR(50) NOT NULL,
                    last_name VARCHAR(50) NOT NULL,
                    email VARCHAR(100) NOT NULL UNIQUE,
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );

                CREATE TABLE project (
                    id SERIAL PRIMARY KEY,
                    name VARCHAR(100) NOT NULL,
                    description TEXT,
                    start_date DATE NOT NULL,
                    end_date DATE,
                    status project_status DEFAULT 'under_analysis'::project_status NOT NULL,
                    risk project_risk DEFAULT 'low_risk'::project_risk NOT NULL,
                    responsible_id INT NOT NULL REFERENCES user_record(id),
                    is_archived BOOL DEFAULT false,
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );

                CREATE OR REPLACE FUNCTION set_date_now()
                RETURNS TRIGGER AS $$
                BEGIN
                    NEW.updated_at = CURRENT_TIMESTAMP;
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER project_updated_at_trigger
                BEFORE UPDATE ON project
                FOR EACH ROW
                EXECUTE FUNCTION set_date_now();

                ALTER TABLE project
                ALTER COLUMN updated_at SET DEFAULT CURRENT_TIMESTAMP;

                CREATE TRIGGER user_record_updated_at_trigger
                BEFORE UPDATE ON user_record
                FOR EACH ROW
                EXECUTE FUNCTION set_date_now();

                ALTER TABLE user_record
                ALTER COLUMN updated_at SET DEFAULT CURRENT_TIMESTAMP;

                CREATE TABLE project_user_record (
                    project_id INT NOT NULL REFERENCES project(id),
                    user_record_id INT NOT NULL references user_record(id),
                    PRIMARY KEY (project_id, user_record_id),
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );

                
                insert into user_record(first_name, last_name, email) values('Kamran', 'Uyar', 'kamran.uyar@example.com');
                insert into user_record(first_name, last_name, email) values('Michelle', 'Marshall', 'michelle.marshall@example.com');
                insert into user_record(first_name, last_name, email) values('Cristian', 'González', 'cristian.gonzalez@example.com');
                insert into project(name, description, start_date, responsible_id) values('Kamran First Project', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla facilisi. Vivamus euismod lorem at lectus sollicitudin, nec hendrerit nulla hendrerit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam mattis, libero a efficitur ultrices, nunc ex finibus libero, nec varius leo neque vel odio. Sed consectetur vestibulum nisi eget aliquet. Nullam posuere magna eget velit eleifend vestibulum. Integer nec velit nulla. Sed ac nisi neque. Sed ullamcorper justo a nisi tempor, sed hendrerit neque tristique. Integer euismod ante sed erat accumsan varius. Ut ultricies odio id malesuada feugiat. Aliquam nec velit vel velit ultricies lacinia. Duis pulvinar felis ac purus ultrices, nec scelerisque dui posuere.', '2024-04-20 09:06', 1);
                insert into project_user_record(project_id, user_record_id) values(1, 2);
                insert into project_user_record(project_id, user_record_id) values(1, 3);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
            (@$"
                DROP TABLE project_user_record;
                DROP TABLE project;
                DROP TABLE user_record;
                DROP TYPE project_status;
	            DROP TYPE project_risk;
            ");
        }
    }
}
