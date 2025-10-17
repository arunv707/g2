    public DatabaseValueController()
    {
        connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
    }

    public ActionResult Index()
    {
        string value = "No data found";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT TOP 2 columnx FROM tablex";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    value = result != null ? result.ToString() : "No data found";
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Database Error: {ex.Message}");
            value = $"Error: {ex.Message}";
        }

        var model = new DatabaseValue { Value = value };
        return View(model);
    }
}
