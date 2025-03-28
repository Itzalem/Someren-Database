﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Someren_Database.Data;
using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public class DbActivityRepository : IActivityRepository
    {
        private readonly string _connectionString;

        public DbActivityRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Someren_Database");
        }

        public async Task<List<Activity>> GetAllActivitiesAsync()
        {
            List<Activity> activities = new List<Activity>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT activity_id, activity_name, startDateTime, endDateTime FROM Activity";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            activities.Add(new Activity
                            {
                                ActivityId = reader.GetInt32(0),
                                ActivityName = reader.GetString(1),
                                StartDateTime = reader.GetDateTime(2),
                                EndDateTime = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            return activities;
        }

        public async Task<Activity> GetActivityByIdAsync(int activityId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT activity_id, activity_name, startDateTime, endDateTime FROM Activity WHERE activity_id = @ActivityId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityId", activityId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Activity
                            {
                                ActivityId = reader.GetInt32(0),
                                ActivityName = reader.GetString(1),
                                StartDateTime = reader.GetDateTime(2),
                                EndDateTime = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<List<Student>> GetParticipantsAsync(int activityId)
        {
            List<Student> participants = new List<Student>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"SELECT s.StudentNumber, s.FirstName, s.LastName 
                         FROM Students s 
                         INNER JOIN ActivityParticipant ap 
                             ON s.StudentNumber = ap.studentNumber
                         WHERE ap.activity_id = @ActivityId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityId", activityId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            participants.Add(new Student
                            {
                                StudentNumber = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return participants;
        }

        public async Task<List<Student>> GetNonParticipantsAsync(int activityId)
        {
            List<Student> nonParticipants = new List<Student>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"SELECT s.StudentNumber, s.FirstName, s.LastName 
                         FROM Students s 
                         WHERE s.StudentNumber NOT IN 
                             (SELECT studentNumber FROM ActivityParticipant WHERE activity_id = @ActivityId)"; 

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityId", activityId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            nonParticipants.Add(new Student
                            {
                                StudentNumber = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return nonParticipants;
        }

        public async Task AddParticipantAsync(int activityId, int studentId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"INSERT INTO ActivityParticipant (activity_id, studentNumber) 
                         VALUES (@ActivityId, @StudentId)"; 

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityId", activityId);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task RemoveParticipantAsync(int activityId, int studentId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"DELETE FROM ActivityParticipant 
                         WHERE activity_id = @ActivityId AND studentNumber = @StudentId"; // Use correct column names

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityId", activityId);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
