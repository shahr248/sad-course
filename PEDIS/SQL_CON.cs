using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Configuration;

namespace PEDIS
{
    /// <summary>
    /// SQL database connection handler for PEDIS.
    /// All database operations pass through this class.
    ///
    /// Two types of operations:
    /// 1. execute_non_query - Data modification operations (INSERT, UPDATE, DELETE)
    /// 2. execute_query     - Data retrieval operations (SELECT)
    /// </summary>
    class SQL_CON
    {
        SqlConnection conn;

        public SQL_CON()
        {
            // Read connection string from app.config
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ConfigurationErrorsException("ConnectionString not found in app.config");
            }
            conn = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Execute a data modification operation (INSERT, UPDATE, DELETE).
        /// Does not return data - only performs changes.
        ///
        /// Flow:
        /// 1. Open connection to database
        /// 2. Bind command to connection
        /// 3. Execute the command
        /// 4. Close connection (always, even if error occurs)
        /// </summary>
        /// <param name="cmd">Prepared SQL command with parameters</param>
        public void execute_non_query(SqlCommand cmd)
        {
            try
            {
                conn.Open();              // Step 1: Open connection
                cmd.Connection = conn;    // Step 2: Bind command to connection
                cmd.ExecuteNonQuery();    // Step 3: Execute (INSERT/UPDATE/DELETE)
                MessageBox.Show("Operation completed successfully", "Success", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing operation: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
            finally
            {
                // Step 4: Close connection - MUST happen always!
                // finally block executes whether there was an error or not
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Execute a data retrieval operation (SELECT).
        /// Returns SqlDataReader - object for reading results row by row.
        ///
        /// Flow:
        /// 1. Open connection to database
        /// 2. Bind command to connection
        /// 3. Execute query and get Reader
        /// 4. Return Reader for caller to use
        ///
        /// Note: Connection is NOT closed here! It stays open because the Reader
        /// needs it to read rows. Connection closes when Reader is finished.
        /// </summary>
        /// <param name="cmd">Prepared SQL command with parameters</param>
        /// <returns>SqlDataReader for reading results, or null if error occurs</returns>
        public SqlDataReader execute_query(SqlCommand cmd)
        {
            try
            {
                conn.Open();              // Step 1: Open connection
                cmd.Connection = conn;    // Step 2: Bind command to connection
                SqlDataReader reader = cmd.ExecuteReader();  // Step 3: Execute SELECT
                return reader;            // Step 4: Return results
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing query: " + ex.Message, "Error", MessageBoxButtons.OK);
                return null;
            }
        }
    }
}
